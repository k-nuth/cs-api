// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Runtime.InteropServices;

namespace Knuth.Native
{
    internal static class StealthCompactNative
    {
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern byte[] kth_chain_stealth_compact_get_ephemeral_public_key_hash(IntPtr stealth);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern byte[] kth_chain_stealth_compact_get_public_key_hash(IntPtr stealth);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern byte[] kth_chain_stealth_compact_get_transaction_hash(IntPtr stealth);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_chain_stealth_compact_destruct(IntPtr stealth);
    }

}