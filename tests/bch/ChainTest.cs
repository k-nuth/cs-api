// Copyright (c) 2016-2021 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using Xunit;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Knuth.Tests {
    [Collection("ChainCollection")]
    public class ChainTest : IClassFixture<NodeFixture> {
        private const int FIRST_NON_COINBASE_BLOCK_HEIGHT = 170;
        private readonly NodeFixture nodeFixture_;

        public ChainTest(NodeFixture fixture) {
            nodeFixture_ = fixture;
        }
        private async Task<Tuple<ErrorCode, UInt64>> GetLastHeight() {
            var ret = await nodeFixture_.Node.Chain.GetLastHeightAsync();
            return new Tuple<ErrorCode, UInt64>(ret.ErrorCode, ret.Result);
        }

        private async Task WaitUntilBlock(UInt64 desiredHeight, string callerName) {
            ErrorCode error = 0;
            UInt64 height = 0;
            while (error == 0 && height < desiredHeight) {
                Console.WriteLine($"--->{callerName} height: {height} desiredHeight: {desiredHeight}");
                var errorAndHeight = await GetLastHeight();

                error = errorAndHeight.Item1;
                height = errorAndHeight.Item2;

                if (height < desiredHeight) {
                    await Task.Delay(10000);
                }
            }
            Console.WriteLine($"--->{callerName} height: {height} desiredHeight: {desiredHeight}");
            Assert.Equal(ErrorCode.Success, error);
        }

        private void CheckFirstNonCoinbaseTxFromHeight170Inputs(ITransaction tx) {
            Assert.Equal(1UL, tx.Inputs.Count);
            //Assert.Equal(50000000UL, tx.TotalInputValue); //TODO Blockdozer says this is 50 BTC
            //Input 0
            Input input = tx.Inputs[0];
            Assert.Equal(4294967295, input.Sequence);
            Assert.Equal(113UL, input.GetSerializedSize(true));
            Assert.Equal(111UL, input.GetSerializedSize(false));

            Assert.Equal(0UL, input.GetSignatureOperations(true));
            Assert.Equal(0UL, input.GetSignatureOperations(false));
            Assert.True(input.IsFinal);
            Assert.True(input.IsValid);
            //Assert.Equal("EPA", input.PreviousOutput.Script.ToString(0)); //TODO Deadlock/hang
            //Script

            Script script = input.Script;
            //Assert.Equal(0UL, script.GetEmbeddedSigOps(input.PreviousOutput.Script)); //TODO Deadlock/hang
            Assert.Equal(0UL, script.GetSigOps(true));
            Assert.Equal(0UL, script.GetSigOps(false));
            Assert.True(script.IsValid);
            Assert.True(script.OperationsAreValid);
            Assert.Equal(72UL, script.SatoshiContentSize);
            Assert.Equal("[304402204e45e16932b8af514961a1d3a1a25fdf3f4f7732e9d624c6c61548ab5fb8cd410220181522ec8eca07de4860a4acdd12909d831cc56cbbac4622082221a8768d1d0901]", script.ToString(0));
        }

        private void CheckFirstNonCoinbaseTxFromHeight170Outputs(ITransaction tx) {
            Assert.Equal(2UL, tx.Outputs.Count);
            Assert.Equal(5000000000UL, tx.TotalOutputValue);
            //Output 0
            Output output0 = tx.Outputs[0];
            Assert.Equal(76UL, output0.GetSerializedSize(true));
            Assert.Equal(76UL, output0.GetSerializedSize(true)); //TODO In inputs, it's two bytes less; does this make sense?
            Assert.True(output0.IsValid);
            Assert.Equal(1UL, output0.SignatureOperations);
            Assert.Equal(1000000000UL, output0.Value);
            Script script0 = output0.Script;
            //script0.GetEmbeddedSigOps TODO Hangs
            Assert.Equal(1UL, script0.GetSigOps(true));
            Assert.Equal(1UL, script0.GetSigOps(false));
            Assert.True(script0.IsValid);
            Assert.True(script0.OperationsAreValid);
            Assert.Equal(67UL, script0.SatoshiContentSize);
            Assert.Equal("[04ae1a62fe09c5f51b13905f07f06b99a2f7159b2225f374cd378d71302fa28414e7aab37397f554a7df5f142c21c1b7303b8a0626f1baded5c72a704f7e6cd84c] checksig", script0.ToString(0));
            //Output 1
            Output output1 = tx.Outputs[1];
            Assert.Equal(76UL, output1.GetSerializedSize(true));
            Assert.Equal(76UL, output1.GetSerializedSize(true));
            Assert.True(output1.IsValid);
            Assert.Equal(1UL, output1.SignatureOperations);
            Assert.Equal(4000000000UL, output1.Value);
            Script script1 = output1.Script;
            //script1.GetEmbeddedSigOps TODO Hangs
            Assert.Equal(1UL, script1.GetSigOps(true));
            Assert.Equal(1UL, script1.GetSigOps(false));
            Assert.True(script1.IsValid);
            Assert.True(script1.OperationsAreValid);
            Assert.Equal(67UL, script1.SatoshiContentSize);
            Assert.Equal("[0411db93e1dcdb8a016b49840f8c53bc1eb68a382e97b1482ecad7b148a6909a5cb2e0eaddfb84ccf9744464f82e160bfa9b8b64f9d4c03f999b8643f656b412a3] checksig", script1.ToString(0));
        }        

        private void CheckFirstNonCoinbaseTxFromHeight170(ITransaction tx, string txHashHexStr) {
            Assert.Equal<UInt32>(1, tx.Version);
            Assert.Equal(txHashHexStr, Binary.ByteArrayToHexString(tx.Hash));
            Assert.Equal<UInt32>(0, tx.Locktime);
            Assert.Equal<UInt64>(275, tx.GetSerializedSize(true));
            Assert.Equal<UInt64>(275, tx.GetSerializedSize(false)); //TODO(dario) Does it make sense that it's the same value?
            Assert.Equal<UInt64>(0, tx.Fees);
            Assert.True(0 <= tx.SignatureOperations && tx.SignatureOperations <= Math.Pow(2, 64));
            Assert.Equal<UInt64>(2, tx.GetSignatureOperationsBip16Active(true));
            Assert.Equal<UInt64>(2, tx.GetSignatureOperationsBip16Active(false)); //TODO(dario) Does it make sense that it's the same value?
            Assert.Equal<UInt64>(0, tx.TotalInputValue);
            Assert.Equal<UInt64>(5000000000, tx.TotalOutputValue); //#50 BTC = 5 M Satoshi
            Assert.False(tx.IsCoinbase);
            Assert.False(tx.IsNullNonCoinbase);
            Assert.False(tx.IsOversizeCoinbase);
            Assert.True(tx.IsOverspent); //TODO Why?
            Assert.False(tx.IsDoubleSpend(true));
            Assert.False(tx.IsDoubleSpend(false));
            Assert.True(tx.IsMissingPreviousOutputs); //TODO Why?
            Assert.True(tx.IsFinal(FIRST_NON_COINBASE_BLOCK_HEIGHT, 0));
            Assert.False(tx.IsLocktimeConflict);
            CheckFirstNonCoinbaseTxFromHeight170Inputs(tx);
            CheckFirstNonCoinbaseTxFromHeight170Outputs(tx);
        }

        [Fact]
        public async Task TestGetLastHeight() {
            Tuple<ErrorCode, UInt64> errorAndHeight = await GetLastHeight();
            Assert.Equal(ErrorCode.Success, errorAndHeight.Item1);
        }

        [Fact]
        public async Task TestGetBlockHeaderByHeight() {
            //https://blockchain.info/es/block-height/0
            using (var ret = await nodeFixture_.Node.Chain.GetBlockHeaderByHeightAsync(0)) {
                Assert.Equal(ret.ErrorCode, ErrorCode.Success);
                VerifyGenesisBlockHeader(ret.Result.BlockData);
            }
        }

        [Fact]
        public async Task TestGetBlockHeaderByHash() {
            //https://blockchain.info/es/block-height/0
            byte[] hash = Binary.HexStringToByteArray("000000000019d6689c085ae165831e934ff763ae46a2a6c172b3f1b60a8ce26f");
            using (var ret = await nodeFixture_.Node.Chain.GetBlockHeaderByHashAsync(hash)) {
                Assert.Equal(ret.ErrorCode, ErrorCode.Success);
                VerifyGenesisBlockHeader(ret.Result.BlockData);
            }
        }

        [Fact]
        public async Task TestGetBlockByHeight() {
            //https://blockchain.info/es/block-height/0
            using (var ret = await nodeFixture_.Node.Chain.GetBlockByHeightAsync(0)) {
                Assert.Equal(ret.ErrorCode, ErrorCode.Success);
                VerifyGenesisBlockHeader(ret.Result.BlockData.Header);
            }
        }

        [Fact]
        public async Task TestGetBlockByHash() {
            //https://blockchain.info/es/block-height/0
            byte[] hash = Binary.HexStringToByteArray("000000000019d6689c085ae165831e934ff763ae46a2a6c172b3f1b60a8ce26f");
            using (var ret = await nodeFixture_.Node.Chain.GetBlockByHashAsync(hash)) {
                Assert.Equal(ret.ErrorCode, ErrorCode.Success);
                VerifyGenesisBlockHeader(ret.Result.BlockData.Header);
            }
        }

        [Fact]
        public async Task TestGetBlockHeightAsync() {
            //https://blockchain.info/es/block-height/0
            var hash = Binary.HexStringToByteArray("000000000019d6689c085ae165831e934ff763ae46a2a6c172b3f1b60a8ce26f");
            var ret = await nodeFixture_.Node.Chain.GetBlockHeightAsync(hash);

            Assert.Equal(ret.ErrorCode, ErrorCode.Success);
            Assert.Equal<UInt64>(0, ret.Result);
        }

        [Fact]
        public async Task TestGetSpend() {
            await WaitUntilBlock(FIRST_NON_COINBASE_BLOCK_HEIGHT, "TestGetSpend");

            byte[] hash = Binary.HexStringToByteArray("0437cd7f8525ceed2324359c2d0ba26006d92d856a9c20fa0241106ee5a597c9");
            OutputPoint outputPoint = new OutputPoint(hash, 0);
            var ret = await nodeFixture_.Node.Chain.GetSpendAsync(outputPoint);

            Assert.Equal(ret.ErrorCode, ErrorCode.Success);
            Assert.NotNull(ret.Result);
            Assert.Equal("f4184fc596403b9d638783cf57adfe4c75c605f6356fbc91338530e9831e9e16", Binary.ByteArrayToHexString(ret.Result.Hash));
            Assert.Equal<UInt32>(0, ret.Result.Index);
        }

        [Fact]
        public async Task TestGetMerkleBlockByHash() {
            //https://blockchain.info/es/block-height/0
            byte[] hash = Binary.HexStringToByteArray("000000000019d6689c085ae165831e934ff763ae46a2a6c172b3f1b60a8ce26f");
            using (var ret = await nodeFixture_.Node.Chain.GetMerkleBlockByHashAsync(hash)) {
                Assert.Equal(ret.ErrorCode, ErrorCode.Success);
                Assert.NotNull(ret.Result.BlockData);
                Assert.Equal<UInt64>(0, ret.Result.BlockHeight);
                Assert.Equal<UInt64>(1, ret.Result.BlockData.TotalTransactionCount);
                VerifyGenesisBlockHeader(ret.Result.BlockData.Header);
            }
        }

        [Fact]
        public async Task TestGetMerkleBlockByHeight() {
            //https://blockchain.info/es/block-height/0
            using (var ret = await nodeFixture_.Node.Chain.GetMerkleBlockByHeightAsync(0)) {
                Assert.Equal(ret.ErrorCode, ErrorCode.Success);
                Assert.NotNull(ret.Result.BlockData);
                Assert.Equal<UInt64>(0, ret.Result.BlockHeight);
                Assert.Equal<UInt64>(1, ret.Result.BlockData.TotalTransactionCount);
                VerifyGenesisBlockHeader(ret.Result.BlockData.Header);
            }
        }

        /*
        [Fact]
        public async Task TestGetStealth() {
            using (var ret = await nodeFixture_.Node.Chain.GetStealthAsync(new Binary("1111"), 0)) {
                Assert.Equal(ret.ErrorCode, ErrorCode.Success);
                Assert.Equal<UInt64>(0, ret.Result.Count);
            }
        }
        */

        [Fact]
        public async Task TestGetTransactionGenesis() {
            string txHashHexStr = "4a5e1e4baab89f3a32518a88c31bc87f618f76673e2cc77ab2127b7afdeda33b";
            byte[] hash = Binary.HexStringToByteArray(txHashHexStr);
            using (var ret = await nodeFixture_.Node.Chain.GetTransactionAsync(hash, true)) {
                Assert.Equal(ret.ErrorCode, ErrorCode.Success);
                Assert.Equal<UInt64>(0, ret.Result.TxPosition.BlockHeight);
                Assert.Equal<UInt64>(0, ret.Result.TxPosition.Index);
                
            }
        }

        [Fact]
        public async Task TestGetTransaction() {
            await WaitUntilBlock(FIRST_NON_COINBASE_BLOCK_HEIGHT, "TestGetTransaction");

            string txHashHexStr = "f4184fc596403b9d638783cf57adfe4c75c605f6356fbc91338530e9831e9e16";
            byte[] hash = Binary.HexStringToByteArray(txHashHexStr);
            using (var ret = await nodeFixture_.Node.Chain.GetTransactionAsync(hash, true)) {
                Assert.Equal(ret.ErrorCode, ErrorCode.Success);
                Assert.Equal<UInt64>(FIRST_NON_COINBASE_BLOCK_HEIGHT, ret.Result.TxPosition.BlockHeight);
                Assert.Equal<UInt64>(1, ret.Result.TxPosition.Index);
                CheckFirstNonCoinbaseTxFromHeight170(ret.Result.Tx, txHashHexStr);
            }
        }

        [Fact]
        public async Task TestGetTransactionPosition() {
            await WaitUntilBlock(FIRST_NON_COINBASE_BLOCK_HEIGHT, "TestGetTransactionPosition");

            string txHashHexStr = "f4184fc596403b9d638783cf57adfe4c75c605f6356fbc91338530e9831e9e16";
            byte[] hash = Binary.HexStringToByteArray(txHashHexStr);
            var ret = await nodeFixture_.Node.Chain.GetTransactionPositionAsync(hash, true);

            Assert.Equal(ret.ErrorCode, ErrorCode.Success);
            Assert.Equal<UInt64>(1, ret.Result.Index);
            Assert.Equal<UInt64>(FIRST_NON_COINBASE_BLOCK_HEIGHT, ret.Result.BlockHeight);
        }

        [Fact]
        public async Task TestGetBlockByHash170() {
            await WaitUntilBlock(FIRST_NON_COINBASE_BLOCK_HEIGHT, "TestGetBlockByHash170");

            //https://blockchain.info/es/block-height/170 - 2
            byte[] hash = Binary.HexStringToByteArray("00000000d1145790a8694403d4063f323d499e655c83426834d4ce2f8dd4a2ee");
            using (var ret = await nodeFixture_.Node.Chain.GetBlockByHashAsync(hash)) {
                Assert.Equal(ret.ErrorCode, ErrorCode.Success);
                Assert.NotNull(ret.Result);
                VerifyBlock170Header(ret.Result.BlockData.Header);
            }
        }

     
        /*[Fact]
        public void TestSubscribeToBlockchain() {
            var handlerDone = new AutoResetEvent(false);
            UInt64 height = 0;
            BlockList incomingBlocks = null;
            BlockList outgoingBlocks = null;
            Action<UInt64, BlockList, BlockList> handler = delegate(UInt64 theHeight, BlockList incoming, BlockList outgoing) {
                height = theHeight;
                incomingBlocks = incoming;
                outgoingBlocks = outgoing;
                handlerDone.Set();
            };
            nodeFixture_.Node.Chain.SubscribeBlockNotifications(handler);
            handlerDone.WaitOne();
            //Get the block from another service in order to cross-validate these
            Assert.NotNull(incomingBlocks);
            var firstIncomingBlock = incomingBlocks[0];
            Assert.NotNull(firstIncomingBlock);
            dynamic blockDataFromExternalSource = GetBlockDataFromExternalSource(height);
            Assert.Equal(blockDataFromExternalSource.blocks[0].hash, ByteArrayToHexString(firstIncomingBlock.Hash));
        }*/

        private static dynamic GetBlockDataFromExternalSource(UInt64 height) {
            string uri = @"https://blockchain.info/block-height/" + height + "?format=json";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream)) {
                var jsonObject = JsonConvert.DeserializeObject<dynamic>(reader.ReadToEnd());
                return jsonObject;
            }
        }

        private static void VerifyGenesisBlockHeader(IHeader header) {
            Assert.NotNull(header);
            Assert.Equal("000000000019d6689c085ae165831e934ff763ae46a2a6c172b3f1b60a8ce26f", Binary.ByteArrayToHexString(header.Hash));
            Assert.Equal("4a5e1e4baab89f3a32518a88c31bc87f618f76673e2cc77ab2127b7afdeda33b", Binary.ByteArrayToHexString(header.Merkle));
            Assert.Equal("0000000000000000000000000000000000000000000000000000000000000000", Binary.ByteArrayToHexString(header.PreviousBlockHash));
            Assert.Equal<UInt32>(1, header.Version);
            Assert.Equal<UInt32>(486604799, header.Bits);
            Assert.Equal<UInt32>(2083236893, header.Nonce);
            DateTime utcTime = DateTimeOffset.FromUnixTimeSeconds(header.Timestamp).DateTime;
            Assert.Equal("2009-01-03 18:15:05", utcTime.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        private static void VerifyBlock170Header(IHeader header) {
            Assert.NotNull(header);
            Assert.Equal("00000000d1145790a8694403d4063f323d499e655c83426834d4ce2f8dd4a2ee", Binary.ByteArrayToHexString(header.Hash));
            Assert.Equal("7dac2c5666815c17a3b36427de37bb9d2e2c5ccec3f8633eb91a4205cb4c10ff", Binary.ByteArrayToHexString(header.Merkle));
            Assert.Equal("000000002a22cfee1f2c846adbd12b3e183d4f97683f85dad08a79780a84bd55", Binary.ByteArrayToHexString(header.PreviousBlockHash));
            Assert.Equal<UInt32>(1, header.Version);
            Assert.Equal<UInt32>(486604799, header.Bits);
            Assert.Equal<UInt32>(1889418792, header.Nonce);
            DateTime utcTime = DateTimeOffset.FromUnixTimeSeconds(header.Timestamp).DateTime;
            Assert.Equal("2009-01-12 03:30:25", utcTime.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        [Fact]
        public async Task GetBlockHeaderByHashTxSizesAsync() {
            var hash = Binary.HexStringToByteArray("000000000019d6689c085ae165831e934ff763ae46a2a6c172b3f1b60a8ce26f");
            using (var ret = await nodeFixture_.Node.Chain.GetBlockHeaderByHashTxSizesAsync(hash)) {
                Assert.Equal(ret.ErrorCode, ErrorCode.Success);
                VerifyGenesisBlockHeader(ret.Result.Header.BlockData);
            }
        }

        [Fact]
        public async Task GetBlockByHeightHashTimestampAsync() {
            var ret = await nodeFixture_.Node.Chain.GetBlockByHeightHashTimestampAsync(0);
            Assert.Equal(ret.ErrorCode, ErrorCode.Success);
            Assert.Equal("2009-01-03 18:15:05", ret.Result.BlockTimestamp.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        [Fact]
        public void GetMempoolTransactions() {
            using (var addresses = new PaymentAddressList()) {
                addresses.Add(new PaymentAddress("bchtest:qp7d6x2weeca9fn6eakwvgd9ryq8g6h0tuyks75rt7"));
                using (var list = nodeFixture_.Node.Chain.GetMempoolTransactions(addresses, true))
                {
                    Assert.True(list.Count >= 0);
                }
            }
        }

        /*
        [Fact]
        public async Task GetBlockLocatorAsync() {
            using (var list = new BlockIndexList()) {
                list.Add(1);
                list.Add(2);
                list.Add(3);

                using (DisposableApiCallResult<HeaderReader> ret = await nodeFixture_.Node.Chain.GetBlockLocatorAsync(list))
                {
                    Assert.True(ret.Result.IsValid);
                }
            }

        }*/

       /* [Fact]
        public async Task GetCompactBlockByHashAsync() {
            var hash = Binary.HexStringToByteArray("000000000933ea01ad0ee984209779baaec3ced90fa3f408719526f8d77f4943");
            using (var ret = await nodeFixture_.Node.Chain.GetCompactBlockByHashAsync(hash)) {
                Assert.True(ret.Result.BlockData.IsValid); 
            }
        }*/
        
        /*
        [Fact]
        public async Task GetCompactBlockByHeightAsync() {
            using (var ret = await nodeFixture_.Node.Chain.GetCompactBlockByHeightAsync(1)) {
                Assert.True(ret.Result.BlockData.IsValid); 
            }
        }*/

        [Fact]
        public async Task GetHistoryAsync() {
            using (var address = new PaymentAddress("bchtest:qp7d6x2weeca9fn6eakwvgd9ryq8g6h0tuyks75rt7")) {
                using (var ret = await nodeFixture_.Node.Chain.GetHistoryAsync(address,10,1))
                {
                    foreach (var x in ret.Result) {

                    }
                    Assert.True(ret.Result.Count >= 0); 
                }
            }
        }

        [Fact]
        public async Task GetConfirmedTransactionsAsync() {
            using (var address = new PaymentAddress("bchtest:qp7d6x2weeca9fn6eakwvgd9ryq8g6h0tuyks75rt7"))
            using (var ret = await nodeFixture_.Node.Chain.GetConfirmedTransactionsAsync(address,10,1)) {
                Assert.True(ret.Result.Count >= 0); 
            }
        }

        [Fact]
        public async Task OrganizeBlockAsync() {
            using (var block = await nodeFixture_.Node.Chain.GetBlockByHeightAsync(0)) {
                var aa = block.Result.BlockData.IsValid;
                var bb = block.Result.BlockData.IsValidMerkleRoot;
                var ret = await nodeFixture_.Node.Chain.OrganizeBlockAsync((Block)block.Result.BlockData);
                Assert.True(ret == ErrorCode.DuplicateBlock);
            }
        }

        [Fact]
        public async Task OrganizeTransactionAsync() {
            using (var block = await nodeFixture_.Node.Chain.GetBlockByHeightAsync(0)) {
                var ret = await nodeFixture_.Node.Chain.OrganizeTransactionAsync((Transaction)block.Result.BlockData.GetNthTransaction(0));
                Assert.True(ret == ErrorCode.CoinbaseTransaction);
            }
        }

        [Fact]
        public async Task ValidateTransactionAsync() {
            using (var block = await nodeFixture_.Node.Chain.GetBlockByHeightAsync(0)) {
                var ret = await nodeFixture_.Node.Chain.ValidateTransactionAsync((Transaction)block.Result.BlockData.GetNthTransaction(0));
                Assert.True(ret.ErrorCode == ErrorCode.CoinbaseTransaction);
            }
        }

        [Fact]
        public void IsStale() {
            var ret = nodeFixture_.Node.Chain.IsStale;
            Assert.True(ret);
        }

        [Fact]
        public void CreateBlockFromHex() {
            var hex = "0100000000000000000000000000000000000000000000000000000000000000000000003ba3edfd7a7b12b27ac72c3e67768f617fc81bc3888a51323a9fb8aa4b1e5e4adae5494dffff7f20020000000101000000010000000000000000000000000000000000000000000000000000000000000000ffffffff4d04ffff001d0104455468652054696d65732030332f4a616e2f32303039204368616e63656c6c6f72206f6e206272696e6b206f66207365636f6e64206261696c6f757420666f722062616e6b73ffffffff0100f2052a01000000434104678afdb0fe5548271967f1a67130b7105cd6a828e03909a67962e0ea1f61deb649f6bc3f4cef38c4f35504e51ec112de5c384df7ba0b8d578a4c702b6bf11d5fac00000000";
            using (Block b = new Block(1, hex)) {
                Assert.True(b.IsValid); 
            }
        }
    }
}
