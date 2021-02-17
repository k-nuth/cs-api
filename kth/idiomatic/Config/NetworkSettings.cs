// Copyright (c) 2016-2021 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Knuth.Config
{
    public class NetworkSettings {
        public static NetworkSettings GetDefault(NetworkType network) {
            var native = Knuth.Native.Config.NetworkSettingsNative.kth_config_network_settings_default(network);
            return FromNative(native);
        }

        public UInt32 Threads { get; set; }
        public UInt32 ProtocolMaximum { get; set; }
        public UInt32 ProtocolMinimum { get; set; }
        public UInt64 Services { get; set; }
        public UInt64 InvalidServices { get; set; }
        public bool RelayTransactions { get; set; }
        public bool ValidateChecksum { get; set; }
        public UInt32 Identifier { get; set; }
        public UInt16 InboundPort { get; set; }
        public UInt32 InboundConnections { get; set; }
        public UInt32 OutboundConnections { get; set; }
        public UInt32 ManualAttemptLimit { get; set; }
        public UInt32 ConnectBatchSize { get; set; }
        public UInt32 ConnectTimeoutSeconds { get; set; }
        public UInt32 ChannelHandshakeSeconds { get; set; }
        public UInt32 ChannelHeartbeatMinutes { get; set; }
        public UInt32 ChannelInactivityMinutes { get; set; }
        public UInt32 ChannelExpirationMinutes { get; set; }
        public UInt32 ChannelGerminationSeconds { get; set; }
        public UInt32 HostPoolCapacity { get; set; }
        public string HostsFile { get; set; }
        public Authority Self { get; set; }
        public IList<Authority> Blacklists { get; set; }
        public IList<Endpoint> Peers { get; set; }
        public IList<Endpoint> Seeds { get; set; }
        public string DebugFile { get; set; }
        public string ErrorFile { get; set; }
        public string ArchiveDirectory { get; set; }
        public UInt64 RotationSize { get; set; }
        public UInt64 MinimumFreeSpace { get; set; }
        public UInt64 MaximumArchiveSize { get; set; }
        public UInt64 MaximumArchiveFiles { get; set; }
        public Authority StatisticsServer { get; set; }
        public bool Verbose { get; set; }
        public bool UseIpV6 { get; set; }
        public IList<string> UserAgentBlacklist { get; set; }

        public Knuth.Native.Config.NetworkSettings ToNative() {
            var native = new Knuth.Native.Config.NetworkSettings();
            native.threads = this.Threads;
            native.protocol_maximum = this.ProtocolMaximum;
            native.protocol_minimum = this.ProtocolMinimum;
            native.services = this.Services;
            native.invalid_services = this.InvalidServices;
            native.relay_transactions = this.RelayTransactions;
            native.validate_checksum = this.ValidateChecksum;
            native.identifier = this.Identifier;
            native.inbound_port = this.InboundPort;
            native.inbound_connections = this.InboundConnections;
            native.outbound_connections = this.OutboundConnections;
            native.manual_attempt_limit = this.ManualAttemptLimit;
            native.connect_batch_size = this.ConnectBatchSize;
            native.connect_timeout_seconds = this.ConnectTimeoutSeconds;
            native.channel_handshake_seconds = this.ChannelHandshakeSeconds;
            native.channel_heartbeat_minutes = this.ChannelHeartbeatMinutes;
            native.channel_inactivity_minutes = this.ChannelInactivityMinutes;
            native.channel_expiration_minutes = this.ChannelExpirationMinutes;
            native.channel_germination_seconds = this.ChannelGerminationSeconds;
            native.host_pool_capacity = this.HostPoolCapacity;
            // native.hosts_file = Helper.StringToPtr(this.HostsFile);
            native.hosts_file = this.HostsFile;
            
            native.self = this.Self.ToNative();

            native.blacklists = Helper.ListToNative(this.Blacklists, 
                Knuth.Native.Config.AuthorityNative.kth_config_authority_allocate_n,
                x => x.ToNative(),
                ref native.blacklist_count);

            native.peers = Helper.ListToNative(this.Peers, 
                Knuth.Native.Config.EndpointNative.kth_config_endpoint_allocate_n,
                x => x.ToNative(),
                ref native.peer_count);

            native.seeds = Helper.ListToNative(this.Seeds, 
                Knuth.Native.Config.EndpointNative.kth_config_endpoint_allocate_n,
                x => x.ToNative(),
                ref native.seed_count);

            // native.debug_file = Helper.StringToPtr(this.DebugFile);
            // native.error_file = Helper.StringToPtr(this.ErrorFile);
            // native.archive_directory = Helper.StringToPtr(this.ArchiveDirectory);
            native.debug_file = this.DebugFile;
            native.error_file = this.ErrorFile;
            native.archive_directory = this.ArchiveDirectory;

            native.rotation_size = this.RotationSize;
            native.minimum_free_space = this.MinimumFreeSpace;
            native.maximum_archive_size = this.MaximumArchiveSize;
            native.maximum_archive_files = this.MaximumArchiveFiles;
            native.statistics_server = this.StatisticsServer.ToNative();
            native.verbose = this.Verbose;
            native.use_ipv6 = this.UseIpV6;

            native.user_agent_blacklist = Helper.StringListToNative(this.UserAgentBlacklist, ref native.user_agent_blacklist_count);
            return native;
        }
        public static NetworkSettings FromNative(Knuth.Native.Config.NetworkSettings native) {
            var res = new NetworkSettings();
            res.Threads = native.threads;
            res.ProtocolMaximum = native.protocol_maximum;
            res.ProtocolMinimum = native.protocol_minimum;
            res.Services = native.services;
            res.InvalidServices = native.invalid_services;
            res.RelayTransactions = native.relay_transactions;
            res.ValidateChecksum = native.validate_checksum;
            res.Identifier = native.identifier;
            res.InboundPort = native.inbound_port;
            res.InboundConnections = native.inbound_connections;
            res.OutboundConnections = native.outbound_connections;
            res.ManualAttemptLimit = native.manual_attempt_limit;
            res.ConnectBatchSize = native.connect_batch_size;
            res.ConnectTimeoutSeconds = native.connect_timeout_seconds;
            res.ChannelHandshakeSeconds = native.channel_handshake_seconds;
            res.ChannelHeartbeatMinutes = native.channel_heartbeat_minutes;
            res.ChannelInactivityMinutes = native.channel_inactivity_minutes;
            res.ChannelExpirationMinutes = native.channel_expiration_minutes;
            res.ChannelGerminationSeconds = native.channel_germination_seconds;
            res.HostPoolCapacity = native.host_pool_capacity;

            // res.HostsFile = Helper.PtrToString(native.hosts_file);
            res.HostsFile = native.hosts_file;

            res.Self = Authority.FromNative(native.self);
            res.Blacklists = Helper.ArrayOfPointersToManaged<Authority, Native.Config.Authority>(native.blacklists, native.blacklist_count, Authority.FromNative);
            res.Peers = Helper.ArrayOfPointersToManaged<Endpoint, Native.Config.Endpoint>(native.peers, native.peer_count, Endpoint.FromNative);
            res.Seeds = Helper.ArrayOfPointersToManaged<Endpoint, Native.Config.Endpoint>(native.seeds, native.seed_count, Endpoint.FromNative);
            
            // res.DebugFile = Helper.PtrToString(native.debug_file);
            // res.ErrorFile = Helper.PtrToString(native.error_file);
            // res.ArchiveDirectory = Helper.PtrToString(native.archive_directory);
            res.DebugFile = native.debug_file;
            res.ErrorFile = native.error_file;
            res.ArchiveDirectory = native.archive_directory;

            res.RotationSize = native.rotation_size;
            res.MinimumFreeSpace = native.minimum_free_space;
            res.MaximumArchiveSize = native.maximum_archive_size;
            res.MaximumArchiveFiles = native.maximum_archive_files;
            res.StatisticsServer = Authority.FromNative(native.statistics_server);
            res.Verbose = native.verbose;
            res.UseIpV6 = native.use_ipv6;
            res.UserAgentBlacklist = Helper.ArrayOfStringsToManaged(native.user_agent_blacklist, native.user_agent_blacklist_count);
            return res;
        }
    }
}
