using System;
using System.Runtime.InteropServices;

namespace BitprimCs.Native
{

public static class BlockIndexesNative
{

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern IntPtr chain_block_indexes_construct_default();

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_block_indexes_push_back(IntPtr list, UIntPtr /*size_t*/ index);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_block_indexes_destruct(IntPtr list);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UIntPtr /*size_t*/ chain_block_indexes_count(IntPtr list);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UIntPtr /*size_t*/ chain_block_indexes_nth(IntPtr list, UIntPtr /*size_t*/ n);

}

}