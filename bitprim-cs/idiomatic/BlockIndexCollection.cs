using System;
using BitprimCs.Native;
using System.Collections;

namespace BitprimCs
{

/// <summary>
/// Represents a set of block indexes
/// </summary>
public class BlockIndexCollection : IDisposable
{

    private IntPtr nativeInstance_;

    /// <summary>
    /// Default constructor
    /// </summary>
    public BlockIndexCollection()
    {
        nativeInstance_ = BlockIndexesNative.chain_block_indexes_construct_default();
    }

    ~BlockIndexCollection()
    {
        Dispose(false);
    }


    /// <summary>
    /// Needed to iterate collection using foreach
    /// </summary>
    /// <returns></returns>
    public IEnumerator GetEnumerator()
    {
        return new BlockIndexCollectionEnumerator(nativeInstance_);
    }

    /// <summary>
    /// Indexes count
    /// </summary>
    /// <returns></returns>
    public uint Count
    {
        get
        {
            return (uint) BlockIndexesNative.chain_block_indexes_count(nativeInstance_);
        }
    }


    /// <summary>
    /// Add a block index to collection
    /// </summary>
    /// <param name="blockIndex">Block index to add</param>
    public void Add(uint blockIndex)
    {
        BlockIndexesNative.chain_block_indexes_push_back(nativeInstance_, (UIntPtr) blockIndex);
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
        BlockIndexesNative.chain_block_indexes_destruct(nativeInstance_);
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
public class BlockIndexCollectionEnumerator : IEnumerator
{
    private uint counter_;
    private IntPtr nativeCollection_;

    /// <summary>
    /// Create object from reference to native instance
    /// </summary>
    /// <param name="nativeCollection"></param>
    public BlockIndexCollectionEnumerator(IntPtr nativeCollection)
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
        return counter_ != (uint) BlockIndexesNative.chain_block_indexes_count(nativeCollection_);
    }

    /// <summary>
    /// Return current element
    /// </summary>
    /// <returns></returns>
    public object Current
    {
        get
        {
            return BlockIndexesNative.chain_block_indexes_nth(nativeCollection_, (UIntPtr) counter_);
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
