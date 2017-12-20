using System;

namespace Bitprim.Tests
{

public class ExecutorFixture : IDisposable
{
    private Executor exec_;

    public ExecutorFixture()
    {
        exec_ = new Executor("", 0, 0);
        int result = exec_.InitChain();
        /*if(result != 0)
        {
            throw new InvalidOperationException("InitChain error: " + result);
        }*/
        result = exec_.RunWait();
    }

    public Executor Executor
    {
        get
        {
            return exec_;
        }
    }

    public void Dispose()
    {
        exec_.Stop();
        GC.Collect();
        GC.WaitForPendingFinalizers();
    }
}

}