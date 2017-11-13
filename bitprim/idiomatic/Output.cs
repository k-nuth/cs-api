using System;
using Bitprim.Native;

namespace Bitprim
{

public class Output : IDisposable
{
    private IntPtr nativeInstance_;

    public Output()
    {
        nativeInstance_ = OutputNative.chain_output_construct_default();
    }

    public Output(UInt64 value, Script script)
    {
        nativeInstance_ = OutputNative.chain_output_construct(value, script.NativeInstance);
    }

    internal Output(IntPtr nativeInstance)
    {
        nativeInstance_ = nativeInstance;
    }

    ~Output()
    {
        Dispose(false);
    }

    public bool IsValid
    {
        get
        {
            return OutputNative.chain_output_is_valid(nativeInstance_) != 0;
        }
    }

    public byte[] Hash
    {
        get
        {
            return OutputNative.chain_output_get_hash(nativeInstance_);
        }
    }

    public Script Script
    {
        get
        {
            return new Script(OutputNative.chain_output_script(nativeInstance_));
        }
    }

    public UInt64 Value
    {
        get
        {
            return OutputNative.chain_output_value(nativeInstance_);
        }
    }

    public UIntPtr SignatureOperationCount
    {
        get
        {
            return OutputNative.chain_output_signature_operations(nativeInstance_);
        }
    }

    public UIntPtr GetSerializedSize(bool wire)
    {
        return OutputNative.chain_output_serialized_size(nativeInstance_, wire? 1:0);
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
        OutputNative.chain_output_destruct(nativeInstance_);
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