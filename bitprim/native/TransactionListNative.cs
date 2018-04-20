using System;
using System.Runtime.InteropServices;

namespace Bitprim.Native
{

    public static class TransactionListNative
    {
        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern IntPtr chain_transaction_list_construct_default();

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern IntPtr chain_transaction_list_nth(IntPtr list, UIntPtr /*size_t*/ n);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern UIntPtr /*size_t*/ chain_transaction_list_count(IntPtr list);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern void chain_transaction_list_destruct(IntPtr list);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern void chain_transaction_list_push_back(IntPtr list, IntPtr input);

    }

}