using System;
using Bitprim.Native;

namespace Bitprim
{

    public class HistoryCompactList : NativeList<HistoryCompact>
    {
        public override IntPtr CreateNativeList()
        {
            //There is no native call for this, so we deem it invalid.
            //Use the IntPtr constructor instead.
            throw new NotImplementedException();
        }

        public override HistoryCompact GetNthNativeElement(int n)
        {
            return new HistoryCompact(HistoryCompactListNative.chain_history_compact_list_nth(NativeInstance, (UInt64)n), false);
        }

        public override uint GetCount()
        {
            return (uint) HistoryCompactListNative.chain_history_compact_list_count(NativeInstance);
        }

        public override void AddElement(HistoryCompact element)
        {
            //No native call for this
            throw new NotImplementedException();
        }

        public override void DestroyNativeList()
        {
            Logger.Log("Destroying history compact list " + NativeInstance.ToString("X"));
            HistoryCompactListNative.chain_history_compact_list_destruct(NativeInstance);
        }

        internal HistoryCompactList(IntPtr nativeInstance) : base(nativeInstance)
        {            
        }
    }

}