using System;
using System.Runtime.InteropServices;
using Bitprim.Native;

namespace Bitprim
{

    /// <summary>
    /// Controls the execution of the Bitprim bitcoin node.
    /// </summary>
    public class Executor : IDisposable
    {
        public delegate bool BlockHandler(ErrorCode e, UInt64 u, BlockList incoming, BlockList outgoing);
        public delegate bool TransactionHandler(ErrorCode e, Transaction newTx);

        private Chain chain_;
        private IntPtr nativeInstance_;
        private ExecutorNative.ReorganizeHandler internalBlockHandler_;
        private ExecutorNative.RunNodeHandler internalRunNodeHandler_;
        private ExecutorNative.TransactionHandler internalTxHandler_;

        /// <summary>
        /// Create an executor object. Only for internal use, to instantiate delegates.
        /// </summary>
        private Executor()
        {
            //TODO(fernando): create the delegate object only when it is necessary
            internalBlockHandler_ = new ExecutorNative.ReorganizeHandler(InternalBlockHandler);
            internalRunNodeHandler_ = new ExecutorNative.RunNodeHandler(InternalRunNodeHandler);
            internalTxHandler_ = new ExecutorNative.TransactionHandler(InternalTransactionHandler);
        }

        /// <summary>
        /// Create executor. Does not init database or start execution yet.
        /// </summary>
        /// <param name="configFile"> Path to configuration file. </param>
        public Executor(string configFile) : this()
        {
            nativeInstance_ = ExecutorNative.executor_construct_fd(configFile, 0, 0);
        }

        /// <summary> //TODO See BIT-20
        /// Create executor. Does not init database or start execution yet.
        /// </summary>
        /// <param name="configFile"> Path to configuration file. </param>
        /// <param name="stdOut"> File descriptor for redirecting standard output. </param>
        /// <param name="stdErr"> File descriptor for redirecting standard error output. </param>
        // public Executor(string configFile, int stdOut, int stdErr)
        // {
        //     nativeInstance_ = ExecutorNative.executor_construct_fd(configFile, stdOut, stdErr);
        // }

        /// <summary>
        /// Create executor. Does not init database or start execution yet.
        /// </summary>
        /// <param name="configFile"> Path to configuration file. </param>
        /// <param name="stdOut"> Handle for redirecting standard output. </param>
        /// <param name="stdErr"> Handle for redirecting standard output. </param>
        public Executor(string configFile, IntPtr stdOut, IntPtr stdErr) : this()
        {
            nativeInstance_ = ExecutorNative.executor_construct_handles(configFile, stdOut, stdErr);
        }

        ~Executor()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Returns true iif the current network is a testnet.
        /// </summary>
        public bool UseTestnetRules
        {
            get
            {
                return NetworkType == NetworkType.Testnet;
            }
        }

        /// <summary>
        /// The node's query interface. Will be null until node starts running
        /// (i.e. Run or RunWait succeeded)
        /// </summary>
        public Chain Chain
        {
            get
            {
                return chain_;
            }
        }

        /// <summary>
        /// The node's network. Won't be valid until node starts running
        /// (i.e. Run or RunWait succeeded)
        /// </summary>
        public NetworkType NetworkType
        {
            get
            {
                return ExecutorNative.executor_get_network(nativeInstance_);
            }
        }

        /// <summary>
        /// Initialize the local dabatase structure.
        /// </summary>
        /// <returns>True iif local chain init succeeded</returns>
        public bool InitChain()
        {
            return ExecutorNative.executor_initchain(nativeInstance_) != 0;
        }

        /// <summary>
        /// Starts running the node; blockchain starts synchronizing (downloading).
        /// The call returns right away, and the handler is invoked
        /// when the node actually starts running.
        /// </summary>
        /// <param name="handler"> Callback which will be invoked when node starts running. </param>
        /// <returns> Error code (0 = success) </returns>
        public int Run(Action<int> handler)
        {
            GCHandle handlerHandle = GCHandle.Alloc(handler);
            IntPtr handlerPtr = (IntPtr)handlerHandle;
            int result = ExecutorNative.executor_run(nativeInstance_, handlerPtr, internalRunNodeHandler_);
            if(result == 0)
            {
                chain_ = new Chain(ExecutorNative.executor_get_chain(nativeInstance_));
            }
            return result;
        }

        /// <summary>
        /// Starts running the node; blockchain start synchronizing (downloading).
        /// Call blocks until node starts running.
        /// </summary>
        /// <returns> Error code (0 = success) </returns>
        public int RunWait()
        {
            int result = ExecutorNative.executor_run_wait(nativeInstance_);
            if(result == 0)
            {
                chain_ = new Chain(ExecutorNative.executor_get_chain(nativeInstance_));
            }
            return result;
        }

        /// <summary>
        /// Stops the node; that includes all activies, such as synchronization and networking.
        /// </summary>
        public void Stop()
        {
            ExecutorNative.executor_stop(nativeInstance_);
        }

        public bool IsStopped
        {
            get
            {
                return ExecutorNative.executor_stopped(nativeInstance_) != 0;
            }
        }


        /// <summary>
        /// Be notified (called back) when the local copy of the blockchain is reorganized.
        /// </summary>
        /// <param name="handler"> Callback which will be called when blocks are added or removed.
        /// The callback returns 3 parameters:
        ///     - Height (UInt64): The chain height at which reorganization takes place
        ///     - Incoming (Blocklist): Incoming blocks (added to the blockchain).
        ///     - Outgoing (Blocklist): Outgoing blocks (removed from the blockchain).
        /// </param>
        public void SubscribeToBlockChain(BlockHandler handler)
        {
            GCHandle handlerHandle = GCHandle.Alloc(handler);
            IntPtr handlerPtr = (IntPtr)handlerHandle;
            ExecutorNative.chain_subscribe_blockchain(nativeInstance_, Chain.NativeInstance, handlerPtr, internalBlockHandler_);
        }

        /// <summary>
        /// Be notified (called back) when the local copy of the blockchain is updated at the transaction level.
        /// </summary>
        /// <param name="handler"> Callback which will be called when a transaction is added. </param>
        public void SubscribeToTransaction(TransactionHandler handler)
        {
            GCHandle handlerHandle = GCHandle.Alloc(handler);
            IntPtr handlerPtr = (IntPtr)handlerHandle;
            ExecutorNative.chain_subscribe_transaction(nativeInstance_, Chain.NativeInstance, handlerPtr, internalTxHandler_);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //Release managed resources and call Dispose for member variables
            }
            //Release unmanaged resources
            if( ExecutorNative.executor_stopped(nativeInstance_) != 0 )
            {
                ExecutorNative.executor_stop(nativeInstance_);
            }
            ExecutorNative.executor_destruct(nativeInstance_);
        }

        private static int InternalBlockHandler(IntPtr executor, IntPtr chain, IntPtr context, ErrorCode error, UInt64 u, IntPtr incoming, IntPtr outgoing)
        {
            GCHandle handlerHandle = (GCHandle)context;
            if (ExecutorNative.executor_stopped(executor) != 0 || error == ErrorCode.ServiceStopped)
            {
                handlerHandle.Free();
                return 0;
            }
            var incomingBlocks = incoming != IntPtr.Zero? new BlockList(incoming) : null;
            var outgoingBlocks = outgoing != IntPtr.Zero? new BlockList(outgoing) : null;
            var handler = (handlerHandle.Target as BlockHandler);
            bool keepSubscription = handler(error, u, incomingBlocks, outgoingBlocks);
            
            incomingBlocks?.Dispose();
            outgoingBlocks?.Dispose();

            if ( ! keepSubscription )
            {
                handlerHandle.Free();
            }
            return keepSubscription ? 1 : 0;
        }

        private static void InternalRunNodeHandler(IntPtr handlerPtr, int error)
        {
            GCHandle handlerHandle = (GCHandle)handlerPtr;
            Action<int> handler = (handlerHandle.Target as Action<int>);
            handler(error);
            handlerHandle.Free();
        }

        private static int InternalTransactionHandler(IntPtr executor, IntPtr chain, IntPtr context, ErrorCode error, IntPtr transaction)
        {
            GCHandle handlerHandle = (GCHandle)context;
            if (ExecutorNative.executor_stopped(executor) != 0 || error == ErrorCode.ServiceStopped)
            {
                handlerHandle.Free();
                return 0;
            }
            var newTransaction = transaction != IntPtr.Zero? new Transaction(transaction) : null;
            var handler = (handlerHandle.Target as TransactionHandler);
            bool keepSubscription = handler(error, newTransaction);
            if( ! keepSubscription )
            {
                handlerHandle.Free();
            }
            return keepSubscription ? 1 : 0;
        }

    }

}
