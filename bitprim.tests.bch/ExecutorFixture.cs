using System;

namespace Bitprim.Tests
{

    public class ExecutorFixture : IDisposable
    {
        private readonly Executor exec_;

        public ExecutorFixture()
        {
            exec_ = new Executor("");
            int initChainOk = exec_.InitAndRunAsync().GetAwaiter().GetResult();
            if (initChainOk != 0)
            {
                throw new InvalidOperationException("Executor::InitChain failed, check log");
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
            exec_.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }

}