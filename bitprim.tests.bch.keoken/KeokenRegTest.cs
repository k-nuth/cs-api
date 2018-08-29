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
            Assert.Equal<UInt64>(106, height.Result);
        }

        [Fact]
        public void TestInitializeFromBlockchain()
        {
            Assert.True(keokenManager_.Initialized);
        }

        [Fact]
        public void TestGetAssets()
        {
            using (var assets = keokenManager_.GetAssets())
            {
                Assert.Equal<UInt64>(2, assets.Count);

                foreach (GetAssetsData asset in assets)
                {
                    if (asset.AssetId == 1)
                    {
                        Assert.Equal(1000000, asset.Amount);
                        Assert.Equal("Bitprim", asset.AssetName);
                        Assert.Equal("16TGufqQ9FPnEbixbD4ZjVabaP455roE6t", asset.AssetCreator.Encoded);
                    }
                    else
                    {
                        Assert.Equal<UInt32>(2, asset.AssetId);
                        Assert.Equal(100, asset.Amount);
                        Assert.Equal("HanchonCoin", asset.AssetName);
                        Assert.Equal("16TGufqQ9FPnEbixbD4ZjVabaP455roE6t", asset.AssetCreator.Encoded);
                    }
                }
            }
        }

        [Fact]
        public void TestGetAssetsByAddress()
        {
            using (var address = new PaymentAddress("16TGufqQ9FPnEbixbD4ZjVabaP455roE6t"))
            {
                using (var assets = keokenManager_.GetAssetsByAddress(address))
                {
                    Assert.Equal<UInt64>(2, assets.Count);

                    foreach (GetAssetsByAddressData asset in assets)
                    {
                        if (asset.AssetId == 1)
                        {
                            Assert.Equal(1000000, asset.Amount);
                            Assert.Equal("Bitprim", asset.AssetName);
                            Assert.Equal("16TGufqQ9FPnEbixbD4ZjVabaP455roE6t", asset.AssetCreator.Encoded);
                        }
                        else
                        {
                            Assert.Equal<UInt32>(2, asset.AssetId);
                            Assert.Equal(90, asset.Amount);
                            Assert.Equal("HanchonCoin", asset.AssetName);
                            Assert.Equal("16TGufqQ9FPnEbixbD4ZjVabaP455roE6t", asset.AssetCreator.Encoded);
                        }
                    }
                }
            }

        }

        [Fact]
        public void TestGetAllAssetAddresses()
        {
            using (GetAllAssetsAddressesDataList assets = keokenManager_.GetAllAssetAddresses())
            {
                Assert.Equal<UInt64>(3, assets.Count);

                foreach (GetAllAssetsAddressesData asset in assets)
                {
                    if (asset.AssetId == 1)
                    {
                        Assert.Equal(1000000, asset.Amount);
                        Assert.Equal("Bitprim", asset.AssetName);
                        Assert.Equal("16TGufqQ9FPnEbixbD4ZjVabaP455roE6t", asset.AssetCreator.Encoded);
                        Assert.Equal("16TGufqQ9FPnEbixbD4ZjVabaP455roE6t", asset.AmountOwner.Encoded);
                    }
                    else
                    {
                        if (asset.AmountOwner.Encoded == "16TGufqQ9FPnEbixbD4ZjVabaP455roE6t")
                        {
                            Assert.Equal<UInt32>(2, asset.AssetId);
                            Assert.Equal(90, asset.Amount);
                            Assert.Equal("HanchonCoin", asset.AssetName);
                            Assert.Equal("16TGufqQ9FPnEbixbD4ZjVabaP455roE6t", asset.AssetCreator.Encoded);
                        }
                        else
                        {
                            Assert.Equal<UInt32>(2, asset.AssetId);
                            Assert.Equal(10, asset.Amount);
                            Assert.Equal("HanchonCoin", asset.AssetName);
                            Assert.Equal("16TGufqQ9FPnEbixbD4ZjVabaP455roE6t", asset.AssetCreator.Encoded);
                            Assert.Equal("18csrhcoVBvjMpuhLpna726CeYCXe1ZxiH", asset.AmountOwner.Encoded);
                        }
                    }
                }
            }
        }
    }
}