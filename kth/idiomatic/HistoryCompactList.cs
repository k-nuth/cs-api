// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using Knuth.Native;

namespace Knuth
{
    /// <summary>
    /// List of output points, values, and spends for a given payment address
    /// </summary>
    public class HistoryCompactList : NativeReadableList<IHistoryCompact>
    {
        protected override IHistoryCompact GetNthNativeElement(UInt64 n)
        {
            return new HistoryCompact(HistoryCompactListNative.kth_chain_history_compact_list_nth(NativeInstance, n), false);
        }

        protected override UInt64 GetCount()
        {
            return HistoryCompactListNative.kth_chain_history_compact_list_count(NativeInstance);
        }

        protected override void DestroyNativeList()
        {
            HistoryCompactListNative.kth_chain_history_compact_list_destruct(NativeInstance);
        }

        internal HistoryCompactList(IntPtr nativeInstance) : base(nativeInstance)
        {            
        }
    }

}