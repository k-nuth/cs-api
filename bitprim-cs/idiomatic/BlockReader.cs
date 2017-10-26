using System;
using BitprimCs.Native;

namespace BitprimCs
{

public class BlockReader : IDisposable
{
    private IntPtr nativeInstance_;

    public BlockReader()
    {
        nativeInstance_ = GetBlocksNative.chain_get_blocks_construct_default();
    }

    public BlockReader(HashList start, byte[] stop)
    {
        nativeInstance_ = GetBlocksNative.chain_get_blocks_construct(start.NativeInstance, stop);
    }

    ~BlockReader()
    {
        Dispose(false);
    }

    public bool IsValid
    {
        get
        {
            return GetBlocksNative.chain_get_blocks_is_valid(nativeInstance_) != 0;
        }
    }

    public byte[] StopHash
    {
        get
        {
            return GetBlocksNative.chain_get_blocks_stop_hash(nativeInstance_);
        }
        set
        {
            GetBlocksNative.chain_get_blocks_set_stop_hash(nativeInstance_, value);
        }
    }

    public HashList StartHashes
    {
        get
        {
            return new HashList(GetBlocksNative.chain_get_blocks_start_hashes(nativeInstance_));
        }
        set
        {
            GetBlocksNative.chain_get_blocks_set_start_hashes(nativeInstance_, value.NativeInstance);
        }
    }

    public UInt64 GetSerializedSize(UInt32 version)
    {
        return GetBlocksNative.chain_get_blocks_serialized_size(nativeInstance_, version);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Reset()
    {
        GetBlocksNative.chain_get_blocks_reset(nativeInstance_);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            //Release managed resources and call Dispose for member variables
        }   
        //Release unmanaged resources
        GetBlocksNative.chain_get_blocks_destruct(nativeInstance_);
    }
}

}