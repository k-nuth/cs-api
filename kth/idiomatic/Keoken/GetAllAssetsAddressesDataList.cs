using System;
using System.Runtime.InteropServices;


#if KEOKEN

using Knuth.Native.Keoken;

namespace Knuth.Keoken
{
    public class GetAllAssetsAddressesDataList : NativeReadableList<GetAllAssetsAddressesData>
    {
        internal GetAllAssetsAddressesDataList(IntPtr nativeInstance) : base(nativeInstance)
        {            
        }

        protected override GetAllAssetsAddressesData GetNthNativeElement(UInt64 n)
        {
            return new GetAllAssetsAddressesData(GetAllAssetsAddressesDataListNative.keoken_get_all_asset_addresses_list_nth(NativeInstance, n));
        }

        protected override UInt64 GetCount()
        {
            return GetAllAssetsAddressesDataListNative.keoken_get_all_asset_addresses_list_count(NativeInstance);
        }

        protected override void DestroyNativeList()
        {
            GetAllAssetsAddressesDataListNative.keoken_get_all_asset_addresses_list_destruct(NativeInstance);
        }
    }
}

#endif