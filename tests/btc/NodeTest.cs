using Xunit;

namespace Knuth.Tests
{
    [Collection("ChainCollection")]
    public class NodeTest
    {
        [Fact]
        public void TestConfigValid()
        {
            using (var exec_ = new Node("config/valid.cfg"))
            {
                Assert.True(exec_.IsLoadConfigValid); 
            }
        }

        [Fact]
        public void TestConfigInvalid()
        {
            using (var exec_ = new Node("config/invalid.cfg"))
            {
                Assert.False(exec_.IsLoadConfigValid); 
            }
        }
    }
}