// Copyright (c) 2016-2022 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Runtime.InteropServices;

namespace Knuth.Native.Config
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct DatabaseSettings {
        public string directory;
        public bool flush_writes;
        public UInt16 file_growth_rate;
        public UInt32 index_start_height;
        public UInt32 reorg_pool_limit;
        public UInt64 db_max_size;
        public bool safe_mode;
        public UInt32 cache_capacity;
    }

    public static class DatabaseSettingsNative
    {
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern DatabaseSettings kth_config_database_settings_default(NetworkType network);
    }
}