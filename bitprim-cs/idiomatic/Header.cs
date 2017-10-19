using System;
using System.Runtime.InteropServices;
using BitprimCs.Native;

namespace BitprimCs
{

public class Header : IDisposable
{
    private IntPtr nativeInstance_;

    ~Header()
    {
        Dispose(false);
    }

    public bool IsValid
    {
        get
        {
            return HeaderNative.header_is_valid(nativeInstance_) != 0; 
        }
    }

    public byte[] Hash
    {
        get
        {
            return HeaderNative.header_hash(nativeInstance_);
        }
    }

    public byte[] Merkle
    {
        get
        {
            return HeaderNative.header_merkle(nativeInstance_);
        }
    }

    public byte[] PreviousBlockHash
    {
        get
        {
            return HeaderNative.header_previous_block_hash(nativeInstance_);
        }
    }

    public UInt32 Bits
    {
        get
        {
            return HeaderNative.header_bits(nativeInstance_);
        }
        set
        {
            HeaderNative.header_set_bits(nativeInstance_, value);
        }
    }

    public UInt32 Nonce
    {
        get
        {
            return HeaderNative.header_nonce(nativeInstance_);
        }
        set
        {
            HeaderNative.header_set_nonce(nativeInstance_, value);
        }
    }

    public UInt32 Timestamp
    {
        get
        {
            return HeaderNative.header_timestamp(nativeInstance_);
        }
        set
        {
            HeaderNative.header_set_timestamp(nativeInstance_, value);
        }
    }

    public UInt32 Version
    {
        get
        {
            return HeaderNative.header_version(nativeInstance_);
        }
        set
        {
            HeaderNative.header_set_version(nativeInstance_, value);
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
        HeaderNative.header_destruct(nativeInstance_);
    }

    internal Header(IntPtr nativeInstance)
    {
        nativeInstance_ = nativeInstance;
    }
}

}