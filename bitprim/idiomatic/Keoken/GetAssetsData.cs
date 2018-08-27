using System;
using System.Runtime.InteropServices;

#if KEOKEN

using Bitprim.Native.Keoken;

namespace Bitprim.Keoken
{
    public interface IGetAssetsData : IDisposable
    {
        UInt32 AssetId { get; }
        string AssetName { get; }
        PaymentAddress AssetCreator { get; }
        Int64 Amount { get; }
    }

    /// <summary>
    /// TODO: Add docs
    /// </summary>
    public class GetAssetsData: IGetAssetsData
    {
        private readonly IntPtr nativeInstance_;
        private readonly bool ownsNativeObject_;

        public GetAssetsData(IntPtr nativeInstance)
        {
            nativeInstance_ = nativeInstance;
            ownsNativeObject_ = false;
        }

        ~GetAssetsData()
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
                GetAssetsDataNative.keoken_get_assets_data_destruct(nativeInstance_);
            }
            
        }

        public UInt32 AssetId => GetAssetsDataNative.keoken_get_assets_data_asset_id(nativeInstance_);

        public string AssetName 
        {
            get 
            {
                using (var native = new NativeString(GetAssetsDataNative.keoken_get_assets_data_asset_name(nativeInstance_)))
                {
                    return native.ToString();
                }
            }
        }

        public PaymentAddress AssetCreator => new PaymentAddress(GetAssetsDataNative.keoken_get_assets_data_asset_creator(nativeInstance_));

        public Int64 Amount => GetAssetsDataNative.keoken_get_assets_data_amount(nativeInstance_);
    }
}

#endif  