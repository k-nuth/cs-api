using System;
using BitprimCs.Native;

namespace BitprimCs{

public class Transaction : IDisposable
{
    private IntPtr nativeInstance_;

    public Transaction(IntPtr nativeInstance)
    {
        nativeInstance_ = nativeInstance;
    }

    ~Transaction()
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
        if (disposing){
            //Release managed resources and call Dispose for member variables
        }   
        //Release unmanaged resources
        TransactionNative.transaction_destruct(nativeInstance_);
    }

}

}