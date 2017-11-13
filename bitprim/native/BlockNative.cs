using System;
using System.Runtime.InteropServices;

namespace BitprimCs.Native
{

public static class BlockNative
{

    //Note: The user is responsible for releasing memory
    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern byte[] chain_block_generate_merkle_root(IntPtr block);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern byte[] chain_block_hash(IntPtr block);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int /*bool*/ chain_block_is_distinct_transaction_set(IntPtr block);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int /*bool*/ chain_block_is_extra_coinbases(IntPtr block);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int /*bool*/ chain_block_is_final(IntPtr block, UIntPtr height);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int /*bool*/ chain_block_is_internal_double_spend(IntPtr block);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int chain_block_is_valid(IntPtr block);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int /*bool*/ chain_block_is_valid_merkle_root(IntPtr block);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int /*bool*/ chain_block_is_valid_coinbase_claim(IntPtr block, UIntPtr height);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int /*bool*/ chain_block_is_valid_coinbase_script(IntPtr block, UIntPtr height);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern IntPtr chain_block_header(IntPtr block);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern IntPtr chain_block_transaction_nth(IntPtr block, UIntPtr n);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UInt64 chain_block_claim(IntPtr block);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UInt64 chain_block_fees(IntPtr block);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UInt64 chain_block_reward(IntPtr block, UIntPtr height);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UInt64 chain_block_subsidy(UIntPtr height);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UIntPtr chain_block_serialized_size(IntPtr block, UInt32 version);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UIntPtr chain_block_signature_operations(IntPtr block);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UIntPtr chain_block_signature_operations_bip16_active(IntPtr block, int /*bool*/ bip16_active);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UIntPtr chain_block_total_inputs(IntPtr block, int /*bool*/ with_coinbase /*= true*/);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UIntPtr chain_block_transaction_count(IntPtr block);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_block_destruct(IntPtr block);

}

}