using System;
using System.Runtime.InteropServices;

#if KEOKEN

namespace Bitprim.Native.Keoken
{
    internal static class KeokenManagerNative  
    {
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
    }
}

#endif