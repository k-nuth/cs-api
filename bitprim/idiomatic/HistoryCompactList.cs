using System;
using Bitprim.Native;

namespace Bitprim
{
    /// <summary>
    /// List of output points, values, and spends for a given payment address
    /// </summary>
    public class HistoryCompactList : NativeReadOnlyList<HistoryCompact>
    {
        protected override HistoryCompact GetNthNativeElement(UInt64 n)
        {
            return new HistoryCompact(HistoryCompactListNative.chain_history_compact_list_nth(NativeInstance, n), false);
        }

        protected override UInt64 GetCount()
        {
            return HistoryCompactListNative.chain_history_compact_list_count(NativeInstance);
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