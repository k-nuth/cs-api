using System;
using System.Runtime.InteropServices;

namespace Bitprim.Native
{
    internal static class PaymentAddressNative
    {
        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern byte chain_payment_address_version(IntPtr payment_address);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern int chain_payment_address_is_valid(IntPtr payment_address);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern IntPtr chain_payment_address_construct_from_string([MarshalAs(UnmanagedType.LPStr)]string address);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern IntPtr chain_payment_address_encoded(IntPtr payment_address);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern void chain_payment_address_destruct(IntPtr payment_address);
    }

}