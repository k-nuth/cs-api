using System;
using System.Runtime.InteropServices;

namespace Bitprim.Native
{

public static class TransactionNative
{

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_transaction_hash_out(IntPtr transaction, ref hash_t hash);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_transaction_hash_sighash_type_out(IntPtr transaction, UInt32 sighash_type, ref hash_t hash);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int /*bool*/ chain_transaction_is_coinbase(IntPtr transaction);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int /*bool*/ chain_transaction_is_double_spend(IntPtr transaction, int /*bool*/ include_unconfirmed);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int /*bool*/ chain_transaction_is_final(IntPtr transaction, UInt64 block_height, UInt32 block_time);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int /*bool*/ chain_transaction_is_immature(IntPtr transaction, UInt64 target_height);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int /*bool*/ chain_transaction_is_locktime_conflict(IntPtr transaction);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int /*bool*/ chain_transaction_is_missing_previous_outputs(IntPtr transaction);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int /*bool*/ chain_transaction_is_null_non_coinbase(IntPtr transaction);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int /*bool*/ chain_transaction_is_oversized_coinbase(IntPtr transaction);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int /*bool*/ chain_transaction_is_overspent(IntPtr transaction);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int /*bool*/ chain_transaction_is_valid(IntPtr transaction);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern IntPtr chain_transaction_construct(UInt32 version, UInt32 locktime, IntPtr inputs, IntPtr outputs);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern IntPtr chain_transaction_construct_default();

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern IntPtr chain_transaction_inputs(IntPtr transaction);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern IntPtr chain_transaction_outputs(IntPtr transaction);

    //Note: Returned memory must be freed manually using platform_free
    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern IntPtr chain_transaction_to_data(IntPtr transaction, int /*bool*/ wire, ref int size);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UInt32 chain_transaction_locktime(IntPtr transaction);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UInt32 chain_transaction_version(IntPtr transaction);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UInt64 chain_transaction_fees(IntPtr transaction);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UInt64 chain_transaction_total_input_value(IntPtr transaction);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UInt64 chain_transaction_total_output_value(IntPtr transaction);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UInt64 chain_transaction_serialized_size(IntPtr transaction, int wire /*= true*/);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UInt64 chain_transaction_signature_operations(IntPtr transaction);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UInt64 chain_transaction_signature_operations_bip16_active(IntPtr transaction, int /*bool*/ bip16_active);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_transaction_destruct(IntPtr transaction);    

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_transaction_set_version(IntPtr transaction, UInt32 version);

}

}