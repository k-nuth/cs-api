// Copyright (c) 2016-2024 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Runtime.InteropServices;

namespace Knuth.Native
{
    internal static class BlockListNative
    {
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr kth_chain_block_list_construct_default();

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr kth_chain_block_list_nth(IntPtr list, UInt64 n);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern UInt64 kth_chain_block_list_count(IntPtr list);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_chain_block_list_push_back(IntPtr list, IntPtr block);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_chain_block_list_destruct(IntPtr list);

    }

}