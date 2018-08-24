using System;
using System.Runtime.InteropServices;

#if KEOKEN

using Bitprim.Native.Keoken;

namespace Bitprim.Keoken
{
    /// <summary>
    /// TODO: Add docs
    /// </summary>
    public class KeokenManager:IDisposable
    {
        private readonly IntPtr nativeInstance_;
        //private DelegatedState delegatedState_;

        public KeokenManager(IntPtr nativeInstance)
        {
            nativeInstance_ = nativeInstance;
        }

        public void InitializeFromBlockchain()
        {
            KeokenManagerNative.keoken_manager_initialize_from_blockchain(nativeInstance_);
        }

        public void ConfigureState()
        {
            //delegatedState_ = delegatedState;
            KeokenManagerNative.keoken_manager_configure_state(nativeInstance_,IntPtr.Zero,DelegatedState.KeokenStateDelegatedSetInitialAssetIdHandler
                                                                ,DelegatedState.KeokenStateDelegatedCreateAssetHandler
                                                                ,DelegatedState.KeokenStateDelegatedCreateBalanceEntryHandler
                                                                ,DelegatedState.KeokenStateDelegatedAssetIdExistsHandler
                                                                ,DelegatedState.KeokenStateDelegatedGetBalanceHandler
                                                                ,DelegatedState.KeokenStateDelegatedGetAssetsByAddressHandler
                                                                ,DelegatedState.KeokenStateDelegatedGetAssetsListHandler
                                                                ,DelegatedState.KeokenStateDelegatedGetAllAssetAddressesListHandler);
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

        private void ReleaseUnmanagedResources()
        {
            //delegatedState_?.Dispose();
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

        ~KeokenManager()
        {
            Dispose(false);
        }
    }
}

#endif  