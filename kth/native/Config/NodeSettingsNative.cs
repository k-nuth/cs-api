// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Runtime.InteropServices;

namespace Knuth.Native.Config
{
    [StructLayout(LayoutKind.Sequential)]
    public struct NodeSettings {
        public int sync_peers;
        public int sync_timeout_seconds;
        public int block_latency_seconds;

        [MarshalAs(UnmanagedType.Bool)]
        public bool refresh_transactions;
        
        [MarshalAs(UnmanagedType.Bool)]
        public bool compact_blocks_high_bandwidth;
    }

    public static class NodeSettingsNative
    {
        // kth_node_settings kth_config_node_settings_default(kth_network_t network);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern NodeSettings kth_config_node_settings_default(NetworkType network);
        // public static extern NodeSettings kth_config_node_settings_default(int network);

    }
}
