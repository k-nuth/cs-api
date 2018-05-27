using System;
using Bitprim.Native;

namespace Bitprim
{

    public class MempoolTransactionList : NativeList<MempoolTransaction>
    {
        private bool ownsNativeObject_;

        public override IntPtr CreateNativeList()
        {
            ownsNativeObject_ = true;
            return MempoolTransactionListNative.chain_mempool_transaction_list_construct_default();
        }

        public override MempoolTransaction GetNthNativeElement(uint n)
        {
            return new MempoolTransaction(MempoolTransactionListNative.chain_mempool_transaction_list_nth(NativeInstance, n));
        }

        public override uint GetCount()
        {
            return (uint) MempoolTransactionListNative.chain_mempool_transaction_list_count(NativeInstance);
        }

        public override void AddElement(MempoolTransaction element)
        {
            throw new NotImplementedException("Read only list");
        }

        public override void DestroyNativeList()
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