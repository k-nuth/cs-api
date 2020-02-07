using System;
using System.Runtime.InteropServices;


#if KEOKEN

using Knuth.Native.Keoken;

namespace Knuth.Keoken
{
    public class GetAssetsByAddressDataList : NativeReadableList<GetAssetsByAddressData>
    {
        internal GetAssetsByAddressDataList(IntPtr nativeInstance) : base(nativeInstance)
        {            
        }

        protected override GetAssetsByAddressData GetNthNativeElement(UInt64 n)
        {
            return new GetAssetsByAddressData(GetAssetsByAddressDataListNative.keoken_get_assets_by_address_list_nth(NativeInstance, n));
        }

        protected override UInt64 GetCount()
        {
            return GetAssetsByAddressDataListNative.keoken_get_assets_by_address_list_count(NativeInstance);
        }

        protected override void DestroyNativeList()
        {
            GetAssetsByAddressDataListNative.keoken_get_assets_by_address_list_destruct(NativeInstance);
        }
    }
}

#endif