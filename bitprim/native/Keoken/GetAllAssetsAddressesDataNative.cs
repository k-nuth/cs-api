using System;
using System.Runtime.InteropServices;

#if KEOKEN

namespace Bitprim.Native.Keoken
{
    internal static class GetAllAssetsAddressesDataNative  
    {
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void keoken_get_all_asset_addresses_data_destruct(IntPtr getAllAssetAddressesData);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern UInt32 keoken_get_all_asset_addresses_data_asset_id(IntPtr getAllAssetAddressesData);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr keoken_get_all_asset_addresses_data_asset_name(IntPtr getAllAssetAddressesData);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr keoken_get_all_asset_addresses_data_asset_creator(IntPtr getAllAssetAddressesData);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern Int64 keoken_get_all_asset_addresses_data_amount(IntPtr getAllAssetAddressesData);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr keoken_get_all_asset_addresses_data_amount_owner(IntPtr getAllAssetAddressesData);
    }
}

#endif