using System;
using System.Runtime.InteropServices;

namespace Bitprim.Native
{
    internal static class MerkleBlockNative
    {

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern int /*bool*/ chain_merkle_block_is_valid(IntPtr block);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr chain_merkle_block_header(IntPtr block);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern UIntPtr /*size_t*/ chain_merkle_block_hash_count(IntPtr block);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern UIntPtr /*size_t*/ chain_merkle_block_serialized_size(IntPtr block, UInt32 version);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern UIntPtr /*size_t*/ chain_merkle_block_total_transaction_count(IntPtr block);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_merkle_block_destruct(IntPtr block);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_merkle_block_reset(IntPtr block);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_merkle_block_hash_nth_out(IntPtr block, UIntPtr /*size_t*/ n, ref hash_t out_hash);

    }

}