using System;
using System.Runtime.InteropServices;

public static class ExecutorNative{

    //Delegates for callbacks
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void BlockFetchHandler(int error, IntPtr header, UIntPtr height);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void BlockHeaderFetchHandler(int error, IntPtr header, UIntPtr height);


    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void BlockHeightFetchHandler(int error, UIntPtr height);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void LastHeightFetchHandler(int error, UIntPtr height);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void OutputFetchHandler(int error, IntPtr output);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TransactionFetchHandler(int error, IntPtr transaction, UIntPtr height, UIntPtr index);

    //Functions
    [DllImport("bitprim-node-cint")]
    static extern int executor_initchain(IntPtr exec);

    [DllImport("bitprim-node-cint")]
    static extern int executor_run(IntPtr exec);

    [DllImport("bitprim-node-cint")]
    static extern int executor_run_wait(IntPtr exec);

    [DllImport("bitprim-node-cint")]
    static extern int get_block(IntPtr exec, UIntPtr height, ref IntPtr block, ref UIntPtr ret_height);

    [DllImport("bitprim-node-cint")]
    static extern int get_block_by_hash(IntPtr exec, byte[] hash, ref IntPtr block, ref UIntPtr ret_height);

    [DllImport("bitprim-node-cint")]
    static extern int get_block_header(IntPtr exec, UIntPtr height, IntPtr header, ref UIntPtr ret_height);

    [DllImport("bitprim-node-cint")]
    static extern int get_block_header_by_hash(IntPtr exec, byte[] hash, IntPtr header, ref UIntPtr ret_height);

    [DllImport("bitprim-node-cint")]
    static extern int get_block_height(IntPtr exec, byte[] hash, ref UIntPtr height);

    [DllImport("bitprim-node-cint")]
    static extern int get_last_height(IntPtr exec, ref UIntPtr height);

    [DllImport("bitprim-node-cint")]
    static extern int get_output(IntPtr exec, byte[] hash, UInt32 index, int require_confirmed, ref IntPtr output);

    [DllImport("bitprim-node-cint")]
    static extern int get_transaction(IntPtr exec, byte[] hash, int require_confirmed, ref IntPtr transaction, ref UIntPtr ret_height, ref UIntPtr ret_index);

    [DllImport("bitprim-node-cint")]
    static extern IntPtr executor_construct_fd([MarshalAs(UnmanagedType.LPStr)]string path, IntPtr sin, IntPtr sout, IntPtr serr);

    [DllImport("bitprim-node-cint")]
    static extern void executor_destruct(IntPtr exec);

    [DllImport("bitprim-node-cint")]
    static extern void executor_stop(IntPtr exec);

    [DllImport("bitprim-node-cint")]
    static extern void fetch_block(IntPtr exec, UIntPtr height, [MarshalAs(UnmanagedType.FunctionPtr)]BlockFetchHandler handler);

    [DllImport("bitprim-node-cint")]
    static extern void fetch_block_by_hash(IntPtr exec, byte[] hash, [MarshalAs(UnmanagedType.FunctionPtr)]BlockFetchHandler handler);

    [DllImport("bitprim-node-cint")]
    static extern void fetch_block_header(IntPtr exec, UIntPtr height, [MarshalAs(UnmanagedType.FunctionPtr)]BlockHeaderFetchHandler handler);

    [DllImport("bitprim-node-cint")]
    static extern void fetch_block_header_by_hash(IntPtr exec, byte[] hash, [MarshalAs(UnmanagedType.FunctionPtr)]BlockHeaderFetchHandler handler);

    [DllImport("bitprim-node-cint")]
    static extern void fetch_block_height(IntPtr exec, byte[] hash, [MarshalAs(UnmanagedType.FunctionPtr)]BlockHeightFetchHandler handler);

    [DllImport("bitprim-node-cint")]
    static extern void fetch_last_height(IntPtr exec, [MarshalAs(UnmanagedType.FunctionPtr)]LastHeightFetchHandler handler);

    [DllImport("bitprim-node-cint")]
    static extern void fetch_output(IntPtr exec, byte[] hash, UInt32 index, int require_confirmed, [MarshalAs(UnmanagedType.FunctionPtr)]OutputFetchHandler handler);


    [DllImport("bitprim-node-cint")]
    static extern void fetch_transaction(IntPtr exec, byte[] hash, int require_confirmed, [MarshalAs(UnmanagedType.FunctionPtr)]TransactionFetchHandler handler);

}