using System;
using System.Runtime.InteropServices;

namespace Bitprim.Native
{
    internal static class MempoolTransactionListNative
    {
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr chain_mempool_transaction_list_construct_default();

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_mempool_transaction_list_destruct(IntPtr list);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern UInt64 chain_mempool_transaction_list_count(IntPtr list);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr chain_mempool_transaction_list_nth(IntPtr list, UInt64 n);

    }

}