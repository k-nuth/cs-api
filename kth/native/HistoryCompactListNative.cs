// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Runtime.InteropServices;

namespace Knuth.Native
{
    internal static class HistoryCompactListNative
    {
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr chain_history_compact_list_nth(IntPtr list, UInt64 n);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern UInt64 chain_history_compact_list_count(IntPtr list);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_history_compact_list_destruct(IntPtr list);
    }

}