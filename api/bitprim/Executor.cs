using System;

public class Executor : IDisposable
{
    private IntPtr nativeInstance_;

    public Executor()
    {
        nativeInstance_ = ExecutorNative.executor_construct_fd(Constants.CONFIG_FILE, new IntPtr(0), new IntPtr(0), new IntPtr(0));
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

    protected virtual void Dispose(bool disposing)
    {
        if (disposing){
            //Release managed resources and call Dispose for member variables
        }   
        //Release unmanaged resources
        ExecutorNative.executor_destruct(nativeInstance_);
    }

}