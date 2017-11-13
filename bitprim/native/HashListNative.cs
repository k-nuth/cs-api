using System;
using System.Runtime.InteropServices;

namespace Bitprim.Native
{

public static class HashListNative
{
    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern IntPtr chain_hash_list_construct_default();

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern byte[] chain_hash_list_nth(IntPtr list, UIntPtr /*size_t*/ n);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UIntPtr /*size_t*/ chain_hash_list_count(IntPtr list);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_hash_list_destruct(IntPtr list);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_hash_list_push_back(IntPtr list, byte[] hash);
}

}