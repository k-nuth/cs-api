// Copyright (c) 2016-2021 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using Knuth.Native;

namespace Knuth
{
    /*
    /// <summary>
    /// Allows user to read a specific set of blocks from the blockchain.
    /// </summary>
    public class BlockReader : IDisposable
    {
        private IntPtr nativeInstance_;

        public BlockReader() {
            nativeInstance_ = GetBlocksNative.kth_chain_get_blocks_construct_default();
        }

        public BlockReader(HashList start, byte[] stop) {
            nativeInstance_ = GetBlocksNative.kth_chain_get_blocks_construct(start.NativeInstance, stop);
        }

        ~BlockReader() {
            Dispose(false);
        }

        /// <summary>
        /// Return true iif all blocks in the specified set are valid
        /// </summary>
        public bool IsValid
        {
            get
            {
                return GetBlocksNative.kth_chain_get_blocks_is_valid(nativeInstance_) != 0;
            }
        }

        /// <summary>
        /// Get or set on which block to stop reading.
        /// </summary>
        public byte[] StopHash
        {
            get
            {
                return GetBlocksNative.kth_chain_get_blocks_stop_hash(nativeInstance_);
            }
            set
            {
                GetBlocksNative.kth_chain_get_blocks_set_stop_hash(nativeInstance_, value);
            }
        }

        /// <summary>
        /// Get or set the hashes that have to be read in order to start reading.
        /// </summary>
        public HashList StartHashes
        {
            get
            {
                return new HashList(GetBlocksNative.kth_chain_get_blocks_start_hashes(nativeInstance_), false);
            }
            set
            {
                GetBlocksNative.kth_chain_get_blocks_set_start_hashes(nativeInstance_, value.NativeInstance);
            }
        }

        /// <summary>
        /// The sum of the sizes of the read blocks.
        /// </summary>
        /// <param name="version"> Protocol version to consider when calculating block size. </param>
        /// <returns> UInt64 representation of the sum </returns>
        public UInt64 GetSerializedSize(UInt32 version) {
            return GetBlocksNative.kth_chain_get_blocks_serialized_size(nativeInstance_, version);
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Go back to the beginning of the block set.
        /// </summary>
        public void Reset() {
            GetBlocksNative.kth_chain_get_blocks_reset(nativeInstance_);
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                //Release managed resources and call Dispose for member variables
            }
            //Release unmanaged resources
            //Logger.Log("Destroying block reader " + nativeInstance_.ToString("X"));
            GetBlocksNative.kth_chain_get_blocks_destruct(nativeInstance_);
        }
    }
    */
}