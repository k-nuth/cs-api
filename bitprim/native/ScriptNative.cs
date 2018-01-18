using System;
using System.Runtime.InteropServices;

namespace Bitprim.Native
{
    
public static class ScriptNative
{
    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int /*bool*/ chain_script_is_valid(IntPtr script);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int /*bool*/ chain_script_is_valid_operations(IntPtr script);

    //Note: Returned memory must be freed manually using platform_free
    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern IntPtr chain_script_to_data(IntPtr script, int /*bool*/ prefix, ref int size);

    //Note: Returned memory must be freed manually using platform_free
    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern IntPtr chain_script_to_string(IntPtr script, UInt32 active_forks);

    //Note: Returned memory must be freed manually using platform_free
    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern IntPtr chain_script_type(IntPtr script);


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