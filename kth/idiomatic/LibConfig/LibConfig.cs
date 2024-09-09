// Copyright (c) 2016-2022 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Knuth
{
    public class LibConfig {
        public enum LogLibrary {
            Boost = 0,
            Spdlog = 1,
            Binlog = 2,
        }


        public struct Settings {
            public LogLibrary LogLibrary;
            public bool UseLibmdbx;
            public string Version;
            public string MicroarchitectureId;
            public CurrencyType Currency;
            public bool Mempool;
            public bool DbReadonly;
            public bool DebugMode;
        }

        public static Settings Get() {
            var native = Knuth.Native.LibConfig.LibConfigNative.kth_libconfig_get();
            var ret = new Settings {
                LogLibrary = (LogLibrary)native.log_library,
                UseLibmdbx = native.use_libmdbx,
                Version = Marshal.PtrToStringAnsi(native.version),
                MicroarchitectureId = Marshal.PtrToStringAnsi(native.microarchitecture_id),
                Currency = (CurrencyType)native.currency,
                Mempool = native.mempool,
                DbReadonly = native.db_readonly,
                DebugMode = native.debug_mode
            };
            return ret;
        }
    }
}
