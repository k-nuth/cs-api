// Copyright (c) 2016-2021 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Runtime.InteropServices;

namespace Knuth.Native
{
    internal static class OutputNative
    {

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern int kth_chain_output_is_valid(IntPtr output);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr kth_chain_output_construct(UInt64 value, IntPtr script);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr kth_chain_output_construct_default();

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr kth_chain_output_script(IntPtr output);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern UInt64 kth_chain_output_value(IntPtr output);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr kth_chain_output_payment_address(IntPtr output, int /*bool*/ use_testnet_rules);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern UIntPtr /*size_t*/ kth_chain_output_serialized_size(IntPtr output, int /*bool*/ wire /*= true*/);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern UIntPtr /*size_t*/ kth_chain_output_signature_operations(IntPtr output);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_chain_output_destruct(IntPtr output);

    }

}