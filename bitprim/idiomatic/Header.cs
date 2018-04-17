using System;
using System.Runtime.InteropServices;
using Bitprim.Native;

namespace Bitprim
{

    /// <summary>
    /// Represents a full Bitcoin blockchain block.
    /// </summary>
    public class Header : IDisposable
    {
        private bool ownsNativeObject_;
        private IntPtr nativeInstance_;

        ~Header()
        {
            Dispose(false);
        }

        /// <summary>
        /// Returns true if and only if the header conforms to the Bitcoin protocol format.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return HeaderNative.chain_header_is_valid(nativeInstance_) != 0;
            }
        }

        /// <summary>
        /// Block hash in 32 byte array format.
        /// </summary>
        public byte[] Hash
        {
            get
            {
                var managedHash = new hash_t();
                HeaderNative.chain_header_hash_out(nativeInstance_, ref managedHash);
                return managedHash.hash;
            }
        }

        /// <summary>
        /// Merkle root in 32 byte array format.
        /// </summary>
        public byte[] Merkle
        {
            get
            {
                var managedHash = new hash_t();
                HeaderNative.chain_header_merkle_out(nativeInstance_, ref managedHash);
                return managedHash.hash;
            }
        }

        /// <summary>
        /// Hash belonging to the immediately previous block in the blockchain, as a 32 byte array.
        /// This is all zeros for the first block, a.k.a. Genesis.
        /// </summary>
        public byte[] PreviousBlockHash
        {
            get
            {
                var managedHash = new hash_t();
                HeaderNative.chain_header_previous_block_hash_out(nativeInstance_, ref managedHash);
                return managedHash.hash;
            }
        }

        /// <summary>
        /// Hexadecimal string representation of the block's proof (which is a 256-bit number).
        /// </summary>
        public string ProofString
        {
            get
            {
                return new NativeString(HeaderNative.chain_header_proof_str(nativeInstance_)).ToString();
            }
        }

        /// <summary>
        /// Difficulty threshold.
        /// </summary>
        public UInt32 Bits
        {
            get
            {
                return HeaderNative.chain_header_bits(nativeInstance_);
            }
            set
            {
                HeaderNative.chain_header_set_bits(nativeInstance_, value);
            }
        }

        /// <summary>
        /// The nonce that allowed this block to be added to the blockchain.
        /// </summary>
        public UInt32 Nonce
        {
            get
            {
                return HeaderNative.chain_header_nonce(nativeInstance_);
            }
            set
            {
                HeaderNative.chain_header_set_nonce(nativeInstance_, value);
            }
        }

        /// <summary>
        /// Block timestamp in UNIX Epoch format (seconds since January 1st 1970) Assume UTC 0.
        /// </summary>
        public UInt32 Timestamp
        {
            get
            {
                return HeaderNative.chain_header_timestamp(nativeInstance_);
            }
            set
            {
                HeaderNative.chain_header_set_timestamp(nativeInstance_, value);
            }
        }

        /// <summary>
        /// Header protocol version.
        /// </summary>
        public UInt32 Version
        {
            get
            {
                return HeaderNative.chain_header_version(nativeInstance_);
            }
            set
            {
                HeaderNative.chain_header_set_version(nativeInstance_, value);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal Header(IntPtr nativeInstance, bool ownsNativeMem = true)
        {
            nativeInstance_ = nativeInstance;
            ownsNativeObject_ = ownsNativeMem;
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
                //Logger.Log("Destroying header " + nativeInstance_.ToString("X"));
                HeaderNative.chain_header_destruct(nativeInstance_);
                //Logger.Log("Header " + nativeInstance_.ToString("X") + " destroyed!");
            }
        }

    }

}