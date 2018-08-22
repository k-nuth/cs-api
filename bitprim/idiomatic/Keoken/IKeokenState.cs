using Bitprim;
using System;
using Bitprim.Native;

#if KEOKEN

namespace Bitprim.Keoken
{
    // AssetAmount : Int64;
    // AssetId : UInt32;

    public interface IKeokenState : IDisposable
    {
        UInt32 InitialAssetId { set; }

        bool StateAssetIdExists(UInt32 id);

        GetAllAssetsAddressesDataList GetAllAssetAddresses();

        GetAssetsByAddressDataList GetAssetsByAddress(PaymentAddress addr);

        GetAssetsDataList GetAssets();

        Int64 GetBalance(UInt32 id, PaymentAddress addr);

        void CreateAsset(string assetName, Int64 assetAmount, PaymentAddress owner, UInt64 blockHeight, byte[] txId);

        void CreateBalanceEntry(UInt32 assetId, Int64 assetAmount, PaymentAddress source, PaymentAddress target, UInt64 blockHeight, byte[] txId);
    }



    public class DelegatedState:IDisposable
    {
        private readonly IKeokenState internalState_;

        public DelegatedState(IKeokenState internalState)
        {
            internalState_ = internalState;
        }


        public void KeokenStateDelegatedSetInitialAssetIdHandler(IntPtr state, UInt32 asset_id_initial)
        {
            internalState_.InitialAssetId = asset_id_initial;
        }

        public void KeokenStateDelegatedCreateAssetHandler(IntPtr state, string asset_name, Int64 asset_amount,
            IntPtr owner, UInt64 block_height, hash_t txid)
        {
            using (var owner_address = new PaymentAddress(owner))
            //using (var asset_name_str = new NativeString(asset_name) )
            {
                internalState_.CreateAsset(asset_name,asset_amount,owner_address, block_height,txid.hash);
            }
        }


        public void KeokenStateDelegatedCreateBalanceEntryHandler(IntPtr state, UInt32 asset_id, Int64 asset_amount,
            IntPtr source, IntPtr target, UInt64 block_height, hash_t txid)
        {
            using (var source_address = new PaymentAddress(source))
            using (var target_address = new PaymentAddress(target))
            {
                internalState_.CreateBalanceEntry(asset_id, asset_amount, source_address, target_address, block_height, txid.hash);
            }
        }


        public int KeokenStateDelegatedAssetIdExistsHandler(IntPtr state, UInt32 asset_id)
        {
            return internalState_.StateAssetIdExists(asset_id) ? 1 : 0;
        }


        public Int64 KeokenStateDelegatedGetBalanceHandler(IntPtr state, UInt32 asset_id, IntPtr addr)
        {
            using (var address = new PaymentAddress(addr))
            {
                return internalState_.GetBalance(asset_id,address);
            }
        }


        public IntPtr KeokenStateDelegatedGetAssetsByAddressHandler(IntPtr state, IntPtr addr)
        {
            using (var address = new PaymentAddress(addr))
            {
                return internalState_.GetAssetsByAddress(address).NativeInstance;
            }
        }


        public IntPtr KeokenStateDelegatedGetAssetsListHandler(IntPtr state)
        {
            return internalState_.GetAssets().NativeInstance;
        }


        public IntPtr KeokenStateDelegatedGetAllAssetAddressesListHandler(IntPtr state)
        {
            return internalState_.GetAllAssetAddresses().NativeInstance;
        }

        private void ReleaseUnmanagedResources()
        {
            internalState_.Dispose();
        }

        private void Dispose(bool disposing)
        {
            ReleaseUnmanagedResources();
            if (disposing)
            {
                
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~DelegatedState()
        {
            Dispose(false);
        }
    }
}

#endif