using System;
using System.Runtime.InteropServices;

namespace Bitprim.Native
{
    internal static class GetHeadersNative
    {
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern byte[] chain_get_headers_stop_hash(IntPtr getHeaders);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern int /*bool*/ chain_get_headers_is_valid(IntPtr getHeaders);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr chain_get_headers_construct(IntPtr start, byte[] stop);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr chain_get_headers_construct_default();

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr chain_get_headers_start_hashes(IntPtr getHeaders);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern UInt64 chain_get_headers_serialized_size(IntPtr getHeaders, UInt32 version);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_get_headers_destruct(IntPtr getHeaders);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_get_headers_reset(IntPtr getHeaders);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_get_headers_set_start_hashes(IntPtr getHeaders, IntPtr value);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_get_headers_set_stop_hash(IntPtr get_b, byte[] value);

    }

}