// Copyright (c) 2016-2021 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Runtime.InteropServices;

namespace Knuth.Native
{
    internal static class GetHeadersNative
    {
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern byte[] kth_chain_get_headers_stop_hash(IntPtr getHeaders);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern int /*bool*/ kth_chain_get_headers_is_valid(IntPtr getHeaders);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr kth_chain_get_headers_construct(IntPtr start, byte[] stop);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr kth_chain_get_headers_construct_default();

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr kth_chain_get_headers_start_hashes(IntPtr getHeaders);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern UInt64 kth_chain_get_headers_serialized_size(IntPtr getHeaders, UInt32 version);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_chain_get_headers_destruct(IntPtr getHeaders);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_chain_get_headers_reset(IntPtr getHeaders);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_chain_get_headers_set_start_hashes(IntPtr getHeaders, IntPtr value);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_chain_get_headers_set_stop_hash(IntPtr get_b, byte[] value);

    }

}