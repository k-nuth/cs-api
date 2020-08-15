// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Runtime.InteropServices;

namespace Knuth.Native
{
    internal static class GetBlocksNative
    {
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern byte[] chain_get_blocks_stop_hash(IntPtr get_b);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern int /*bool*/ chain_get_blocks_is_valid(IntPtr get_b);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr chain_get_blocks_construct_default();

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr chain_get_blocks_construct(IntPtr start, byte[] stop);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr chain_get_blocks_start_hashes(IntPtr get_b);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern UInt64 /*size_t*/ chain_get_blocks_serialized_size(IntPtr get_b, UInt32 version);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_get_blocks_destruct(IntPtr get_b);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_get_blocks_reset(IntPtr get_b);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_get_blocks_set_start_hashes(IntPtr get_b, IntPtr value);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_get_blocks_set_stop_hash(IntPtr get_b, byte[] value);

        //virtual bool from_data(uint32_t version, const data_chunk& data);
        //virtual bool from_data(uint32_t version, std::istream& stream);
        //virtual bool from_data(uint32_t version, reader& source);
        //data_chunk to_data(uint32_t version) const;
        //void to_data(uint32_t version, std::ostream& stream) const;
        //void to_data(uint32_t version, writer& sink) const;

    }

}