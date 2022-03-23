// Copyright (c) 2016-2022 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Runtime.InteropServices;

namespace Knuth.Native
{
    internal static class MempoolTransactionNative
    {
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr kth_chain_mempool_transaction_address(IntPtr tx);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr kth_chain_mempool_transaction_hash(IntPtr tx);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern UInt64 kth_chain_mempool_transaction_index(IntPtr tx);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr kth_chain_mempool_transaction_satoshis(IntPtr tx);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern UInt64 kth_chain_mempool_transaction_timestamp(IntPtr tx);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr kth_chain_mempool_transaction_prev_output_id(IntPtr tx);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr kth_chain_mempool_transaction_prev_output_index(IntPtr tx);

    }

}