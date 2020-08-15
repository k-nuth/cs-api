using Xunit;

namespace Knuth.Tests
{
    [Collection("ChainCollection")]
    public class NodeTest
    {
        [Fact]
        public void TestConfigValid()
        {
            using (var exec = new Node("config/valid.cfg"))
            {
                Assert.True(exec.IsLoadConfigValid); 
            }
        }

        [Fact]
        public void TestConfigInvalid()
        {
            using (var exec = new Node("config/invalid.cfg"))
            {
                Assert.False(exec.IsLoadConfigValid); 
            }
        }
    }
}