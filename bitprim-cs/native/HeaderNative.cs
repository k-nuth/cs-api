using System;
using System.Runtime.InteropServices;

namespace BitprimCs.Native
{

public static class HeaderNative
{

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int chain_header_is_valid(IntPtr header);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UInt32 chain_header_bits(IntPtr header);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UInt32 chain_header_nonce(IntPtr header);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UInt32 chain_header_timestamp(IntPtr header);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UInt32 chain_header_version(IntPtr header);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_header_destruct(IntPtr header);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_header_hash_out(IntPtr header, ref hash_t out_hash);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_header_merkle_out(IntPtr header, ref hash_t out_merkle);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_header_previous_block_hash(IntPtr header, ref hash_t out_previous_block_hash);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_header_set_bits(IntPtr header, UInt32 bits);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_header_set_nonce(IntPtr header, UInt32 nonce);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_header_set_timestamp(IntPtr header, UInt32 timestamp);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_header_set_version(IntPtr header, UInt32 version);

}

}