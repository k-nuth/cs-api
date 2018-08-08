using System;
using System.Runtime.InteropServices;

#if KEOKEN

namespace Bitprim.Native.Keoken
{
    internal static class GetAllAssetsAddressesDataListNative  
    {
        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern IntPtr keoken_get_all_asset_addresses_list_construct_default(IntPtr getAllAssetAddressesDataList);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern void keoken_get_all_asset_addresses_list_push_back(IntPtr getAllAssetAddressesDataList, IntPtr getAllAssetAddressesData);
       
        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern IntPtr keoken_get_all_asset_addresses_list_destruct(IntPtr getAllAssetAddressesDataList);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern UInt64 keoken_get_all_asset_addresses_list_count(IntPtr getAllAssetAddressesDataList);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern IntPtr keoken_get_all_asset_addresses_list_nth(IntPtr getAllAssetAddressesDataList, UInt64 index);
    }
}

#endif