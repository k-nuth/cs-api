using System;
using System.Runtime.InteropServices;

namespace BitprimCs.Native
{

public static class HeaderNative
{

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern byte[] header_hash(IntPtr header);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern byte[] header_merkle(IntPtr header);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern byte[] header_previous_block_hash(IntPtr header);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int header_is_valid(IntPtr header);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UInt32 header_bits(IntPtr header);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UInt32 header_nonce(IntPtr header);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UInt32 header_timestamp(IntPtr header);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UInt32 header_version(IntPtr header);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void header_destruct(IntPtr header);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void header_set_bits(IntPtr header, UInt32 bits);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void header_set_nonce(IntPtr header, UInt32 nonce);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void header_set_timestamp(IntPtr header, UInt32 timestamp);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void header_set_version(IntPtr header, UInt32 version);

}

}