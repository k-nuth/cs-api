using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Bitprim.Logging;
using Bitprim.Native;
#if KEOKEN
using Bitprim.Keoken;
#endif
namespace Bitprim
{
    /// <summary>
    /// Controls the execution of the Bitprim bitcoin node.
    /// </summary>
    public class Executor : IDisposable
    {
        private static readonly ILog Logger = LogProvider.For<Executor>();
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

        private Chain chain_;
        #if KEOKEN
        private KeokenManager keokenManager_;   
        #endif
        private readonly IntPtr nativeInstance_;
        private readonly ExecutorNative.ReorganizeHandler internalBlockHandler_;
        private readonly ExecutorNative.RunNodeHandler internalRunNodeHandler_;
        private readonly ExecutorNative.TransactionHandler internalTxHandler_;

        private bool running_;

        /// <summary>
        /// Create an executor object. Only for internal use, to instantiate delegates.
        /// </summary>
        private Executor()
        {
            //TODO(fernando): create the delegate object only when it is necessary
            internalBlockHandler_ = InternalBlockHandler;
            internalRunNodeHandler_ = InternalRunNodeHandler;
            internalTxHandler_ = InternalTransactionHandler;
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

#if KEOKEN

        public KeokenManager KeokenManager
        {
            get
            {
                return keokenManager_;
            }
        }
#endif

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
            Logger.Debug("Calling executor_initchain");
            return ExecutorNative.executor_initchain(nativeInstance_) != 0;
        }

        /// <summary>
        /// Starts running the node; blockchain starts synchronizing (downloading).
        /// The call returns right away, and the handler is invoked
        /// when the node actually starts running.
        /// </summary>
        /// <returns> Error code (0 = success) </returns>
        public async Task<int> RunAsync()
        {
            return await TaskHelper.ToTask<int>(tcs =>
            {
                Run(i =>
                {
                    tcs.TrySetResult(i);
                });   
            });
        }

        /// <summary>
        /// Starts running the node; blockchain starts synchronizing (downloading).
        /// The call returns right away, and the handler is invoked
        /// when the node actually starts running.
        /// </summary>
        /// <param name="handler"> Callback which will be invoked when node starts running. </param>
        /// <returns> Error code (0 = success) </returns>
        private void Run(Action<int> handler)
        {
            var handlerHandle = GCHandle.Alloc(handler);
            var handlerPtr = (IntPtr)handlerHandle;
            ExecutorNative.executor_run(nativeInstance_, handlerPtr, internalRunNodeHandler_);
        }


        /// <summary>
        /// Initialize if necessary and starts running the node; blockchain starts synchronizing (downloading).
        /// The call returns right away, and the handler is invoked
        /// when the node actually starts running.
        /// </summary>
        /// <returns> Error code (0 = success) </returns>
        public async Task<int> InitAndRunAsync()
        {
            return await TaskHelper.ToTask<int>(tcs =>
            {
                InitAndRun(i =>
                {
                    tcs.TrySetResult(i);
                });
            });
        }

        /// <summary>
        /// Initialize if necessary and starts running the node; blockchain starts synchronizing (downloading).
        /// The call returns right away, and the handler is invoked
        /// when the node actually starts running.
        /// </summary>
        /// <param name="handler"> Callback which will be invoked when node starts running. </param>
        /// <returns> Error code (0 = success) </returns>
        private void InitAndRun(Action<int> handler)
        {
            var handlerHandle = GCHandle.Alloc(handler);
            var handlerPtr = (IntPtr)handlerHandle;
            ExecutorNative.executor_init_and_run(nativeInstance_, handlerPtr, internalRunNodeHandler_);
        }


       
        /// <summary>
        /// Stops the node; that includes all activies, such as synchronization and networking.
        /// </summary>
        public void Stop()
        {
            ExecutorNative.executor_stop(nativeInstance_);
        }

        /// <summary>
        /// Returns true if and only if the node is stopped
        /// </summary>
        public bool IsStopped
        {
            get
            {
                return ExecutorNative.executor_stopped(nativeInstance_) != 0;
            }
        }

        /// <summary>
        /// Returns true if and only if and only if the config file is valid
        /// </summary>
        public bool IsLoadConfigValid
        {
            get
            {
                return ExecutorNative.executor_load_config_valid(nativeInstance_) != 0;
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
            var handlerHandle = GCHandle.Alloc(handler);
            var handlerPtr = (IntPtr)handlerHandle;
            ExecutorNative.chain_subscribe_blockchain(nativeInstance_, Chain.NativeInstance, handlerPtr, internalBlockHandler_);
        }

        /// <summary>
        /// Be notified (called back) when the local copy of the blockchain is updated at the transaction level.
        /// </summary>
        /// <param name="handler"> Callback which will be called when a transaction is added. </param>
        public void SubscribeToTransaction(TransactionHandler handler)
        {
            var handlerHandle = GCHandle.Alloc(handler);
            var handlerPtr = (IntPtr)handlerHandle;
            ExecutorNative.chain_subscribe_transaction(nativeInstance_, Chain.NativeInstance, handlerPtr, internalTxHandler_);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //Release managed resources and call Dispose for member variables
            }
            //Release unmanaged resources
            if(running_ && ExecutorNative.executor_stopped(nativeInstance_) != 0 )
            {
                ExecutorNative.executor_stop(nativeInstance_);
            }
            ExecutorNative.executor_destruct(nativeInstance_);

#if KEOKEN
            keokenManager_?.Dispose();   
#endif
        }

        private static int InternalBlockHandler(IntPtr executor, IntPtr chain, IntPtr context, ErrorCode error, UInt64 u, IntPtr incoming, IntPtr outgoing)
        {
            var handlerHandle = (GCHandle)context;
            var closed = false;
            var keepSubscription = false;

            try
            {
                if (ExecutorNative.executor_stopped(executor) != 0 || error == ErrorCode.ServiceStopped)
                {
                    handlerHandle.Free();
                    closed = true;
                    return 0;
                }

                var incomingBlocks = incoming != IntPtr.Zero? new BlockList(incoming) : null;
                var outgoingBlocks = outgoing != IntPtr.Zero? new BlockList(outgoing) : null;
                var handler = (handlerHandle.Target as BlockHandler);
                
                keepSubscription = handler(error, u, incomingBlocks, outgoingBlocks);
            
                incomingBlocks?.Dispose();
                outgoingBlocks?.Dispose();

                if ( ! keepSubscription )
                {
                    handlerHandle.Free();
                    closed = true;
                }
                return keepSubscription ? 1 : 0;
            }
            finally
            {
                if (!keepSubscription && !closed)
                {
                    handlerHandle.Free();
                }
            }
        }

        private  void InternalRunNodeHandler(IntPtr executor,IntPtr handlerPtr, int error)
        {
            var handlerHandle = (GCHandle)handlerPtr;
            var handler = (handlerHandle.Target as Action<int>);
            try
            {
                if (error == 0)
                {
                    chain_ = new Chain(ExecutorNative.executor_get_chain(nativeInstance_));
                    
                    #if KEOKEN
                        keokenManager_ = new KeokenManager(ExecutorNative.executor_get_keoken_manager(nativeInstance_));
                    #endif 
                    
                    running_ = true;
                }
                handler(error);
            }
            finally
            {
                handlerHandle.Free();
            }
        }

        private static int InternalTransactionHandler(IntPtr executor, IntPtr chain, IntPtr context, ErrorCode error, IntPtr transaction)
        {
            var handlerHandle = (GCHandle)context;
            var closed = false;
            var keepSubscription = false;

            try
            {
                if (ExecutorNative.executor_stopped(executor) != 0 || error == ErrorCode.ServiceStopped)
                {
                    handlerHandle.Free();
                    closed = true;
                    return 0;
                }
                
                var newTransaction = transaction != IntPtr.Zero? new Transaction(transaction) : null;
                var handler = (handlerHandle.Target as TransactionHandler);
                
                keepSubscription = handler(error, newTransaction);
                
                if( ! keepSubscription )
                {
                    handlerHandle.Free();
                    closed = true;
                }
                return keepSubscription ? 1 : 0;
            }
            finally
            {
                if (!keepSubscription && !closed)
                {
                    handlerHandle.Free();
                }       
            }
        }
    }

}
