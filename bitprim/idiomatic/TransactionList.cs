using System;
using Bitprim.Native;
using System.Collections;

namespace Bitprim
{

    public class TransactionList : NativeList<Transaction>
    {
        public override IntPtr CreateNativeList()
        {
            return TransactionListNative.chain_transaction_list_construct_default();
        }

        public override Transaction GetNthNativeElement(int n)
        {
            return new Transaction(TransactionListNative.chain_transaction_list_nth(NativeInstance, (UIntPtr) n));
        }

        public override uint GetCount()
        {
            return (uint) TransactionListNative.chain_transaction_list_count(NativeInstance);
        }

        public override void AddElement(Transaction element)
        {
            TransactionListNative.chain_transaction_list_push_back(NativeInstance, element.NativeInstance);
        }

        public override void DestroyNativeList()
        {
            TransactionListNative.chain_transaction_list_destruct(NativeInstance);
        }

        internal new IntPtr NativeInstance
        {
            get
            {
                return base.NativeInstance;
            }
        }

        internal TransactionList(IntPtr nativeInstance) : base(nativeInstance)
        {            
        }        
    }

}