using System;
using System.Runtime.InteropServices;

namespace BitprimCs.Native
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
    public static extern IntPtr executor_construct_fd([MarshalAs(UnmanagedType.LPStr)]string path, IntPtr sout, IntPtr serr);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void executor_destruct(IntPtr exec);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void executor_stop(IntPtr exec);

    
    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern IntPtr executor_get_chain(IntPtr exec);

    //TODO Add when networking interface is ready
    //[DllImport(Constants.BITPRIM_C_LIBRARY)]
    //public static extern IntPtr executor_get_p2p(IntPtr exec);

}

}