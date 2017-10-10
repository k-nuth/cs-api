using System;
using System.Runtime.InteropServices;

namespace BitprimCs.Native
{

public static class TransactionNative
{

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern byte[] transaction_hash(IntPtr transaction);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern byte[] transaction_hash_sighash_type(IntPtr transaction, UInt32 sighash_type);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int /*bool*/ transaction_is_coinbase(IntPtr transaction);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int /*bool*/ transaction_is_double_spend(IntPtr transaction, int /*bool*/ include_unconfirmed);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int /*bool*/ transaction_is_final(IntPtr transaction, UIntPtr block_height, UInt32 block_time);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int /*bool*/ transaction_is_immature(IntPtr transaction, UIntPtr target_height);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int /*bool*/ transaction_is_locktime_conflict(IntPtr transaction);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int /*bool*/ transaction_is_missing_previous_outputs(IntPtr transaction);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int /*bool*/ transaction_is_null_non_coinbase(IntPtr transaction);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int /*bool*/ transaction_is_oversized_coinbase(IntPtr transaction);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int /*bool*/ transaction_is_overspent(IntPtr transaction);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int /*bool*/ transaction_is_valid(IntPtr transaction);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern IntPtr transaction_input_nth(IntPtr transaction, UIntPtr n);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern IntPtr transaction_output_nth(IntPtr transaction, UIntPtr n);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UInt32 transaction_locktime(IntPtr transaction);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UInt32 transaction_version(IntPtr transaction);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UInt64 transaction_fees(IntPtr transaction);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UIntPtr transaction_input_count(IntPtr transaction);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UIntPtr transaction_output_count(IntPtr transaction);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UInt64 transaction_total_input_value(IntPtr transaction);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UInt64 transaction_total_output_value(IntPtr transaction);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UIntPtr transaction_serialized_size(IntPtr transaction, int wire /*= true*/);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UIntPtr transaction_signature_operations(IntPtr transaction);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UIntPtr transaction_signature_operations_bip16_active(IntPtr transaction, int /*bool*/ bip16_active);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void transaction_destruct(IntPtr transaction);    

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void transaction_set_version(IntPtr transaction, UInt32 version);

}

}