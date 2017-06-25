using System;
using System.Runtime.InteropServices;

public class Executor{

    private IntPtr executor_;

    [DllImport("bitprim-node-cint")]
    static extern IntPtr executor_construct_fd([MarshalAs(UnmanagedType.LPStr)]string path, IntPtr sin, IntPtr sout, IntPtr serr);

    public Executor(string path){
        executor_ = executor_construct_fd(path, new IntPtr(0), new IntPtr(0), new IntPtr(0));
    }


}