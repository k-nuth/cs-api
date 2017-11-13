using System;
using System.Runtime.InteropServices;

namespace Bitprim.Native
{

public class HistoryCompact : IDisposable
{
    private IntPtr nativeInstance_;

   ~HistoryCompact()
    {
        Dispose(false);
    }

    public Point Point
    {
        get
        {
            return new Point(HistoryCompactNative.chain_history_compact_get_point(nativeInstance_));
        }
    }

    public PointKind PointKind
    {
        get
        {
            return (PointKind) Enum.ToObject
            (
                typeof(PointKind),
                HistoryCompactNative.chain_history_compact_get_point_kind(nativeInstance_)
            );
        }
    }

    public UInt32 Height
    {
        get
        {
            return HistoryCompactNative.chain_history_compact_get_height(nativeInstance_);
        }
    }

    public UInt64 ValueOrChecksum
    {
        get
        {
            return HistoryCompactNative.chain_history_compact_get_value_or_previous_checksum(nativeInstance_);
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
        HistoryCompactNative.chain_history_compact_destruct(nativeInstance_);
    }

    internal HistoryCompact(IntPtr nativeInstance)
    {
        nativeInstance_ = nativeInstance;
    }
}

}