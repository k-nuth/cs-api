using Xunit;
using Bitprim;

namespace Bitprim.Tests
{
    public class ValidationsTest
    {

        [Fact]
        public void TestAddressValidation()
        {
            Assert.False(Validations.IsValidPaymentAddress("abcd"));
            Assert.False(Validations.IsValidPaymentAddress("my2dxGb5jz43ktwGxg2doUaEb9WhZ9PQ"));
            Assert.True(Validations.IsValidPaymentAddress("my2dxGb5jz43ktwGxg2doUaEb9WhZ9PQ7K"));
        }

        [Fact]
        public void TestHashValidation()
        {
            Assert.False(Validations.IsValidHash("abcd"));
            Assert.False(Validations.IsValidHash("abcg"));
            Assert.False(Validations.IsValidHash("0000000014e6ae5aef5b7b660b160b7572fe14b95609fefb6f87c2d2e33a5fdg"));
            Assert.True(Validations.IsValidHash("0000000014e6ae5aef5b7b660b160b7572fe14b95609fefb6f87c2d2e33a5fdd"));
        }

    }
}