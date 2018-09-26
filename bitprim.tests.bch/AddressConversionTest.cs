using Xunit;
using Bitprim;

namespace Bitprim.Tests
{
    public class AddressConversionTest
    {
        private const string MAINNET_CASH_ADDR = "bitcoincash:qrxzvda6ma26zak2duedfqzxj3lpw0d0nsczra9sfh";
        private const string MAINNET_CASH_ADDR_NO_PREFIX = "qrxzvda6ma26zak2duedfqzxj3lpw0d0nsczra9sfh";
        private const string MAINNET_LEGACY_ADDR = "1KcSdYdo4LJj2n5iHt5Hn3WEJQ6wWyPU3n";
        private const string TESTNET_CASH_ADDR = "bchtest:qqtynp0zz0ltgrtm9nm5z4u536fue4nc5y0rkx0r6a";
        private const string TESTNET_CASH_ADDR_NO_PREFIX = "qqtynp0zz0ltgrtm9nm5z4u536fue4nc5y0rkx0r6a";
        private const string TESTNET_LEGACY_ADDR = "mhYoEjuWJhZsJehPomAQJRnQ713UvYBFa3";
        

        [Fact]
        public void ConvertLegacyTestnetToCashAddr()
        {
            using(var address = new PaymentAddress(TESTNET_LEGACY_ADDR))
            {
                Assert.Equal(TESTNET_CASH_ADDR, address.ToCashAddr(includePrefix: true));
            }
        }

        [Fact]
        public void ConvertLegacyTestnetToCashAddrNoPrefix()
        {
            using(var address = new PaymentAddress(TESTNET_LEGACY_ADDR))
            {
                Assert.Equal(TESTNET_CASH_ADDR_NO_PREFIX, address.ToCashAddr(includePrefix: false));
            }
        }

        [Fact]
        public void ConvertLegacyMainnetToCashAddr()
        {
            using(var address = new PaymentAddress(MAINNET_LEGACY_ADDR))
            {
                Assert.Equal(MAINNET_CASH_ADDR, address.ToCashAddr(includePrefix: true));
            }
        }

        [Fact]
        public void ConvertLegacyMainnetToCashAddrNoPrefix()
        {
            using(var address = new PaymentAddress(MAINNET_LEGACY_ADDR))
            {
                Assert.Equal(MAINNET_CASH_ADDR_NO_PREFIX, address.ToCashAddr(includePrefix: false));
            }
        }

        [Fact]
        public void ConvertMainnetCashAddrToLegacy()
        {
            Assert.Equal(MAINNET_LEGACY_ADDR, PaymentAddress.CashAddressToLegacyAddress(MAINNET_CASH_ADDR));
        }

        [Fact]
        public void ConvertTestnetCashAddrToLegacy()
        {
            Assert.Equal(TESTNET_LEGACY_ADDR, PaymentAddress.CashAddressToLegacyAddress(TESTNET_CASH_ADDR));
        }

        [Fact]
        public void ConvertMainnetLegacyToCashAddr()
        {
            Assert.Equal(MAINNET_CASH_ADDR, PaymentAddress.LegacyAddressToCashAddress(MAINNET_LEGACY_ADDR, includePrefix: true));
        }

        [Fact]
        public void ConvertMainnetLegacyToCashAddrNoPrefix()
        {
            Assert.Equal(MAINNET_CASH_ADDR_NO_PREFIX, PaymentAddress.LegacyAddressToCashAddress(MAINNET_LEGACY_ADDR, includePrefix: false));
        }

        [Fact]
        public void ConvertTestnetLegacyToCashAddr()
        {
            Assert.Equal(TESTNET_CASH_ADDR, PaymentAddress.LegacyAddressToCashAddress(TESTNET_LEGACY_ADDR, includePrefix: true));
        }

        [Fact]
        public void ConvertTestnetLegacyToCashAddrNoPrefix()
        {
            Assert.Equal(TESTNET_CASH_ADDR_NO_PREFIX, PaymentAddress.LegacyAddressToCashAddress(TESTNET_LEGACY_ADDR, includePrefix: false));
        }

        [Fact]
        public void ShouldThrowIfConvertingCashAddrToCashAddr()
        {
            var ex = Assert.Throws<SharpCashAddr.CashAddrConversionException>( () => PaymentAddress.LegacyAddressToCashAddress(TESTNET_CASH_ADDR, includePrefix: true) );
            Assert.Equal("Address contains unexpected character.", ex.Message);
        }

        [Fact]
        public void ShouldThrowIfConvertingLegacyAddrToLegacyAddr()
        {
            var ex = Assert.Throws<SharpCashAddr.CashAddrConversionException>( () => PaymentAddress.CashAddressToLegacyAddress(TESTNET_LEGACY_ADDR) );
            Assert.Equal("Address to be decoded is longer or shorter than expected.", ex.Message);
        }

        [Fact]
        public void ShouldThrowOnEmptyLegacyAddr()
        {
            var ex = Assert.Throws<SharpCashAddr.CashAddrConversionException>( () => PaymentAddress.LegacyAddressToCashAddress("", includePrefix: true) );
            Assert.Equal("Address to be decoded is shorter or longer than expected!", ex.Message);
        }

        [Fact]
        public void ShouldThrowOnEmptyCashAddr()
        {
            var ex = Assert.Throws<SharpCashAddr.CashAddrConversionException>( () => PaymentAddress.CashAddressToLegacyAddress("") );
            Assert.Equal("Address to be decoded is longer or shorter than expected.", ex.Message);
        }
    }
}