// Copyright (c) 2016-2024 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using Xunit;

namespace Knuth.Tests {
    public class PaymentAddressTests
    {
        [Fact]
        public void EmptyAddressShouldFail() {
            Assert.False(PaymentAddress.TryParse("", out PaymentAddress addr));
            Assert.Null(addr);
        }

        [Fact]
        public void WhitespaceAddressShouldFail() {
            Assert.False(PaymentAddress.TryParse(" ", out PaymentAddress addr));
            Assert.Null(addr);
        }

        [Fact]
        public void InvalidAddressShouldFail() {
            Assert.False(PaymentAddress.TryParse("abcd", out PaymentAddress addr));
            Assert.Null(addr);
        }

        [Fact]
        public void ValidMainnetCashAddrAddressShouldParseCorrectly() {
            Assert.True(PaymentAddress.TryParse("bitcoincash:qrcuqadqrzp2uztjl9wn5sthepkg22majyxw4gmv6p", out PaymentAddress addr));
            Assert.NotNull(addr);
            Assert.True(addr.IsValid);
            Assert.Equal("bitcoincash:qrcuqadqrzp2uztjl9wn5sthepkg22majyxw4gmv6p", addr.EncodeCashAddr(false));
            Assert.Equal("bitcoincash:zrcuqadqrzp2uztjl9wn5sthepkg22majypyxk429j", addr.EncodeCashAddr(true));
            Assert.Equal("1P3GQYtcWgZHrrJhUa4ctoQ3QoCU2F65nz", addr.EncodeLegacy());
            addr.Dispose();
        }

        [Fact]
        public void ValidMainnetCashAddrNoPrefixAddressShouldParseCorrectly() {
            Assert.True(PaymentAddress.TryParse("qrcuqadqrzp2uztjl9wn5sthepkg22majyxw4gmv6p", out PaymentAddress addr));
            Assert.NotNull(addr);
            Assert.True(addr.IsValid);
            Assert.Equal("bitcoincash:qrcuqadqrzp2uztjl9wn5sthepkg22majyxw4gmv6p", addr.EncodeCashAddr(false));
            Assert.Equal("bitcoincash:zrcuqadqrzp2uztjl9wn5sthepkg22majypyxk429j", addr.EncodeCashAddr(true));
            Assert.Equal("1P3GQYtcWgZHrrJhUa4ctoQ3QoCU2F65nz", addr.EncodeLegacy());
            addr.Dispose();
        }

        [Fact]
        public void ValidMainnetLegacyAddressShouldParseCorrectly() {
            Assert.True(PaymentAddress.TryParse("1P3GQYtcWgZHrrJhUa4ctoQ3QoCU2F65nz", out PaymentAddress addr));
            Assert.NotNull(addr);
            Assert.True(addr.IsValid);
            Assert.Equal("bitcoincash:qrcuqadqrzp2uztjl9wn5sthepkg22majyxw4gmv6p", addr.EncodeCashAddr(false));
            Assert.Equal("bitcoincash:zrcuqadqrzp2uztjl9wn5sthepkg22majypyxk429j", addr.EncodeCashAddr(true));
            Assert.Equal("1P3GQYtcWgZHrrJhUa4ctoQ3QoCU2F65nz", addr.EncodeLegacy());
            addr.Dispose();
        }

        [Fact]
        public void Valid32ByteCashAddrAddressShouldParseCorrectly() {
            Assert.True(PaymentAddress.TryParse("bitcoincash:pvstqkm54dtvnpyqxt5m5n7sjsn4enrlxc526xyxlnjkaycdzfeu69reyzmqx", out PaymentAddress addr));
            Assert.NotNull(addr);
            Assert.True(addr.IsValid);
            Assert.Equal("bitcoincash:pvstqkm54dtvnpyqxt5m5n7sjsn4enrlxc526xyxlnjkaycdzfeu69reyzmqx", addr.EncodeCashAddr(false));
            Assert.Equal("bitcoincash:rvstqkm54dtvnpyqxt5m5n7sjsn4enrlxc526xyxlnjkaycdzfeu6hs99m6ed", addr.EncodeCashAddr(true));
            Assert.Equal("34frpCV2v6wtzig9xx4Z9XJ6s4jU3zqwR7", addr.EncodeLegacy()); // In fact a 32-byte address is not representable in legacy encoding.
            addr.Dispose();
        }

        // TODO: A standalone testnet address is not considered valid by default; the node needs to be in testnet mode.
        // Analyze removing this hidden dependency.
    }
}
