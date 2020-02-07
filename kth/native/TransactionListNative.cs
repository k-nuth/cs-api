using System;
using System.Runtime.InteropServices;

namespace Knuth.Native
{
    internal static class TransactionListNative
    {
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr chain_transaction_list_construct_default();

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr chain_transaction_list_nth(IntPtr list, UInt64 n);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern UInt64 chain_transaction_list_count(IntPtr list);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_transaction_list_destruct(IntPtr list);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_transaction_list_push_back(IntPtr list, IntPtr input);

    }

}