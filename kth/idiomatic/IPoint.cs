using System;

namespace Knuth
{
    public interface IPoint
    {
        /// <summary>
        /// Returns true if and only if this point is not null.
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// Transaction hash in 32 byte array format.
        /// </summary>
        byte[] Hash { get; }

        /// <summary>
        /// Input position in the transaction (zero-based).
        /// </summary>
        UInt32 Index { get; }

        /// <summary>
        /// This is used with OutputPoint identification within a set of
        /// history rows of the same address.
        /// </summary>
        UInt64 Checksum { get; }
    }
}