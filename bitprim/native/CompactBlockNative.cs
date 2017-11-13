using System;
using System.Runtime.InteropServices;

namespace Bitprim.Native
{

public static class CompactBlockNative
{
    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern Header compact_block_header(IntPtr block);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int /*bool*/ compact_block_is_valid(IntPtr block);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern IntPtr compact_block_transaction_nth(IntPtr block, UInt64 /*size_t*/ n);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UInt64 compact_block_nonce(IntPtr block);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UInt64 /*size_t*/ compact_block_serialized_size(IntPtr block, UInt32 version);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UInt64 /*size_t*/ compact_block_transaction_count(IntPtr block);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void compact_block_destruct(IntPtr block);
    
    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void compact_block_reset(IntPtr block);
}

}