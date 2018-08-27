using System;
using System.Collections.Generic;

#if KEOKEN

using Bitprim.Native.Keoken;

namespace Bitprim.Keoken
{

    public class KeokenMemoryState : IKeokenState
    {
        private bool disposed;

        public KeokenMemoryState()
        {
            NativeInstance = KeokenStateNative.keoken_state_construct_default();
        }

        ~KeokenMemoryState()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public UInt32 InitialAssetId
        {
            set => KeokenStateNative.keoken_state_set_initial_asset_id(NativeInstance, value);
        }

        public bool StateAssetIdExists(UInt32 id)
        {
            return KeokenStateNative.keoken_state_asset_id_exists(NativeInstance, id) != 0;
        }

        public INativeList<IGetAllAssetsAddressesData> GetAllAssetAddresses()
        {
            return new GetAllAssetsAddressesDataList(KeokenStateNative.keoken_state_get_all_asset_addresses(NativeInstance));
        }

        public INativeList<IGetAssetsByAddressData> GetAssetsByAddress(PaymentAddress addr)
        {
            return new GetAssetsByAddressDataList(KeokenStateNative.keoken_state_get_assets_by_address(NativeInstance, addr.NativeInstance));
        }

        public INativeList<IGetAssetsData> GetAssets()
        {
            return new GetAssetsDataList(KeokenStateNative.keoken_state_get_assets(NativeInstance));
        }

        public Int64 GetBalance(UInt32 asset_id, PaymentAddress addr)
        {
            return KeokenStateNative.keoken_state_get_balance(NativeInstance, asset_id, addr.NativeInstance);
        }

        public void CreateAsset(string assetName, Int64 assetAmount, PaymentAddress owner, UInt64 blockHeight, byte[] txId)
        {
            var managedTxId = new Native.hash_t
            {
                hash = txId
            };
            KeokenStateNative.keoken_state_create_asset(NativeInstance, assetName, assetAmount, owner.NativeInstance, blockHeight, managedTxId);
        }

        public void CreateBalanceEntry(UInt32 assetId, Int64 assetAmount, PaymentAddress source, PaymentAddress target, UInt64 blockHeight, byte[] txId)
        {
            var managedTxId = new Native.hash_t
            {
                hash = txId
            };
            KeokenStateNative.keoken_state_create_balance_entry(NativeInstance, assetId, assetAmount, source.NativeInstance, target.NativeInstance, blockHeight, managedTxId);
        }

        internal IntPtr NativeInstance { get; private set; }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            //Release unmanaged resources
            KeokenStateNative.keoken_state_destruct(NativeInstance);

            if (disposing)
            {
                //Release managed resources and call Dispose for member variables
            }
            
            disposed = true;
        }
    }
}

#endif