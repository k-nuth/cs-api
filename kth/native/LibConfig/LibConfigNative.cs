// Copyright (c) 2016-2021 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Runtime.InteropServices;

namespace Knuth.Native.LibConfig
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Settings {
        public int log_library;
        public bool use_libmdbx;
        public IntPtr version;
        public IntPtr microarchitecture;
        public IntPtr microarchitecture_id;
        public int currency;
        public bool mempool;
        public int db_mode;
        public bool db_readonly;
        public bool debug_mode;
    }
    public static class LibConfigNative
    {
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern Settings kth_libconfig_get();
    }
}

