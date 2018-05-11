using Xunit;

namespace Bitprim.Tests
{
    public class ExecutorTest
    {
        [Fact]
        public void TestConfigValid()
        {
            var exec_ = new Executor("config/valid.cfg");
            Assert.True(exec_.IsLoadConfigValid); 
        }

        [Fact]
        public void TestConfigInvalid()
        {
            var exec_ = new Executor("config/invalid.cfg");
            Assert.False(exec_.IsLoadConfigValid); 
        }
    }
}