using System;
using BitprimCs.Native;

namespace BitprimCs
{

public class Point
{
    private IntPtr nativeInstance_;

    internal Point(IntPtr nativeInstance)
    {
        nativeInstance_ = nativeInstance;
    }

    public bool IsValid
    {
        get
        {
            return PointNative.chain_point_is_valid(nativeInstance_) != 0;
        }
    }

    public byte[] Hash
    {
        get
        {
            return PointNative.chain_point_get_hash(nativeInstance_);
        }
    }

    public UInt32 Index
    {
        get
        {
            return PointNative.chain_point_get_index(nativeInstance_);
        }
    }

    public UInt64 Checksum
    {
        get
        {
            return PointNative.chain_point_get_checksum(nativeInstance_);
        }
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