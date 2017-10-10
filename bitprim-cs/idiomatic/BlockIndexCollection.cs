using System;
using BitprimCs.Native;
using System.Collections;

namespace BitprimCs
{

public class BlockIndexCollection : IDisposable
{

    private IntPtr nativeInstance_;

    public BlockIndexCollection()
    {
        nativeInstance_ = BlockIndexesNative.chain_block_indexes_construct_default();
    }

    ~BlockIndexCollection()
    {
        Dispose(false);
    }

    public IEnumerator GetEnumerator()
    {
        return new BlockIndexCollectionEnumerator(nativeInstance_);
    }

    public uint Count
    {
        get
        {
            return (uint) BlockIndexesNative.chain_block_indexes_count(nativeInstance_);
        }
    }

    public void Add(uint blockIndex)
    {
        BlockIndexesNative.chain_block_indexes_push_back(nativeInstance_, (UIntPtr) blockIndex);
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
        BlockIndexesNative.chain_block_indexes_destruct(nativeInstance_);
    }
}

public class BlockIndexCollectionEnumerator : IEnumerator
{
    private uint counter_;
    private IntPtr nativeCollection_;

    public BlockIndexCollectionEnumerator(IntPtr nativeCollection)
    {
        nativeCollection_ = nativeCollection;
        counter_ = 0;
    }

    public bool MoveNext()
    {
        counter_++;
        return counter_ != (uint) BlockIndexesNative.chain_block_indexes_count(nativeCollection_);
    }

    public object Current
    {
        get
        {
            return BlockIndexesNative.chain_block_indexes_nth(nativeCollection_, (UIntPtr) counter_);
        }
    }

    public void Reset()
    {
        counter_ = 0;
    }
}

}