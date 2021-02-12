// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Runtime.InteropServices;

namespace Knuth.Native.Config
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct NetworkSettings {
        public UInt32 threads;
        public UInt32 protocol_maximum;
        public UInt32 protocol_minimum;
        public UInt64 services;
        public UInt64 invalid_services;
        public bool relay_transactions;
        public bool validate_checksum;
        public UInt32 identifier;
        public UInt16 inbound_port;
        public UInt32 inbound_connections;
        public UInt32 outbound_connections;
        public UInt32 manual_attempt_limit;
        public UInt32 connect_batch_size;
        public UInt32 connect_timeout_seconds;
        public UInt32 channel_handshake_seconds;
        public UInt32 channel_heartbeat_minutes;
        public UInt32 channel_inactivity_minutes;
        public UInt32 channel_expiration_minutes;
        public UInt32 channel_germination_seconds;
        public UInt32 host_pool_capacity;
        public string hosts_file;
        public Authority self;
        public UInt64 blacklist_count;
        public IntPtr blacklists;
        public UInt64 peer_count;
        public IntPtr peers;
        public UInt64 seed_count;
        public IntPtr seeds;
        public string debug_file;
        public string error_file;
        public string archive_directory;
        public UInt64 rotation_size;
        public UInt64 minimum_free_space;
        public UInt64 maximum_archive_size;
        public UInt64 maximum_archive_files;
        public Authority statistics_server;
        public bool verbose;
        public bool use_ipv6;
        public UInt64 user_agent_blacklist_count;
        public IntPtr user_agent_blacklist;
    }

    public static class NetworkSettingsNative
    {
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern NetworkSettings kth_config_network_settings_default(NetworkType network);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_config_network_settings_test_something(NetworkSettings settings);
    }
}
