// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using Knuth.Native;

namespace Knuth
{
    public class StealthCompactList : NativeReadableList<IStealthCompact>
    {
        protected override IStealthCompact GetNthNativeElement(UInt64 n)
        {
            return new StealthCompact(StealthCompactListNative.kth_chain_stealth_compact_list_nth(NativeInstance, n), false);
        }

        protected override UInt64 GetCount()
        {
            return StealthCompactListNative.kth_chain_stealth_compact_list_count(NativeInstance);
        }

        protected override void DestroyNativeList()
        {
            StealthCompactListNative.kth_chain_stealth_compact_list_destruct(NativeInstance);
        }

        internal StealthCompactList(IntPtr nativeInstance) : base(nativeInstance)
        {            
        }
    }

}