using System;
using System.Runtime.InteropServices;

#if KEOKEN

namespace Bitprim.Native.Keoken
{
    internal static class KeokenManagerNative  
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
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
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

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern void keoken_manager_initialize_from_blockchain(IntPtr keokenManager);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern int keoken_manager_initialized(IntPtr keokenManager);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern IntPtr keoken_manager_get_assets_by_address(IntPtr keokenManager, IntPtr address);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern IntPtr keoken_manager_get_assets(IntPtr keokenManager);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern IntPtr keoken_manager_get_all_asset_addresses(IntPtr keokenManager);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern void keoken_manager_configure_state(IntPtr keokenManager
                                                                ,[MarshalAs(UnmanagedType.FunctionPtr)]KeokenStateDelegatedSetInitialAssetIdHandler setInitialAssetIdHandler
                                                                ,[MarshalAs(UnmanagedType.FunctionPtr)]KeokenStateDelegatedCreateAssetHandler createAssetHandler
                                                                ,[MarshalAs(UnmanagedType.FunctionPtr)]KeokenStateDelegatedCreateBalanceEntryHandler createBalanceEntryHandler
                                                                ,[MarshalAs(UnmanagedType.FunctionPtr)]KeokenStateDelegatedAssetIdExistsHandler assetIdExistsHandler
                                                                ,[MarshalAs(UnmanagedType.FunctionPtr)]KeokenStateDelegatedGetBalanceHandler getBalanceHandler
                                                                ,[MarshalAs(UnmanagedType.FunctionPtr)]KeokenStateDelegatedGetAssetsByAddressHandler getAssetsByAddressHandler
                                                                ,[MarshalAs(UnmanagedType.FunctionPtr)]KeokenStateDelegatedGetAssetsListHandler getAssetsListHandler
                                                                ,[MarshalAs(UnmanagedType.FunctionPtr)]KeokenStateDelegatedGetAllAssetAddressesListHandler getAllAssetAddressesListHandler        
                                                                );
    }
}

#endif