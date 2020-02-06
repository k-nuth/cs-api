using System;
using Bitprim.Native;

namespace Bitprim
{
    /// <summary>
    /// Represents a list of Payment Addresses.
    /// </summary>
    public class PaymentAddressList : NativeReadableWritableList<PaymentAddress>
    {
        private bool ownsNativeObject_;

        public PaymentAddressList() : base()
        {
        }

        protected override IntPtr CreateNativeList()
        {
            ownsNativeObject_ = true;
            return PaymentAddressListNative.wallet_payment_address_list_construct_default();
        }

        protected override PaymentAddress GetNthNativeElement(UInt64 n)
        {
            return new PaymentAddress(PaymentAddressListNative.wallet_payment_address_list_nth(NativeInstance, n));
        }

        protected override UInt64 GetCount()
        {
            return PaymentAddressListNative.wallet_payment_address_list_count(NativeInstance);
        }

        protected override void AddElement(PaymentAddress element)
        {
            PaymentAddressListNative.wallet_payment_address_list_push_back(NativeInstance, element.NativeInstance);
        }

        protected override void DestroyNativeList()
        {
            if(ownsNativeObject_)
            {
                PaymentAddressListNative.wallet_payment_address_list_destruct(NativeInstance);
            }
        }

        internal PaymentAddressList(IntPtr nativeInstance, bool ownsNativeObject = true) : base(nativeInstance)
        {
            ownsNativeObject_ = ownsNativeObject;
        }
        
    }

}