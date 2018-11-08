using System;
using System.Runtime.InteropServices;

namespace Bitprim.Native
{
    internal static class StringListNative
    {
        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern IntPtr core_string_list_construct();

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern void core_string_list_push_back(IntPtr string_list, [MarshalAs(UnmanagedType.LPStr)]string to_add);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern void core_string_list_destruct(IntPtr string_list);
    }
}