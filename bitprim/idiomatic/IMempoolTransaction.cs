using System;

namespace Bitprim
{
    public interface IMempoolTransaction
    {
        /// <summary>
        /// Transaction output address
        /// </summary>
        string Address { get; }

        /// <summary>
        /// Transaction hash (unique identifier)
        /// </summary>
        string Hash { get; }

        /// <summary>
        /// Previous output transaction hash
        /// </summary>
        string PreviousOutputHash { get ; }

        /// <summary>
        /// Previous output transaction index
        /// </summary>
        string PreviousOutputIndex { get; }

        /// <summary>
        /// Sum of output values in Satoshis
        /// </summary>
        string Satoshis { get; }

        /// <summary>
        /// Transaction index
        /// </summary>
        UInt64 Index { get; }

        /// <summary>
        /// Transaction timestamp
        /// </summary>
        UInt64 Timestamp { get; }
    }
}