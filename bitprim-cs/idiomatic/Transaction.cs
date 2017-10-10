using System;
using BitprimCs.Native;

namespace BitprimCs{

public class Transaction : IDisposable
{
    private IntPtr nativeInstance_;

    public Transaction()
    {
        nativeInstance_ = TransactionNative.chain_transaction_construct_default();
    }

    public Transaction(UInt32 version, UInt32 locktime, InputList inputs, OutputList outputs)
    {
        nativeInstance_ = TransactionNative.chain_transaction_construct
        (
            version, locktime, inputs.NativeInstance, outputs.NativeInstance
        );
    }

    internal Transaction(IntPtr nativeInstance)
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
        TransactionNative.chain_transaction_destruct(nativeInstance_);
    }

    internal IntPtr NativeInstance
    {
        get
        {
            return nativeInstance_;
        }
    }

}

}