// Copyright (c) 2016-2024 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Knuth.Config
{
    public class DatabaseSettings {
        public static DatabaseSettings GetDefault(NetworkType network) {
            var native = Knuth.Native.Config.DatabaseSettingsNative.kth_config_database_settings_default(network);
            return FromNative(native);
        }

        public string Directory { get; set; }
        public DbMode DbMode { get; set; }
        public UInt32 ReorgPoolLimit { get; set; }
        public UInt64 DbMaxSize { get; set; }
        public bool SafeMode { get; set; }
        public UInt32 CacheCapacity { get; set; }

        public Knuth.Native.Config.DatabaseSettings ToNative() {
            var native = new Knuth.Native.Config.DatabaseSettings();
            native.directory = this.Directory;
            native.db_mode = (int)this.DbMode;
            native.reorg_pool_limit = this.ReorgPoolLimit;
            native.db_max_size = this.DbMaxSize;
            native.safe_mode = this.SafeMode;
            native.cache_capacity = this.CacheCapacity;
            return native;
        }

        public static DatabaseSettings FromNative(Knuth.Native.Config.DatabaseSettings native) {
            var res = new DatabaseSettings();
            res.Directory = native.directory;
            res.DbMode = (DbMode)native.db_mode;
            res.ReorgPoolLimit = native.reorg_pool_limit;
            res.DbMaxSize = native.db_max_size;
            res.SafeMode = native.safe_mode;
            res.CacheCapacity = native.cache_capacity;
            return res;
        }
    }
}
