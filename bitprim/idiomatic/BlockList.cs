using System;
using BitprimCs.Native;
using System.Collections;

namespace BitprimCs
{

/// <summary>
/// A collection of blockchain blocks.
/// </summary>
public class BlockList : IDisposable
{

    private IntPtr nativeInstance_;

    /// <summary>
    /// Create an empty list.
    /// </summary>
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

    /// <summary>
    /// Needed to iterate collection using foreach
    /// </summary>
    /// <returns> Collection enumerator. </returns>
    public IEnumerator GetEnumerator()
    {
        return new BlockListEnumerator(nativeInstance_);
    }

    /// <summary>
    /// Block count
    /// </summary>
    /// <returns>Count</returns>
    public uint Count
    {
        get
        {
            return (uint) BlockListNative.chain_block_list_count(nativeInstance_);
        }
    }

    /// <summary>
    /// Add a Block to collection
    /// </summary>
    /// <param name="block">Block to add</param>
    public void Add(Block block)
    {
        BlockListNative.chain_block_list_push_back(nativeInstance_, block.NativeInstance);
    }

    /// <summary>
    /// Release object resources
    /// </summary>
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

/// <summary>
/// For enumerator pattern
/// </summary>
public class BlockListEnumerator : IEnumerator
{
    private UInt64 counter_;
    private IntPtr nativeCollection_;

    /// <summary>
    /// Create object from reference to native instance
    /// </summary>
    /// <param name="nativeCollection">Pointer to the native object</param>
    public BlockListEnumerator(IntPtr nativeCollection)
    {
        nativeCollection_ = nativeCollection;
        counter_ = 0;
    }

    /// <summary>
    /// Advance enumerator to next element
    /// </summary>
    /// <returns>True if and only if enumerator moved to next element</returns>
    public bool MoveNext()
    {
        counter_++;
        return counter_ != (uint) BlockListNative.chain_block_list_count(nativeCollection_);
    }

    /// <summary>
    /// Return current element
    /// </summary>
    /// <returns>Reference to current element, as object</returns>
    public object Current
    {
        get
        {
            return BlockListNative.chain_block_list_nth(nativeCollection_, counter_);
        }
    }

    /// <summary>
    /// Go back to collection's first element
    /// </summary>
    public void Reset()
    {
        counter_ = 0;
    }
}

}