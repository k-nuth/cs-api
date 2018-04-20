using System;
using System.Runtime.InteropServices;

namespace Bitprim.Native
{

    public static class HistoryCompactNative
    {
        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern IntPtr chain_history_compact_get_point(IntPtr history);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern UInt64 chain_history_compact_get_point_kind(IntPtr history);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern UInt32 chain_history_compact_get_height(IntPtr history);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern UInt64 chain_history_compact_get_value_or_previous_checksum(IntPtr history);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern void chain_history_compact_destruct(IntPtr history);
    }

}