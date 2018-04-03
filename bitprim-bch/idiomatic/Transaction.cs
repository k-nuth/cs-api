using System;
using Bitprim.Native;

namespace Bitprim
{

    /// <summary>
    /// Represents a Bitcoin transaction.
    /// </summary>
    public class Transaction : IDisposable
    {
        private bool ownsNativeObject_;
        private IntPtr nativeInstance_;

        /// <summary>
        /// Create an empty tramsaction.
        /// </summary>
        public Transaction()
        {
            nativeInstance_ = TransactionNative.chain_transaction_construct_default();
            ownsNativeObject_ = true;
        }

        /// <summary>
        /// Create a transaction from its binary hex representation.
        /// </summary>
        /// <param name="hexString"></param>
        public Transaction(string hexString)
        {
            nativeInstance_ = ChainNative.hex_to_tx(hexString);
            ownsNativeObject_ = true;
        }

        /// <summary>
        /// Create a transaction from its version, locktime, inputs and outputs (all its data).
        /// </summary>
        /// <param name="version"> Transaction protocol version. </param>
        /// <param name="locktime"> Transaction locktime. </param>
        /// <param name="inputs"> A list with all the transaction inputs. </param>
        /// <param name="outputs"> A list with all the transaction outputs. </param>
        public Transaction(UInt32 version, UInt32 locktime, InputList inputs, OutputList outputs)
        {
            nativeInstance_ = TransactionNative.chain_transaction_construct
            (
                version, locktime, inputs.NativeInstance, outputs.NativeInstance
            );
            ownsNativeObject_ = true;
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

        /// <summary>
        /// Returns true if and only if this is a coinbase transaction (i.e. generates new coins).
        /// </summary>
        public bool IsCoinbase
        {
            get
            {
                return TransactionNative.chain_transaction_is_coinbase(nativeInstance_) != 0;
            }
        }

        /// <summary>
        /// Returns true if and only if the transaction is locked and
        /// every input is final, false otherwise.
        /// </summary>
        public bool IsLocktimeConflict
        {
            get
            {
                return TransactionNative.chain_transaction_is_locktime_conflict(nativeInstance_) != 0;
            }
        }

        /// <summary>
        /// Returns true if and only if at least one of the previous outputs is invalid,
        /// false otherwise.
        /// </summary>
        public bool IsMissingPreviousOutputs
        {
            get
            {
                return TransactionNative.chain_transaction_is_missing_previous_outputs(nativeInstance_) != 0;
            }
        }

        /// <summary>
        /// Return true if and only if the transaction is not coinbase and
        /// has a null previous output, false otherwise.
        /// </summary>
        public bool IsNullNonCoinbase
        {
            get
            {
                return TransactionNative.chain_transaction_is_null_non_coinbase(nativeInstance_) != 0;
            }
        }

        /// <summary>
        /// Returns true if the transaction is coinbase and
        /// has an invalid script size on its first input.
        /// </summary>
        public bool IsOversizeCoinbase
        {
            get
            {
                return TransactionNative.chain_transaction_is_oversized_coinbase(nativeInstance_) != 0;
            }
        }

        /// <summary>
        /// Returns true if transaction is not a coinbase, and
        /// the sum of its outputs is higher than the sum of its inputs,
        /// false otherwise.
        /// </summary>
        public bool IsOverspent
        {
            get
            {
                return TransactionNative.chain_transaction_is_overspent(nativeInstance_) != 0;
            }
        }

        /// <summary>
        /// Returns true if and only if this transaction is valid according to the protocol.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return TransactionNative.chain_transaction_is_valid(nativeInstance_) != 0;
            }
        }

        /// <summary>
        /// Transaction hash in 32 byte array format.
        /// </summary>
        public byte[] Hash
        {
            get
            {
                var managedHash = new hash_t();
                TransactionNative.chain_transaction_hash_out(nativeInstance_, ref managedHash);
                return managedHash.hash;
            }
        }

        /// <summary>
        /// A list with all the transaction inputs.
        /// </summary>
        public InputList Inputs
        {
            get
            {
                return new InputList(TransactionNative.chain_transaction_inputs(nativeInstance_), false);
            }
        }

        /// <summary>
        /// A list with all the transaction outputs.
        /// </summary>
        public OutputList Outputs
        {
            get
            {
                return new OutputList(TransactionNative.chain_transaction_outputs(nativeInstance_), false);
            }
        }

        /// <summary>
        /// Transaction locktime.
        /// </summary>
        public UInt32 Locktime
        {
            get
            {
                return TransactionNative.chain_transaction_locktime(nativeInstance_);
            }
        }

        /// <summary>
        /// Transaction protocol version.
        /// </summary>
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

        /// <summary>
        /// Fees to pay to the winning miner.
        /// </summary>
        public UInt64 Fees
        {
            get
            {
                return TransactionNative.chain_transaction_fees(nativeInstance_);
            }
        }

        /// <summary>
        /// Amount of signature operations in the transaction.
        /// </summary>
        public UInt64 SignatureOperations
        {
            get
            {
                return TransactionNative.chain_transaction_signature_operations(nativeInstance_);
            }
        }

        /// <summary>
        /// Sum of every input value in the transaction.
        /// </summary>
        public UInt64 TotalInputValue
        {
            get
            {
                return TransactionNative.chain_transaction_total_input_value(nativeInstance_);
            }
        }

        /// <summary>
        /// Sum of every output value in the transaction.
        /// </summary>
        public UInt64 TotalOutputValue
        {
            get
            {
                return TransactionNative.chain_transaction_total_output_value(nativeInstance_);
            }
        }

        /// <summary>
        /// 32 bytes transaction hash + 4 bytes signature hash type
        /// </summary>
        /// <param name="sigHashType"> Sighash type. </param>
        /// <returns> Hash and sighash type. </returns>
        public byte[] GetHashBySigHashType(UInt32 sigHashType)
        {
            var managedHash = new hash_t();
            TransactionNative.chain_transaction_hash_sighash_type_out(nativeInstance_, sigHashType, ref managedHash);
            return managedHash.hash;
        }

        /// <summary>
        /// Returns true if at least one of the previous outputs was already spent,
        /// false otherwise.
        /// </summary>
        /// <param name="includeUnconfirmed"> Iif true, consider unconfirmed transactions. </param>
        /// <returns> True if and only if transaction is double spend. </returns>
        public bool IsDoubleSpend(bool includeUnconfirmed)
        {
            return TransactionNative.chain_transaction_is_double_spend(nativeInstance_, includeUnconfirmed ? 1 : 0) != 0;
        }

        /// <summary>
        /// Returns true if and only if the transaction is final, 
        /// false otherwise.
        /// </summary>
        /// <param name="blockHeight"></param>
        /// <param name="blockTime"></param>
        /// <returns></returns>
        public bool IsFinal(UInt64 blockHeight, UInt32 blockTime)
        {
            return TransactionNative.chain_transaction_is_final(nativeInstance_, blockHeight, blockTime) != 0;
        }

        /// <summary>
        /// Returns true if and only if at least one of the inputs is not mature,
        /// false otherwise.
        /// </summary>
        /// <param name="targetHeight"></param>
        /// <returns></returns>
        public bool IsImmature(UInt64 targetHeight)
        {
            return TransactionNative.chain_transaction_is_immature(nativeInstance_, targetHeight) != 0;
        }

        /// <summary>
        /// Raw transaction data.
        /// </summary>
        /// <param name="wire">Iif true, include data size at the beginning.</param>
        /// <returns>Byte array with transaction data.</returns>
        public byte[] ToData(bool wire)
        {
            int txSize = 0;
            var txData = new NativeBuffer(TransactionNative.chain_transaction_to_data(nativeInstance_, wire? 1:0, ref txSize));
            return txData.CopyToManagedArray(txSize);
        }

        /// <summary>
        /// Transaction size in bytes.
        /// </summary>
        /// <param name="wire"> If and only if true, size will
        /// include size of 'uint32' for storing spender output height </param>
        /// <returns> Size in bytes. </returns>
        public UInt64 GetSerializedSize(bool wire = true)
        {
            return TransactionNative.chain_transaction_serialized_size(nativeInstance_, wire ? 1 : 0);
        }

        /// <summary>
        /// Amount of signature operations in the transactions.
        /// </summary>
        /// <param name="bip16Active"> True if and only if BIP16 is active, false otherwise. </param>
        /// <returns></returns>
        public UInt64 GetSignatureOperationsBip16Active(bool bip16Active)
        {
            return TransactionNative.chain_transaction_signature_operations_bip16_active(nativeInstance_, bip16Active ? 1 : 0);
        }

        internal IntPtr NativeInstance
        {
            get
            {
                return nativeInstance_;
            }
        }

        internal Transaction(IntPtr nativeInstance, bool ownsNativeObject = true)
        {
            nativeInstance_ = nativeInstance;
            ownsNativeObject_ = ownsNativeObject;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //Release managed resources and call Dispose for member variables
            }
            //Release unmanaged resources
            if(ownsNativeObject_)
            {
                //Logger.Log("Destroying transaction " + nativeInstance_.ToString("X") + " ...");
                TransactionNative.chain_transaction_destruct(nativeInstance_);
                //Logger.Log("Transaction " + nativeInstance_.ToString("X") + " destroyed!");
            }
        }

    }

}