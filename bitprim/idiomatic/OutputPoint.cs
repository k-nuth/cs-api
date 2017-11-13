using System;
using BitprimCs.Native;

namespace BitprimCs
{

public class OutputPoint : IDisposable
{
    private IntPtr nativeInstance_;

    public OutputPoint()
    {
        nativeInstance_ = OutputPointNative.output_point_construct();
    }

    public OutputPoint(byte[] pointHash, UInt32 index)
    {
        var managedHash = new hash_t
        {
            hash = pointHash
        };
        nativeInstance_ = OutputPointNative.output_point_construct_from_hash_index(managedHash, index);
    }

    ~OutputPoint()
    {
        Dispose(false);
    }

    public byte[] Hash
    {
        get
        {
            var managedHash = new hash_t();
            OutputPointNative.output_point_get_hash_out(nativeInstance_, ref managedHash);
            return managedHash.hash;
        }
    }

    public UInt32 Index
    {
        get
        {
            return OutputPointNative.output_point_get_index(nativeInstance_);
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
        OutputPointNative.output_point_destruct(nativeInstance_);
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