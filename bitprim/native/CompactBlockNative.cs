using System;
using System.Runtime.InteropServices;

namespace Bitprim.Native
{
    internal static class CompactBlockNative
    {
        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern Header chain_compact_block_header(IntPtr block);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern int /*bool*/ chain_compact_block_is_valid(IntPtr block);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern IntPtr chain_compact_block_transaction_nth(IntPtr block, UInt64 /*size_t*/ n);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern UInt64 chain_compact_block_nonce(IntPtr block);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern UInt64 /*size_t*/ chain_compact_block_serialized_size(IntPtr block, UInt32 version);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern UInt64 /*size_t*/ chain_compact_block_transaction_count(IntPtr block);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern void chain_compact_block_destruct(IntPtr block);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern void chain_compact_block_reset(IntPtr block);
    }

}