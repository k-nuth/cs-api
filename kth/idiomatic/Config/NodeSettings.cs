// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Knuth.Config
{
    public class NodeSettings {
        public static NodeSettings GetDefault(NetworkType network) {
            var native = Knuth.Native.Config.NodeSettingsNative.kth_config_node_settings_default(network);
            return FromNative(native);
        }

        public UInt32 SyncPeers { get; set; }
        public UInt32 SyncTimeoutSeconds { get; set; }
        public UInt32 BlockLatencySeconds { get; set; }
        public bool RefreshTransactions { get; set; }
        public bool CompactBlocksHighBandwidth { get; set; }

        public Knuth.Native.Config.NodeSettings ToNative() {
            var native = new Knuth.Native.Config.NodeSettings();
            native.sync_peers = this.SyncPeers;
            native.sync_timeout_seconds = this.SyncTimeoutSeconds;
            native.block_latency_seconds = this.BlockLatencySeconds;
            native.refresh_transactions = this.RefreshTransactions;
            native.compact_blocks_high_bandwidth = this.CompactBlocksHighBandwidth;
            return native;
        }

        public static NodeSettings FromNative(Knuth.Native.Config.NodeSettings native) {
            var res = new NodeSettings();
            res.SyncPeers = native.sync_peers;
            res.SyncTimeoutSeconds = native.sync_timeout_seconds;
            res.BlockLatencySeconds = native.block_latency_seconds;
            res.RefreshTransactions = native.refresh_transactions;
            res.CompactBlocksHighBandwidth = native.compact_blocks_high_bandwidth;
            return res;
        }
    }
}
