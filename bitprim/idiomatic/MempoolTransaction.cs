using System;
using Bitprim.Native;

namespace Bitprim
{

    /// <summary>
    /// Represents an unconfirmed transaction.
    /// </summary>
    public class MempoolTransaction : IMempoolTransaction
    {
        private IntPtr nativeInstance_;

        /// <summary>
        /// Transaction output address
        /// </summary>
        public string Address
        {
            get
            {
                using ( NativeString addressStr = new NativeString(MempoolTransactionNative.chain_mempool_transaction_address(nativeInstance_)) )
                {
                    return addressStr.ToString();
                }
            }
        }

        /// <summary>
        /// Transaction hash (unique identifier)
        /// </summary>
        public string Hash
        {
            get
            {
                using ( NativeString hashStr = new NativeString(MempoolTransactionNative.chain_mempool_transaction_hash(nativeInstance_)) )
                {
                    return hashStr.ToString();
                }
            }
        }

        /// <summary>
        /// Previous output transaction hash
        /// </summary>
        public string PreviousOutputHash
        {
            get
            {
                using ( NativeString prevOutputHashStr = new NativeString(MempoolTransactionNative.chain_mempool_transaction_prev_output_id(nativeInstance_)) )
                {
                    return prevOutputHashStr.ToString();
                }
            }
        }

        /// <summary>
        /// Previous output transaction index
        /// </summary>
        public string PreviousOutputIndex
        {
            get
            {
                using ( NativeString prevOutputIndexStr = new NativeString(MempoolTransactionNative.chain_mempool_transaction_prev_output_index(nativeInstance_)) )
                {
                    return prevOutputIndexStr.ToString();
                }
            }
        }

        /// <summary>
        /// Sum of output values in Satoshis
        /// </summary>
        public string Satoshis
        {
            get
            {
                using ( NativeString satoshisStr = new NativeString(MempoolTransactionNative.chain_mempool_transaction_satoshis(nativeInstance_)) )
                {
                    return satoshisStr.ToString();
                }
            }
        }

        /// <summary>
        /// Transaction index
        /// </summary>
        public UInt64 Index
        {
            get
            {
                return MempoolTransactionNative.chain_mempool_transaction_index(nativeInstance_);
            }
        }

        /// <summary>
        /// Transaction timestamp
        /// </summary>
        public UInt64 Timestamp
        {
            get
            {
                return MempoolTransactionNative.chain_mempool_transaction_timestamp(nativeInstance_);
            }
        }

        internal MempoolTransaction(IntPtr nativeInstance)
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