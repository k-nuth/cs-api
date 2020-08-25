// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using Xunit;

namespace Knuth.Tests {
    [Collection("ChainCollection")]
    public class NodeTest {
        [Fact]
        public void TestConfigValid() {
            using (var node = new Node("config/valid.cfg")) {
                Assert.True(node.IsLoadConfigValid); 
            }
        }

        [Fact]
        public void TestConfigInvalid() {
            using (var node = new Node("config/invalid.cfg")) {
                Assert.False(node.IsLoadConfigValid); 
            }
        }
    }
}