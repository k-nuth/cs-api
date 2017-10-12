using System;
using System.Runtime.InteropServices;

namespace BitprimCs.Native
{

public class MerkleBlock : IDisposable
{
    private IntPtr nativeInstance_;

    ~MerkleBlock()
    {
        Dispose(false);
    }

    public bool IsValid
    {
        get
        {
            return MerkleBlockNative.chain_merkle_block_is_valid(nativeInstance_) != 0;
        }
    }

    public byte[] GetNthHash(int n)
    {
        return MerkleBlockNative.chain_merkle_block_hash_nth(nativeInstance_, (UIntPtr) n);
    }

    public Header Header
    {
        get
        {
            return new Header(MerkleBlockNative.chain_merkle_block_header(nativeInstance_));
        }
    }

    public UInt64 HashCount
    {
        get
        {
            return (UInt64) MerkleBlockNative.chain_merkle_block_hash_count(nativeInstance_);
        }
    }

    public UInt64 TotalTransactionCount
    {
        get
        {
            return (UInt64) MerkleBlockNative.chain_merkle_block_total_transaction_count(nativeInstance_);
        }
    }

    public UInt64 GetSerializedSize(UInt32 version)
    {
        return (UInt64) MerkleBlockNative.chain_merkle_block_serialized_size(nativeInstance_, version);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Reset()
    {
        MerkleBlockNative.chain_merkle_block_reset(nativeInstance_);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            //Release managed resources and call Dispose for member variables
        }   
        //Release unmanaged resources
        MerkleBlockNative.chain_merkle_block_destruct(nativeInstance_);
    }

    internal MerkleBlock(IntPtr nativeInstance)
    {
        nativeInstance_ = nativeInstance;
    }
}

}