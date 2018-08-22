using System;

namespace Bitprim.Tests
{
    public class ExecutorFixture : IDisposable
    {
        public ExecutorFixture()
        {
            Executor = new Executor("");
            int initChainOk = Executor.InitAndRunAsync().GetAwaiter().GetResult();
            if (initChainOk != 0)
            {
                throw new InvalidOperationException("Executor::InitAndRunAsync failed, check log");
            }
        }

        public Executor Executor { get; }

        public void Dispose()
        {
            Executor.Stop();
            Executor.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}