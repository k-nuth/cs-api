using System;

namespace Knuth.Tests
{
    public class ExecutorFixture : IDisposable
    {
        public ExecutorFixture()
        {
            Executor = new Executor("config/mainnet.cfg");
            int initChainOk = Executor.InitAndRunAsync().GetAwaiter().GetResult();
            if (initChainOk != 0)
            {
                throw new InvalidOperationException("Executor::InitAndRunAsync failed, check log");
            }
        }

        public Executor Executor { get; }
     
        private void ReleaseUnmanagedResources()
        {
            Executor.Stop();
            Executor.Dispose();
        }

        protected virtual void Dispose(bool disposing)
        {
            ReleaseUnmanagedResources();
            if (disposing)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ExecutorFixture()
        {
            Dispose(false);
        }
    }
}