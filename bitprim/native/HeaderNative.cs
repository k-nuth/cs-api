using System;
using System.Runtime.InteropServices;

namespace Bitprim.Native
{
    internal static class HeaderNative
    {

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern int chain_header_is_valid(IntPtr header);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern UInt32 chain_header_bits(IntPtr header);

        //Note: Returned memory must be freed manually using platform_free
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr chain_header_proof_str(IntPtr header);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern UInt32 chain_header_nonce(IntPtr header);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern UInt32 chain_header_timestamp(IntPtr header);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern UInt32 chain_header_version(IntPtr header);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_header_destruct(IntPtr header);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_header_hash_out(IntPtr header, ref hash_t out_hash);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_header_merkle_out(IntPtr header, ref hash_t out_merkle);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_header_previous_block_hash_out(IntPtr header, ref hash_t out_previous_block_hash);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_header_set_bits(IntPtr header, UInt32 bits);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_header_set_nonce(IntPtr header, UInt32 nonce);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_header_set_timestamp(IntPtr header, UInt32 timestamp);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_header_set_version(IntPtr header, UInt32 version);

    }

}