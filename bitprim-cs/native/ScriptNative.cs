using System;
using System.Runtime.InteropServices;

namespace BitprimCs.Native
{
    
public static class ScriptNative
{
    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int /*bool*/ chain_script_is_valid(IntPtr script);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int /*bool*/ chain_script_is_valid_operations(IntPtr script);

    //Note: user of the function has to release the resource (memory) manually
    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    [return: MarshalAs(UnmanagedType.LPStr)] //TODO Check return value is deallocated correctly
    public static extern string chain_script_to_string(IntPtr script, UInt32 active_forks);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UIntPtr /*size_t*/ chain_script_embedded_sigops(IntPtr script, IntPtr prevout_script);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UIntPtr /*size_t*/ chain_script_satoshi_content_size(IntPtr script);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UIntPtr /*size_t*/ chain_script_serialized_size(IntPtr script, int /*bool*/ prefix);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern UIntPtr /*size_t*/ chain_script_sigops(IntPtr script, int /*bool*/ embedded);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_script_destruct(IntPtr script);

}

}