using System;
using System.Runtime.InteropServices;

namespace Bitprim.Native
{
    internal static class StealthCompactNative
    {
        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern byte[] stealth_compact_get_ephemeral_public_key_hash(IntPtr stealth);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern byte[] stealth_compact_get_public_key_hash(IntPtr stealth);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern byte[] stealth_compact_get_transaction_hash(IntPtr stealth);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern void stealth_compact_destruct(IntPtr stealth);
    }

}