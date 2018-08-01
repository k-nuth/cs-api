using System;
using System.Runtime.InteropServices;

namespace Bitprim.Native
{
    internal static class HistoryCompactListNative
    {

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern IntPtr chain_history_compact_list_nth(IntPtr list, UInt64 n);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern UInt64 chain_history_compact_list_count(IntPtr list);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern void chain_history_compact_list_destruct(IntPtr list);
    }

}