// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Runtime.InteropServices;

namespace Knuth.Native
{
    internal static class BlockIndexesNative
    {

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr chain_block_indexes_construct_default();

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_block_indexes_push_back(IntPtr list, UIntPtr /*size_t*/ index);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_block_indexes_destruct(IntPtr list);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern UIntPtr /*size_t*/ chain_block_indexes_count(IntPtr list);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern UIntPtr /*size_t*/ chain_block_indexes_nth(IntPtr list, UIntPtr /*size_t*/ n);

    }

}