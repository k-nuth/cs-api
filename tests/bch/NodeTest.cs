// Copyright (c) 2016-2021 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using Xunit;

namespace Knuth.Tests {
    [Collection("ChainCollection")]
    public class NodeTest {
        [Fact]
        public void TestConfigValid() {
            var config = Knuth.Config.Settings.GetFromFile("config/valid.cfg");
            Assert.True(config.Ok);
        }

        [Fact]
        public void TestConfigInvalid() {
            var config = Knuth.Config.Settings.GetFromFile("config/invalid.cfg");
            Assert.False(config.Ok);
        }
    }
}