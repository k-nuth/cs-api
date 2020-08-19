// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

namespace Knuth
{
    /// <summary>
    /// Represents the network type (Mainnet, Testnet, Regtest)
    /// </summary>
    public enum NetworkType
    {
        /// <summary>
        /// Default value
        /// </summary>
        None = 0,
        /// <summary>
        /// Main network
        /// </summary>
        Mainnet = 1,
        /// <summary>
        /// Test network
        /// </summary>
        Testnet = 2,
        /// <summary>
        /// Regression test network
        /// </summary>
        Regtest = 3
    }
}