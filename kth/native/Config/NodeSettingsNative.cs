// Copyright (c) 2016-2021 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Runtime.InteropServices;

namespace Knuth.Native.Config
{
    [StructLayout(LayoutKind.Sequential)]
    public struct NodeSettings {
        public UInt32 sync_peers;
        public UInt32 sync_timeout_seconds;
        public UInt32 block_latency_seconds;

        [MarshalAs(UnmanagedType.Bool)]
        public bool refresh_transactions;

        [MarshalAs(UnmanagedType.Bool)]
        public bool compact_blocks_high_bandwidth;

        [MarshalAs(UnmanagedType.Bool)]
        public bool ds_proofs_enabled;
    }

    public static class NodeSettingsNative
    {
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern NodeSettings kth_config_node_settings_default(NetworkType network);
    }
}
