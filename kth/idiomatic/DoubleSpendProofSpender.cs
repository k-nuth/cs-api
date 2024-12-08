// Copyright (c) 2016-2024 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using Knuth.Native;

namespace Knuth
{
    public class DoubleSpendProofSpender
    {
        private IntPtr nativeInstance_;

        public UInt32 Version
        {
            get
            {
                return DoubleSpendProofSpenderNative.kth_chain_double_spend_proof_spender_version(nativeInstance_);
            }
        }

        public UInt32 OutSequence
        {
            get
            {
                return DoubleSpendProofSpenderNative.kth_chain_double_spend_proof_spender_out_sequence(nativeInstance_);
            }
        }

        public UInt32 Locktime
        {
            get
            {
                return DoubleSpendProofSpenderNative.kth_chain_double_spend_proof_spender_locktime(nativeInstance_);
            }
        }

        public byte[] PrevOutsHash
        {
            get
            {
                var managedHash = new hash_t();
                DoubleSpendProofSpenderNative.kth_chain_double_spend_proof_spender_prev_outs_hash_out(nativeInstance_, ref managedHash);
                return managedHash.hash;
            }
        }

        public byte[] SequenceHash
        {
            get
            {
                var managedHash = new hash_t();
                DoubleSpendProofSpenderNative.kth_chain_double_spend_proof_spender_sequence_hash_out(nativeInstance_, ref managedHash);
                return managedHash.hash;
            }
        }

        public byte[] OutputsHash {
            get {
                var managedHash = new hash_t();
                DoubleSpendProofSpenderNative.kth_chain_double_spend_proof_spender_outputs_hash_out(nativeInstance_, ref managedHash);
                return managedHash.hash;
            }
        }

        public byte[] PushData(bool wire) {
            int size = 0;
            var data = new NativeBuffer(DoubleSpendProofSpenderNative.kth_chain_double_spend_proof_spender_push_data(nativeInstance_, ref size));
            return data.CopyToManagedArray(size);
        }

        public bool IsValid => DoubleSpendProofSpenderNative.kth_chain_double_spend_proof_spender_is_valid(nativeInstance_) != 0;

        public UInt64 GetSerializedSize() {
            return DoubleSpendProofSpenderNative.kth_chain_double_spend_proof_spender_serialized_size(nativeInstance_);
        }

        internal DoubleSpendProofSpender(IntPtr nativeInstance) {
            nativeInstance_ = nativeInstance;
        }

        internal IntPtr NativeInstance
        {
            get
            {
                return nativeInstance_;
            }
        }
    }

}