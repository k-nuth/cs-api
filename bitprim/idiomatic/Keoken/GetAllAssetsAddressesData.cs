using System;
using System.Runtime.InteropServices;

#if KEOKEN

using Bitprim.Native.Keoken;

namespace Bitprim.Keoken
{
    public interface IGetAllAssetsAddressesData : IDisposable
    {
        UInt32 AssetId { get; }
        string AssetName { get; }
        PaymentAddress AssetCreator { get; }
        Int64 Amount { get; }
        PaymentAddress AmountOwner { get; }
    }

    /// <summary>
    /// TODO: Add docs
    /// </summary>
    public class GetAllAssetsAddressesData: IGetAllAssetsAddressesData
    {
        private readonly IntPtr nativeInstance_;
        private readonly bool ownsNativeObject_;

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
            if (ownsNativeObject_)
            {
                GetAllAssetsAddressesDataNative.keoken_get_all_asset_addresses_data_destruct(nativeInstance_);
            }
        }

        public UInt32 AssetId => GetAllAssetsAddressesDataNative.keoken_get_all_asset_addresses_data_asset_id(nativeInstance_);

        public string AssetName 
        {
            get 
            {
                using (var native = new NativeString(GetAllAssetsAddressesDataNative.keoken_get_all_asset_addresses_data_asset_name(nativeInstance_)))
                {
                    return native.ToString();
                }
            }
        }

        public PaymentAddress AssetCreator => new PaymentAddress(GetAllAssetsAddressesDataNative.keoken_get_all_asset_addresses_data_asset_creator(nativeInstance_));

        public Int64 Amount => GetAllAssetsAddressesDataNative.keoken_get_all_asset_addresses_data_amount(nativeInstance_);

        public PaymentAddress AmountOwner => new PaymentAddress(GetAllAssetsAddressesDataNative.keoken_get_all_asset_addresses_data_amount_owner(nativeInstance_));
    }
}

#endif  