// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;

namespace Knuth
{
    public interface IHeader : IDisposable
    {
        /// <summary>
        /// Returns true if and only if the header conforms to the Bitcoin protocol format.
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// Block hash in 32 byte array format.
        /// </summary>
        byte[] Hash { get; }

        /// <summary>
        /// Merkle root in 32 byte array format.
        /// </summary>
        byte[] Merkle { get; }

        /// <summary>
        /// Hash belonging to the immediately previous block in the blockchain, as a 32 byte array.
        /// This is all zeros for the first block, a.k.a. Genesis.
        /// </summary>
        byte[] PreviousBlockHash { get; }

        /// <summary>
        /// Hexadecimal string representation of the block's proof (which is a 256-bit number).
        /// </summary>
        string ProofString { get; }

        /// <summary>
        /// Difficulty threshold.
        /// </summary>
        UInt32 Bits { get; set; }

        /// <summary>
        /// The nonce that allowed this block to be added to the blockchain.
        /// </summary>
        UInt32 Nonce { get; set; }

        /// <summary>
        /// Block timestamp in UNIX Epoch format (seconds since January 1st 1970) Assume UTC 0.
        /// </summary>
        UInt32 Timestamp { get; set; }

        /// <summary>
        /// Header protocol version.
        /// </summary>
        UInt32 Version { get; set; }
    }
}