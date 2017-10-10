using System;
using BitprimCs.Native;
using System.Collections;

namespace BitprimCs
{

public class PointList : IDisposable
{

    private IntPtr nativeInstance_;

    public PointList()
    {
        nativeInstance_ = PointListNative.chain_point_list_construct_default();
    }

    ~PointList()
    {
        Dispose(false);
    }

    public IEnumerator GetEnumerator()
    {
        return new PointListEnumerator(nativeInstance_);
    }

    public uint Count
    {
        get
        {
            return (uint) PointListNative.chain_point_list_count(nativeInstance_);
        }
    }

    public void Add(Point point)
    {
        PointListNative.chain_point_list_push_back(nativeInstance_, point.NativeInstance);
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
        PointListNative.chain_point_list_destruct(nativeInstance_);
    }

    internal IntPtr NativeInstance
    {
        get
        {
            return nativeInstance_;
        }
    }
}

public class PointListEnumerator : IEnumerator
{
    private uint counter_;
    private IntPtr nativeCollection_;

    public PointListEnumerator(IntPtr nativeCollection)
    {
        nativeCollection_ = nativeCollection;
        counter_ = 0;
    }

    public bool MoveNext()
    {
        counter_++;
        return counter_ != (uint) PointListNative.chain_point_list_count(nativeCollection_);
    }

    public object Current
    {
        get
        {
            return new Point(PointListNative.chain_point_list_nth(nativeCollection_, (UIntPtr) counter_));
        }
    }

    public void Reset()
    {
        counter_ = 0;
    }
}

}