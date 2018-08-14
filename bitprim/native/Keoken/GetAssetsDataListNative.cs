using System;
using System.Runtime.InteropServices;

#if KEOKEN

namespace Bitprim.Native.Keoken
{
    internal static class GetAssetsDataListNative  
    {
        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern IntPtr keoken_get_assets_list_construct_default(IntPtr getAssetDataList);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern void keoken_get_assets_list_push_back(IntPtr getAssetDataList, IntPtr getAssetData);
       
        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern IntPtr keoken_get_assets_list_destruct(IntPtr getAssetDataList);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern UInt64 keoken_get_assets_list_count(IntPtr getAssetDataList);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern IntPtr keoken_get_assets_list_nth(IntPtr getAssetDataList, UInt64 index);
    }
}

#endif