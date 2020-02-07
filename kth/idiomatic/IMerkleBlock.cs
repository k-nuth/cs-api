using System;

namespace Knuth
{
    public interface IMerkleBlock : IDisposable
    {
        /// <summary>
        /// Returns true if and only if it the block contains txs hashes, and the header is valid.
        /// </summary>
        bool IsValid { get ; }

        /// <summary>
        /// The block's header.
        /// </summary>
        IHeader Header { get; }

        /// <summary>
        /// Transaction hashes list element count.
        /// </summary>
        UInt64 HashCount { get; }

        /// <summary>
        /// Amount of transactions inside the block.
        /// </summary>
        UInt64 TotalTransactionCount { get; }

        /// <summary>
        /// Get the Nth transaction hash from the block.
        /// </summary>
        /// <param name="n">Zerp-based index.</param>
        /// <returns> Transaction hash in 32 byte array format. </returns>
        byte[] GetNthHash(int n);

        /// <summary>
        /// Block size in bytes (as a Merkle block, not as a full block).
        /// </summary>
        /// <param name="version"> Protocol version to consider when calculating size. </param>
        /// <returns> Size in bytes. </returns>
        UInt64 GetSerializedSize(UInt32 version);

        /// <summary>
        /// Delete all the data inside the block.
        /// </summary>
        void Reset();
    }
}