using System;

namespace Bitprim.Tests
{

public class ExecutorFixture : IDisposable
{
    private Executor exec_;

    public ExecutorFixture()
    {
        exec_ = new Executor("", 0, 0);
        bool initChainOk = exec_.InitChain();
        if(!initChainOk)
        {
            throw new InvalidOperationException("Executor::InitChain failed, check log");
        }
        int runResult = exec_.RunWait();
        if(runResult != 0)
        {
            throw new InvalidOperationException("Executor::RunWait failed, check log");
        }
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