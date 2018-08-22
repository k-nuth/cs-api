using System;
using Bitprim.Keoken;
using Xunit;

namespace Bitprim.Tests
{
    [Collection("ChainCollection")]
    public class KeokenTest : IClassFixture<ExecutorFixture>
    {
        private readonly ExecutorFixture executorFixture_;
        private readonly KeokenManager keokenManager_;

        public KeokenTest(ExecutorFixture fixture)
        {
            executorFixture_ = fixture;
            keokenManager_ = executorFixture_.Executor.KeokenManager;
        }

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
            using (var address = new PaymentAddress("bchtest:qp7d6x2weeca9fn6eakwvgd9ryq8g6h0tuyks75rt7"))
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
            using (var address_source = new PaymentAddress("bchtest:qp7d6x2weeca9fn6eakwvgd9ryq8g6h0tuyks75rt7"))
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
            using (var address_source = new PaymentAddress("bchtest:qp7d6x2weeca9fn6eakwvgd9ryq8g6h0tuyks75rt7"))
            {
                byte[] hash = Binary.HexStringToByteArray("000000000019d6689c085ae165831e934ff763ae46a2a6c172b3f1b60a8ce26f");
                memoryState.InitialAssetId = 5;
                
                memoryState.CreateAsset("test asset", 15, address_source, 10,hash);
                memoryState.CreateAsset("test asset2", 1500, address_source, 10,hash);


                using (GetAssetsDataList ret = memoryState.GetAssets())
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
            using (var address_source = new PaymentAddress("bchtest:qp7d6x2weeca9fn6eakwvgd9ryq8g6h0tuyks75rt7"))
            using (var address_target = new PaymentAddress("my2dxGb5jz43ktwGxg2doUaEb9WhZ9PQ7K"))
            {
                byte[] hash = Binary.HexStringToByteArray("000000000019d6689c085ae165831e934ff763ae46a2a6c172b3f1b60a8ce26f");
                memoryState.InitialAssetId = 5;
                
                memoryState.CreateAsset("test asset", 15, address_source, 10,hash);

                memoryState.CreateBalanceEntry(5, 15, address_source, address_target, 5,hash);

                using (GetAssetsByAddressDataList ret = memoryState.GetAssetsByAddress(address_target))
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
            using (var address_source = new PaymentAddress("bchtest:qp7d6x2weeca9fn6eakwvgd9ryq8g6h0tuyks75rt7"))
            using (var address_target = new PaymentAddress("my2dxGb5jz43ktwGxg2doUaEb9WhZ9PQ7K"))
            {
                byte[] hash = Binary.HexStringToByteArray("000000000019d6689c085ae165831e934ff763ae46a2a6c172b3f1b60a8ce26f");
                memoryState.InitialAssetId = 5;
                
                memoryState.CreateAsset("test asset", 15, address_source, 10,hash);
                memoryState.CreateAsset("test asset2", 999, address_source, 10,hash);

                memoryState.CreateBalanceEntry(5, 15, address_source, address_target, 5,hash);

                using (GetAllAssetsAddressesDataList ret = memoryState.GetAllAssetAddresses())
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

        class DummyState : IKeokenState
        {
            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public uint InitialAssetId { get; set; }
            public bool StateAssetIdExists(uint id)
            {
                throw new NotImplementedException();
            }

            public GetAllAssetsAddressesDataList GetAllAssetAddresses()
            {
                throw new NotImplementedException();
            }

            public GetAssetsByAddressDataList GetAssetsByAddress(PaymentAddress addr)
            {
                throw new NotImplementedException();
            }

            public GetAssetsDataList GetAssets()
            {
                throw new NotImplementedException();
            }

            public long GetBalance(uint id, PaymentAddress addr)
            {
                throw new NotImplementedException();
            }

            public void CreateAsset(string assetName, long assetAmount, PaymentAddress owner, ulong blockHeight, byte[] txId)
            {
                throw new NotImplementedException();
            }

            public void CreateBalanceEntry(uint assetId, long assetAmount, PaymentAddress source, PaymentAddress target, ulong blockHeight,
                byte[] txId)
            {
                throw new NotImplementedException();
            }
        }

        [Fact]
        public void TestInitializeFromBlockchain()
        {
            Assert.Equal(false, keokenManager_.Initialized);

            using (var state = new DummyState())
            {
                using (var delegatedState = new DelegatedState(state))
                {
                    keokenManager_.ConfigureState(delegatedState);
                    keokenManager_.InitializeFromBlockchain();
                    Assert.Equal(true, keokenManager_.Initialized);
                }    
            }
        }

        [Fact]
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
        }
    }
}
