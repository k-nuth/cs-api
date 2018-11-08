using System;
using System.Runtime.InteropServices;

#if KEOKEN
using Bitprim.Keoken;

namespace Bitprim.Native.Keoken
{
    internal static class KeokenStateNative  
    {
        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern IntPtr keoken_memory_state_construct_default();

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern void keoken_memory_state_destruct(IntPtr state);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern void keoken_memory_state_set_initial_asset_id(IntPtr state, UInt32 asset_id_initial);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern void keoken_memory_state_reset(IntPtr state);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern void keoken_memory_state_remove_up_to(IntPtr state, UInt64 height);


        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern void keoken_memory_state_create_asset(IntPtr state, [MarshalAs(UnmanagedType.LPStr)]string asset_name, Int64 asset_amount, IntPtr owner, UInt64 block_height, hash_t txid);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern void keoken_memory_state_create_balance_entry(IntPtr state, UInt32 asset_id, Int64 asset_amount, IntPtr source, IntPtr target, UInt64 block_height, hash_t txid);

        // Queries.
        // ---------------------------------------------------------------------------------
        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern int /*bool*/ keoken_memory_state_asset_id_exists(IntPtr state, UInt32 id);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern Int64 keoken_memory_state_get_balance(IntPtr state, UInt32 asset_id, IntPtr addr);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern IntPtr keoken_memory_state_get_assets_by_address(IntPtr state, IntPtr addr);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern IntPtr keoken_memory_state_get_assets(IntPtr state);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern IntPtr keoken_memory_state_get_all_asset_addresses(IntPtr state);
    }
}

#endif