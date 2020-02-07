using System;
using Knuth.Native;

namespace Knuth
{
    /// <summary>
    /// BIP 512 representation of a block for reduced propagation bandwidth.
    /// </summary>
    public class CompactBlock : IDisposable
    {
        private IntPtr nativeInstance_;

        ~CompactBlock()
        {
            Dispose(false);
        }

        /// <summary>
        /// Returns true iif this is a valid compact representation of a block (as per BIP 512).
        /// </summary>
        public bool IsValid => CompactBlockNative.chain_compact_block_is_valid(nativeInstance_) != 0;

        /// <summary>
        /// Block nonce (i.e. value which makes hash start with leading zeroes), as a 64-bit unsigned integer.
        /// </summary>
        public UInt64 Nonce => CompactBlockNative.chain_compact_block_nonce(nativeInstance_);

        /// <summary>
        /// Amount of transactions that belong to the block.
        /// </summary>
        public UInt64 TransactionCount => CompactBlockNative.chain_compact_block_transaction_count(nativeInstance_);

        /// <summary>
        /// Get the block's nth transaction, synchronously.
        /// </summary>
        /// <param name="n"> Zero-based index </param>
        /// <returns> Full transaction object </returns>
        public Transaction GetNthTransaction(UInt64 n)
        {
            return new Transaction(CompactBlockNative.chain_compact_block_transaction_nth(nativeInstance_, n), false);
        }

        /// <summary>
        /// Get the compact block's serialized size.
        /// </summary>
        /// <param name="version"> Protocol version to consider when calculating size. </param>
        /// <returns> Size in bytes. </returns>
        public UInt64 GetSerializedSize(UInt32 version)
        {
            return CompactBlockNative.chain_compact_block_serialized_size(nativeInstance_, version);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// TODO: Document
        /// </summary>
        public void Reset()
        {
            CompactBlockNative.chain_compact_block_reset(nativeInstance_);
        }

        internal CompactBlock(IntPtr nativeInstance)
        {
            nativeInstance_ = nativeInstance;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //Release managed resources and call Dispose for member variables
            }
            //Release unmanaged resources
            CompactBlockNative.chain_compact_block_destruct(nativeInstance_);
        }
    }
}