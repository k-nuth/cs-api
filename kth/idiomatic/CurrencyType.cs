// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

namespace Knuth
{
    /// <summary>
    /// Represents the supported coins
    /// </summary>
    public enum CurrencyType
    {
        /// <summary>
        /// Default value
        /// </summary>
        None = 0,
        /// <summary>
        /// Bitcoin (BTC)
        /// </summary>
        Bitcoin = 1,
        /// <summary>
        /// Bitcoin Cash (BCH)
        /// </summary>
        BitcoinCash = 2,
        /// <summary>
        /// Litecoin (LTC)
        /// </summary>
        Litecoin = 3
    }
}