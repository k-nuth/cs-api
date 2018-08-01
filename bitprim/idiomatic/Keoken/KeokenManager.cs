using System;
using System.Runtime.InteropServices;

#if KEOKEN

using Bitprim.Native.Keoken;

namespace Bitprim.Keoken
{
    /// <summary>
    /// TODO: Add docs
    /// </summary>
    public class KeokenManager
    {
        private readonly IntPtr nativeInstance_;

        public KeokenManager(IntPtr nativeInstance)
        {
            nativeInstance_ = nativeInstance;
        }

        public void InitializeFromBlockchain()
        {
            KeokenManagerNative.keoken_manager_initialize_from_blockchain(nativeInstance_);
        }

        public bool Initialized => KeokenManagerNative.keoken_manager_initialized(nativeInstance_) != 0;

        public GetAssetsByAddressDataList GetAssetsByAddress (PaymentAddress address)
        {
           return new GetAssetsByAddressDataList(KeokenManagerNative.keoken_manager_get_assets_by_address(nativeInstance_, address.NativeInstance));
        }

        public GetAssetsDataList GetAssets () 
        {
            return new GetAssetsDataList(KeokenManagerNative.keoken_manager_get_assets(nativeInstance_));
        }

        public GetAllAssetsAddressesDataList GetAllAssetAddresses () 
        {
            return new GetAllAssetsAddressesDataList(KeokenManagerNative.keoken_manager_get_all_asset_addresses(nativeInstance_));
        }
    }
}

#endif  