// Copyright (c) 2016-2022 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Runtime.InteropServices;

namespace Knuth.Native
{
    internal static class BinaryNative
    {
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern byte[] kth_core_binary_blocks(IntPtr binary, ref UIntPtr /*size_t*/ out_n);

        //Note: The user is responsible for releasing the resource
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr kth_core_binary_construct();

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr kth_core_binary_construct_string([MarshalAs(UnmanagedType.LPStr)]string hexString);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr kth_core_binary_construct_blocks(UIntPtr /*size_t*/ bits_size, byte[] blocks, UIntPtr /*size_t*/ n);

        //TODO(fernando): check how to deallocate this string, is it automatically?
        [DllImport(Constants.KTH_C_LIBRARY)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string kth_core_binary_encoded(IntPtr binary);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_core_binary_destruct(IntPtr binary); //TODO Implement in C API
    }
}
