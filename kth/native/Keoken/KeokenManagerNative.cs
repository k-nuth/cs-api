// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Runtime.InteropServices;

#if KEOKEN

namespace Knuth.Native.Keoken
{
    public static class KeokenManagerNativeDelegates
    {
        //typedef void (*keoken_state_delegated_set_initial_asset_id_t)
        //        (void* /*ctx*/, 
        //        keoken_asset_id_t /*asset_id_initial*/);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void KeokenStateDelegatedSetInitialAssetIdHandler(IntPtr state, UInt32 asset_id_initial);

        // typedef void (*keoken_state_delegated_create_asset_t)
        //         (void* /*ctx*/,
        //         char const* /*asset_name*/, 
        //         keoken_amount_t /*asset_amount*/, 
        //         payment_address_t /*owner*/, 
        //         uint64_t /*size_t*/ /*block_height*/, 
        //         hash_t /*txid*/);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl,CharSet=CharSet.Ansi)]
        public delegate void KeokenStateDelegatedCreateAssetHandler(IntPtr state, string asset_name, Int64 asset_amount, IntPtr owner, UInt64 block_height, hash_t txid);


        // typedef void (*keoken_state_delegated_create_balance_entry_t)
        //                 (void* /*ctx*/,
        //                 keoken_asset_id_t /*asset_id*/, 
        //                 keoken_amount_t /*asset_amount*/, 
        //                 payment_address_t /*source*/, 
        //                 payment_address_t /*target*/,  
        //                 uint64_t /*size_t*/ /*block_height*/, 
        //                 hash_t /*txid*/);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void KeokenStateDelegatedCreateBalanceEntryHandler(IntPtr state, UInt32 asset_id, Int64 asset_amount, IntPtr source, IntPtr target, UInt64 block_height, hash_t txid);

        // typedef bool_t (*keoken_state_delegated_asset_id_exists_t)
        //                 (void* /*ctx*/,
        //                 keoken_asset_id_t /*id*/);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int KeokenStateDelegatedAssetIdExistsHandler(IntPtr state, UInt32 asset_id);

        // typedef keoken_amount_t (*keoken_state_delegated_get_balance_t)
        //                 (void* /*ctx*/,
        //                 keoken_asset_id_t /*id*/, 
        //                 payment_address_t /*addr*/);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate Int64 KeokenStateDelegatedGetBalanceHandler(IntPtr state, UInt32 asset_id, IntPtr addr);

        // typedef get_assets_by_address_list_t (*keoken_state_delegated_get_assets_by_address_t)
        //                 (void* /*ctx*/,
        //                 payment_address_t /*addr*/);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr KeokenStateDelegatedGetAssetsByAddressHandler(IntPtr state, IntPtr addr);

        // typedef get_assets_list_t (*keoken_state_delegated_get_assets_t)
        //                 (void* /*ctx*/);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr KeokenStateDelegatedGetAssetsListHandler(IntPtr state);

        // typedef get_all_asset_addresses_list_t (*keoken_state_delegated_get_all_asset_addresses_t)
        //                 (void* /*ctx*/);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr KeokenStateDelegatedGetAllAssetAddressesListHandler(IntPtr state);   


        //typedef void (*keoken_state_delegated_remove_up_to_t)
        //        (void* /*ctx*/, 
        //        uint64_t /*size_t*/ /*height*/);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void KeokenStateDelegatedRemoveUpToHandler(IntPtr state, UInt64 height);   


        //typedef void (*keoken_state_delegated_reset_t)
        //        (void* /*ctx*/);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void KeokenStateDelegatedResetHandler(IntPtr state);   
    }

    internal static class KeokenManagerNative  
    { 
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void keoken_manager_initialize_from_blockchain(IntPtr keokenManager);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern int keoken_manager_initialized(IntPtr keokenManager);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr keoken_manager_get_assets_by_address(IntPtr keokenManager, IntPtr address);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr keoken_manager_get_assets(IntPtr keokenManager);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr keoken_manager_get_all_asset_addresses(IntPtr keokenManager);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void keoken_manager_configure_state(IntPtr keokenManager,IntPtr context
                                                                ,[MarshalAs(UnmanagedType.FunctionPtr)]KeokenManagerNativeDelegates.KeokenStateDelegatedSetInitialAssetIdHandler setInitialAssetIdHandler
                                                                ,[MarshalAs(UnmanagedType.FunctionPtr)]KeokenManagerNativeDelegates.KeokenStateDelegatedResetHandler resetHandler
                                                                ,[MarshalAs(UnmanagedType.FunctionPtr)]KeokenManagerNativeDelegates.KeokenStateDelegatedRemoveUpToHandler removeUpToHandler
                                                                ,[MarshalAs(UnmanagedType.FunctionPtr)]KeokenManagerNativeDelegates.KeokenStateDelegatedCreateAssetHandler createAssetHandler
                                                                ,[MarshalAs(UnmanagedType.FunctionPtr)]KeokenManagerNativeDelegates.KeokenStateDelegatedCreateBalanceEntryHandler createBalanceEntryHandler
                                                                ,[MarshalAs(UnmanagedType.FunctionPtr)]KeokenManagerNativeDelegates.KeokenStateDelegatedAssetIdExistsHandler assetIdExistsHandler
                                                                ,[MarshalAs(UnmanagedType.FunctionPtr)]KeokenManagerNativeDelegates.KeokenStateDelegatedGetBalanceHandler getBalanceHandler
                                                                ,[MarshalAs(UnmanagedType.FunctionPtr)]KeokenManagerNativeDelegates.KeokenStateDelegatedGetAssetsByAddressHandler getAssetsByAddressHandler
                                                                ,[MarshalAs(UnmanagedType.FunctionPtr)]KeokenManagerNativeDelegates.KeokenStateDelegatedGetAssetsListHandler getAssetsListHandler
                                                                ,[MarshalAs(UnmanagedType.FunctionPtr)]KeokenManagerNativeDelegates.KeokenStateDelegatedGetAllAssetAddressesListHandler getAllAssetAddressesListHandler        
                                                                );
    }
}

#endif