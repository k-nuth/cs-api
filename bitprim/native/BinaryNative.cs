using System;
using System.Runtime.InteropServices;

namespace Knuth.Native
{
    internal static class BinaryNative
    {
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern byte[] core_binary_blocks(IntPtr binary, ref UIntPtr /*size_t*/ out_n);

        //Note: The user is responsible for releasing the resource
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr core_binary_construct();

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr core_binary_construct_string([MarshalAs(UnmanagedType.LPStr)]string hexString);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr core_binary_construct_blocks(UIntPtr /*size_t*/ bits_size, byte[] blocks, UIntPtr /*size_t*/ n);

        [DllImport(Constants.KTH_C_LIBRARY)]
        [return: MarshalAs(UnmanagedType.LPStr)] //TODO Check return value is deallocated correctly
        public static extern string core_binary_encoded(IntPtr binary);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void core_binary_destruct(IntPtr binary); //TODO Implement in C API
    }
}
