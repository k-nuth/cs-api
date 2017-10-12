using System;
using System.Runtime.InteropServices;

namespace BitprimCs.Native
{

public static class MerkleBlockNative
{

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern byte[] chain_merkle_block_hash_nth(IntPtr block, UIntPtr /*size_t*/ n);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int /*bool*/ chain_merkle_block_is_valid(IntPtr block);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern IntPtr chain_merkle_block_header(IntPtr block);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UIntPtr /*size_t*/ chain_merkle_block_hash_count(IntPtr block);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UIntPtr /*size_t*/ chain_merkle_block_serialized_size(IntPtr block, UInt32 version);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UIntPtr /*size_t*/ chain_merkle_block_total_transaction_count(IntPtr block);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_merkle_block_destruct(IntPtr block);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_merkle_block_reset(IntPtr block);

}

}