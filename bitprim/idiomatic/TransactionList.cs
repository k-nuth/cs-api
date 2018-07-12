using System;
using Bitprim.Native;

namespace Bitprim
{
    /// <summary>
    /// Represents a list of Transactions
    /// </summary>
    public class TransactionList : NativeList<Transaction>
    {
        protected override IntPtr CreateNativeList()
        {
            return TransactionListNative.chain_transaction_list_construct_default();
        }

        protected override Transaction GetNthNativeElement(uint n)
        {
            return new Transaction(TransactionListNative.chain_transaction_list_nth(NativeInstance, (UIntPtr) n), false);
        }

        protected override uint GetCount()
        {
            return (uint) TransactionListNative.chain_transaction_list_count(NativeInstance);
        }

        protected override void AddElement(Transaction element)
        {
            TransactionListNative.chain_transaction_list_push_back(NativeInstance, element.NativeInstance);
        }

        protected override void DestroyNativeList()
        {
            //Logger.Log("Destroying transaction list " + NativeInstance.ToString("X"));
            TransactionListNative.chain_transaction_list_destruct(NativeInstance);
            //Logger.Log("Transaction list " + NativeInstance.ToString("X") + " destroyed!");
        }

        internal TransactionList(IntPtr nativeInstance) : base(nativeInstance)
        {            
        }        
    }

}