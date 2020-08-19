// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

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