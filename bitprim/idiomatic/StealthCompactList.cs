using System;
using Bitprim.Native;

namespace Bitprim
{
    public class StealthCompactList : NativeList<StealthCompact>
    {
        protected override IntPtr CreateNativeList()
        {
            //No native call for this
            throw new NotImplementedException();
        }

        protected override StealthCompact GetNthNativeElement(uint n)
        {
            return new StealthCompact(StealthCompactListNative.stealth_compact_list_nth(NativeInstance, (UInt64) n), false);
        }

        protected override uint GetCount()
        {
            return (uint) StealthCompactListNative.stealth_compact_list_count(NativeInstance);
        }

        protected override void AddElement(StealthCompact element)
        {
            //No native call for this
            throw new NotImplementedException();
        }

        protected override void DestroyNativeList()
        {
            //Logger.Log("Destroying stealth compact list " + NativeInstance.ToString("X"));
            StealthCompactListNative.stealth_compact_list_destruct(NativeInstance);
        }

        internal StealthCompactList(IntPtr nativeInstance) : base(nativeInstance)
        {            
        }
    }

}