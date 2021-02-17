// Copyright (c) 2016-2021 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using Knuth.Native;

namespace Knuth
{

    /// <summary>
    /// Merkle tree representation of a blockchain block.
    /// </summary>
    public class MerkleBlock : IMerkleBlock
    {
        private readonly Header header_;
        private readonly IntPtr nativeInstance_;

        ~MerkleBlock() {
            Dispose(false);
        }

        /// <summary>
        /// Returns true if and only if it the block contains txs hashes, and the header is valid.
        /// </summary>
        public bool IsValid => MerkleBlockNative.kth_chain_merkle_block_is_valid(nativeInstance_) != 0;

        /// <summary>
        /// The block's header.
        /// </summary>
        public IHeader Header => header_;

        /// <summary>
        /// Transaction hashes list element count.
        /// </summary>
        public UInt64 HashCount => (UInt64)MerkleBlockNative.kth_chain_merkle_block_hash_count(nativeInstance_);

        /// <summary>
        /// Amount of transactions inside the block.
        /// </summary>
        public UInt64 TotalTransactionCount => (UInt64)MerkleBlockNative.kth_chain_merkle_block_total_transaction_count(nativeInstance_);

        /// <summary>
        /// Get the Nth transaction hash from the block.
        /// </summary>
        /// <param name="n">Zerp-based index.</param>
        /// <returns> Transaction hash in 32 byte array format. </returns>
        public byte[] GetNthHash(int n) {
            var managedHash = new hash_t();
            MerkleBlockNative.kth_chain_merkle_block_hash_nth_out(nativeInstance_, (UIntPtr)n, ref managedHash);
            return managedHash.hash;
        }

        /// <summary>
        /// Block size in bytes (as a Merkle block, not as a full block).
        /// </summary>
        /// <param name="version"> Protocol version to consider when calculating size. </param>
        /// <returns> Size in bytes. </returns>
        public UInt64 GetSerializedSize(UInt32 version) {
            return (UInt64)MerkleBlockNative.kth_chain_merkle_block_serialized_size(nativeInstance_, version);
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Delete all the data inside the block.
        /// </summary>
        public void Reset() {
            MerkleBlockNative.kth_chain_merkle_block_reset(nativeInstance_);
        }

        internal MerkleBlock(IntPtr nativeInstance) {
            nativeInstance_ = nativeInstance;
            header_ = new Header(MerkleBlockNative.kth_chain_merkle_block_header(nativeInstance_), false);
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                //Release managed resources and call Dispose for member variables
                header_.Dispose();
            }
            //Release unmanaged resources
            //Logger.Log("Destroying merkle block " + nativeInstance_.ToString("X") + " ...");
            MerkleBlockNative.kth_chain_merkle_block_destruct(nativeInstance_);
            //Logger.Log("Merkle block " + nativeInstance_.ToString("X") + " destroyed!");
        }
    }

}