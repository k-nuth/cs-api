using System;
using System.Runtime.InteropServices;

namespace Bitprim.Native
{
    internal static class MempoolTransactionNative
    {
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr chain_mempool_transaction_address(IntPtr tx);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr chain_mempool_transaction_hash(IntPtr tx);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern UInt64 chain_mempool_transaction_index(IntPtr tx);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr chain_mempool_transaction_satoshis(IntPtr tx);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern UInt64 chain_mempool_transaction_timestamp(IntPtr tx);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr chain_mempool_transaction_prev_output_id(IntPtr tx);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr chain_mempool_transaction_prev_output_index(IntPtr tx);

    }

}