using System;
using Bitprim.Native;
using System.Collections;

namespace Bitprim
{

    public class TransactionList : IDisposable
    {

        private IntPtr nativeInstance_;

        public TransactionList()
        {
            nativeInstance_ = TransactionListNative.chain_transaction_list_construct_default();
        }

        ~TransactionList()
        {
            Dispose(false);
        }

        public IEnumerator GetEnumerator()
        {
            return new TransactionListEnumerator(nativeInstance_);
        }

        public uint Count
        {
            get
            {
                return (uint)TransactionListNative.chain_transaction_list_count(nativeInstance_);
            }
        }

        public void Add(Transaction transaction)
        {
            TransactionListNative.chain_transaction_list_push_back(nativeInstance_, transaction.NativeInstance);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal IntPtr NativeInstance
        {
            get
            {
                return nativeInstance_;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //Release managed resources and call Dispose for member variables
            }
            //Release unmanaged resources
            TransactionListNative.chain_transaction_list_destruct(nativeInstance_);
        }

    }

    public class TransactionListEnumerator : IEnumerator
    {
        private uint counter_;
        private IntPtr nativeCollection_;

        public TransactionListEnumerator(IntPtr nativeCollection)
        {
            nativeCollection_ = nativeCollection;
            counter_ = 0;
        }

        public bool MoveNext()
        {
            counter_++;
            return counter_ != (uint)TransactionListNative.chain_transaction_list_count(nativeCollection_);
        }

        public object Current
        {
            get
            {
                return new Transaction(TransactionListNative.chain_transaction_list_nth(nativeCollection_, (UIntPtr)counter_));
            }
        }

        public void Reset()
        {
            counter_ = 0;
        }
    }

}