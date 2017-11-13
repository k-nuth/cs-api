using System;
using Bitprim.Native;
using System.Collections;

namespace Bitprim
{

public class HistoryCompactList : IDisposable
{

    private IntPtr nativeInstance_;

    ~HistoryCompactList()
    {
        Dispose(false);
    }

    public IEnumerator GetEnumerator()
    {
        return new HistoryCompactListEnumerator(nativeInstance_);
    }

    public uint Count
    {
        get
        {
            return (uint) HistoryCompactListNative.chain_history_compact_list_count(nativeInstance_);
        }
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
        HistoryCompactListNative.chain_history_compact_list_destruct(nativeInstance_);
    }

    internal HistoryCompactList(IntPtr nativeInstance)
    {
        nativeInstance_ = nativeInstance;
    }

    internal IntPtr NativeInstance
    {
        get
        {
            return nativeInstance_;
        }
    }
}

public class HistoryCompactListEnumerator : IEnumerator
{
    private UInt64 counter_;
    private IntPtr nativeCollection_;

    public HistoryCompactListEnumerator(IntPtr nativeCollection)
    {
        nativeCollection_ = nativeCollection;
        counter_ = 0;
    }

    public bool MoveNext()
    {
        counter_++;
        return counter_ != (uint) HistoryCompactListNative.chain_history_compact_list_count(nativeCollection_);
    }

    public object Current
    {
        get
        {
            return HistoryCompactListNative.chain_history_compact_list_nth(nativeCollection_, counter_);
        }
    }

    public void Reset()
    {
        counter_ = 0;
    }

}

}