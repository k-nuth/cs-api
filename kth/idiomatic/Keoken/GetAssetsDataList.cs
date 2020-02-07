using System;
using System.Runtime.InteropServices;


#if KEOKEN

using Knuth.Native.Keoken;

namespace Knuth.Keoken
{
    public class GetAssetsDataList : NativeReadableList<GetAssetsData>
    {
        internal GetAssetsDataList(IntPtr nativeInstance) : base(nativeInstance)
        {            
        }

        protected override GetAssetsData GetNthNativeElement(UInt64 n)
        {
            return new GetAssetsData(GetAssetsDataListNative.keoken_get_assets_list_nth(NativeInstance, n));
        }

        protected override UInt64 GetCount()
        {
            return GetAssetsDataListNative.keoken_get_assets_list_count(NativeInstance);
        }

        protected override void DestroyNativeList()
        {
            GetAssetsDataListNative.keoken_get_assets_list_destruct(NativeInstance);
        }
    }
}

#endif