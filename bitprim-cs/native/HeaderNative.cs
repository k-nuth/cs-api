using System;
using System.Runtime.InteropServices;

namespace BitprimCs.Native
{

public static class HeaderNative
{

    //TODO Try marshaling as out param instead
    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    [return:MarshalAs( UnmanagedType.Struct)]
    public static extern hash_t chain_header_hash(IntPtr header);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern byte[] chain_header_merkle(IntPtr header);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern byte[] chain_header_previous_block_hash(IntPtr header);

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
    public static extern void chain_header_set_bits(IntPtr header, UInt32 bits);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_header_set_nonce(IntPtr header, UInt32 nonce);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_header_set_timestamp(IntPtr header, UInt32 timestamp);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_header_set_version(IntPtr header, UInt32 version);

}

}