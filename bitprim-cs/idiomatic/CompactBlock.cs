using System;
using BitprimCs.Native;
using System.Collections;

namespace BitprimCs
{

public class CompactBlock : IDisposable
{
    private IntPtr nativeInstance_;

    ~CompactBlock()
    {
        Dispose(false);
    }

    public bool IsValid
    {
        get
        {
            return CompactBlockNative.compact_block_is_valid(nativeInstance_) != 0;
        }
    }

    public UInt64 Nonce
    {
        get
        {
            return CompactBlockNative.compact_block_nonce(nativeInstance_);
        }
    }

    public UInt64 TransactionCount
    {
        get
        {
            return CompactBlockNative.compact_block_transaction_count(nativeInstance_);
        }
    }

    public Transaction GetNthTransaction(UInt64 n)
    {
        return new Transaction(CompactBlockNative.compact_block_transaction_nth(nativeInstance_, n));
    }

    public UInt64 GetSerializedSize(UInt32 version)
    {
        return CompactBlockNative.compact_block_serialized_size(nativeInstance_, version);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Reset()
    {
        CompactBlockNative.compact_block_reset(nativeInstance_);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            //Release managed resources and call Dispose for member variables
        }   
        //Release unmanaged resources
        CompactBlockNative.compact_block_destruct(nativeInstance_);
    }

    internal CompactBlock(IntPtr nativeInstance)
    {
        nativeInstance_ = nativeInstance;
    }
}

}