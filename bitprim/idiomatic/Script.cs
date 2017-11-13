using System;
using Bitprim.Native;

namespace Bitprim
{

public class Script : IDisposable
{
    private IntPtr nativeInstance_;

   ~Script()
    {
        Dispose(false);
    }

    internal Script(IntPtr nativeInstance)
    {
        nativeInstance_ = nativeInstance;
    }

    public bool IsValid
    {
        get
        {
            return ScriptNative.chain_script_is_valid(nativeInstance_) != 0;
        }
    }

    public bool OperationsAreValid
    {
        get
        {
            return ScriptNative.chain_script_is_valid_operations(nativeInstance_) != 0;
        }
    }

    public string ToString(UInt32 activeForks)
    {
        return ScriptNative.chain_script_to_string(nativeInstance_, activeForks);
    }

    public UIntPtr SatoshiContentSize
    {
        get
        {
            return ScriptNative.chain_script_satoshi_content_size(nativeInstance_);
        }
    }

    public UIntPtr GetEmbeddedSigOps(Script prevOutScript)
    {
        return ScriptNative.chain_script_embedded_sigops(nativeInstance_, prevOutScript.nativeInstance_);
    }

    public UIntPtr GetSigOps(bool embedded)
    {
        return ScriptNative.chain_script_sigops(nativeInstance_, embedded? 1:0);
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
        ScriptNative.chain_script_destruct(nativeInstance_);
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