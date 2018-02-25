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

        private IntPtr nativeInstance_;

        /// <summary>
        /// Create executor. Does not init database or start execution yet.
        /// </summary>
        /// <param name="configFile"> Path to configuration file. </param>
        public Executor(string configFile)
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
        public Executor(string configFile, IntPtr stdOut, IntPtr stdErr)
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
        /// The node's query interface.
        /// </summary>
        public Chain Chain
        {
            get
            {
                return new Chain(ExecutorNative.executor_get_chain(nativeInstance_));
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
            return ExecutorNative.executor_run(nativeInstance_, handlerPtr, NativeCallbackHandler);
        }

        /// <summary>
        /// Starts running the node; blockchain start synchronizing (downloading).
        /// Call blocks until node starts running.
        /// </summary>
        /// <returns> Error code (0 = success) </returns>
        public int RunWait()
        {
            int result = ExecutorNative.executor_run_wait(nativeInstance_);
            return result;
        }

        /// <summary>
        /// Stops the node; that includes all activies, such as synchronization and networking.
        /// </summary>
        public void Stop()
        {
            ExecutorNative.executor_stop(nativeInstance_);
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
            ExecutorNative.chain_subscribe_blockchain(nativeInstance_, Chain.NativeInstance, handlerPtr, ReorganizeHandler);
        }

        /// <summary>
        /// Be notified (called back) when the local copy of the blockchain is updated at the transaction level.
        /// </summary>
        /// <param name="handler"> Callback which will be called when a transaction is added. </param>
        public void SubscribeToTransaction(TransactionHandler handler)
        {
            GCHandle handlerHandle = GCHandle.Alloc(handler);
            IntPtr handlerPtr = (IntPtr)handlerHandle;
            ExecutorNative.chain_subscribe_transaction(nativeInstance_, Chain.NativeInstance, handlerPtr, TransactionInternalHandler);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //Release managed resources and call Dispose for member variables
            }
            //Release unmanaged resources
            Logger.Log("Destroying executor " + nativeInstance_.ToString("X"));
            ExecutorNative.executor_destruct(nativeInstance_);
        }

        private static int ReorganizeHandler(IntPtr executor, IntPtr chain, IntPtr context, ErrorCode error, UInt64 u, IntPtr blockList, IntPtr blockList2)
        {
            GCHandle handlerHandle = (GCHandle)context;
            if (ExecutorNative.executor_stopped(executor) != 0 || error == ErrorCode.ServiceStopped)
            {
                handlerHandle.Free();
                return 0;
            }
            var handler = (handlerHandle.Target as BlockHandler);
            bool keepSubscription = handler(error, u, new BlockList(blockList), new BlockList(blockList2));
            if ( ! keepSubscription )
            {
                handlerHandle.Free();
            }
            return keepSubscription ? 1 : 0;
        }

        private static int TransactionInternalHandler(IntPtr executor, IntPtr chain, IntPtr context, ErrorCode error, IntPtr transaction)
        {
            GCHandle handlerHandle = (GCHandle)context;
            if (ExecutorNative.executor_stopped(executor) != 0 || error == ErrorCode.ServiceStopped)
            {
                handlerHandle.Free();
                return 0;
            }
            var handler = (handlerHandle.Target as TransactionHandler);
            bool keepSubscription = handler(error, new Transaction(transaction));
            if( ! keepSubscription )
            {
                handlerHandle.Free();
            }
            return keepSubscription ? 1 : 0;
        }

        private static void NativeCallbackHandler(IntPtr handlerPtr, int error)
        {
            GCHandle handlerHandle = (GCHandle)handlerPtr;
            Action<int> handler = (handlerHandle.Target as Action<int>);
            handler(error);
            handlerHandle.Free();
        }

    }

}