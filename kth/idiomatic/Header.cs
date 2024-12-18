// Copyright (c) 2016-2024 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using Knuth.Native;

namespace Knuth
{

    /// <summary>
    /// Represents a Bitcoin block header.
    /// </summary>
    public class Header : IHeader
    {
        private readonly bool ownsNativeObject_;
        private readonly IntPtr nativeInstance_;

        ~Header() {
            Dispose(false);
        }

        /// <summary>
        /// Returns true if and only if the header conforms to the Bitcoin protocol format.
        /// </summary>
        public bool IsValid => HeaderNative.kth_chain_header_is_valid(nativeInstance_) != 0;

        /// <summary>
        /// Header/Block hash in 32 byte array format.
        /// </summary>
        public byte[] Hash
        {
            get
            {
                var managedHash = new hash_t();
                HeaderNative.kth_chain_header_hash_out(nativeInstance_, ref managedHash);
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
                HeaderNative.kth_chain_header_merkle_out(nativeInstance_, ref managedHash);
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
                HeaderNative.kth_chain_header_previous_block_hash_out(nativeInstance_, ref managedHash);
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
                using ( var proofString = new NativeString(HeaderNative.kth_chain_header_proof_str(nativeInstance_)) ) {
                    return proofString.ToString();
                }
            }
        }

        /// <summary>
        /// Difficulty threshold.
        /// </summary>
        public UInt32 Bits
        {
            get => HeaderNative.kth_chain_header_bits(nativeInstance_);
            set => HeaderNative.kth_chain_header_set_bits(nativeInstance_, value);
        }

        /// <summary>
        /// The nonce that allowed this block to be added to the blockchain.
        /// </summary>
        public UInt32 Nonce
        {
            get => HeaderNative.kth_chain_header_nonce(nativeInstance_);
            set => HeaderNative.kth_chain_header_set_nonce(nativeInstance_, value);
        }

        /// <summary>
        /// Block timestamp in UNIX Epoch format (seconds since January 1st 1970) Assume UTC 0.
        /// </summary>
        public UInt32 Timestamp
        {
            get => HeaderNative.kth_chain_header_timestamp(nativeInstance_);
            set => HeaderNative.kth_chain_header_set_timestamp(nativeInstance_, value);
        }

        /// <summary>
        /// Header protocol version.
        /// </summary>
        public UInt32 Version
        {
            get => HeaderNative.kth_chain_header_version(nativeInstance_);
            set => HeaderNative.kth_chain_header_set_version(nativeInstance_, value);
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Raw header data.
        /// </summary>
        /// <param name="version">Version of the header.</param>
        /// <returns>Byte array with header data.</returns>
        public byte[] ToData(UInt32 version) {
            int headerSize = 0;
            using (var headerData = new NativeBuffer(HeaderNative.kth_chain_header_to_data(nativeInstance_, version, ref headerSize))) {
                return headerData.CopyToManagedArray(headerSize);
            }
        }

        internal Header(IntPtr nativeInstance, bool ownsNativeMem = true) {
            nativeInstance_ = nativeInstance;
            ownsNativeObject_ = ownsNativeMem;
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                //Release managed resources and call Dispose for member variables
            }
            //Release unmanaged resources
            if (ownsNativeObject_) {
                //Logger.Log("Destroying header " + nativeInstance_.ToString("X"));
                HeaderNative.kth_chain_header_destruct(nativeInstance_);
                //Logger.Log("Header " + nativeInstance_.ToString("X") + " destroyed!");
            }
        }

    }

}