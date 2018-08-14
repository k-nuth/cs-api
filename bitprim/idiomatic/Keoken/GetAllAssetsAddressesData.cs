using System;
using System.Runtime.InteropServices;

#if KEOKEN

using Bitprim.Native.Keoken;

namespace Bitprim.Keoken
{
    /// <summary>
    /// TODO: Add docs
    /// </summary>
    public class GetAllAssetsAddressesData:IDisposable
    {
        private bool ownsNativeObject_;
        private readonly IntPtr nativeInstance_;

        public GetAllAssetsAddressesData(IntPtr nativeInstance)
        {
            nativeInstance_ = nativeInstance;
            ownsNativeObject_ = false;
        }

        ~GetAllAssetsAddressesData()
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
            if(ownsNativeObject_)
            {
                GetAllAssetsAddressesDataNative.keoken_get_all_asset_addresses_data_destruct(nativeInstance_);
            }
        }

        public UInt32 AssetId 
        {
            get 
            {
                return  GetAllAssetsAddressesDataNative.keoken_get_all_asset_addresses_data_asset_id(nativeInstance_);
            }
        }

        public string AssetName 
        {
            get 
            {
                using (NativeString native = new NativeString(GetAllAssetsAddressesDataNative.keoken_get_all_asset_addresses_data_asset_name(nativeInstance_)))
                {
                    return native.ToString();
                }
            }
        }

        public PaymentAddress AssetCreator
        {
            get 
            {
                return new PaymentAddress(GetAllAssetsAddressesDataNative.keoken_get_all_asset_addresses_data_asset_creator(nativeInstance_));
            }
        }

        public Int64 Amount 
        {
            get 
            {
                return GetAllAssetsAddressesDataNative.keoken_get_all_asset_addresses_data_amount(nativeInstance_);
            }
        }

        public PaymentAddress AmountOwner
        {
            get 
            {
                return new PaymentAddress(GetAllAssetsAddressesDataNative.keoken_get_all_asset_addresses_data_amount_owner(nativeInstance_));
            }
        }
    }
}

#endif  