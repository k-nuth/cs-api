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
        private IntPtr nativeInstance_;

        /// <summary>
        /// Create executor. Does not init database or start execution yet.
        /// </summary>
        /// <param name="configFile"> Path to configuration file. </param>
        /// <param name="stdOut"> File descriptor for redirecting standard output.
        /// If zero, output goes to debug file. </param>
        /// <param name="stdErr"> File descriptor for redirecting standard error output.
        /// If zero, output goes to error file. </param>
        public Executor(string configFile, int stdOut = 0, int stdErr = 0)
        {
            nativeInstance_ = ExecutorNative.executor_construct_fd(configFile, stdOut, stdErr);
        }

        /// <summary>
        /// Create executor. Does not init database or start execution yet.
        /// </summary>
        /// <param name="configFile"> Path to configuration file. </param>
        /// <param name="stdOut"> Handle for redirecting standard output.
        /// If IntPtr.Zero, output goes to debug file. </param>
        /// <param name="stdErr"> Handle for redirecting standard output.
        /// If IntPtr.Zero, output goes to debug file. </param>
        public Executor(string configFile, IntPtr stdOut = default(IntPtr), IntPtr stdErr = default(IntPtr))
        {
            nativeInstance_ = ExecutorNative.executor_construct_handles(configFile, stdOut, stdErr);
        }

        ~Executor()
        {
            Dispose(false);
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Stops the node; that includes all activies, such as synchronization and networking.
        /// </summary>
        public void Stop()
        {
            ExecutorNative.executor_stop(nativeInstance_);
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

        private static void NativeCallbackHandler(IntPtr handlerPtr, int error)
        {
            GCHandle handlerHandle = (GCHandle)handlerPtr;
            Action<int> handler = (handlerHandle.Target as Action<int>);
            handler(error);
            handlerHandle.Free();
        }

    }

}