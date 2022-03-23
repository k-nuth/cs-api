// Copyright (c) 2016-2022 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
// using Knuth.Logging;
using Knuth.Native;
using Knuth.Config;

namespace Knuth {
    /// <summary>
    /// Controls the execution of the Knuth bitcoin node.
    /// </summary>
    public class Node : IDisposable {
        // private static readonly ILog Logger = LogProvider.For<Node>();
        /// <summary>
        /// Contains information about new blocks
        /// </summary>
        /// <param name="errorCode">Error code</param>
        /// <param name="height">Branch height</param>
        /// <param name="incoming">List of incoming blocks</param>
        /// <param name="outgoing">List of outgoing blocks</param>
        /// <returns></returns>
        public delegate bool BlockHandler(ErrorCode errorCode, UInt64 height, BlockList incoming, BlockList outgoing);

        /// <summary>
        /// Contains information about new transactions
        /// </summary>
        /// <param name="errorCode">Error code</param>
        /// <param name="newTx">The new transaction</param>
        /// <returns></returns>
        public delegate bool TransactionHandler(ErrorCode errorCode, Transaction newTx);

        public delegate bool DsProofHandler(ErrorCode errorCode, DoubleSpendProof dsp);

        private Chain chain_;
        private readonly IntPtr nativeInstance_;
        private readonly NodeNative.ReorganizeHandler internalBlockHandler_;
        private readonly NodeNative.RunNodeHandler internalRunNodeHandler_;
        private readonly NodeNative.TransactionHandler internalTxHandler_;
        private readonly NodeNative.DsProofHandler internalDsProofHandler_;

        private bool running_;
        private bool stopped_;

        /// <summary>
        /// Create an node object. Only for internal use, to instantiate delegates.
        /// </summary>
        private Node() {
            //TODO(fernando): create the delegate object only when it is necessary
            internalBlockHandler_ = InternalBlockHandler;
            internalRunNodeHandler_ = InternalRunNodeHandler;
            internalTxHandler_ = InternalTransactionHandler;
            internalDsProofHandler_ = InternalDsProofHandler;
        }

        /// <summary>
        /// Create node object. Does not init database or start execution yet.
        /// </summary>
        /// <param name="settings"> Settings object. </param>
        /// <param name="showNodeLog"> Print Native node stdout and stderr. </param>
        public Node(Settings settings, bool showNodeLog = false)
            : this()
        {
            var native = settings.ToNative();
            nativeInstance_ = NodeNative.kth_node_construct(ref native, showNodeLog ? 1 : 0);
        }

        ~Node() {
            Dispose(false);
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Returns true iif the current network is a testnet.
        /// </summary>
        public bool UseTestnetRules {
            get { return NetworkType == NetworkType.Testnet; }
        }

        /// <summary>
        /// The node's query interface. Will be null until node starts running
        /// (i.e. Run or RunWait succeeded)
        /// </summary>
        public Chain Chain {
            get { return chain_; }
        }

        /// <summary>
        /// The node's network. Won't be valid until node starts running
        /// (i.e. Run or RunWait succeeded)
        /// </summary>
        public NetworkType NetworkType {
            get { return NodeNative.kth_node_settings_get_network(nativeInstance_); }
        }

        /// <summary>
        /// Initialize if necessary and starts running the node; blockchain starts synchronizing (downloading).
        /// The call returns right away, and the handler is invoked
        /// when the node actually starts running.
        /// </summary>
        /// <returns> Error code (0 = success) </returns>
        public async Task<ErrorCode> LaunchAsync(StartModules mods = StartModules.All) {
            var completionSource = new TaskCompletionSource<ErrorCode>();
            Launch(mods, ec => {
                completionSource.TrySetResult(ec);
            });
            // await Task.Delay(100);   // Note: attempt to avoid deadlocks with C-API v0.4.3
            return await completionSource.Task;
        }

        private void Launch(StartModules mods, Action<ErrorCode> handler) {
            var handlerHandle = GCHandle.Alloc(handler);
            var handlerPtr = (IntPtr)handlerHandle;
            Task.Run( () => {
                NodeNative.kth_node_init_run_and_wait_for_signal(nativeInstance_, handlerPtr, mods, internalRunNodeHandler_);
                stopped_ = true;
            });
        }

        /// <summary>
        /// Stops the node; that includes all activies, such as synchronization and networking.
        /// </summary>
        public void Stop() {
            // NodeNative.kth_node_stop(nativeInstance_);
            NodeNative.kth_node_signal_stop(nativeInstance_);
            while (running_ &&  ! stopped_) {
                System.Threading.Thread.Sleep(100);
            }
        }

        /// <summary>
        /// Closes the node; that includes all activies, such as synchronization and networking.
        /// </summary>
        public void Close() {
            NodeNative.kth_node_close(nativeInstance_);
        }

        /// <summary>
        /// Returns true if and only if the node is stopped
        /// </summary>
        public bool IsStopped => NodeNative.kth_node_stopped(nativeInstance_) != 0;

        /// <summary>
        /// Be notified (called back) when the local copy of the blockchain is reorganized.
        /// </summary>
        /// <param name="handler"> Callback which will be called when blocks are added or removed.
        /// The callback returns 3 parameters:
        ///     - Height (UInt64): The chain height at which reorganization takes place
        ///     - Incoming (Blocklist): Incoming blocks (added to the blockchain).
        ///     - Outgoing (Blocklist): Outgoing blocks (removed from the blockchain).
        /// </param>
        public void SubscribeBlockNotifications(BlockHandler handler) {
            var handlerHandle = GCHandle.Alloc(handler);
            var handlerPtr = (IntPtr)handlerHandle;
            NodeNative.kth_chain_subscribe_blockchain(nativeInstance_, Chain.NativeInstance, handlerPtr, internalBlockHandler_);
        }

        /// <summary>
        /// Be notified (called back) when the local copy of the blockchain is updated at the transaction level.
        /// </summary>
        /// <param name="handler"> Callback which will be called when a transaction is added. </param>
        public void SubscribeTransactionNotifications(TransactionHandler handler) {
            var handlerHandle = GCHandle.Alloc(handler);
            var handlerPtr = (IntPtr)handlerHandle;
            NodeNative.kth_chain_subscribe_transaction(nativeInstance_, Chain.NativeInstance, handlerPtr, internalTxHandler_);
        }

        public void SubscribeDsProofNotifications(DsProofHandler handler) {
            var handlerHandle = GCHandle.Alloc(handler);
            var handlerPtr = (IntPtr)handlerHandle;
            NodeNative.kth_chain_subscribe_ds_proof(nativeInstance_, Chain.NativeInstance, handlerPtr, internalDsProofHandler_);
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                //Release managed resources and call Dispose for member variables
            }
            Stop();
            NodeNative.kth_node_destruct(nativeInstance_);
        }

        private static int InternalBlockHandler(IntPtr node, IntPtr chain, IntPtr context, ErrorCode error, UInt64 forkHeight, IntPtr incoming, IntPtr outgoing) {
            var handlerHandle = (GCHandle)context;
            var closed = false;
            var keepSubscription = false;

            try {
                if (NodeNative.kth_node_stopped(node) != 0 || error == ErrorCode.ServiceStopped) {
                    handlerHandle.Free();
                    closed = true;
                    return 0;
                }

                var incomingBlocks = incoming != IntPtr.Zero? new BlockList(incoming) : null;
                var outgoingBlocks = outgoing != IntPtr.Zero? new BlockList(outgoing) : null;
                var handler = (handlerHandle.Target as BlockHandler);

                keepSubscription = handler(error, forkHeight, incomingBlocks, outgoingBlocks);

                incomingBlocks?.Dispose();
                outgoingBlocks?.Dispose();

                if ( ! keepSubscription ) {
                    handlerHandle.Free();
                    closed = true;
                }
                return Helper.BoolToC(keepSubscription);
            } finally {
                if ( ! keepSubscription && ! closed) {
                    handlerHandle.Free();
                }
            }
        }

        private void InternalRunNodeHandler(IntPtr node, IntPtr handlerPtr, int error) {
            var handlerHandle = (GCHandle)handlerPtr;
            var handler = (handlerHandle.Target as Action<ErrorCode>);
            try {
                if (error == 0) {
                    chain_ = new Chain(NodeNative.kth_node_get_chain(nativeInstance_));
                    running_ = true;
                    stopped_ = false;
                }
                handler((ErrorCode)error);
            } finally {
                handlerHandle.Free();
            }
        }

        private static int InternalTransactionHandler(IntPtr node, IntPtr chain, IntPtr context, ErrorCode error, IntPtr transaction) {
            var handlerHandle = (GCHandle)context;
            var closed = false;
            var keepSubscription = false;

            try {
                if (NodeNative.kth_node_stopped(node) != 0 || error == ErrorCode.ServiceStopped) {
                    handlerHandle.Free();
                    closed = true;
                    return 0;
                }

                var newTransaction = transaction != IntPtr.Zero? new Transaction(transaction) : null;
                var handler = (handlerHandle.Target as TransactionHandler);

                keepSubscription = handler(error, newTransaction);

                if ( ! keepSubscription ) {
                    handlerHandle.Free();
                    closed = true;
                }
                return Helper.BoolToC(keepSubscription);
            } finally {
                if ( ! keepSubscription && ! closed) {
                    handlerHandle.Free();
                }
            }
        }

        private static int InternalDsProofHandler(IntPtr node, IntPtr chain, IntPtr context, ErrorCode error, IntPtr dsp) {
            var handlerHandle = (GCHandle)context;
            var closed = false;
            var keepSubscription = false;

            try {
                if (NodeNative.kth_node_stopped(node) != 0 || error == ErrorCode.ServiceStopped) {
                    handlerHandle.Free();
                    closed = true;
                    return 0;
                }

                var newDsp = dsp != IntPtr.Zero? new DoubleSpendProof(dsp) : null;
                var handler = (handlerHandle.Target as DsProofHandler);

                keepSubscription = handler(error, newDsp);

                if ( ! keepSubscription ) {
                    handlerHandle.Free();
                    closed = true;
                }
                return Helper.BoolToC(keepSubscription);
            } finally {
                if ( ! keepSubscription && ! closed) {
                    handlerHandle.Free();
                }
            }
        }
    }
}
