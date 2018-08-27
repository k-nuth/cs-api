using System;
using Bitprim.Keoken;
using Xunit;

namespace Bitprim.Tests
{
    [Collection("ChainCollection")]
    public class KeokenManagerTest : IClassFixture<ExecutorFixture>
    {
        private readonly ExecutorFixture executorFixture_;
        
        public KeokenManagerTest(ExecutorFixture fixture)
        {
            executorFixture_ = fixture;   
        }

        [Fact]
        public void TestInitializeFromBlockchain()
        {
            using (var state = new KeokenMemoryState())
            {
                DelegatedState.SetDelegatedState(state);
                executorFixture_.Executor.KeokenManager.ConfigureState();
                executorFixture_.Executor.KeokenManager.InitializeFromBlockchain();
                Assert.Equal(true, executorFixture_.Executor.KeokenManager.Initialized);
            }
            
            /*using (var delegatedState = new DelegatedState(state))
            {
                keokenManager_.ConfigureState(delegatedState);
                keokenManager_.InitializeFromBlockchain();
                Assert.Equal(true, keokenManager_.Initialized);
            } */
        }

        [Fact]
        public void TestGetAllAssetAddresses()
        {
            
            using (var state = new KeokenMemoryState())
            {
                DelegatedState.SetDelegatedState(state);
                executorFixture_.Executor.KeokenManager.ConfigureState();
                executorFixture_.Executor.KeokenManager.InitializeFromBlockchain();
                using (var ret = executorFixture_.Executor.KeokenManager.GetAllAssetAddresses())
                {
                    Assert.Equal<UInt64>(0,ret.Count);
                }
            }
        }

        [Fact]
        public void TestGetAssets()
        {
            using (var state = new KeokenMemoryState())
            {
                DelegatedState.SetDelegatedState(state);
                executorFixture_.Executor.KeokenManager.ConfigureState();
                executorFixture_.Executor.KeokenManager.InitializeFromBlockchain();
                using (var ret = executorFixture_.Executor.KeokenManager.GetAssets())
                {
                    Assert.Equal<UInt64>(0, ret.Count);
                }
            }
        }

       

        [Fact]
        public void TestGetAssetsByAddress()
        {
            using (var state = new KeokenMemoryState())
            {
                DelegatedState.SetDelegatedState(state);
                executorFixture_.Executor.KeokenManager.ConfigureState();
                executorFixture_.Executor.KeokenManager.InitializeFromBlockchain();
                using (var address = new PaymentAddress("16TGufqQ9FPnEbixbD4ZjVabaP455roE6t"))
                {
                    //TODO Mario
                    using (var ret = executorFixture_.Executor.KeokenManager.GetAssetsByAddress(address))
                    {
                        Assert.Equal<UInt64>(0, ret.Count);
                    }
                }
            }

        }
    }

     /*class GetAllAssetsAddressesDataManaged : IGetAllAssetsAddressesData
        {
            public GetAllAssetsAddressesDataManaged(uint assetId, string assetName, PaymentAddress assetCreator, long amount, PaymentAddress amountOwner)
            {
                AssetId = assetId;
                AssetName = assetName;
                AssetCreator = assetCreator;
                Amount = amount;
                AmountOwner = amountOwner;
            }

            public void Dispose()
            {
                AssetCreator.Dispose();
                AmountOwner.Dispose();
            }

            public uint AssetId { get; }
            public string AssetName { get; }
            public PaymentAddress AssetCreator { get; }
            public long Amount { get; }
            public PaymentAddress AmountOwner { get; }
        }

        class GetAssetsByAddressDataManaged : IGetAssetsByAddressData
        {
            public GetAssetsByAddressDataManaged(uint assetId, string assetName, PaymentAddress assetCreator, long amount)
            {
                AssetId = assetId;
                AssetName = assetName;
                AssetCreator = assetCreator;
                Amount = amount;
            }

            public void Dispose()
            {
                AssetCreator.Dispose();
            }

            public uint AssetId { get; }
            public string AssetName { get; }
            public PaymentAddress AssetCreator { get; }
            public long Amount { get; }
        }

        class GetAssetsDataManaged : IGetAssetsData
        {
            public GetAssetsDataManaged(uint assetId, string assetName, PaymentAddress assetCreator, long amount)
            {
                AssetId = assetId;
                AssetName = assetName;
                AssetCreator = assetCreator;
                Amount = amount;
            }

            public void Dispose()
            {
                AssetCreator.Dispose();
            }

            public uint AssetId { get; }
            public string AssetName { get; }
            public PaymentAddress AssetCreator { get; }
            public long Amount { get; }
        }

        class DummyState : IKeokenState
        {
            class Entry
            {
                public UInt64 BlockHeight { get; set; }
                private byte[] TxId { get; set; }
            }

            class Asset:Entry
            {
                public UInt32 AssetId { get; set; }
                public string AssetName { get; set; }
                public PaymentAddress Owner { get; set; }
            }

            class BalanceItem:Entry
            {
                public UInt32 AssetId { get; set; }
                public Int64 AssetAmount { get; set; }
                public PaymentAddress Source { get; set; } 
                public PaymentAddress Target { get; set; }
            }

            private Dictionary<UInt64,Asset> assets = new Dictionary<ulong, Asset>();
            private Dictionary<Tuple<string,UInt32>,BalanceItem> items = new Dictionary<Tuple<string, uint>, BalanceItem>();

            public void Dispose()
            {
               
            }

            public UInt32 InitialAssetId { private get; set; }
            
            public bool StateAssetIdExists(UInt32 id)
            {
                return id < InitialAssetId;
            }

            public INativeList<IGetAllAssetsAddressesData> GetAllAssetAddresses()
            {
                return new ManagedReadableList<GetAllAssetsAddressesData>();
            }

            public INativeList<IGetAssetsByAddressData> GetAssetsByAddress(PaymentAddress addr)
            {
                return new ManagedReadableList<GetAssetsByAddressDataManaged>();
            }

            public INativeList<IGetAssetsData> GetAssets()
            {
                return new ManagedReadableList<GetAssetsDataManaged>();
            }

            public Int64 GetBalance(UInt32 assetId, PaymentAddress addr)
            {
                return 0;
            }

            public void CreateAsset(string assetName, Int64 assetAmount, PaymentAddress owner, UInt64 blockHeight, byte[] txId)
            {
                
            }

            public void CreateBalanceEntry(UInt32 assetId, Int64 assetAmount, PaymentAddress source, PaymentAddress target, UInt64 blockHeight,
                byte[] txId)
            {
                
            }
        }
        */
}
