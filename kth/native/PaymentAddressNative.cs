using System;
using System.Runtime.InteropServices;

namespace Knuth.Native
{
    internal static class PaymentAddressNative
    {
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern byte wallet_payment_address_version(IntPtr payment_address);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern int wallet_payment_address_is_valid(IntPtr payment_address);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr wallet_payment_address_construct_from_string([MarshalAs(UnmanagedType.LPStr)]string address);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr wallet_payment_address_encoded(IntPtr payment_address);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void wallet_payment_address_destruct(IntPtr payment_address);
    }

}