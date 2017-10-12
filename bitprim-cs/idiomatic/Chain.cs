using System;
using System.Runtime.InteropServices;

namespace BitprimCs.Native
{

public class Chain : IDisposable
{
    private IntPtr nativeInstance_;

    ~Chain()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            //Release managed resources and call Dispose for member variables
        }   
        //Release unmanaged resources
        ChainNative.chain_destruct(nativeInstance_);
    }
}

}