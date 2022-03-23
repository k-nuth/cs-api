// Copyright (c) 2016-2022 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Runtime.InteropServices;

namespace Knuth.Native
{
    internal static class InputNative
    {

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern int /*bool*/ kth_chain_input_is_final(IntPtr input);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern int kth_chain_input_is_valid(IntPtr input);

        //input(output_point&& previous_output, chain::script&& script, uint32_t sequence);
        //input(const output_point& previous_output, const chain::script& script, uint32_t sequence);
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr kth_chain_input_construct(IntPtr previous_output, IntPtr script, UInt32 sequence);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr kth_chain_input_construct_default();

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr kth_chain_input_previous_output(IntPtr input);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr kth_chain_input_script(IntPtr input);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern UInt32 kth_chain_input_sequence(IntPtr input);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern UIntPtr /*size_t*/ kth_chain_input_serialized_size(IntPtr input, int /*bool*/ wire /* = true*/);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern UIntPtr /*size_t*/ kth_chain_input_signature_operations(IntPtr input, int /*bool*/ bip16_active);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_chain_input_destruct(IntPtr input);

    }

}