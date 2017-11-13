using System;
using Bitprim.Native;

namespace Bitprim{

public class Transaction : IDisposable
{
    private IntPtr nativeInstance_;

    public Transaction()
    {
        nativeInstance_ = TransactionNative.chain_transaction_construct_default();
    }

    public Transaction(string hexString)
    {
        nativeInstance_ = ChainNative.hex_to_tx(hexString);
    }

    public Transaction(UInt32 version, UInt32 locktime, InputList inputs, OutputList outputs)
    {
        nativeInstance_ = TransactionNative.chain_transaction_construct
        (
            version, locktime, inputs.NativeInstance, outputs.NativeInstance
        );
    }

    internal Transaction(IntPtr nativeInstance)
    {
        nativeInstance_ = nativeInstance;
    }

    ~Transaction()
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
        if (disposing){
            //Release managed resources and call Dispose for member variables
        }   
        //Release unmanaged resources
        TransactionNative.chain_transaction_destruct(nativeInstance_);
    }

    public bool IsCoinbase
    {
        get
        {
            return TransactionNative.chain_transaction_is_coinbase(nativeInstance_) != 0;
        }
    }

    public bool IsLocktimeConflict
    {
        get
        {
            return TransactionNative.chain_transaction_is_locktime_conflict(nativeInstance_) != 0;
        }
    }

    public bool IsMissingPreviousOutputs
    {
        get
        {
            return TransactionNative.chain_transaction_is_missing_previous_outputs(nativeInstance_) != 0;
        }
    }

    public bool IsNullNonCoinbase
    {
        get
        {
            return TransactionNative.chain_transaction_is_null_non_coinbase(nativeInstance_) != 0;
        }
    }

    public bool IsOversizeCoinbase
    {
        get
        {
            return TransactionNative.chain_transaction_is_oversized_coinbase(nativeInstance_) != 0;
        }
    }

    public bool IsOverspent
    {
        get
        {
            return TransactionNative.chain_transaction_is_overspent(nativeInstance_) != 0;
        }
    }

    public bool IsValid
    {
        get
        {
            return TransactionNative.chain_transaction_is_valid(nativeInstance_) != 0;
        }
    }

    public byte[] Hash
    {
        get
        {
            var managedHash = new hash_t();
            TransactionNative.chain_transaction_hash_out(nativeInstance_, ref managedHash);
            return managedHash.hash;
        }
    }

    public InputList Inputs
    {
        get
        {
            return new InputList(TransactionNative.chain_transaction_inputs(nativeInstance_));
        }
    }

    public OutputList Outputs
    {
        get
        {
            return new OutputList(TransactionNative.chain_transaction_outputs(nativeInstance_));
        }
    }

    public UInt32 Locktime
    {
        get
        {
            return TransactionNative.chain_transaction_locktime(nativeInstance_);
        }
    }

    public UInt32 Version
    {
        get
        {
            return TransactionNative.chain_transaction_version(nativeInstance_);
        }
        set
        {
            TransactionNative.chain_transaction_set_version(nativeInstance_, value);
        }
    }

    public UInt64 Fees
    {
        get
        {
            return TransactionNative.chain_transaction_fees(nativeInstance_);
        }
    }

    public UInt64 SignatureOperations
    {
        get
        {
            return TransactionNative.chain_transaction_signature_operations(nativeInstance_);
        }
    }

    public UInt64 TotalInputValue
    {
        get
        {
            return TransactionNative.chain_transaction_total_input_value(nativeInstance_);
        }
    }

    public UInt64 TotalOutputValue
    {
        get
        {
            return TransactionNative.chain_transaction_total_output_value(nativeInstance_);
        }
    }

    public byte[] GetHashBySigHashType(UInt32 sigHashType)
    {
        var managedHash = new hash_t();
        TransactionNative.chain_transaction_hash_sighash_type_out(nativeInstance_, sigHashType, ref managedHash);
        return managedHash.hash;
    }

    public bool IsDoubleSpend(bool includeUnconfirmed)
    {
        return TransactionNative.chain_transaction_is_double_spend(nativeInstance_, includeUnconfirmed? 1:0) != 0;
    }

    public bool IsFinal(UInt64 blockHeight, UInt32 blockTime)
    {
        return TransactionNative.chain_transaction_is_final(nativeInstance_, blockHeight, blockTime) != 0;
    }

    public bool IsImmature(UInt64 targetHeight)
    {
        return TransactionNative.chain_transaction_is_immature(nativeInstance_, targetHeight) != 0;
    }

    public UInt64 GetSerializedSize(bool wire = true)
    {
        return TransactionNative.chain_transaction_serialized_size(nativeInstance_, wire? 1:0);
    }

    public UInt64 GetSignatureOperationsBip16Active(bool bip16Active)
    {
        return TransactionNative.chain_transaction_signature_operations_bip16_active(nativeInstance_, bip16Active? 1:0);
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