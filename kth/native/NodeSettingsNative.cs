// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System.Runtime.InteropServices;

namespace Knuth.Native
{
    internal static class NodeSettingsNative
    {
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern CurrencyType node_settings_get_currency();
    }

}