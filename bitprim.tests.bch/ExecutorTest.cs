using Xunit;

namespace Bitprim.Tests
{
    [Collection("ChainCollection")]
    public class ExecutorTest
    {
        [Fact]
        public void TestConfigValid()
        {
            using (var exec = new Executor("config/valid.cfg"))
            {
                Assert.True(exec.IsLoadConfigValid); 
            }
        }

        [Fact]
        public void TestConfigInvalid()
        {
            using (var exec = new Executor("config/invalid.cfg"))
            {
                Assert.False(exec.IsLoadConfigValid); 
            }
        }
    }
}