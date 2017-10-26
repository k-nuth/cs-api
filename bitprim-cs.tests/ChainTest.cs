using BitprimCs;
using System;
using System.Text;
using System.Threading;
using Xunit;

namespace BitprimCs.Tests
{
    public class ChainTest : IClassFixture<ExecutorFixture>
    {
        ExecutorFixture executorFixture_;

        public ChainTest(ExecutorFixture fixture)
        {
            executorFixture_ = fixture;
        }

        [Fact]
        public void TestFetchLastHeight()
        {
            var handlerDone = new AutoResetEvent(false);
            int error = 0;
            UInt64 height = 0;
            Action<int,UInt64> handler = delegate(int theError, UInt64 theHeight)
            {
                error = theError;
                height = theHeight;
                handlerDone.Set();
            };
            executorFixture_.Executor.Chain.FetchLastHeight(handler);
            handlerDone.WaitOne();
            Assert.Equal(error, 0);
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

            Assert.Equal(error, 0);
            Assert.NotNull(header);
            Assert.Equal("000000000019d6689c085ae165831e934ff763ae46a2a6c172b3f1b60a8ce26f", ByteArrayToHexString(header.Hash));
            Assert.Equal("4a5e1e4baab89f3a32518a88c31bc87f618f76673e2cc77ab2127b7afdeda33b", ByteArrayToHexString(header.Merkle));
            Assert.Equal("0000000000000000000000000000000000000000000000000000000000000000", ByteArrayToHexString(header.PreviousBlockHash));
            Assert.Equal<UInt32>(1, header.Version);
            Assert.Equal<UInt32>(486604799, header.Bits);
            Assert.Equal<UInt32>(2083236893, header.Nonce);
            
            DateTime utcTime = DateTimeOffset.FromUnixTimeSeconds(header.Timestamp).DateTime;
            Assert.Equal("2009-01-03 18:15:05", utcTime.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        [Fact]
        public void TestFetchBlockHeaderByHash()
        {
            //https://blockchain.info/es/block-height/0
            var handlerDone = new AutoResetEvent(false);
            int error = 0;
            Header header = null;

            Action<int, Header> handler = delegate(int theError, Header theHeader)
            {
                error = theError;
                header = theHeader;
                handlerDone.Set();
            };
            byte[] hash = HexStringToByteArray("000000000019d6689c085ae165831e934ff763ae46a2a6c172b3f1b60a8ce26f");
            executorFixture_.Executor.Chain.FetchBlockHeaderByHash(hash, handler);
            handlerDone.WaitOne();

            Assert.Equal(error, 0);
            Assert.NotNull(header);
            Assert.Equal("000000000019d6689c085ae165831e934ff763ae46a2a6c172b3f1b60a8ce26f", ByteArrayToHexString(header.Hash));
            Assert.Equal("4a5e1e4baab89f3a32518a88c31bc87f618f76673e2cc77ab2127b7afdeda33b", ByteArrayToHexString(header.Merkle));
            Assert.Equal("0000000000000000000000000000000000000000000000000000000000000000", ByteArrayToHexString(header.PreviousBlockHash));
            Assert.Equal<UInt32>(1, header.Version);
            Assert.Equal<UInt32>(486604799, header.Bits);
            Assert.Equal<UInt32>(2083236893, header.Nonce);
            
            DateTime utcTime = DateTimeOffset.FromUnixTimeSeconds(header.Timestamp).DateTime;
            Assert.Equal("2009-01-03 18:15:05", utcTime.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        private static string ByteArrayToHexString(byte[] ba)
        {
            StringBuilder hexString = new StringBuilder(ba.Length * 2);
            for(int i=ba.Length-1; i>=0; i--)
            {
                hexString.AppendFormat("{0:x2}", ba[i]);
            }
            return hexString.ToString();
        }

        private static byte[] HexStringToByteArray(string hex)
        {
            int numberChars = hex.Length;
            byte[] bytes = new byte[numberChars / 2];
            for (int i = 0; i < numberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }

    }
}
