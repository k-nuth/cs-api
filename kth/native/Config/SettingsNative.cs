// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Runtime.InteropServices;

namespace Knuth.Native.Config
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Settings {
        public Native.Config.NodeSettings node;
        public Native.Config.BlockchainSettings chain;
        public Native.Config.DatabaseSettings database;
        public Native.Config.NetworkSettings network;
    }

    public static class SettingsNative
    {
        // [DllImport(Constants.KTH_C_LIBRARY)]
        // public static extern NodeSettings kth_config_settings_default(NetworkType network);

        // [DllImport(Constants.KTH_C_LIBRARY, CharSet=Constants.KTH_OS_CHARSET)]
        [DllImport(Constants.KTH_C_LIBRARY, CharSet=CharSet.Ansi)]
        public static extern Settings kth_config_settings_get_from_file(string path, out bool ok, out string error_message);

    }
}

