using System;
using System.Runtime.InteropServices;

namespace BitprimCs.Native
{

public class Block : IDisposable
{
    private IntPtr nativeInstance_;

    ~Block()
    {
        Dispose(false);
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
        BlockNative.block_destruct(nativeInstance_);
    }

    public bool IsDistinctTransactionSet
    {
        get
        {
            return BlockNative.block_is_distinct_transaction_set(nativeInstance_) != 0;
        }
    }

    public bool IsExtraCoinbase
    {
        get
        {
            return BlockNative.block_is_extra_coinbases(nativeInstance_) != 0;
        }
    }

    public bool IsInternalDoubleSpend
    {
        get
        {
            return BlockNative.block_is_internal_double_spend(nativeInstance_) != 0;
        }
    }

    public bool IsValid
    {
        get
        {
            return BlockNative.block_is_valid(nativeInstance_) != 0;
        }
    }

    public bool IsValidMerkleRoot
    {
        get
        {
            return BlockNative.block_is_valid_merkle_root(nativeInstance_) != 0;
        }
    }

    public byte[] Hash
    {
        get
        {
            return BlockNative.block_hash(nativeInstance_);
        }
    }

    public byte[] MerkleRoot
    {
        get
        {
            return BlockNative.block_generate_merkle_root(nativeInstance_);
        }
    }

    public Header Header
    {
        get
        {
            return new Header(BlockNative.block_header(nativeInstance_));
        }
    }

    public UInt64 Fees
    {
        get
        {
            return BlockNative.block_fees(nativeInstance_);
        }
    }

    public UInt64 Claim
    {
        get
        {
            return BlockNative.block_claim(nativeInstance_);
        }
    }

    //public UIntPtr

    public bool IsFinal(UIntPtr height)
    {
        return BlockNative.block_is_final(nativeInstance_, height) != 0;
    }

    public bool IsValidCoinbaseClaim(UIntPtr height)
    {
        return BlockNative.block_is_valid_coinbase_claim(nativeInstance_, height) != 0;
    }

    public bool IsValidCoinbaseScript(UIntPtr height)
    {
        return BlockNative.block_is_valid_coinbase_script(nativeInstance_, height) != 0;
    }

    public static UInt64 GetSubsidy(UIntPtr height)
    {
        return BlockNative.block_subsidy(height);
    }

    public Transaction GetNthTransaction(UIntPtr n)
    {
        return new Transaction(BlockNative.block_transaction_nth(nativeInstance_, n));
    }

    public UInt64 GetBlockReward(UIntPtr height)
    {
        return BlockNative.block_reward(nativeInstance_, height);
    }

    public UIntPtr GetSerializedSize(UInt32 version)
    {
        return BlockNative.block_serialized_size(nativeInstance_, version);
    }

}

}