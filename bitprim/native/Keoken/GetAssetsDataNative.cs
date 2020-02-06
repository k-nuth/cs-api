using System;
using System.Runtime.InteropServices;

#if KEOKEN

namespace Bitprim.Native.Keoken
{
    internal static class GetAssetsDataNative
    {
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void keoken_get_assets_data_destruct(IntPtr getAssetData);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern UInt32 keoken_get_assets_data_asset_id(IntPtr getAssetData);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr keoken_get_assets_data_asset_name(IntPtr getAssetData);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr keoken_get_assets_data_asset_creator(IntPtr getAssetData);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern Int64 keoken_get_assets_data_amount(IntPtr getAssetData);
    }
}

#endif