// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Runtime.InteropServices;

namespace Knuth.Native
{
    internal static class HistoryCompactNative
    {
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr chain_history_compact_get_point(IntPtr history);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern UInt64 chain_history_compact_get_point_kind(IntPtr history);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern UInt32 chain_history_compact_get_height(IntPtr history);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern UInt64 chain_history_compact_get_value_or_previous_checksum(IntPtr history);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_history_compact_destruct(IntPtr history);
    }

}