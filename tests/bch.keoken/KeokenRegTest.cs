using System;
using System.Threading.Tasks;
using Knuth.Keoken;
using Xunit;

namespace Tests.Bch.Keoken
{
    [Collection("KeokenCollection")]
    public class KeokenRegTestRemove : IClassFixture<RegtestNodeFixture>
    {
        private readonly RegtestNodeFixture fixture_;
        private readonly KeokenManager keokenManager_;

        public KeokenRegTestRemove(RegtestNodeFixture fixture)
        {
            fixture_ = fixture;
            keokenManager_ = fixture_.Node.KeokenManager;
        }

        
        [Fact]
        public void TestRemove()
        {
            using (GetAllAssetsAddressesDataList assets = keokenManager_.GetAllAssetAddresses())
            {
                Assert.Equal<UInt64>(3, assets.Count);
            }

            fixture_.State.RemoveUpTo(0);

            using (GetAllAssetsAddressesDataList assets = keokenManager_.GetAllAssetAddresses())
            {
                Assert.Equal<UInt64>(0, assets.Count);
            }
        }

    }    

    [Collection("KeokenCollection")]
    public class KeokenRegTestReset : IClassFixture<RegtestNodeFixture>
    {
        private readonly RegtestNodeFixture fixture_;
        private readonly KeokenManager keokenManager_;

        public KeokenRegTestReset(RegtestNodeFixture fixture)
        {
            fixture_ = fixture;
            keokenManager_ = fixture_.Node.KeokenManager;
        }

        [Fact]
        public void TestReset()
        {
            using (GetAllAssetsAddressesDataList assets = keokenManager_.GetAllAssetAddresses())
            {
                Assert.Equal<UInt64>(3, assets.Count);
            }

            fixture_.State.Reset();

            using (GetAllAssetsAddressesDataList assets = keokenManager_.GetAllAssetAddresses())
            {
                Assert.Equal<UInt64>(0, assets.Count);
            }
        }
        
    }    
    
    [Collection("KeokenCollection")]
    public class KeokenRegTest: IClassFixture<RegtestNodeFixture>
    {
        private readonly RegtestNodeFixture fixture_;
        private readonly KeokenManager keokenManager_;

        public KeokenRegTest(RegtestNodeFixture fixture)
        {
            fixture_ = fixture;
            keokenManager_ = fixture_.Node.KeokenManager;
        }

        [Fact]
        public async Task TestFetchLastHeightAsync()
        {
            var height = await fixture_.Node.Chain.FetchLastHeightAsync();
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
                        Assert.Equal("Knuth", asset.AssetName);
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
                            Assert.Equal("Knuth", asset.AssetName);
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
                        Assert.Equal("Knuth", asset.AssetName);
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