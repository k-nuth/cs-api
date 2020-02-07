using System;
using System.Runtime.InteropServices;

#if KEOKEN

namespace Knuth.Native.Keoken
{
    internal static class GetAssetsByAddressDataNative  
    {
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void keoken_get_assets_by_address_data_destruct(IntPtr getAssetByAddressData);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern UInt32 keoken_get_assets_by_address_data_asset_id(IntPtr getAssetByAddressData);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr keoken_get_assets_by_address_data_asset_name(IntPtr getAssetByAddressData);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr keoken_get_assets_by_address_data_asset_creator(IntPtr getAssetByAddressData);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern Int64 keoken_get_assets_by_address_data_amount(IntPtr getAssetByAddressData);
    }
}

#endif