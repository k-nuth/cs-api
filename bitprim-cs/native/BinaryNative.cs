using System;
using System.Runtime.InteropServices;

namespace BitprimCs.Native
{

public static class BinaryNative
{

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern byte[] binary_blocks(IntPtr binary, ref UIntPtr /*size_t*/ out_n);

    //Note: The user is responsible for releasing the resource
    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern IntPtr binary_construct();

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern IntPtr binary_construct_string([MarshalAs(UnmanagedType.LPStr)]string hexString);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern IntPtr binary_construct_blocks(UIntPtr /*size_t*/ bits_size, byte[] blocks, UIntPtr /*size_t*/ n);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    [return: MarshalAs(UnmanagedType.LPStr)] //TODO Check return value is deallocated correctly
    public static extern string binary_encoded(IntPtr binary);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void binary_destruct(IntPtr binary ); //TODO Implement in C API
}

}