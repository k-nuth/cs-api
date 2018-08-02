using System;
using System.Threading.Tasks;
using Bitprim.Keoken;
using Xunit;

namespace Bitprim.Tests.Bch.Keoken
{
    public class KeokenRegTest: IClassFixture<RegtestExecutorFixture>
    {
        private readonly RegtestExecutorFixture fixture_;
        private readonly KeokenManager keokenManager_;

        public KeokenRegTest(RegtestExecutorFixture fixture)
        {
            fixture_ = fixture;
            keokenManager_ = fixture_.Executor.KeokenManager;
        }

        [Fact]
        public async Task TestFetchLastHeightAsync()
        {
            var height = await fixture_.Executor.Chain.FetchLastHeightAsync();
            Assert.Equal<UInt64>(106,height.Result);
        }

        [Fact]
        public void TestInitializeFromBlockchain()
        {
            Assert.True(keokenManager_.Initialized);
        }

        [Fact]
        public void TestGetAssets()
        {
            var assets = keokenManager_.GetAssets();
            Assert.Equal<UInt64>(2,assets.Count);
        }

    }
}