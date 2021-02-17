// Copyright (c) 2016-2021 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using Knuth.Native;

namespace Knuth
{
    /// <summary>
    /// Represents a list of Payment Addresses.
    /// </summary>
    public class PaymentAddressList : NativeReadableWritableList<PaymentAddress> {
        private bool ownsNativeObject_;

        public PaymentAddressList() : base() {
        }

        protected override IntPtr CreateNativeList() {
            ownsNativeObject_ = true;
            return PaymentAddressListNative.kth_wallet_payment_address_list_construct_default();
        }

        protected override PaymentAddress GetNthNativeElement(UInt64 n) {
            return new PaymentAddress(PaymentAddressListNative.kth_wallet_payment_address_list_nth(NativeInstance, n));
        }

        protected override UInt64 GetCount() {
            return PaymentAddressListNative.kth_wallet_payment_address_list_count(NativeInstance);
        }

        protected override void AddElement(PaymentAddress element) {
            PaymentAddressListNative.kth_wallet_payment_address_list_push_back(NativeInstance, element.NativeInstance);
        }

        protected override void DestroyNativeList() {
            if (ownsNativeObject_) {
                PaymentAddressListNative.kth_wallet_payment_address_list_destruct(NativeInstance);
            }
        }

        internal PaymentAddressList(IntPtr nativeInstance, bool ownsNativeObject = true) : base(nativeInstance) {
            ownsNativeObject_ = ownsNativeObject;
        }
        
    }

}