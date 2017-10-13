using System;
using System.Threading;
using Xunit;

namespace BitprimCs.Tests
{
    public class ChainTest : IClassFixture<ExecutorFixture>
    {
        private ExecutorFixture executorFixture_;

        public ChainTest(ExecutorFixture fixture)
        {
            executorFixture_ = fixture;
        }

        [Fact]
        public void TestFetchLastHeight()
        {
            var handlerDone = new AutoResetEvent(false);
            int error = 0;
            UInt64 height = 0;
            Action<int,UInt64> handler = delegate(int theError, UInt64 theHeight)
            {
                error = theError;
                height = theHeight;
            };
            executorFixture_.Executor.Chain.FetchLastHeight(handler);
            handlerDone.WaitOne();
            Assert.Equal(error, 0);
        }

    }
}
