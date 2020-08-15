// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

#if KEOKEN

using Knuth.Keoken;
using Xunit;

namespace Knuth.Tests
{
    [Collection("ChainCollection")]
    public class KeokenMemoryStateTest 
    {
        [Fact]
        public void TestSetInitialAssetId()
        {
            using (var memoryState = new KeokenMemoryState())
            {
                memoryState.InitialAssetId = 2;
            }
        }

        [Fact]
        public void TestExistAssetId()
        {
            using (var memoryState = new KeokenMemoryState())
            {
                Assert.Equal(false,memoryState.StateAssetIdExists(5));
            }
        }

        [Fact]
        public void TestCreateAsset()
        {
            using (var memoryState = new KeokenMemoryState())
            using (var address = new PaymentAddress("16TGufqQ9FPnEbixbD4ZjVabaP455roE6t"))
            {
                byte[] hash = Binary.HexStringToByteArray("000000000019d6689c085ae165831e934ff763ae46a2a6c172b3f1b60a8ce26f");
                memoryState.InitialAssetId = 5;
                Assert.Equal(false,memoryState.StateAssetIdExists(5));
                memoryState.CreateAsset("test asset", 1547, address, 10,hash);
                Assert.Equal(true,memoryState.StateAssetIdExists(5));
            }
        }

        [Fact]
        public void TestCreateBalanceEntry()
        {
            using (var memoryState = new KeokenMemoryState())
            using (var address_source = new PaymentAddress("16TGufqQ9FPnEbixbD4ZjVabaP455roE6t"))
            using (var address_target = new PaymentAddress("my2dxGb5jz43ktwGxg2doUaEb9WhZ9PQ7K"))
            {
                byte[] hash = Binary.HexStringToByteArray("000000000019d6689c085ae165831e934ff763ae46a2a6c172b3f1b60a8ce26f");
                memoryState.InitialAssetId = 5;
                
                memoryState.CreateAsset("test asset", 15, address_source, 10,hash);

                memoryState.CreateBalanceEntry(5, 15, address_source, address_target, 5,hash);

                Assert.Equal(0,memoryState.GetBalance(5,address_source)); 
                Assert.Equal(15,memoryState.GetBalance(5,address_target)); 
            }
        }

        [Fact]
        public void TestGetAssetsReturnsValidData()
        {
            using (var memoryState = new KeokenMemoryState())
            using (var address_source = new PaymentAddress("16TGufqQ9FPnEbixbD4ZjVabaP455roE6t"))
            {
                byte[] hash = Binary.HexStringToByteArray("000000000019d6689c085ae165831e934ff763ae46a2a6c172b3f1b60a8ce26f");
                memoryState.InitialAssetId = 5;
                
                memoryState.CreateAsset("test asset", 15, address_source, 10,hash);
                memoryState.CreateAsset("test asset2", 1500, address_source, 10,hash);


                using (var ret = memoryState.GetAssets())
                {
                    Assert.Equal<ulong>(2,ret.Count);
                    foreach (GetAssetsData data in ret)
                    {
                        if (data.AssetId == 5)
                        {
                            Assert.Equal(15,data.Amount);
                            Assert.Equal("test asset",data.AssetName);
                            Assert.Equal(address_source.Encoded,data.AssetCreator.Encoded);
                        }
                        else
                        {
                            Assert.Equal(1500,data.Amount);
                            Assert.Equal("test asset2",data.AssetName);
                            Assert.Equal(address_source.Encoded,data.AssetCreator.Encoded);
                        }
                    }
                }
            }
        }

        [Fact]
        public void TestGetAssetsByAddressReturnsValidData()
        {
            using (var memoryState = new KeokenMemoryState())
            using (var address_source = new PaymentAddress("16TGufqQ9FPnEbixbD4ZjVabaP455roE6t"))
            using (var address_target = new PaymentAddress("my2dxGb5jz43ktwGxg2doUaEb9WhZ9PQ7K"))
            {
                byte[] hash = Binary.HexStringToByteArray("000000000019d6689c085ae165831e934ff763ae46a2a6c172b3f1b60a8ce26f");
                memoryState.InitialAssetId = 5;
                
                memoryState.CreateAsset("test asset", 15, address_source, 10,hash);

                memoryState.CreateBalanceEntry(5, 15, address_source, address_target, 5,hash);

                using (var ret = memoryState.GetAssetsByAddress(address_target))
                {
                    Assert.Equal<ulong>(1,ret.Count);

                    var data = ret[0];

                    Assert.Equal<uint>(5,data.AssetId);
                    Assert.Equal(15,data.Amount);
                    Assert.Equal("test asset",data.AssetName);
                    Assert.Equal(address_source.Encoded,data.AssetCreator.Encoded);
                }
            }
        }

        [Fact]
        public void TestGetAllAssetAddressesReturnsValidData()
        {
            using (var memoryState = new KeokenMemoryState())
            using (var address_source = new PaymentAddress("16TGufqQ9FPnEbixbD4ZjVabaP455roE6t"))
            using (var address_target = new PaymentAddress("my2dxGb5jz43ktwGxg2doUaEb9WhZ9PQ7K"))
            {
                byte[] hash = Binary.HexStringToByteArray("000000000019d6689c085ae165831e934ff763ae46a2a6c172b3f1b60a8ce26f");
                memoryState.InitialAssetId = 5;
                
                memoryState.CreateAsset("test asset", 15, address_source, 10,hash);
                memoryState.CreateAsset("test asset2", 999, address_source, 10,hash);

                memoryState.CreateBalanceEntry(5, 15, address_source, address_target, 5,hash);

                using (var ret = memoryState.GetAllAssetAddresses())
                {
                    Assert.Equal<ulong>(3,ret.Count);
                    foreach (GetAllAssetsAddressesData data in ret)
                    {
                        if (data.AssetId == 5)
                        {
                            if (data.AmountOwner.Encoded == address_target.Encoded)
                            {
                                Assert.Equal(15,data.Amount);
                                Assert.Equal("test asset",data.AssetName);
                                Assert.Equal(address_source.Encoded,data.AssetCreator.Encoded);
                            }
                            else
                            {
                                Assert.Equal(0,data.Amount);
                                Assert.Equal("test asset",data.AssetName);
                                Assert.Equal(address_source.Encoded,data.AssetCreator.Encoded);
                                Assert.Equal(address_source.Encoded,data.AmountOwner.Encoded);
                            }
                            
                        }
                        else
                        {
                            Assert.Equal<ulong>(6,data.AssetId);
                            Assert.Equal(999,data.Amount);
                            Assert.Equal("test asset2",data.AssetName);
                            Assert.Equal(address_source.Encoded,data.AssetCreator.Encoded);
                            Assert.Equal(address_source.Encoded,data.AmountOwner.Encoded);
                        }
                    }
                }
            }
        }

        [Fact]
        public void TestGetBalanceReturnsValidData()
        {
            using (var memoryState = new KeokenMemoryState())
            using (var address_source = new PaymentAddress("16TGufqQ9FPnEbixbD4ZjVabaP455roE6t"))
            using (var address_target = new PaymentAddress("my2dxGb5jz43ktwGxg2doUaEb9WhZ9PQ7K"))
            {
                byte[] hash = Binary.HexStringToByteArray("000000000019d6689c085ae165831e934ff763ae46a2a6c172b3f1b60a8ce26f");
                memoryState.InitialAssetId = 5;
                
                memoryState.CreateAsset("test asset", 15, address_source, 10,hash);
                
                memoryState.CreateBalanceEntry(5, 10, address_source, address_target, 5,hash);

                Assert.Equal(5,memoryState.GetBalance(5, address_source));
                Assert.Equal(10,memoryState.GetBalance(5, address_target));
            }
        }
    }
}

#endif // KEOKEN