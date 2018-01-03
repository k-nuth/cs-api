using Bitprim;
using System;
using System.Text;
using System.Threading;
using Xunit;
using System.Net;
using System.IO;
using Newtonsoft.Json;

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
            Tuple<int,UInt64> errorAndHeight = GetLastHeight();
            Assert.Equal(0, errorAndHeight.Item1);
        }

        [Fact]
        public void TestFetchBlockHeaderByHeight()
        {
            var handlerDone = new AutoResetEvent(false);
            int error = 0;
            Header header = null;

            Action<int, Header> handler = delegate(int theError, Header theHeader)
            {
                error = theError;
                header = theHeader;
                handlerDone.Set();
            };
            //https://blockchain.info/es/block-height/0
            executorFixture_.Executor.Chain.FetchBlockHeaderByHeight(0, handler);
            handlerDone.WaitOne();

            Assert.Equal(0, error);
            VerifyGenesisBlockHeader(header);
        }

        [Fact]
        public void TestFetchBlockHeaderByHash()
        {
            var handlerDone = new AutoResetEvent(false);
            int error = 0;
            Header header = null;

            Action<int, Header> handler = delegate(int theError, Header theHeader)
            {
                error = theError;
                header = theHeader;
                handlerDone.Set();
            };
            //https://blockchain.info/es/block-height/0
            byte[] hash = Binary.HexStringToByteArray("000000000019d6689c085ae165831e934ff763ae46a2a6c172b3f1b60a8ce26f");
            executorFixture_.Executor.Chain.FetchBlockHeaderByHash(hash, handler);
            handlerDone.WaitOne();

            Assert.Equal(0, error);
            VerifyGenesisBlockHeader(header);
        }

        [Fact]
        public void TestFetchBlockByHeight()
        {
            var handlerDone = new AutoResetEvent(false);
            int error = 0;
            Block block = null;

            Action<int, Block> handler = delegate(int theError, Block theBlock)
            {
                error = theError;
                block = theBlock;
                handlerDone.Set();
            };
            //https://blockchain.info/es/block-height/0
            executorFixture_.Executor.Chain.FetchBlockByHeight(0, handler);
            handlerDone.WaitOne();

            Assert.Equal(0, error);
            VerifyGenesisBlockHeader(block.Header);
        }

        [Fact]
        public void TestFetchBlockByHash()
        {
            var handlerDone = new AutoResetEvent(false);
            int error = 0;
            Block block = null;

            Action<int, Block> handler = delegate(int theError, Block theBlock)
            {
                error = theError;
                block = theBlock;
                handlerDone.Set();
            };
            //https://blockchain.info/es/block-height/0
            byte[] hash = Binary.HexStringToByteArray("000000000019d6689c085ae165831e934ff763ae46a2a6c172b3f1b60a8ce26f");
            executorFixture_.Executor.Chain.FetchBlockByHash(hash, handler);
            handlerDone.WaitOne();

            Assert.Equal(0, error);
            VerifyGenesisBlockHeader(block.Header);
        }

        [Fact]
        public void TestFetchBlockHeight()
        {
            var handlerDone = new AutoResetEvent(false);
            int error = 0;
            UInt64 height = 0;

            Action<int, UInt64> handler = delegate(int theError, UInt64 theHeight)
            {
                error = theError;
                height = theHeight;
                handlerDone.Set();
            };
            //https://blockchain.info/es/block-height/0
            byte[] hash = Binary.HexStringToByteArray("000000000019d6689c085ae165831e934ff763ae46a2a6c172b3f1b60a8ce26f");
            executorFixture_.Executor.Chain.FetchBlockHeight(hash, handler);
            handlerDone.WaitOne();

            Assert.Equal(0, error);
            Assert.Equal<UInt64>(0, height);
        }

        [Fact]
        public void TestFetchSpend()
        {
            var handlerDone = new AutoResetEvent(false);
            WaitUntilBlock(FIRST_NON_COINBASE_BLOCK_HEIGHT, "TestFetchSpend");

            int error = 0;
            Point point = null;

            Action<int, Point> handler = delegate(int theError, Point thePoint)
            {
                error = theError;
                point = thePoint;
                handlerDone.Set();
            };
            byte[] hash = Binary.HexStringToByteArray("0437cd7f8525ceed2324359c2d0ba26006d92d856a9c20fa0241106ee5a597c9");
            OutputPoint outputPoint = new OutputPoint(hash, 0);
            executorFixture_.Executor.Chain.FetchSpend(outputPoint, handler);
            handlerDone.WaitOne();

            Assert.Equal(0, error);
            Assert.NotNull(point);
            Assert.Equal("f4184fc596403b9d638783cf57adfe4c75c605f6356fbc91338530e9831e9e16", Binary.ByteArrayToHexString(point.Hash));
            Assert.Equal<UInt32>(0, point.Index);
        }

        [Fact]
        public void TestFetchMerkleBlockByHash()
        {
            var handlerDone = new AutoResetEvent(false);
            int error = 0;
            MerkleBlock merkleBlock = null;
            UInt64 height = 0;

            Action<int, MerkleBlock, UInt64> handler = delegate(int theError, MerkleBlock theBlock, UInt64 theHeight)
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

            Assert.Equal(0, error);
            Assert.NotNull(merkleBlock);
            Assert.Equal<UInt64>(0, height);
            Assert.Equal<UInt64>(1, merkleBlock.TotalTransactionCount);
            VerifyGenesisBlockHeader(merkleBlock.Header);
        }

        [Fact]
        public void TestFetchMerkleBlockByHeight()
        {
            var handlerDone = new AutoResetEvent(false);
            int error = 0;
            MerkleBlock merkleBlock = null;
            UInt64 height = 0;

            Action<int, MerkleBlock, UInt64> handler = delegate(int theError, MerkleBlock theBlock, UInt64 theHeight)
            {
                error = theError;
                merkleBlock = theBlock;
                height = theHeight;
                handlerDone.Set();
            };
            //https://blockchain.info/es/block-height/0
            executorFixture_.Executor.Chain.FetchMerkleBlockByHeight(0, handler);
            handlerDone.WaitOne();

            Assert.Equal(0, error);
            Assert.NotNull(merkleBlock);
            Assert.Equal<UInt64>(0, height);
            Assert.Equal<UInt64>(1, merkleBlock.TotalTransactionCount);
            VerifyGenesisBlockHeader(merkleBlock.Header);
        }

        [Fact]
        public void TestFetchStealth()
        {
            var handlerDone = new AutoResetEvent(false);
            int error = 0;
            StealthCompactList list = null;

            Action<int, StealthCompactList> handler = delegate(int theError, StealthCompactList theList)
            {
                error = theError;
                list = theList;
                handlerDone.Set();
            };
            executorFixture_.Executor.Chain.FetchStealth(new Binary("1111"), 0, handler);
            handlerDone.WaitOne();
            
            Assert.Equal(0, error);
            Assert.Equal<uint>(0, list.Count);
        }

        [Fact]
        public void TestFetchTransaction()
        {
            var handlerDone = new AutoResetEvent(false);
            int error = 0;
            Transaction tx = null;
            UInt64 height = 0;
            UInt64 index = 0;

            WaitUntilBlock(FIRST_NON_COINBASE_BLOCK_HEIGHT, "TestFetchTransaction");

            Action<int, Transaction, UInt64, UInt64> handler = delegate(int theError, Transaction theTx, UInt64 theIndex, UInt64 theHeight)
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

            Assert.Equal(0, error);
            Assert.Equal<UInt64>(FIRST_NON_COINBASE_BLOCK_HEIGHT, height);
            Assert.Equal<UInt64>(1, index);
            CheckFirstNonCoinbaseTxFromHeight170(tx, txHashHexStr);
        }

        [Fact]
        public void TestFetchTransactionPosition()
        {
            var handlerDone = new AutoResetEvent(false);
            int error = 0;
            UInt64 height = 0;
            UInt64 index = 0;

            WaitUntilBlock(FIRST_NON_COINBASE_BLOCK_HEIGHT, "TestFetchTransactionPosition");

            Action<int, UInt64, UInt64> handler = delegate(int theError, UInt64 theIndex, UInt64 theHeight)
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

            Assert.Equal(0, error);
            Assert.Equal<UInt64>(1, index);
            Assert.Equal<UInt64>(FIRST_NON_COINBASE_BLOCK_HEIGHT, height);
        }

        [Fact]
        public void TestFetchBlockByHash170()
        {
            var handlerDone = new AutoResetEvent(false);
            int error = 0;
            Block block = null;

            WaitUntilBlock(FIRST_NON_COINBASE_BLOCK_HEIGHT, "TestFetchBlockByHash170");

            Action<int, Block> handler = delegate(int theError, Block theBlock)
            {
                error = theError;
                block = theBlock;
                handlerDone.Set();
            };
            //https://blockchain.info/es/block-height/170 - 2
            byte[] hash = Binary.HexStringToByteArray("00000000d1145790a8694403d4063f323d499e655c83426834d4ce2f8dd4a2ee");
            executorFixture_.Executor.Chain.FetchBlockByHash(hash, handler);
            handlerDone.WaitOne();

            Assert.Equal(0, error);
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
            Assert.Equal("00000000d1145790a8694403d4063f323d499e655c83426834d4ce2f8dd4a2ee", ByteArrayToHexString(header.Hash));
            Assert.Equal("7dac2c5666815c17a3b36427de37bb9d2e2c5ccec3f8633eb91a4205cb4c10ff", ByteArrayToHexString(header.Merkle));
            Assert.Equal("000000002a22cfee1f2c846adbd12b3e183d4f97683f85dad08a79780a84bd55", ByteArrayToHexString(header.PreviousBlockHash));
            Assert.Equal<UInt32>(1, header.Version);
            Assert.Equal<UInt32>(486604799, header.Bits);
            Assert.Equal<UInt32>(1889418792, header.Nonce);
            DateTime utcTime = DateTimeOffset.FromUnixTimeSeconds(header.Timestamp).DateTime;
            Assert.Equal("2009-01-12 03:30:25", utcTime.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        private Tuple<int, UInt64> GetLastHeight()
        {
            var handlerDone = new AutoResetEvent(false);
            int error = 0;
            UInt64 height = 0;
            Action<int, UInt64> handler = delegate(int theError, UInt64 theHeight)
            {
                error = theError;
                height = theHeight;
                handlerDone.Set();
            };
            executorFixture_.Executor.Chain.FetchLastHeight(handler);
            handlerDone.WaitOne();
            return new Tuple<int, UInt64>(error, height);
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
            Assert.True(tx.IsOverspent); //Because it's coinbase, inputs don't add up to outputs
            Assert.False(tx.IsDoubleSpend(true));
            Assert.False(tx.IsDoubleSpend(false));
            Assert.True(tx.IsMissingPreviousOutputs); //Because it's coinbase
            Assert.True(tx.IsFinal(FIRST_NON_COINBASE_BLOCK_HEIGHT, 0));
            Assert.False(tx.IsLocktimeConflict);
        }

        private void WaitUntilBlock(UInt64 desiredHeight, string callerName)
        {
            int error = 0;
            UInt64 height = 0;            
            while(error == 0 && height < desiredHeight){
                Console.WriteLine("--->" + callerName + " checking height: " + height);
                Tuple<int, UInt64> errorAndHeight = GetLastHeight();
                error = errorAndHeight.Item1;
                height = errorAndHeight.Item2;
                if(height < desiredHeight)
                {
                    System.Threading.Thread.Sleep(10000);
                }
            }
            Assert.Equal(error, 0);
        }

    }
}
