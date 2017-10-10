using System;
using BitprimCs.Native;

namespace BitprimCs
{

public class Input : IDisposable
{
    private IntPtr nativeInstance_;

    public Input()
    {
        nativeInstance_ = InputNative.chain_input_construct_default();
    }

    public Input(Output previousOutput, Script script, UInt32 sequence)
    {
        nativeInstance_ = InputNative.chain_input_construct(previousOutput.NativeInstance, script.NativeInstance, sequence);
    }

    internal Input(IntPtr nativeInstance)
    {
        nativeInstance_ = nativeInstance;
    }

    ~Input()
    {
        Dispose(false);
    }

    public bool IsFinal
    {
        get
        {
            return InputNative.chain_input_is_final(nativeInstance_) != 0;
        }
    }

    public bool IsValid
    {
        get
        {
            return InputNative.chain_input_is_valid(nativeInstance_) != 0;
        }
    }

    public Output PreviousOutput
    {
        get
        {
            return new Output(InputNative.chain_input_previous_output(nativeInstance_));
        }
    }

    public Script Script
    {
        get
        {
            return new Script(InputNative.chain_input_script(nativeInstance_));
        }
    }

    public UInt32 Sequence
    {
        get
        {
            return InputNative.chain_input_sequence(nativeInstance_);
        }
    }

    public UIntPtr GetSerializedSize(bool wire)
    {
        return InputNative.chain_input_serialized_size(nativeInstance_, wire? 1:0);
    }

    public UIntPtr GetSignatureOperationsCount(bool bip16Active)
    {
        return InputNative.chain_input_signature_operations(nativeInstance_, bip16Active? 1:0);
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
        InputNative.chain_input_destruct(nativeInstance_);
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