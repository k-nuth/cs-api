using System;
using BitprimCs.Native;
using System.Collections;

namespace BitprimCs
{

public class BlockList : IDisposable
{

    private IntPtr nativeInstance_;

    public BlockList()
    {
        nativeInstance_ = BlockListNative.chain_block_list_construct_default();
    }

    internal BlockList(IntPtr nativeInstance)
    {
        nativeInstance_ = nativeInstance;
    }

    ~BlockList()
    {
        Dispose(false);
    }

    public IEnumerator GetEnumerator()
    {
        return new BlockListEnumerator(nativeInstance_);
    }

    public uint Count
    {
        get
        {
            return (uint) BlockListNative.chain_block_list_count(nativeInstance_);
        }
    }

    public void Add(Block block)
    {
        BlockListNative.chain_block_list_push_back(nativeInstance_, block.NativeInstance);
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
        BlockListNative.chain_block_list_destruct(nativeInstance_);
    }

    internal IntPtr NativeInstance
    {
        get
        {
            return nativeInstance_;
        }
    }
}

public class BlockListEnumerator : IEnumerator
{
    private UInt64 counter_;
    private IntPtr nativeCollection_;

    public BlockListEnumerator(IntPtr nativeCollection)
    {
        nativeCollection_ = nativeCollection;
        counter_ = 0;
    }

    public bool MoveNext()
    {
        counter_++;
        return counter_ != (uint) BlockListNative.chain_block_list_count(nativeCollection_);
    }

    public object Current
    {
        get
        {
            return BlockListNative.chain_block_list_nth(nativeCollection_, counter_);
        }
    }

    public void Reset()
    {
        counter_ = 0;
    }
}

}