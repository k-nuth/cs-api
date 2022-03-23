// Copyright (c) 2016-2022 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Runtime.InteropServices;

namespace Knuth.Native
{
    internal static class DoubleSpendProofNative
    {
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr kth_chain_double_spend_proof_out_point(IntPtr dsp);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr kth_chain_double_spend_proof_spender1(IntPtr dsp);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr kth_chain_double_spend_proof_spender2(IntPtr dsp);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern byte[] kth_chain_double_spend_proof_hash(IntPtr dsp);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_chain_double_spend_proof_hash_out(IntPtr block, ref hash_t out_hash);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern int /*bool*/ kth_chain_double_spend_proof_is_valid(IntPtr dsp);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern UInt64 kth_chain_double_spend_proof_serialized_size(IntPtr dsp, UInt32 version);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_chain_double_spend_proof_destruct(IntPtr dsp);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_chain_double_spend_proof_reset(IntPtr dsp);
    }
}