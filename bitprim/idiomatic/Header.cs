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
            return HeaderNative.chain_header_is_valid(nativeInstance_) != 0; 
        }
    }

    public byte[] Hash
    {
        get
        {
            var managedHash = new hash_t();
            HeaderNative.chain_header_hash_out(nativeInstance_, ref managedHash);
            return managedHash.hash;
        }
    }

    public byte[] Merkle
    {
        get
        {
            var managedHash = new hash_t();
            HeaderNative.chain_header_merkle_out(nativeInstance_, ref managedHash);
            return managedHash.hash;
        }
    }

    public byte[] PreviousBlockHash
    {
        get
        {
            var managedHash = new hash_t();
            HeaderNative.chain_header_previous_block_hash_out(nativeInstance_, ref managedHash);
            return managedHash.hash;
        }
    }

    public UInt32 Bits
    {
        get
        {
            return HeaderNative.chain_header_bits(nativeInstance_);
        }
        set
        {
            HeaderNative.chain_header_set_bits(nativeInstance_, value);
        }
    }

    public UInt32 Nonce
    {
        get
        {
            return HeaderNative.chain_header_nonce(nativeInstance_);
        }
        set
        {
            HeaderNative.chain_header_set_nonce(nativeInstance_, value);
        }
    }

    public UInt32 Timestamp
    {
        get
        {
            return HeaderNative.chain_header_timestamp(nativeInstance_);
        }
        set
        {
            HeaderNative.chain_header_set_timestamp(nativeInstance_, value);
        }
    }

    public UInt32 Version
    {
        get
        {
            return HeaderNative.chain_header_version(nativeInstance_);
        }
        set
        {
            HeaderNative.chain_header_set_version(nativeInstance_, value);
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
        HeaderNative.chain_header_destruct(nativeInstance_);
    }

    internal Header(IntPtr nativeInstance)
    {
        nativeInstance_ = nativeInstance;
    }
}

}