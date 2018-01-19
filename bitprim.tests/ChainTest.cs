using Bitprim;
using System;
using System.Text;
using System.Threading;
using Xunit;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Bitprim.Native;

namespace Bitprim.Tests
{
    public class ChainTest : IClassFixture<ExecutorFixture>
    {
        private const int FIRST_NON_COINBASE_BLOCK_HEIGHT = 170;
        private ExecutorFixture executorFixture_;

        public ChainTest(ExecutorFixture fixture)
        {
            executorFixture_ = fixture;
        }

        [Fact]
        public void TestFetchLastHeight()
        {
            Tuple<ErrorCode,UInt64> errorAndHeight = GetLastHeight();
            Assert.Equal(ErrorCode.Success, errorAndHeight.Item1);
        }

        [Fact]
        public void TestFetchBlockHeaderByHeight()
        {
            var handlerDone = new AutoResetEvent(false);
            ErrorCode error = 0;
            Header header = null;

            Action<ErrorCode, Header> handler = delegate(ErrorCode theError, Header theHeader)
            {
                error = theError;
                header = theHeader;
                handlerDone.Set();
            };
            //https://blockchain.info/es/block-height/0
            executorFixture_.Executor.Chain.FetchBlockHeaderByHeight(0, handler);
            handlerDone.WaitOne();

            Assert.Equal(ErrorCode.Success, error);
            VerifyGenesisBlockHeader(header);
        }

        [Fact]
        public void TestFetchBlockHeaderByHash()
        {
            var handlerDone = new AutoResetEvent(false);
            ErrorCode error = 0;
            Header header = null;

            Action<ErrorCode, Header> handler = delegate(ErrorCode theError, Header theHeader)
            {
                error = theError;
                header = theHeader;
                handlerDone.Set();
            };
            //https://blockchain.info/es/block-height/0
            byte[] hash = Binary.HexStringToByteArray("000000000019d6689c085ae165831e934ff763ae46a2a6c172b3f1b60a8ce26f");
            executorFixture_.Executor.Chain.FetchBlockHeaderByHash(hash, handler);
            handlerDone.WaitOne();

            Assert.Equal(ErrorCode.Success, error);
            VerifyGenesisBlockHeader(header);
        }

        [Fact]
        public void TestFetchBlockByHeight()
        {
            var handlerDone = new AutoResetEvent(false);
            ErrorCode error = 0;
            Block block = null;

            Action<ErrorCode, Block> handler = delegate(ErrorCode theError, Block theBlock)
            {
                error = theError;
                block = theBlock;
                handlerDone.Set();
            };
            //https://blockchain.info/es/block-height/0
            executorFixture_.Executor.Chain.FetchBlockByHeight(0, handler);
            handlerDone.WaitOne();

            Assert.Equal(ErrorCode.Success, error);
            VerifyGenesisBlockHeader(block.Header);
        }

        [Fact]
        public void TestFetchBlockByHash()
        {
            var handlerDone = new AutoResetEvent(false);
            ErrorCode error = 0;
            Block block = null;

            Action<ErrorCode, Block> handler = delegate(ErrorCode theError, Block theBlock)
            {
                error = theError;
                block = theBlock;
                handlerDone.Set();
            };
            //https://blockchain.info/es/block-height/0
            byte[] hash = Binary.HexStringToByteArray("000000000019d6689c085ae165831e934ff763ae46a2a6c172b3f1b60a8ce26f");
            executorFixture_.Executor.Chain.FetchBlockByHash(hash, handler);
            handlerDone.WaitOne();

            Assert.Equal(ErrorCode.Success, error);
            VerifyGenesisBlockHeader(block.Header);
        }

        [Fact]
        public void TestGetBlockByHash()
        {
            //https://blockchain.info/es/block-height/0
            byte[] hash = Binary.HexStringToByteArray("000000000019d6689c085ae165831e934ff763ae46a2a6c172b3f1b60a8ce26f");
            Tuple<ErrorCode, Block, UInt64> result = executorFixture_.Executor.Chain.GetBlockByHash(hash);
            ErrorCode error = result.Item1;
            Block block = result.Item2;
            UInt64 height = result.Item3;
            Assert.Equal(ErrorCode.Success, error);
            VerifyGenesisBlockHeader(block.Header);
            Assert.Equal(0UL, height);
        }

        [Fact]
        public void TestFetchBlockHeight()
        {
            var handlerDone = new AutoResetEvent(false);
            ErrorCode error = 0;
            UInt64 height = 0;

            Action<ErrorCode, UInt64> handler = delegate(ErrorCode theError, UInt64 theHeight)
            {
                error = theError;
                height = theHeight;
                handlerDone.Set();
            };
            //https://blockchain.info/es/block-height/0
            byte[] hash = Binary.HexStringToByteArray("000000000019d6689c085ae165831e934ff763ae46a2a6c172b3f1b60a8ce26f");
            executorFixture_.Executor.Chain.FetchBlockHeight(hash, handler);
            handlerDone.WaitOne();

            Assert.Equal(ErrorCode.Success, error);
            Assert.Equal<UInt64>(0, height);
        }

        [Fact]
        public void TestFetchSpend()
        {
            var handlerDone = new AutoResetEvent(false);
            WaitUntilBlock(FIRST_NON_COINBASE_BLOCK_HEIGHT, "TestFetchSpend");

            ErrorCode error = 0;
            Point point = null;

            Action<ErrorCode, Point> handler = delegate(ErrorCode theError, Point thePoint)
            {
                error = theError;
                point = thePoint;
                handlerDone.Set();
            };
            byte[] hash = Binary.HexStringToByteArray("0437cd7f8525ceed2324359c2d0ba26006d92d856a9c20fa0241106ee5a597c9");
            OutputPoint outputPoint = new OutputPoint(hash, 0);
            executorFixture_.Executor.Chain.FetchSpend(outputPoint, handler);
            handlerDone.WaitOne();

            Assert.Equal(ErrorCode.Success, error);
            Assert.NotNull(point);
            Assert.Equal("f4184fc596403b9d638783cf57adfe4c75c605f6356fbc91338530e9831e9e16", Binary.ByteArrayToHexString(point.Hash));
            Assert.Equal<UInt32>(0, point.Index);
        }

        [Fact]
        public void TestFetchMerkleBlockByHash()
        {
            var handlerDone = new AutoResetEvent(false);
            ErrorCode error = 0;
            MerkleBlock merkleBlock = null;
            UInt64 height = 0;

            Action<ErrorCode, MerkleBlock, UInt64> handler = delegate(ErrorCode theError, MerkleBlock theBlock, UInt64 theHeight)
            {
                error = theError;
                merkleBlock = theBlock;
                height = theHeight;
                handlerDone.Set();
            };
            //https://blockchain.info/es/block-height/0
            byte[] hash = Binary.HexStringToByteArray("000000000019d6689c085ae165831e934ff763ae46a2a6c172b3f1b60a8ce26f");
            executorFixture_.Executor.Chain.FetchMerkleBlockByHash(hash, handler);
            handlerDone.WaitOne();

            Assert.Equal(ErrorCode.Success, error);
            Assert.NotNull(merkleBlock);
            Assert.Equal<UInt64>(0, height);
            Assert.Equal<UInt64>(1, merkleBlock.TotalTransactionCount);
            VerifyGenesisBlockHeader(merkleBlock.Header);
        }

        [Fact]
        public void TestFetchMerkleBlockByHeight()
        {
            var handlerDone = new AutoResetEvent(false);
            ErrorCode error = 0;
            MerkleBlock merkleBlock = null;
            UInt64 height = 0;

            Action<ErrorCode, MerkleBlock, UInt64> handler = delegate(ErrorCode theError, MerkleBlock theBlock, UInt64 theHeight)
            {
                error = theError;
                merkleBlock = theBlock;
                height = theHeight;
                handlerDone.Set();
            };
            //https://blockchain.info/es/block-height/0
            executorFixture_.Executor.Chain.FetchMerkleBlockByHeight(0, handler);
            handlerDone.WaitOne();

            Assert.Equal(ErrorCode.Success, error);
            Assert.NotNull(merkleBlock);
            Assert.Equal<UInt64>(0, height);
            Assert.Equal<UInt64>(1, merkleBlock.TotalTransactionCount);
            VerifyGenesisBlockHeader(merkleBlock.Header);
        }

        [Fact]
        public void TestFetchStealth()
        {
            var handlerDone = new AutoResetEvent(false);
            ErrorCode error = 0;
            StealthCompactList list = null;

            Action<ErrorCode, StealthCompactList> handler = delegate(ErrorCode theError, StealthCompactList theList)
            {
                error = theError;
                list = theList;
                handlerDone.Set();
            };
            executorFixture_.Executor.Chain.FetchStealth(new Binary("1111"), 0, handler);
            handlerDone.WaitOne();
            
            Assert.Equal(ErrorCode.Success, error);
            Assert.Equal<uint>(0, list.Count);
        }

        [Fact]
        public void TestFetchTransaction()
        {
            var handlerDone = new AutoResetEvent(false);
            ErrorCode error = 0;
            Transaction tx = null;
            UInt64 height = 0;
            UInt64 index = 0;

            WaitUntilBlock(FIRST_NON_COINBASE_BLOCK_HEIGHT, "TestFetchTransaction");

            Action<ErrorCode, Transaction, UInt64, UInt64> handler = delegate(ErrorCode theError, Transaction theTx, UInt64 theIndex, UInt64 theHeight)
            {
                error = theError;
                tx = theTx;
                height = theHeight;
                index = theIndex;
                handlerDone.Set();
            };
            string txHashHexStr = "f4184fc596403b9d638783cf57adfe4c75c605f6356fbc91338530e9831e9e16";
            byte[] hash = Binary.HexStringToByteArray(txHashHexStr);
            executorFixture_.Executor.Chain.FetchTransaction(hash, true, handler);
            handlerDone.WaitOne();

            Assert.Equal(ErrorCode.Success, error);
            Assert.Equal<UInt64>(FIRST_NON_COINBASE_BLOCK_HEIGHT, height);
            Assert.Equal<UInt64>(1, index);
            CheckFirstNonCoinbaseTxFromHeight170(tx, txHashHexStr);
        }

        [Fact]
        public void TestFetchTransactionPosition()
        {
            var handlerDone = new AutoResetEvent(false);
            ErrorCode error = 0;
            UInt64 height = 0;
            UInt64 index = 0;

            WaitUntilBlock(FIRST_NON_COINBASE_BLOCK_HEIGHT, "TestFetchTransactionPosition");

            Action<ErrorCode, UInt64, UInt64> handler = delegate(ErrorCode theError, UInt64 theIndex, UInt64 theHeight)
            {
                error = theError;
                index = theIndex;
                height = theHeight;
                handlerDone.Set();
            };
            string txHashHexStr = "f4184fc596403b9d638783cf57adfe4c75c605f6356fbc91338530e9831e9e16";
            byte[] hash = Binary.HexStringToByteArray(txHashHexStr);
            executorFixture_.Executor.Chain.FetchTransactionPosition(hash, true, handler);
            handlerDone.WaitOne();

            Assert.Equal(ErrorCode.Success, error);
            Assert.Equal<UInt64>(1, index);
            Assert.Equal<UInt64>(FIRST_NON_COINBASE_BLOCK_HEIGHT, height);
        }

        [Fact]
        public void TestFetchBlockByHash170()
        {
            var handlerDone = new AutoResetEvent(false);
            ErrorCode error = 0;
            Block block = null;

            WaitUntilBlock(FIRST_NON_COINBASE_BLOCK_HEIGHT, "TestFetchBlockByHash170");

            Action<ErrorCode, Block> handler = delegate(ErrorCode theError, Block theBlock)
            {
                error = theError;
                block = theBlock;
                handlerDone.Set();
            };
            //https://blockchain.info/es/block-height/170 - 2
            byte[] hash = Binary.HexStringToByteArray("00000000d1145790a8694403d4063f323d499e655c83426834d4ce2f8dd4a2ee");
            executorFixture_.Executor.Chain.FetchBlockByHash(hash, handler);
            handlerDone.WaitOne();

            Assert.Equal(ErrorCode.Success, error);
            Assert.NotNull(block);
            VerifyBlock170Header(block.Header);
        }

        /*[Fact]
        public void TestSubscribeToBlockchain()
        {
            var handlerDone = new AutoResetEvent(false);
            UInt64 height = 0;
            BlockList incomingBlocks = null;
            BlockList outgoingBlocks = null;
            Action<UInt64, BlockList, BlockList> handler = delegate(UInt64 theHeight, BlockList incoming, BlockList outgoing)
            {
                height = theHeight;
                incomingBlocks = incoming;
                outgoingBlocks = outgoing;
                handlerDone.Set();
            };
            executorFixture_.Executor.Chain.SubscribeToBlockChain(handler);
            handlerDone.WaitOne();
            //Get the block from another service in order to cross-validate these
            Assert.NotNull(incomingBlocks);
            var firstIncomingBlock = incomingBlocks[0];
            Assert.NotNull(firstIncomingBlock);
            dynamic blockDataFromExternalSource = GetBlockDataFromExternalSource(height);
            Assert.Equal(blockDataFromExternalSource.blocks[0].hash, ByteArrayToHexString(firstIncomingBlock.Hash));
        }*/

        private static dynamic GetBlockDataFromExternalSource(UInt64 height)
        {
            string uri = @"https://blockchain.info/block-height/" + height + "?format=json";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            using(HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using(Stream stream = response.GetResponseStream())
            using(StreamReader reader = new StreamReader(stream))
            {
                var jsonObject = JsonConvert.DeserializeObject<dynamic>(reader.ReadToEnd());
                return jsonObject;
            }
        }

        private static void VerifyGenesisBlockHeader(Header header)
        {
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

        private static void VerifyBlock170Header(Header header)
        {
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

        private Tuple<ErrorCode, UInt64> GetLastHeight()
        {
            var handlerDone = new AutoResetEvent(false);
            ErrorCode error = 0;
            UInt64 height = 0;
            Action<ErrorCode, UInt64> handler = delegate(ErrorCode theError, UInt64 theHeight)
            {
                error = theError;
                height = theHeight;
                handlerDone.Set();
            };
            executorFixture_.Executor.Chain.FetchLastHeight(handler);
            handlerDone.WaitOne();
            return new Tuple<ErrorCode, UInt64>(error, height);
        }

        private void CheckFirstNonCoinbaseTxFromHeight170(Transaction tx, string txHashHexStr)
        {
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

        private void CheckFirstNonCoinbaseTxFromHeight170Inputs(Transaction tx)
        {
            Assert.Equal(1UL, tx.Inputs.Count);
            //Assert.Equal(50000000UL, tx.TotalInputValue); //TODO Blockdozer says this is 50 BTC
            //Input 0
            Input input = tx.Inputs[0];
            Assert.Equal(4294967295, input.Sequence);
            Assert.Equal(113UL, input.GetSerializedSize(true));
            Assert.Equal(111UL, input.GetSerializedSize(false));
            Assert.Equal(0UL, input.GetSignatureOperationsCount(true));
            Assert.Equal(0UL, input.GetSignatureOperationsCount(false));
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

        private void CheckFirstNonCoinbaseTxFromHeight170Outputs(Transaction tx)
        {
            Assert.Equal(2UL, tx.Outputs.Count);
            Assert.Equal(5000000000UL, tx.TotalOutputValue);
            //Output 0
            Output output0 = tx.Outputs[0];
            Assert.Equal(76UL, output0.GetSerializedSize(true));
            Assert.Equal(76UL, output0.GetSerializedSize(true)); //TODO In inputs, it's two bytes less; does this make sense?
            Assert.True(output0.IsValid);
            Assert.Equal(1UL, output0.SignatureOperationCount);
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
            Assert.Equal(1UL, output1.SignatureOperationCount);
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

        private void WaitUntilBlock(UInt64 desiredHeight, string callerName)
        {
            ErrorCode error = 0;
            UInt64 height = 0;            
            while(error == 0 && height < desiredHeight){
                Console.WriteLine("--->" + callerName + " checking height: " + height);
                Tuple<ErrorCode, UInt64> errorAndHeight = GetLastHeight();
                error = errorAndHeight.Item1;
                height = errorAndHeight.Item2;
                if(height < desiredHeight)
                {
                    System.Threading.Thread.Sleep(10000);
                }
            }
            Assert.Equal(ErrorCode.Success, error);
        }

    }
}
