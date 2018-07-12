using System;
using Bitprim.Native;

namespace Bitprim
{
    /// <summary>
    /// List of output points, values, and spends for a given payment address
    /// </summary>
    public class HistoryCompactList : NativeList<HistoryCompact>
    {
        protected override IntPtr CreateNativeList()
        {
            //There is no native call for this, so we deem it invalid.
            //Use the IntPtr constructor instead.
            throw new NotImplementedException();
        }

        protected override HistoryCompact GetNthNativeElement(uint n)
        {
            return new HistoryCompact(HistoryCompactListNative.chain_history_compact_list_nth(NativeInstance, (UInt64)n), false);
        }

        protected override uint GetCount()
        {
            return (uint) HistoryCompactListNative.chain_history_compact_list_count(NativeInstance);
        }

        protected override void AddElement(HistoryCompact element)
        {
            //No native call for this
            throw new NotImplementedException();
        }

        protected override void DestroyNativeList()
        {
            //Logger.Log("Destroying history compact list " + NativeInstance.ToString("X"));
            HistoryCompactListNative.chain_history_compact_list_destruct(NativeInstance);
        }

        internal HistoryCompactList(IntPtr nativeInstance) : base(nativeInstance)
        {            
        }
    }

}