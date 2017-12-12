using System;
using Bitprim.Native;
using System.Collections;

namespace Bitprim
{

    public class StealthCompactList : NativeList<StealthCompact>
    {
        public override IntPtr CreateNativeList()
        {
            //No native call for this
            throw new NotImplementedException();
        }

        public override StealthCompact GetNthNativeElement(int n)
        {
            return new StealthCompact(StealthCompactListNative.stealth_compact_list_nth(NativeInstance, (UInt64) n));
        }

        public override uint GetCount()
        {
            return (uint) StealthCompactListNative.stealth_compact_list_count(NativeInstance);
        }

        public override void AddElement(StealthCompact element)
        {
            //No native call for this
            throw new NotImplementedException();
        }

        public override void DestroyNativeList()
        {
            StealthCompactListNative.stealth_compact_list_destruct(NativeInstance);
        }

        internal new IntPtr NativeInstance
        {
            get
            {
                return base.NativeInstance;
            }
        }

        internal StealthCompactList(IntPtr nativeInstance) : base(nativeInstance)
        {            
        }
    }

}