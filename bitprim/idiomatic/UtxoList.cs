using System;
using Bitprim.Native;

namespace Bitprim
{
    public class UtxoList : NativeReadableList<IUtxo>
    {
        protected override IUtxo GetNthNativeElement(UInt64 n)
        {
            return new Utxo(UtxoListNative.chain_utxo_list_nth(NativeInstance, n) );
        }

        protected override UInt64 GetCount()
        {
            return UtxoListNative.chain_utxo_list_count(NativeInstance);
        }

        protected override void DestroyNativeList()
        {
            UtxoListNative.chain_utxo_list_destruct(NativeInstance);
        }

        internal UtxoList(IntPtr nativeInstance) : base(nativeInstance)
        {
        }
    }

}