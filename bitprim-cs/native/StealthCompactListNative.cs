using System;
using System.Runtime.InteropServices;

namespace BitprimCs.Native
{

public static class StealthCompactListNative
{

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern IntPtr stealth_compact_list_nth(IntPtr list, UInt64 /*size_t*/ n);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UInt64 /*size_t*/ stealth_compact_list_count(IntPtr list);


    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void stealth_compact_list_destruct(IntPtr list);
}

}