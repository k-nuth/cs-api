// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Runtime.InteropServices;

#if KEOKEN

namespace Knuth.Native.Keoken
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