using System;
using System.Runtime.InteropServices;

namespace Bitprim.Native
{
    internal static class MempoolTransactionListNative
    {
        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern IntPtr chain_mempool_transaction_list_construct_default();

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern void chain_mempool_transaction_list_destruct(IntPtr list);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern UInt64 /*size_t*/ chain_mempool_transaction_list_count(IntPtr list);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern IntPtr chain_mempool_transaction_list_nth(IntPtr list, UInt64 /*size_t*/ n);

    }

}