using System;
using System.Runtime.InteropServices;

namespace Bitprim.Tests
{

public class ExecutorFixture : IDisposable
{
    private Executor exec_;

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern IntPtr GetStdHandle(int nStdHandle);

    public ExecutorFixture()
    {
        IntPtr stdOutHandle = GetStdHandle(-11);
        IntPtr stdErrHandle = GetStdHandle(-12);
        exec_ = new Executor("", stdOutHandle, stdErrHandle);
        int result = exec_.InitChain();
        /*if(result != 0)
        {
            throw new InvalidOperationException("InitChain error: " + result);
        }*/
        result = exec_.RunWait();
    }

    public Executor Executor
    {
        get
        {
            return exec_;
        }
    }

    public void Dispose()
    {
        //exec_.Stop();
        GC.Collect();
        GC.WaitForPendingFinalizers();
    }
}

}