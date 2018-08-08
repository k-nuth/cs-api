using System;
using Bitprim.Native;

namespace Bitprim
{
    public class StealthCompactList : NativeReadOnlyList<IStealthCompact>
    {
        protected override IStealthCompact GetNthNativeElement(UInt64 n)
        {
            return new StealthCompact(StealthCompactListNative.chain_stealth_compact_list_nth(NativeInstance, n), false);
        }

        protected override UInt64 GetCount()
        {
            return StealthCompactListNative.chain_stealth_compact_list_count(NativeInstance);
        }

        protected override void DestroyNativeList()
        {
            StealthCompactListNative.chain_stealth_compact_list_destruct(NativeInstance);
        }

        internal StealthCompactList(IntPtr nativeInstance) : base(nativeInstance)
        {            
        }
    }

}