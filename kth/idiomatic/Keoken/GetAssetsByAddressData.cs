using System;
using System.Runtime.InteropServices;

#if KEOKEN

using Knuth.Native.Keoken;

namespace Knuth.Keoken
{
    public interface IGetAssetsByAddressData : IDisposable
    {
        UInt32 AssetId { get; }
        string AssetName { get; }
        PaymentAddress AssetCreator { get; }
        Int64 Amount { get; }
    }

    /// <summary>
    /// TODO: Add docs
    /// </summary>
    public class GetAssetsByAddressData: IGetAssetsByAddressData
    {
        private readonly IntPtr nativeInstance_;
        private readonly bool ownsNativeObject_;

        public GetAssetsByAddressData(IntPtr nativeInstance)
        {
            nativeInstance_ = nativeInstance;
            ownsNativeObject_ = false;
        }

        ~GetAssetsByAddressData()
        {
            Dispose(false);
        }

        /// <summary>
        /// Release resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //Release managed resources and call Dispose for member variables
            }
            //Release unmanaged resources
            if (ownsNativeObject_)
            {
                GetAssetsByAddressDataNative.keoken_get_assets_by_address_data_destruct(nativeInstance_);
            }
            
        }

        public UInt32 AssetId => GetAssetsByAddressDataNative.keoken_get_assets_by_address_data_asset_id(nativeInstance_);

        public string AssetName 
        {
            get 
            {
                using (var native = new NativeString(GetAssetsByAddressDataNative.keoken_get_assets_by_address_data_asset_name(nativeInstance_)))
                {
                    return native.ToString();
                }
            }
        }

        public PaymentAddress AssetCreator => new PaymentAddress(GetAssetsByAddressDataNative.keoken_get_assets_by_address_data_asset_creator(nativeInstance_));

        public Int64 Amount => GetAssetsByAddressDataNative.keoken_get_assets_by_address_data_amount(nativeInstance_);
    }
}

#endif  