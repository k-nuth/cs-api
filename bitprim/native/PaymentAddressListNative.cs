using System;
using System.Runtime.InteropServices;

namespace Bitprim.Native
{
    internal static class PaymentAddressListNative
    {
        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern IntPtr wallet_payment_address_list_construct_default();

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern IntPtr wallet_payment_address_list_nth(IntPtr list, UInt64 n);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern UInt64 wallet_payment_address_list_count(IntPtr list);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern void wallet_payment_address_list_destruct(IntPtr list);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern void wallet_payment_address_list_push_back(IntPtr list, IntPtr input);
    }

}