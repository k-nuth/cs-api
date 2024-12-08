// Copyright (c) 2016-2024 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using Xunit;
using Knuth;

namespace Knuth.Tests {
    public class ValidationsTest
    {
        [Fact]
        public void TestAddressValidation() {
            Assert.False(Validations.IsValidPaymentAddress("abcd"));
            Assert.False(Validations.IsValidPaymentAddress("my2dxGb5jz43ktwGxg2doUaEb9WhZ9PQ"));
            Assert.True(Validations.IsValidPaymentAddress("my2dxGb5jz43ktwGxg2doUaEb9WhZ9PQ7K"));
        }

        [Fact]
        public void TestCashAddressValidation() {
            Assert.False(Validations.IsValidPaymentAddress("bitcoincash:qz7cgzl5mavucstffsr8qf8mp2v60zncqv7tn3nlea"));
            Assert.True(Validations.IsValidPaymentAddress("bitcoincash:qz7cgzl5mavucstffsr8qf8mp2v60zncqv7tn3nler"));
        }

        [Fact]
        public void TestHashValidation() {
            Assert.False(Validations.IsValidHash("abcd"));
            Assert.False(Validations.IsValidHash("abcg"));
            Assert.False(Validations.IsValidHash("0000000014e6ae5aef5b7b660b160b7572fe14b95609fefb6f87c2d2e33a5fdg"));
            Assert.True(Validations.IsValidHash("0000000014e6ae5aef5b7b660b160b7572fe14b95609fefb6f87c2d2e33a5fdd"));
        }
    }
}
