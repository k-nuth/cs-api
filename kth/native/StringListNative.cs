// Copyright (c) 2016-2024 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Runtime.InteropServices;

namespace Knuth.Native
{
    internal static class StringListNative
    {
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr kth_core_string_list_construct();

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_core_string_list_push_back(IntPtr string_list, [MarshalAs(UnmanagedType.LPStr)]string to_add);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_core_string_list_destruct(IntPtr string_list);
    }
}