// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using Xunit;

namespace Knuth.Tests
{
    public class TransactionTest
    {
        [Fact]
        public void TestCreateTransactionFromHexString()
        {
            const string HEX_TX = "0100000001f489b1b843aa38dc9bebf516597b441bbfc8bb333b38897cc3c68fd1058f9b03010000006a473044022004bdaf540efdbb9cb738428900c5aa22bc1040f5d85a7dffa8dddc65066740de022016adf1b09ad5f3f30981f0ca2f7594d2029e6baacf7199bd81c3cf776c2a54b5412103c50c66822aecd3a464e047fd6581a71e6f2b34d866f9dfc37c4264dcebe978c1ffffffff0200000000000000004a6a4c478d0377f98f339f9761bf1586dba31dbe647805246c34ae0d76a5d9b8aee75e9e20835468697320697320616c6c206f6e636861696e3f204c696b65206d656d6f3f204e6963652e180d0100000000001976a914f1b9b568df97700ed0086bfc5357400d7a2ab43f88ac00000000";

            using (var tx = new Transaction(1, HEX_TX))
            {
                var hash = Binary.ByteArrayToHexString(tx.Hash);
                Assert.Equal(hash,"d4c7289fdc74dc5043372f343863c635799b40355f7cae46c915eb5fc07598c7");
                Assert.True(tx.IsValid); 
            }
        }
    }
}