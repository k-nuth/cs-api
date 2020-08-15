// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Runtime.InteropServices;

namespace Knuth.Native
{
    internal static class PointNative
    {

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern int /*bool*/ chain_point_is_valid(IntPtr point);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern UInt32 chain_point_get_index(IntPtr point);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern UInt64 chain_point_get_checksum(IntPtr point);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_point_get_hash_out(IntPtr point, ref hash_t hash);

    }

}