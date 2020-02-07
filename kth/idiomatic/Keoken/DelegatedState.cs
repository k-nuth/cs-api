using Knuth;
using System;
using System.Collections.Generic;
using Knuth.Native;

#if KEOKEN
using Knuth.Native.Keoken;

namespace Knuth.Keoken
{
    public static class DelegatedState
    {
        public static KeokenManagerNativeDelegates.KeokenStateDelegatedSetInitialAssetIdHandler KeokenStateDelegatedSetInitialAssetIdHandler;
        public static KeokenManagerNativeDelegates.KeokenStateDelegatedRemoveUpToHandler KeokenStateDelegatedRemoveUpToHandler;
        public static KeokenManagerNativeDelegates.KeokenStateDelegatedResetHandler KeokenStateDelegatedResetHandler; 
        public static KeokenManagerNativeDelegates.KeokenStateDelegatedCreateAssetHandler KeokenStateDelegatedCreateAssetHandler;
        public static KeokenManagerNativeDelegates.KeokenStateDelegatedAssetIdExistsHandler KeokenStateDelegatedAssetIdExistsHandler;
        public static KeokenManagerNativeDelegates.KeokenStateDelegatedGetAllAssetAddressesListHandler KeokenStateDelegatedGetAllAssetAddressesListHandler;
        public static KeokenManagerNativeDelegates.KeokenStateDelegatedCreateBalanceEntryHandler KeokenStateDelegatedCreateBalanceEntryHandler;
        public static KeokenManagerNativeDelegates.KeokenStateDelegatedGetAssetsListHandler KeokenStateDelegatedGetAssetsListHandler;
        public static KeokenManagerNativeDelegates.KeokenStateDelegatedGetAssetsByAddressHandler KeokenStateDelegatedGetAssetsByAddressHandler;
        public static KeokenManagerNativeDelegates.KeokenStateDelegatedGetBalanceHandler KeokenStateDelegatedGetBalanceHandler;
        

        static DelegatedState()
        {
            KeokenStateDelegatedSetInitialAssetIdHandler = KeokenStateDelegatedSetInitialAssetIdHandlerInternal;

            KeokenStateDelegatedRemoveUpToHandler = KeokenStateDelegatedRemoveUpToHandlerInternal;
            KeokenStateDelegatedResetHandler = KeokenStateDelegatedResetHandlerInternal;
            
            KeokenStateDelegatedCreateAssetHandler = KeokenStateDelegatedCreateAssetHandlerInternal;
            KeokenStateDelegatedAssetIdExistsHandler = KeokenStateDelegatedAssetIdExistsHandlerInternal;
            KeokenStateDelegatedGetAllAssetAddressesListHandler = KeokenStateDelegatedGetAllAssetAddressesListHandlerInternal;
            KeokenStateDelegatedCreateBalanceEntryHandler = KeokenStateDelegatedCreateBalanceEntryHandlerInternal;
            KeokenStateDelegatedGetAssetsListHandler = KeokenStateDelegatedGetAssetsListHandlerInternal;
            KeokenStateDelegatedGetAssetsByAddressHandler = KeokenStateDelegatedGetAssetsByAddressHandlerInternal;
            KeokenStateDelegatedGetBalanceHandler = KeokenStateDelegatedGetBalanceHandlerInternal;
        }

        private static IKeokenState internalState_;

        public static void SetDelegatedState(IKeokenState internalState)
        {
            internalState_ = internalState;
        }

        private static void KeokenStateDelegatedSetInitialAssetIdHandlerInternal(IntPtr state, UInt32 asset_id_initial)
        {
            internalState_.InitialAssetId = asset_id_initial;
        }

        private static void KeokenStateDelegatedRemoveUpToHandlerInternal(IntPtr state, UInt64 height)
        {
            internalState_.RemoveUpTo(height);          
        }

        private static void KeokenStateDelegatedResetHandlerInternal(IntPtr state)
        {
            internalState_.Reset();
        }

        private static void KeokenStateDelegatedCreateAssetHandlerInternal(IntPtr state, string asset_name, Int64 asset_amount,
            IntPtr owner, UInt64 block_height, hash_t txid)
        {
            using (var owner_address = new PaymentAddress(owner))
            {
                internalState_.CreateAsset(asset_name,asset_amount,owner_address, block_height,txid.hash);
            }
        }


        private static void KeokenStateDelegatedCreateBalanceEntryHandlerInternal(IntPtr state, UInt32 asset_id, Int64 asset_amount,
            IntPtr source, IntPtr target, UInt64 block_height, hash_t txid)
        {
            using (var source_address = new PaymentAddress(source))
            using (var target_address = new PaymentAddress(target))
            {
                internalState_.CreateBalanceEntry(asset_id, asset_amount, source_address, target_address, block_height, txid.hash);
            }
        }


        private static int KeokenStateDelegatedAssetIdExistsHandlerInternal(IntPtr state, UInt32 asset_id)
        {
            return internalState_.StateAssetIdExists(asset_id) ? 1 : 0;
        }


        private static Int64 KeokenStateDelegatedGetBalanceHandlerInternal(IntPtr state, UInt32 asset_id, IntPtr addr)
        {
            using (var address = new PaymentAddress(addr))
            {
                return internalState_.GetBalance(asset_id,address);
            }
        }


        private static IntPtr KeokenStateDelegatedGetAssetsByAddressHandlerInternal(IntPtr state, IntPtr addr)
        {
            using (var address = new PaymentAddress(addr))
            {
                return internalState_.GetAssetsByAddress(address).NativeInstance;
            }
        }


        private static IntPtr KeokenStateDelegatedGetAssetsListHandlerInternal(IntPtr state)
        {
            return internalState_.GetAssets().NativeInstance;
        }


        private static IntPtr KeokenStateDelegatedGetAllAssetAddressesListHandlerInternal(IntPtr state)
        {
            return internalState_.GetAllAssetAddresses().NativeInstance;
        }
    }
}

#endif