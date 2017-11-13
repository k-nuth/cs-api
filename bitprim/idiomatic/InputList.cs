using System;
using Bitprim.Native;
using System.Collections;

namespace Bitprim
{

public class InputList : IDisposable
{

    private IntPtr nativeInstance_;

    public InputList()
    {
        nativeInstance_ = InputListNative.chain_input_list_construct_default();
    }

    internal InputList(IntPtr nativeInstance)
    {
        nativeInstance_ = nativeInstance;
    }

    ~InputList()
    {
        Dispose(false);
    }

    public IEnumerator GetEnumerator()
    {
        return new InputListEnumerator(nativeInstance_);
    }

    public uint Count
    {
        get
        {
            return (uint) InputListNative.chain_input_list_count(nativeInstance_);
        }
    }

    public void Add(Input input)
    {
        InputListNative.chain_input_list_push_back(nativeInstance_, input.NativeInstance);
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
        InputListNative.chain_input_list_destruct(nativeInstance_);
    }

    internal IntPtr NativeInstance
    {
        get
        {
            return nativeInstance_;
        }
    }
}

public class InputListEnumerator : IEnumerator
{
    private uint counter_;
    private IntPtr nativeCollection_;

    public InputListEnumerator(IntPtr nativeCollection)
    {
        nativeCollection_ = nativeCollection;
        counter_ = 0;
    }

    public bool MoveNext()
    {
        counter_++;
        return counter_ != (uint) InputListNative.chain_input_list_count(nativeCollection_);
    }

    public object Current
    {
        get
        {
            return new Input(InputListNative.chain_input_list_nth(nativeCollection_, (UIntPtr) counter_));
        }
    }

    public void Reset()
    {
        counter_ = 0;
    }
}

}