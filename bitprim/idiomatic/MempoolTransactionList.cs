using System;
using Bitprim.Native;

namespace Bitprim
{
    /// <summary>
    /// Represents a list of MempoolTransaction
    /// </summary>
    public class MempoolTransactionList : NativeReadableWritableList<IMempoolTransaction>
    {
        private bool ownsNativeObject_;

        protected override IntPtr CreateNativeList()
        {
            ownsNativeObject_ = true;
            return MempoolTransactionListNative.chain_mempool_transaction_list_construct_default();
        }

        protected override IMempoolTransaction GetNthNativeElement(UInt64 n)
        {
            return new MempoolTransaction(MempoolTransactionListNative.chain_mempool_transaction_list_nth(NativeInstance, n));
        }

        protected override UInt64 GetCount()
        {
            return MempoolTransactionListNative.chain_mempool_transaction_list_count(NativeInstance);
        }

        protected override void AddElement(IMempoolTransaction element)
        {
            throw new NotImplementedException("Read only list");
        }

        protected override void DestroyNativeList()
        {
            if(ownsNativeObject_)
            {
                MempoolTransactionListNative.chain_mempool_transaction_list_destruct(NativeInstance);
            }
        }

        internal MempoolTransactionList(IntPtr nativeInstance, bool ownsNativeObject = true) : base(nativeInstance)
        {
            ownsNativeObject_ = ownsNativeObject;
        }
        
    }

}