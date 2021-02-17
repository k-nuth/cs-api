// Copyright (c) 2016-2021 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Runtime.InteropServices;

namespace Knuth.Native
{
    internal static class PaymentAddressNative
    {
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern byte kth_wallet_payment_address_version(IntPtr payment_address);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern int kth_wallet_payment_address_is_valid(IntPtr payment_address);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr kth_wallet_payment_address_construct_from_string(
            [MarshalAs(UnmanagedType.LPStr)]string address
        );

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr kth_wallet_payment_address_encoded(IntPtr payment_address);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr kth_wallet_payment_address_encoded_cashaddr(IntPtr payment_address);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_wallet_payment_address_destruct(IntPtr payment_address);
    }

}