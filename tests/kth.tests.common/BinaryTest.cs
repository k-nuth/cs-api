using Xunit;
using Bitprim;
using System;

namespace Bitprim.Tests
{
    public class BinaryTest
    {
        private const string MERKLE_HEX = "13c44d0b48e0cfffaa953293ab1726684bb3ecc08a705a1133ad3e4638f59982";
        private static readonly byte[] MERKLE_BIN =  new byte[]{0x82, 0x99, 0xf5, 0x38, 0x46, 0x3e, 0xad, 0x33, 0x11, 0x5a, 0x70, 0x8a, 0xc0, 0xec, 0xb3, 0x4b, 0x68, 0x26, 0x17, 0xab, 0x93, 0x32, 0x95, 0xaa, 0xff, 0xcf, 0xe0, 0x48, 0x0b, 0x4d, 0xc4, 0x13};

        [Fact]
        public void TestByteArrayToHexString()
        {
            Assert.Equal(MERKLE_HEX, Binary.ByteArrayToHexString(MERKLE_BIN));
        }

        [Fact]
        public void TestByteArrayToHexStringReverse()
        {
            Assert.Equal(ReverseHexString(MERKLE_HEX), Binary.ByteArrayToHexString(MERKLE_BIN, reverse: true));
        }

        [Fact]
        public void TestHexStringToByteArray()
        {
            Assert.Equal(MERKLE_BIN, Binary.HexStringToByteArray(MERKLE_HEX));
        }

        [Fact]
        public void TestHexStringToByteArrayReverse()
        {
            // This conversion already reverses by default, so test without reversing
            byte[] reversed = new byte[MERKLE_BIN.Length];
            MERKLE_BIN.CopyTo(reversed, 0);
            Array.Reverse(reversed);
            Assert.Equal(reversed, Binary.HexStringToByteArray(MERKLE_HEX, reverse: false));
        }

        [Fact]
        public void TestRoundTripConversion()
        {
            Assert.Equal(MERKLE_HEX, Binary.ByteArrayToHexString(Binary.HexStringToByteArray(MERKLE_HEX)) );
            Assert.Equal(MERKLE_BIN, Binary.HexStringToByteArray(Binary.ByteArrayToHexString(MERKLE_BIN)) );
        }

        private static string ReverseHexString(string s)
        {
            char[] array = new char[s.Length];
            int forward = 0;
            for (int i = s.Length - 1; i >= 0; i-=2)
            {
                array[forward] = s[i-1];
                array[forward + 1] = s[i];
                forward += 2;
            }
            return new string(array);
        }
    }
}