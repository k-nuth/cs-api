using System;
using System.Runtime.InteropServices;

public static class BlockNative{

    //Note: The user is responsible for the resource release
    [DllImport("bitprim-node-cint")]
    static extern byte[] block_generate_merkle_root(IntPtr block);

    [DllImport("bitprim-node-cint")]
    static extern byte[] block_hash(IntPtr block);

    [DllImport("bitprim-node-cint")]
    static extern int /*bool*/ block_is_distinct_transaction_set(IntPtr block);

    [DllImport("bitprim-node-cint")]
    static extern int /*bool*/ block_is_extra_coinbases(IntPtr block);

    [DllImport("bitprim-node-cint")]
    static extern int /*bool*/ block_is_final(IntPtr block, UIntPtr height);

    [DllImport("bitprim-node-cint")]
    static extern int /*bool*/ block_is_internal_double_spend(IntPtr block);

    [DllImport("bitprim-node-cint")]
    static extern int block_is_valid(IntPtr block);

    [DllImport("bitprim-node-cint")]
    static extern int /*bool*/ block_is_valid_merkle_root(IntPtr block);

    [DllImport("bitprim-node-cint")]
    static extern int /*bool*/ block_is_valid_coinbase_claim(IntPtr block, UIntPtr height);

    [DllImport("bitprim-node-cint")]
    static extern int /*bool*/ block_is_valid_coinbase_script(IntPtr block, UIntPtr height);

    [DllImport("bitprim-node-cint")]
    static extern IntPtr block_header(IntPtr block);

    [DllImport("bitprim-node-cint")]
    static extern IntPtr block_transaction_nth(IntPtr block, UIntPtr n);

    [DllImport("bitprim-node-cint")]
    static extern UInt64 block_claim(IntPtr block);

    [DllImport("bitprim-node-cint")]
    static extern UInt64 block_fees(IntPtr block);

    [DllImport("bitprim-node-cint")]
    static extern UInt64 block_reward(IntPtr block, UIntPtr height);

    [DllImport("bitprim-node-cint")]
    static extern UInt64 block_subsidy(UIntPtr height);

    [DllImport("bitprim-node-cint")]
    static extern UIntPtr block_serialized_size(IntPtr block, UInt32 version);

    [DllImport("bitprim-node-cint")]
    static extern UIntPtr block_signature_operations(IntPtr block);

    [DllImport("bitprim-node-cint")]
    static extern UIntPtr block_signature_operations_bip16_active(IntPtr block, int /*bool*/ bip16_active);

    [DllImport("bitprim-node-cint")]
    static extern UIntPtr block_total_inputs(IntPtr block, int /*bool*/ with_coinbase /*= true*/);


    [DllImport("bitprim-node-cint")]
    static extern UIntPtr block_transaction_count(IntPtr block);

    [DllImport("bitprim-node-cint")]
    static extern void block_destruct(IntPtr block);

}