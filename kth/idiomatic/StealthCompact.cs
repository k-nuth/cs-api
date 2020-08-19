// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using Knuth.Native;

namespace Knuth
{

    /// <summary>
    /// Stealth payment related data.
    /// </summary>
    public class StealthCompact : IStealthCompact
    {
        private bool ownsNativeObject_;
        private IntPtr nativeInstance_;

        ~StealthCompact()
        {
            Dispose(false);
        }

        /// <summary>
        /// 33 bytes. Includes the sign byte (0x02).
        /// </summary>
        public byte[] EphemeralPublicKeyHash
        {
            get
            {
                return StealthCompactNative.stealth_compact_get_ephemeral_public_key_hash(nativeInstance_);
            }
        }

        /// <summary>
        /// Public key hash in 32 bytes array format.
        /// </summary>
        public byte[] PublicKeyHash
        {
            get
            {
                return StealthCompactNative.stealth_compact_get_public_key_hash(nativeInstance_);
            }
        }

        /// <summary>
        /// Transaction hash in 32 byte array format.
        /// </summary>
        public byte[] TransactionHash
        {
            get
            {
                return StealthCompactNative.stealth_compact_get_transaction_hash(nativeInstance_);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal StealthCompact(IntPtr nativeInstance, bool ownsNativeObject = true)
        {
            nativeInstance_ = nativeInstance;
            ownsNativeObject_ = ownsNativeObject;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //Release managed resources and call Dispose for member variables
            }
            //Release unmanaged resources
            if(ownsNativeObject_)
            {
                //Logger.Log("Destroying stealth compact " + nativeInstance_.ToString("X") + " ...");
                StealthCompactNative.stealth_compact_destruct(nativeInstance_);
                //Logger.Log("Stealth compact " + nativeInstance_.ToString("X") + " destroyed!");
            }
        }
    }

}