// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using Knuth.Native;

namespace Knuth
{
    /*
    /// <summary>
    /// Helper for reading the header for each block in a specific set of blocks.
    /// </summary>
    public class HeaderReader : IDisposable
    {
        private IntPtr nativeInstance_;

        /// <summary>
        /// Create an empty reader.
        /// </summary>
        public HeaderReader() {
            nativeInstance_ = GetHeadersNative.kth_chain_get_headers_construct_default();
        }

        /// <summary>
        /// Create a reader with predefined start hashes and stop hash.
        /// </summary>
        /// <param name="start"> When all of these blocks are synced, start reading. </param>
        /// <param name="stop"> Stop at this block. </param>
        public HeaderReader(HashList start, byte[] stop) {
            nativeInstance_ = GetHeadersNative.kth_chain_get_headers_construct(start.NativeInstance, stop);
        }

        ~HeaderReader() {
            Dispose(false);
        }

        /// <summary>
        /// The block set is valid iif all its blocks are valid.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return GetHeadersNative.kth_chain_get_headers_is_valid(nativeInstance_) != 0;
            }
        }

        /// <summary>
        /// Stop at this block (include it in the set).
        /// </summary>
        public byte[] StopHash
        {
            get
            {
                return GetHeadersNative.kth_chain_get_headers_stop_hash(nativeInstance_);
            }
            set
            {
                GetHeadersNative.kth_chain_get_headers_set_stop_hash(nativeInstance_, value);
            }
        }

        /// <summary>
        /// Define when to start reading: Once these blocks are synced (include the newest one).
        /// </summary>
        public HashList StartHashes
        {
            get
            {
                return new HashList(GetHeadersNative.kth_chain_get_headers_start_hashes(nativeInstance_), false);
            }
            set
            {
                GetHeadersNative.kth_chain_get_headers_set_start_hashes(nativeInstance_, value.NativeInstance);
            }
        }

        /// <summary>
        /// The sum of the header sizes for this set.
        /// </summary>
        /// <param name="version"> Protocol version to consider when calculating header size. </param>
        /// <returns> Sum of header sizes. </returns>
        public UInt64 GetSerializedSize(UInt32 version) {
            return GetHeadersNative.kth_chain_get_headers_serialized_size(nativeInstance_, version);
        }

        /// <summary>
        /// Release resources.
        /// </summary>
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Go back to first block in the set.
        /// </summary>
        public void Reset() {
            GetHeadersNative.kth_chain_get_headers_reset(nativeInstance_);
        }

        internal HeaderReader(IntPtr nativeInstance) {
            nativeInstance_ = nativeInstance;
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                //Release managed resources and call Dispose for member variables
            }
            //Release unmanaged resources
            //Logger.Log("Destroying header reader " + nativeInstance_.ToString("X"));
            GetHeadersNative.kth_chain_get_headers_destruct(nativeInstance_);
        }

    }*/

}