using Bitprim;
using System;
using System.Collections.Generic;
using Bitprim.Native;

#if KEOKEN

namespace Bitprim.Keoken
{
    public static class DelegatedState
    {
        private static IKeokenState internalState_;

        public static void SetDelegatedState(IKeokenState internalState)
        {
            internalState_ = internalState;
        }


        public static void KeokenStateDelegatedSetInitialAssetIdHandler(IntPtr state, UInt32 asset_id_initial)
        {
            internalState_.InitialAssetId = asset_id_initial;
        }

        public static void KeokenStateDelegatedCreateAssetHandler(IntPtr state, string asset_name, Int64 asset_amount,
            IntPtr owner, UInt64 block_height, hash_t txid)
        {
            using (var owner_address = new PaymentAddress(owner))
            //using (var asset_name_str = new NativeString(asset_name) )
            {
                internalState_.CreateAsset(asset_name,asset_amount,owner_address, block_height,txid.hash);
            }
        }


        public static void KeokenStateDelegatedCreateBalanceEntryHandler(IntPtr state, UInt32 asset_id, Int64 asset_amount,
            IntPtr source, IntPtr target, UInt64 block_height, hash_t txid)
        {
            using (var source_address = new PaymentAddress(source))
            using (var target_address = new PaymentAddress(target))
            {
                internalState_.CreateBalanceEntry(asset_id, asset_amount, source_address, target_address, block_height, txid.hash);
            }
        }


        public static int KeokenStateDelegatedAssetIdExistsHandler(IntPtr state, UInt32 asset_id)
        {
            return internalState_.StateAssetIdExists(asset_id) ? 1 : 0;
        }


        public static Int64 KeokenStateDelegatedGetBalanceHandler(IntPtr state, UInt32 asset_id, IntPtr addr)
        {
            using (var address = new PaymentAddress(addr))
            {
                return internalState_.GetBalance(asset_id,address);
            }
        }


        public static IntPtr KeokenStateDelegatedGetAssetsByAddressHandler(IntPtr state, IntPtr addr)
        {
            using (var address = new PaymentAddress(addr))
            {
                return internalState_.GetAssetsByAddress(address).NativeInstance;
            }
        }


        public static IntPtr KeokenStateDelegatedGetAssetsListHandler(IntPtr state)
        {
            return internalState_.GetAssets().NativeInstance;
        }


        public static IntPtr KeokenStateDelegatedGetAllAssetAddressesListHandler(IntPtr state)
        {
            return internalState_.GetAllAssetAddresses().NativeInstance;
        }

        //private static void ReleaseUnmanagedResources()
        //{
        //    internalState_.Dispose();
        //}

        //private static void Dispose(bool disposing)
        //{
        //    ReleaseUnmanagedResources();
        //    if (disposing)
        //    {
                
        //    }
        //}

        //public static void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        //~DelegatedState()
        //{
        //    Dispose(false);
        //}
    }
}

#endif