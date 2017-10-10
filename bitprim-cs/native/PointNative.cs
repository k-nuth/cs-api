using System;
using System.Runtime.InteropServices;

namespace BitprimCs.Native
{
    
public static class PointNative
{

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern byte[] chain_point_get_hash(IntPtr point);

   [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int /*bool*/ chain_point_is_valid(IntPtr point);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UInt32 chain_point_get_index(IntPtr point);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UInt64 chain_point_get_checksum(IntPtr point);

}

}