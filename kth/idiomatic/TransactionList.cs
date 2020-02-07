using System;
using Knuth.Native;

namespace Knuth
{
    /// <summary>
    /// Represents a list of Transactions
    /// </summary>
    public class TransactionList : NativeReadableWritableList<Transaction>
    {
        protected override IntPtr CreateNativeList()
        {
            return TransactionListNative.chain_transaction_list_construct_default();
        }

        protected override Transaction GetNthNativeElement(UInt64 n)
        {
            return new Transaction(TransactionListNative.chain_transaction_list_nth(NativeInstance, n), false);
        }

        protected override UInt64 GetCount()
        {
            return TransactionListNative.chain_transaction_list_count(NativeInstance);
        }

        protected override void AddElement(Transaction element)
        {
            TransactionListNative.chain_transaction_list_push_back(NativeInstance, element.NativeInstance);
        }

        protected override void DestroyNativeList()
        {
            TransactionListNative.chain_transaction_list_destruct(NativeInstance);
        }

        internal TransactionList(IntPtr nativeInstance) : base(nativeInstance)
        {            
        }        
    }

}