// Copyright (c) 2016-2022 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using Xunit;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Knuth.Tests {
    public class ConfigTest {

        [Fact]
        public async Task TestDefaultMainnetSettings() {
            var config = Knuth.Config.Settings.GetDefault(NetworkType.Mainnet);
            Assert.Equal(config.Chain.Cores, 0U);
            Assert.True(config.Chain.Priority);
            Assert.Equal(config.Chain.ByteFeeSatoshis, 0.1, 2);
            Assert.Equal(config.Chain.SigopFeeSatoshis, 100.0);
            Assert.Equal(config.Chain.MinimumOutputSatoshis, 500UL);
            Assert.Equal(config.Chain.NotifyLimitHours, 24U);
            Assert.Equal(config.Chain.ReorganizationLimit, 256U);
            // Assert.True(config.Chain.Checkpoints.Count >= 64);
            Assert.Equal(config.Chain.Checkpoints.Count, 64);
            Assert.Equal(config.Chain.Checkpoints[0].Height, 0UL);
            Assert.Equal(Binary.ByteArrayToHexString(config.Chain.Checkpoints[0].Hash), "000000000019d6689c085ae165831e934ff763ae46a2a6c172b3f1b60a8ce26f");
            Assert.True(config.Chain.FixCheckpoints);
            Assert.True(config.Chain.AllowCollisions);
            Assert.False(config.Chain.EasyBlocks);
            Assert.True(config.Chain.Retarget);
            Assert.True(config.Chain.Bip16);
            Assert.True(config.Chain.Bip30);
            Assert.True(config.Chain.Bip34);
            Assert.True(config.Chain.Bip66);
            Assert.True(config.Chain.Bip65);
            Assert.True(config.Chain.Bip90);
            Assert.True(config.Chain.Bip68);
            Assert.True(config.Chain.Bip112);
            Assert.True(config.Chain.Bip113);
            Assert.True(config.Chain.BchUahf);
            Assert.True(config.Chain.BchDaaCw144);
            Assert.True(config.Chain.BchPythagoras);
            Assert.True(config.Chain.BchEuclid);
            Assert.True(config.Chain.BchPisano);
            Assert.True(config.Chain.BchMersenne);
            Assert.True(config.Chain.BchFermat);
            Assert.True(config.Chain.BchEuler);
            // Assert.False(config.Chain.BchGauss);
            Assert.Equal(config.Chain.GaussActivationTime, 1652616000UL);
            Assert.Equal(config.Chain.DescartesActivationTime, 1684152000UL);
            Assert.Equal(config.Chain.AsertHalfLife, 2UL * 24 * 60 * 60); //two days
            // ------------------------------------------------------------------------------------
            Assert.Equal(config.Database.Directory, "blockchain");
            Assert.False(config.Database.FlushWrites);
            Assert.Equal(config.Database.FileGrowthRate, (ushort)50);
            Assert.Equal(config.Database.IndexStartHeight, 0U);
            Assert.Equal(config.Database.ReorgPoolLimit, 100U);
            Assert.Equal(config.Database.DbMaxSize, 600UL * 1024 * 1024 * 1024);
            Assert.True(config.Database.SafeMode);
            Assert.Equal(config.Database.CacheCapacity, 0U);
            // ------------------------------------------------------------------------------------
            Assert.Equal(config.Network.Threads, 0U);
            Assert.Equal(config.Network.ProtocolMaximum, 70015U);
            Assert.Equal(config.Network.ProtocolMinimum, 31402U);
            Assert.Equal(config.Network.Services, 1U);
            Assert.Equal(config.Network.InvalidServices, 0U);
            Assert.True(config.Network.RelayTransactions);
            Assert.False(config.Network.ValidateChecksum);
            Assert.Equal(config.Network.Identifier, 3908297187U);
            Assert.Equal(config.Network.InboundPort, 8333U);
            Assert.Equal(config.Network.InboundConnections, 0U);
            Assert.Equal(config.Network.OutboundConnections, 8U);
            Assert.Equal(config.Network.ManualAttemptLimit, 0U);
            Assert.Equal(config.Network.ConnectBatchSize, 5U);
            Assert.Equal(config.Network.ConnectTimeoutSeconds, 5U);
            Assert.Equal(config.Network.ChannelHandshakeSeconds, 6000U);
            Assert.Equal(config.Network.ChannelHeartbeatMinutes, 5U);
            Assert.Equal(config.Network.ChannelInactivityMinutes, 10U);
            Assert.Equal(config.Network.ChannelExpirationMinutes, 60U);
            Assert.Equal(config.Network.ChannelGerminationSeconds, 30U);
            Assert.Equal(config.Network.HostPoolCapacity, 1000U);
            Assert.Equal(config.Network.HostsFile, "hosts.cache");
            // Assert.Equal(config.Network.Self.Ip, "0.0.0.0");
            Assert.Equal(config.Network.Self.Port, 0U);
            Assert.Equal(config.Network.Blacklist.Count, 0);
            Assert.Equal(config.Network.Peers.Count, 0);
            Assert.Equal(config.Network.Seeds.Count, 6);
            Assert.Equal(config.Network.Seeds[0].Scheme, "");
            Assert.Equal(config.Network.Seeds[0].Host, "seed.flowee.cash");
            Assert.Equal(config.Network.Seeds[0].Port, 8333U);
            Assert.Equal(config.Network.DebugFile, "debug.log");
            Assert.Equal(config.Network.ErrorFile, "error.log");
            Assert.Equal(config.Network.ArchiveDirectory, "archive");
            Assert.Equal(config.Network.RotationSize, 0U);
            Assert.Equal(config.Network.MinimumFreeSpace, 0U);
            Assert.Equal(config.Network.MaximumArchiveSize, 0U);
            Assert.Equal(config.Network.MaximumArchiveFiles, 0U);
            // Assert.Equal(config.Network.StatisticsServer.Ip, "0.0.0.0");
            Assert.Equal(config.Network.StatisticsServer.Port, 0U);
            Assert.False(config.Network.Verbose);
            Assert.True(config.Network.UseIpV6);
            Assert.Equal(config.Network.UserAgentBlacklist.Count, 1);
            Assert.Equal(config.Network.UserAgentBlacklist[0], "/Bitcoin SV:");
            // ------------------------------------------------------------------------------------
            Assert.Equal(config.Node.SyncPeers, 0U);
            Assert.Equal(config.Node.SyncTimeoutSeconds, 5U);
            Assert.Equal(config.Node.BlockLatencySeconds, 60U);
            Assert.True(config.Node.RefreshTransactions);
            Assert.True(config.Node.CompactBlocksHighBandwidth);
            Assert.False(config.Node.DsProofsEnabled);

        }

        [Fact]
        public async Task TestDefaultTestnet4Settings() {
            var config = Knuth.Config.Settings.GetDefault(NetworkType.Testnet4);
            Assert.Equal(config.Chain.Cores, 0U);
            Assert.True(config.Chain.Priority);
            Assert.Equal(config.Chain.ByteFeeSatoshis, 0.1, 2);
            Assert.Equal(config.Chain.SigopFeeSatoshis, 100.0);
            Assert.Equal(config.Chain.MinimumOutputSatoshis, 500UL);
            Assert.Equal(config.Chain.NotifyLimitHours, 24U);
            Assert.Equal(config.Chain.ReorganizationLimit, 256U);
            Assert.Equal(config.Chain.Checkpoints.Count, 18);
            // Assert.True(config.Chain.Checkpoints.Count >= 18);
            Assert.Equal(config.Chain.Checkpoints[0].Height, 0UL);
            Assert.Equal(Binary.ByteArrayToHexString(config.Chain.Checkpoints[0].Hash), "000000001dd410c49a788668ce26751718cc797474d3152a5fc073dd44fd9f7b");
            Assert.True(config.Chain.FixCheckpoints);
            Assert.True(config.Chain.AllowCollisions);
            Assert.True(config.Chain.EasyBlocks);
            Assert.True(config.Chain.Retarget);
            Assert.True(config.Chain.Bip16);
            Assert.True(config.Chain.Bip30);
            Assert.True(config.Chain.Bip34);
            Assert.True(config.Chain.Bip66);
            Assert.True(config.Chain.Bip65);
            Assert.True(config.Chain.Bip90);
            Assert.True(config.Chain.Bip68);
            Assert.True(config.Chain.Bip112);
            Assert.True(config.Chain.Bip113);
            Assert.True(config.Chain.BchUahf);
            Assert.True(config.Chain.BchDaaCw144);
            Assert.True(config.Chain.BchPythagoras);
            Assert.True(config.Chain.BchEuclid);
            Assert.True(config.Chain.BchPisano);
            Assert.True(config.Chain.BchMersenne);
            Assert.True(config.Chain.BchFermat);
            Assert.True(config.Chain.BchEuler);
            // Assert.False(config.Chain.BchGauss);
            Assert.Equal(config.Chain.GaussActivationTime, 1652616000UL);
            Assert.Equal(config.Chain.DescartesActivationTime, 1684152000UL);
            Assert.Equal(config.Chain.AsertHalfLife, 60UL * 60); //one hour
            // ------------------------------------------------------------------------------------
            Assert.Equal(config.Database.Directory, "blockchain");
            Assert.False(config.Database.FlushWrites);
            Assert.Equal(config.Database.FileGrowthRate, (ushort)50);
            Assert.Equal(config.Database.IndexStartHeight, 0U);
            Assert.Equal(config.Database.ReorgPoolLimit, 100U);
            Assert.Equal(config.Database.DbMaxSize, 20UL * 1024 * 1024 * 1024);
            Assert.True(config.Database.SafeMode);
            Assert.Equal(config.Database.CacheCapacity, 0U);
            // ------------------------------------------------------------------------------------
            Assert.Equal(config.Network.Threads, 0U);
            Assert.Equal(config.Network.ProtocolMaximum, 70015U);
            Assert.Equal(config.Network.ProtocolMinimum, 31402U);
            Assert.Equal(config.Network.Services, 1U);
            Assert.Equal(config.Network.InvalidServices, 0U);
            Assert.True(config.Network.RelayTransactions);
            Assert.False(config.Network.ValidateChecksum);
            Assert.Equal(config.Network.Identifier, 2950346722U);
            Assert.Equal(config.Network.InboundPort, 28333U);
            Assert.Equal(config.Network.InboundConnections, 0U);
            Assert.Equal(config.Network.OutboundConnections, 8U);
            Assert.Equal(config.Network.ManualAttemptLimit, 0U);
            Assert.Equal(config.Network.ConnectBatchSize, 5U);
            Assert.Equal(config.Network.ConnectTimeoutSeconds, 5U);
            Assert.Equal(config.Network.ChannelHandshakeSeconds, 6000U);
            Assert.Equal(config.Network.ChannelHeartbeatMinutes, 5U);
            Assert.Equal(config.Network.ChannelInactivityMinutes, 10U);
            Assert.Equal(config.Network.ChannelExpirationMinutes, 60U);
            Assert.Equal(config.Network.ChannelGerminationSeconds, 30U);
            Assert.Equal(config.Network.HostPoolCapacity, 1000U);
            Assert.Equal(config.Network.HostsFile, "hosts.cache");
            // Assert.Equal(config.Network.Self.Ip, "0.0.0.0");
            Assert.Equal(config.Network.Self.Port, 0U);
            Assert.Equal(config.Network.Blacklist.Count, 0);
            Assert.Equal(config.Network.Peers.Count, 0);
            Assert.Equal(config.Network.Seeds.Count, 3);
            Assert.Equal(config.Network.Seeds[0].Scheme, "");
            Assert.Equal(config.Network.Seeds[0].Host, "testnet4-seed-bch.bitcoinforks.org");
            Assert.Equal(config.Network.Seeds[0].Port, 28333U);
            Assert.Equal(config.Network.DebugFile, "debug.log");
            Assert.Equal(config.Network.ErrorFile, "error.log");
            Assert.Equal(config.Network.ArchiveDirectory, "archive");
            Assert.Equal(config.Network.RotationSize, 0U);
            Assert.Equal(config.Network.MinimumFreeSpace, 0U);
            Assert.Equal(config.Network.MaximumArchiveSize, 0U);
            Assert.Equal(config.Network.MaximumArchiveFiles, 0U);
            // Assert.Equal(config.Network.StatisticsServer.Ip, "0.0.0.0");
            Assert.Equal(config.Network.StatisticsServer.Port, 0U);
            Assert.False(config.Network.Verbose);
            Assert.True(config.Network.UseIpV6);
            Assert.Equal(config.Network.UserAgentBlacklist.Count, 1);
            Assert.Equal(config.Network.UserAgentBlacklist[0], "/Bitcoin SV:");
            // ------------------------------------------------------------------------------------
            Assert.Equal(config.Node.SyncPeers, 0U);
            Assert.Equal(config.Node.SyncTimeoutSeconds, 5U);
            Assert.Equal(config.Node.BlockLatencySeconds, 60U);
            Assert.True(config.Node.RefreshTransactions);
            Assert.True(config.Node.CompactBlocksHighBandwidth);
            Assert.False(config.Node.DsProofsEnabled);
        }
    }
}
