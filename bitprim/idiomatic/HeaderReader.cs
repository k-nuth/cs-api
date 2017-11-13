using System;
using Bitprim.Native;

namespace Bitprim
{

public class HeaderReader : IDisposable
{
    private IntPtr nativeInstance_;

    public HeaderReader()
    {
        nativeInstance_ = GetHeadersNative.chain_get_headers_construct_default();
    }

    public HeaderReader(HashList start, byte[] stop)
    {
        nativeInstance_ = GetHeadersNative.chain_get_headers_construct(start.NativeInstance, stop);
    }

    ~HeaderReader()
    {
        Dispose(false);
    }

    public bool IsValid
    {
        get
        {
            return GetHeadersNative.chain_get_headers_is_valid(nativeInstance_) != 0;
        }
    }

    public byte[] StopHash
    {
        get
        {
            return GetHeadersNative.chain_get_headers_stop_hash(nativeInstance_);
        }
        set
        {
            GetHeadersNative.chain_get_headers_set_stop_hash(nativeInstance_, value);
        }
    }

    public HashList StartHashes
    {
        get
        {
            return new HashList(GetHeadersNative.chain_get_headers_start_hashes(nativeInstance_));
        }
        set
        {
            GetHeadersNative.chain_get_headers_set_start_hashes(nativeInstance_, value.NativeInstance);
        }
    }

    public UInt64 GetSerializedSize(UInt32 version)
    {
        return GetHeadersNative.chain_get_headers_serialized_size(nativeInstance_, version);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Reset()
    {
        GetHeadersNative.chain_get_headers_reset(nativeInstance_);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            //Release managed resources and call Dispose for member variables
        }   
        //Release unmanaged resources
        GetHeadersNative.chain_get_headers_destruct(nativeInstance_);
    }

    internal HeaderReader(IntPtr nativeInstance)
    {
        nativeInstance_ = nativeInstance;
    }
}

}