using BitprimCs.Native;
using System;
using System.Runtime.InteropServices;

namespace BitprimCs
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
        BlockNative.chain_block_destruct(nativeInstance_);
    }

    public bool IsDistinctTransactionSet
    {
        get
        {
            return BlockNative.chain_block_is_distinct_transaction_set(nativeInstance_) != 0;
        }
    }

    public bool IsExtraCoinbase
    {
        get
        {
            return BlockNative.chain_block_is_extra_coinbases(nativeInstance_) != 0;
        }
    }

    public bool IsInternalDoubleSpend
    {
        get
        {
            return BlockNative.chain_block_is_internal_double_spend(nativeInstance_) != 0;
        }
    }

    public bool IsValid
    {
        get
        {
            return BlockNative.chain_block_is_valid(nativeInstance_) != 0;
        }
    }

    public bool IsValidMerkleRoot
    {
        get
        {
            return BlockNative.chain_block_is_valid_merkle_root(nativeInstance_) != 0;
        }
    }

    public byte[] Hash
    {
        get
        {
            return BlockNative.chain_block_hash(nativeInstance_);
        }
    }

    public byte[] MerkleRoot
    {
        get
        {
            return BlockNative.chain_block_generate_merkle_root(nativeInstance_);
        }
    }

    public Header Header
    {
        get
        {
            return new Header(BlockNative.chain_block_header(nativeInstance_));
        }
    }

    public UInt64 Fees
    {
        get
        {
            return BlockNative.chain_block_fees(nativeInstance_);
        }
    }

    public UInt64 Claim
    {
        get
        {
            return BlockNative.chain_block_claim(nativeInstance_);
        }
    }

    public UIntPtr SignatureOperationsCount
    {
        get
        {
            return BlockNative.chain_block_signature_operations(nativeInstance_);
        }
    }

    public UIntPtr TransactionCount
    {
        get
        {
            return BlockNative.chain_block_transaction_count(nativeInstance_);
        }
    }

    public bool IsFinal(UIntPtr height)
    {
        return BlockNative.chain_block_is_final(nativeInstance_, height) != 0;
    }

    public bool IsValidCoinbaseClaim(UIntPtr height)
    {
        return BlockNative.chain_block_is_valid_coinbase_claim(nativeInstance_, height) != 0;
    }

    public bool IsValidCoinbaseScript(UIntPtr height)
    {
        return BlockNative.chain_block_is_valid_coinbase_script(nativeInstance_, height) != 0;
    }

    public static UInt64 GetSubsidy(UIntPtr height)
    {
        return BlockNative.chain_block_subsidy(height);
    }

    public Transaction GetNthTransaction(UIntPtr n)
    {
        return new Transaction(BlockNative.chain_block_transaction_nth(nativeInstance_, n));
    }

    public UInt64 GetBlockReward(UIntPtr height)
    {
        return BlockNative.chain_block_reward(nativeInstance_, height);
    }

    public UIntPtr GetSerializedSize(UInt32 version)
    {
        return BlockNative.chain_block_serialized_size(nativeInstance_, version);
    }

    public UIntPtr GetSignatureOperationsCount(bool bip16Active)
    {
        return BlockNative.chain_block_signature_operations_bip16_active
        (
            nativeInstance_, bip16Active? 1:0
        );
    }

    public UIntPtr GetTotalInputs(bool withCoinbase)
    {
        return BlockNative.chain_block_total_inputs
        (
            nativeInstance_, withCoinbase? 1:0
        );
    }

    internal Block(IntPtr nativeInstance)
    {
        nativeInstance_ = nativeInstance;
    }

    internal IntPtr NativeInstance
    {
        get
        {
            return nativeInstance_;
        }
    }

}

}