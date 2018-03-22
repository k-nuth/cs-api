using System;
using System.Runtime.InteropServices;

namespace Bitprim.Native
{

public static class ExecutorNative
{
    //Delegates for callbacks
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void RunHandler(IntPtr ctx, int error);

    //Functions
    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int executor_initchain(IntPtr exec);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int executor_run(IntPtr exec, IntPtr ctx, [MarshalAs(UnmanagedType.FunctionPtr)]RunHandler handler);
    
    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int executor_run_wait(IntPtr exec);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern IntPtr executor_construct_fd([MarshalAs(UnmanagedType.LPStr)]string path, int sout, int serr);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern IntPtr executor_construct_handles([MarshalAs(UnmanagedType.LPStr)]string path, IntPtr sout, IntPtr serr);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void executor_destruct(IntPtr exec);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void executor_stop(IntPtr exec);

    //typedef int (*reorganize_handler_t)(chain_t, void*, error_code_t, uint64_t /*size_t*/, block_list_t, block_list_t);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int ReorganizeHandler(IntPtr executor, IntPtr chain, IntPtr context, ErrorCode error, UInt64 u, IntPtr blockList, IntPtr blockList2);

    //typedef int (*transaction_handler_t)(chain_t, void*, error_code_t, transaction_t);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int TransactionHandler(IntPtr executor, IntPtr chain, IntPtr context, ErrorCode error, IntPtr transaction);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_subscribe_blockchain(IntPtr exec, IntPtr chain, IntPtr context, [MarshalAs(UnmanagedType.FunctionPtr)]ReorganizeHandler handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_subscribe_transaction(IntPtr exec, IntPtr chain, IntPtr context, [MarshalAs(UnmanagedType.FunctionPtr)]TransactionHandler handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern IntPtr executor_get_chain(IntPtr exec);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int /*bool*/ executor_stopped(IntPtr exec);

    //TODO Add when networking interface is ready
    //[DllImport(Constants.BITPRIM_C_LIBRARY)]
    //public static extern IntPtr executor_get_p2p(IntPtr exec);

    }

}