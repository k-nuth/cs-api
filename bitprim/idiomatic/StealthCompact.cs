using System;
using Bitprim.Native;

namespace Bitprim
{

    /// <summary>
    /// Stealth payment related data.
    /// </summary>
    public class StealthCompact : IDisposable
    {
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

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //Release managed resources and call Dispose for member variables
            }
            //Release unmanaged resources
            StealthCompactNative.stealth_compact_destruct(nativeInstance_);
        }

        internal StealthCompact(IntPtr nativeInstance)
        {
            nativeInstance_ = nativeInstance;
        }
    }

}