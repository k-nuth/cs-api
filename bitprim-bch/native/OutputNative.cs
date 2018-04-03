using System;
using System.Runtime.InteropServices;

namespace Bitprim.Native
{
    
public static class OutputNative
{

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int chain_output_is_valid(IntPtr output);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern IntPtr chain_output_construct(UInt64 value, IntPtr script);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern IntPtr chain_output_construct_default();

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern IntPtr chain_output_script(IntPtr output);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UInt64 chain_output_value(IntPtr output);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern IntPtr chain_output_payment_address(IntPtr output, int /*bool*/ use_testnet_rules);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UIntPtr /*size_t*/ chain_output_serialized_size(IntPtr output, int /*bool*/ wire /*= true*/);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UIntPtr /*size_t*/ chain_output_signature_operations(IntPtr output);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_output_destruct(IntPtr output);

}

}