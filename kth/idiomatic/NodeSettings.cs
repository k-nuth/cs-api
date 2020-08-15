// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using Knuth.Native;

namespace Knuth
{
    /// <summary>
    /// Holds node settings
    /// </summary>
    public class NodeSettings
    {
        /// <summary>
        /// Returns the node's currency
        /// </summary>
        public static CurrencyType CurrencyType
        {
            get
            {
                return NodeSettingsNative.node_settings_get_currency();
            }
        }

    }
}