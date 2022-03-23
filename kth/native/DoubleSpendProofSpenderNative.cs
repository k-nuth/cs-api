// Copyright (c) 2016-2022 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Runtime.InteropServices;

namespace Knuth.Native
{
    internal static class DoubleSpendProofSpenderNative
    {
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern UInt32 kth_chain_double_spend_proof_spender_version(IntPtr spender);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern UInt32 kth_chain_double_spend_proof_spender_out_sequence(IntPtr spender);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern UInt32 kth_chain_double_spend_proof_spender_locktime(IntPtr spender);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern byte[] kth_chain_double_spend_proof_spender_prev_outs_hash(IntPtr spender);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_chain_double_spend_proof_spender_prev_outs_hash_out(IntPtr block, ref hash_t out_hash);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern byte[] kth_chain_double_spend_proof_spender_sequence_hash(IntPtr spender);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_chain_double_spend_proof_spender_sequence_hash_out(IntPtr block, ref hash_t out_hash);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern byte[] kth_chain_double_spend_proof_spender_outputs_hash(IntPtr spender);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_chain_double_spend_proof_spender_outputs_hash_out(IntPtr block, ref hash_t out_hash);

        //Note: Returned memory must be freed manually using platform_free
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr kth_chain_double_spend_proof_spender_push_data(IntPtr spender, ref int size);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern int /*bool*/ kth_chain_double_spend_proof_spender_is_valid(IntPtr spender);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern UInt64 kth_chain_double_spend_proof_spender_serialized_size(IntPtr spender);
    }

}