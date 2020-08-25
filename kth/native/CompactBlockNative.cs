// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Runtime.InteropServices;

namespace Knuth.Native
{
    internal static class CompactBlockNative
    {
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern Header kth_chain_compact_block_header(IntPtr block);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern int /*bool*/ kth_chain_compact_block_is_valid(IntPtr block);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr kth_chain_compact_block_transaction_nth(IntPtr block, UInt64 /*size_t*/ n);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern UInt64 kth_chain_compact_block_nonce(IntPtr block);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern UInt64 /*size_t*/ kth_chain_compact_block_serialized_size(IntPtr block, UInt32 version);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern UInt64 /*size_t*/ kth_chain_compact_block_transaction_count(IntPtr block);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_chain_compact_block_destruct(IntPtr block);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_chain_compact_block_reset(IntPtr block);
    }

}