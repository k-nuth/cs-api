// Copyright (c) 2016-2024 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using Knuth.Native;

namespace Knuth
{
    public class DoubleSpendProof : IDisposable
    {
        private IntPtr nativeInstance_;

        ~DoubleSpendProof() {
            Dispose(false);
        }

        public OutputPoint OutPoint => new OutputPoint(DoubleSpendProofNative.kth_chain_double_spend_proof_out_point(nativeInstance_));

        public DoubleSpendProofSpender Spender1 => new DoubleSpendProofSpender(DoubleSpendProofNative.kth_chain_double_spend_proof_spender1(nativeInstance_));

        public DoubleSpendProofSpender Spender2 => new DoubleSpendProofSpender(DoubleSpendProofNative.kth_chain_double_spend_proof_spender2(nativeInstance_));

        public byte[] Hash
        {
            get
            {
                var managedHash = new hash_t();
                DoubleSpendProofNative.kth_chain_double_spend_proof_hash_out(nativeInstance_, ref managedHash);
                return managedHash.hash;
            }
        }

        public bool IsValid => DoubleSpendProofNative.kth_chain_double_spend_proof_is_valid(nativeInstance_) != 0;

        public UInt64 GetSerializedSize(UInt32 version) {
            return DoubleSpendProofNative.kth_chain_double_spend_proof_serialized_size(nativeInstance_, version);
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void Reset() {
            DoubleSpendProofNative.kth_chain_double_spend_proof_reset(nativeInstance_);
        }

        internal DoubleSpendProof(IntPtr nativeInstance) {
            nativeInstance_ = nativeInstance;
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                //Release managed resources and call Dispose for member variables
            }
            //Release unmanaged resources
            DoubleSpendProofNative.kth_chain_double_spend_proof_destruct(nativeInstance_);
        }
    }
}