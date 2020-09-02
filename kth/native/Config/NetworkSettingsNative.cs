// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Runtime.InteropServices;

namespace Knuth.Native.Config
{
    [StructLayout(LayoutKind.Sequential)]
    public struct NetworkSettings {
        public int sync_peers;
        public int sync_timeout_seconds;
        public int block_latency_seconds;

        [MarshalAs(UnmanagedType.Bool)]
        public bool refresh_transactions;
        
        [MarshalAs(UnmanagedType.Bool)]
        public bool compact_blocks_high_bandwidth;
    }

    public static class NetworkSettingsNative
    {
        // kth_node_settings kth_config_node_settings_default(kth_network_t network);

        [DllImport(Constants.KTH_C_LIBRARY)]
        // public static extern NodeSettings kth_config_node_settings_default(NetworkType network);
        public static extern NodeSettings kth_config_node_settings_default(int network);
        // public static extern int kth_config_node_settings_default(int network);


        // [DllImport(Constants.KTH_C_LIBRARY)]
        // public static extern byte[] kth_chain_block_generate_merkle_root(IntPtr block);
    }
}

// typedef struct {
//     uint32_t sync_peers;
//     uint32_t sync_timeout_seconds;
//     uint32_t block_latency_seconds;
//     kth_bool_t refresh_transactions;
//     kth_bool_t compact_blocks_high_bandwidth;
// } kth_node_settings;
