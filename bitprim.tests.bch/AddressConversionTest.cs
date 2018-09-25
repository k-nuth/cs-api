using Xunit;
using Bitprim;

namespace Bitprim.Tests
{
    public class AddressConversionTest
    {
        [Fact]
        public void ConvertLegacyTestnetToCashAddr()
        {
            using(var address = new PaymentAddress("mhYoEjuWJhZsJehPomAQJRnQ713UvYBFa3"))
            {
                Assert.Equal("bchtest:qqtynp0zz0ltgrtm9nm5z4u536fue4nc5y0rkx0r6a", address.ToCashAddr());
            }
        }

        [Fact]
        public void ConvertLegacyMainnetToCashAddr()
        {
            using(var address = new PaymentAddress("1KcSdYdo4LJj2n5iHt5Hn3WEJQ6wWyPU3n"))
            {
                Assert.Equal("bitcoincash:qrxzvda6ma26zak2duedfqzxj3lpw0d0nsczra9sfh", address.ToCashAddr());
            }
        }

    }
}