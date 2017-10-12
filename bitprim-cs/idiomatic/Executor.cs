using System;
using System.Runtime.InteropServices;
using BitprimCs.Native;

namespace BitprimCs{

public class Executor : IDisposable
{
    private IntPtr nativeInstance_;

    public Executor(string configFile)
    {
        nativeInstance_ = ExecutorNative.executor_construct_fd(configFile, new IntPtr(0), new IntPtr(0));
    }

    ~Executor()
    {
        Dispose(false);
    }

    public Chain Chain
    {
        get
        {
            return new Chain(ExecutorNative.executor_get_chain(nativeInstance_));
        }
    }

    public int InitChain()
    {
        return ExecutorNative.executor_initchain(nativeInstance_);
    }

    public int Run(Action<int> handler)
    {
        GCHandle handlerHandle = GCHandle.Alloc(handler);
        IntPtr handlerPtr = (IntPtr) handlerHandle;
        return ExecutorNative.executor_run(nativeInstance_, handlerPtr, NativeCallbackHandler);
    }

    public int RunWait()
    {
        return ExecutorNative.executor_run_wait(nativeInstance_);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Stop()
    {
        ExecutorNative.executor_stop(nativeInstance_);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing){
            //Release managed resources and call Dispose for member variables
        }   
        //Release unmanaged resources
        ExecutorNative.executor_destruct(nativeInstance_);
    }

    private static void NativeCallbackHandler(IntPtr handlerPtr, int error)
    {
        GCHandle handlerHandle = (GCHandle) handlerPtr;
        Action<int> handler = (handlerHandle.Target as Action<int>);
        handler(error);
    }

}

}