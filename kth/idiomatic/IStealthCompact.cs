// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;

namespace Knuth
{
    public interface IStealthCompact : IDisposable
    {
        /// <summary>
        /// 33 bytes. Includes the sign byte (0x02).
        /// </summary>
        byte[] EphemeralPublicKeyHash { get; }

        /// <summary>
        /// Public key hash in 32 bytes array format.
        /// </summary>
        byte[] PublicKeyHash { get; }

        /// <summary>
        /// Transaction hash in 32 byte array format.
        /// </summary>
        byte[] TransactionHash { get; }
    }
}