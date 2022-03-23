// Copyright (c) 2016-2022 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Runtime.InteropServices;

namespace Knuth.Native
{
    internal static class Platform
    {
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_platform_free(IntPtr nativePtr);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr kth_platform_allocate_string(UInt64 n);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_platform_allocate_string_at(ref IntPtr where, UInt64 n);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr kth_platform_allocate_array_of_strings(UInt64 n);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_platform_print_string(IntPtr str);

        [DllImport(Constants.KTH_C_LIBRARY, CharSet=CharSet.Ansi)]
        public static extern IntPtr kth_platform_allocate_and_copy_string_at(IntPtr ptr, UInt64 offset, string str);
    }
}