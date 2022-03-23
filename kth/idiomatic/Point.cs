// Copyright (c) 2016-2022 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using Knuth.Native;

namespace Knuth
{

    /// <summary>
    /// Represents one of the transaction inputs.
    /// It's a transaction hash and index pair.
    /// </summary>
    public class Point : IPoint
    {
        private IntPtr nativeInstance_;

        /// <summary>
        /// Returns true if and only if this point is not null.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return PointNative.kth_chain_point_is_valid(nativeInstance_) != 0;
            }
        }

        /// <summary>
        /// Transaction hash in 32 byte array format.
        /// </summary>
        public byte[] Hash
        {
            get
            {
                var managedHash = new hash_t();
                PointNative.kth_chain_point_get_hash_out(nativeInstance_, ref managedHash);
                return managedHash.hash;
            }
        }

        /// <summary>
        /// Input position in the transaction (zero-based).
        /// </summary>
        public UInt32 Index
        {
            get
            {
                return PointNative.kth_chain_point_get_index(nativeInstance_);
            }
        }

        /// <summary>
        /// This is used with OutputPoint identification within a set of
        /// history rows of the same address.
        /// </summary>
        public UInt64 Checksum
        {
            get
            {
                return PointNative.kth_chain_point_get_checksum(nativeInstance_);
            }
        }

        internal Point(IntPtr nativeInstance) {
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