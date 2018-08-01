using System;
using Bitprim.Keoken;
using Xunit;

namespace Bitprim.Tests
{
    //[Collection("KeokenCollection")]
    public class KeokenTest : IClassFixture<ExecutorFixture>
    {
        private readonly ExecutorFixture executorFixture_;
        private readonly KeokenManager keokenManager_;

        public KeokenTest(ExecutorFixture fixture)
        {
            executorFixture_ = fixture;
            keokenManager_ = executorFixture_.Executor.KeokenManager;
        }

        /*[Fact]
        public void TestInitializeFromBlockchain()
        {
            keokenManager_.InitializeFromBlockchain();
            Assert.Equal(true, keokenManager_.Initialized);
        }*/

        /*[Fact]
        public void TestGetAllAssetAddresses()
        {
            keokenManager_.InitializeFromBlockchain();
            using (var ret = keokenManager_.GetAllAssetAddresses())
            {
                Assert.Equal<UInt64>(0,ret.Count);
            }
        }

        [Fact]
        public void TestGetAssets()
        {
            keokenManager_.InitializeFromBlockchain();
            using (var ret = keokenManager_.GetAssets())
            {
                Assert.Equal<UInt64>(0,ret.Count);
            }
        }

        [Fact]
        public void TestGetAssetsByAddress()
        {
            keokenManager_.InitializeFromBlockchain();
            using (var address = new PaymentAddress("bchtest:qp7d6x2weeca9fn6eakwvgd9ryq8g6h0tuyks75rt7"))
            {
                using (var ret = keokenManager_.GetAssetsByAddress(address))
                {
                    Assert.Equal<UInt64>(0,ret.Count);
                }
            }
        }*/
    }
}
