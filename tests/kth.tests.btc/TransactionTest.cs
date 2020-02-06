﻿using Xunit;

namespace Bitprim.Tests
{
    public class TransactionTest
    {
        [Fact]
        public void TestCreateTransactionFromHexString()
        {
            const string HEX_TX = "01000000017b1eabe0209b1fe794124575ef807057c77ada2138ae4fa8d6c4de0398a14f3f00000000494830450221008949f0cb400094ad2b5eb399d59d01c14d73d8fe6e96df1a7150deb388ab8935022079656090d7f6bac4c9a94e0aad311a4268e082a725f8aeae0573fb12ff866a5f01ffffffff01f0ca052a010000001976a914cbc20a7664f2f69e5355aa427045bc15e7c6c77288ac00000000";

            using (var tx = new Transaction(1, HEX_TX))
            {
                var hash = Binary.ByteArrayToHexString(tx.Hash);
                Assert.Equal(hash,"c7736a0a0046d5a8cc61c8c3c2821d4d7517f5de2bc66a966011aaa79965ffba");
                Assert.True(tx.IsValid); 
            }
        }
    }
}