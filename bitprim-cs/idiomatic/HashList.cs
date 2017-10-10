using System;
using BitprimCs.Native;
using System.Collections;

namespace BitprimCs
{

public class HashList : IDisposable
{

    private IntPtr nativeInstance_;

    public HashList()
    {
        nativeInstance_ = HashListNative.chain_hash_list_construct_default();
    }

    ~HashList()
    {
        Dispose(false);
    }

    public IEnumerator GetEnumerator()
    {
        return new HashListEnumerator(nativeInstance_);
    }

    public uint Count
    {
        get
        {
            return (uint) HashListNative.chain_hash_list_count(nativeInstance_);
        }
    }

    public void Add(byte[] hash)
    {
        HashListNative.chain_hash_list_push_back(nativeInstance_, hash);
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
        HashListNative.chain_hash_list_destruct(nativeInstance_);
    }

    internal IntPtr NativeInstance
    {
        get
        {
            return nativeInstance_;
        }
    }
}

public class HashListEnumerator : IEnumerator
{
    private uint counter_;
    private IntPtr nativeCollection_;

    public HashListEnumerator(IntPtr nativeCollection)
    {
        nativeCollection_ = nativeCollection;
        counter_ = 0;
    }

    public bool MoveNext()
    {
        counter_++;
        return counter_ != (uint) HashListNative.chain_hash_list_count(nativeCollection_);
    }

    public object Current
    {
        get
        {
            return HashListNative.chain_hash_list_nth(nativeCollection_, (UIntPtr) counter_);
        }
    }

    public void Reset()
    {
        counter_ = 0;
    }
}

}