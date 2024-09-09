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
        public void TestDefaultMainnetSettings() {
            var config = Knuth.Config.Settings.GetDefault(NetworkType.Mainnet);
            Assert.Equal(0U, config.Chain.Cores);
            Assert.True(config.Chain.Priority);
            Assert.Equal(0.1, config.Chain.ByteFeeSatoshis, 2);
            Assert.Equal(100.0, config.Chain.SigopFeeSatoshis);
            Assert.Equal(500UL, config.Chain.MinimumOutputSatoshis);
            Assert.Equal(24U, config.Chain.NotifyLimitHours);
            Assert.Equal(256U, config.Chain.ReorganizationLimit);
            Assert.Equal(79, config.Chain.Checkpoints.Count);
            Assert.Equal(0UL, config.Chain.Checkpoints[0].Height);
            Assert.Equal("000000000019d6689c085ae165831e934ff763ae46a2a6c172b3f1b60a8ce26f", Binary.ByteArrayToHexString(config.Chain.Checkpoints[0].Hash));
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
            Assert.True(config.Chain.BchGauss);
            Assert.True(config.Chain.BchDescartes);
            Assert.True(config.Chain.BchLobachevski);
            // Assert.False(config.Chain.BchGalois);
            // Assert.False(config.Chain.BchLeibniz);

            Assert.Equal(1747310400UL, config.Chain.GaloisActivationTime);
            Assert.Equal(1778846400UL, config.Chain.LeibnizActivationTime);
            Assert.Equal(2UL * 24 * 60 * 60, config.Chain.AsertHalfLife); //two days
            // ------------------------------------------------------------------------------------
            Assert.Equal("blockchain", config.Database.Directory);
            Assert.Equal(Config.DbMode.Normal, config.Database.DbMode);
            Assert.Equal(100U, config.Database.ReorgPoolLimit);
            Assert.Equal(200UL * 1024 * 1024 * 1024, config.Database.DbMaxSize);
            Assert.True(config.Database.SafeMode);
            Assert.Equal(0U, config.Database.CacheCapacity);
            // ------------------------------------------------------------------------------------
            Assert.Equal(0U, config.Network.Threads);
            Assert.Equal(70015U, config.Network.ProtocolMaximum);
            Assert.Equal(31402U, config.Network.ProtocolMinimum);
            Assert.Equal(1U, config.Network.Services);
            Assert.Equal(0U, config.Network.InvalidServices);
            Assert.True(config.Network.RelayTransactions);
            Assert.False(config.Network.ValidateChecksum);
            Assert.Equal(3908297187U, config.Network.Identifier);
            Assert.Equal(8333U, config.Network.InboundPort);
            Assert.Equal(0U, config.Network.InboundConnections);
            Assert.Equal(8U, config.Network.OutboundConnections);
            Assert.Equal(0U, config.Network.ManualAttemptLimit);
            Assert.Equal(5U, config.Network.ConnectBatchSize);
            Assert.Equal(5U, config.Network.ConnectTimeoutSeconds);
            Assert.Equal(6000U, config.Network.ChannelHandshakeSeconds);
            Assert.Equal(5U, config.Network.ChannelHeartbeatMinutes);
            Assert.Equal(10U, config.Network.ChannelInactivityMinutes);
            Assert.Equal(60U, config.Network.ChannelExpirationMinutes);
            Assert.Equal(30U, config.Network.ChannelGerminationSeconds);
            Assert.Equal(1000U, config.Network.HostPoolCapacity);
            Assert.Equal("hosts.cache", config.Network.HostsFile);
            // Assert.Equal(config.Network.Self.Ip, "0.0.0.0");
            Assert.Equal(0U, config.Network.Self.Port);
            Assert.Empty(config.Network.Blacklist);
            Assert.Empty(config.Network.Peers);
            Assert.Equal(6, config.Network.Seeds.Count);
            Assert.Equal("", config.Network.Seeds[0].Scheme);
            Assert.Equal("seed.flowee.cash", config.Network.Seeds[0].Host);
            Assert.Equal(8333U, config.Network.Seeds[0].Port);
            Assert.Equal("debug.log", config.Network.DebugFile);
            Assert.Equal("error.log", config.Network.ErrorFile);
            Assert.Equal("archive", config.Network.ArchiveDirectory);
            Assert.Equal(0U, config.Network.RotationSize);
            Assert.Equal(0U, config.Network.MinimumFreeSpace);
            Assert.Equal(0U, config.Network.MaximumArchiveSize);
            Assert.Equal(0U, config.Network.MaximumArchiveFiles);
            // Assert.Equal(config.Network.StatisticsServer.Ip, "0.0.0.0");
            Assert.Equal(0U, config.Network.StatisticsServer.Port);
            Assert.False(config.Network.Verbose);
            Assert.True(config.Network.UseIpV6);
            Assert.Single(config.Network.UserAgentBlacklist);
            Assert.Equal("/Bitcoin SV:", config.Network.UserAgentBlacklist[0]);
            // ------------------------------------------------------------------------------------
            Assert.Equal(0U, config.Node.SyncPeers);
            Assert.Equal(5U, config.Node.SyncTimeoutSeconds);
            Assert.Equal(60U, config.Node.BlockLatencySeconds);
            Assert.True(config.Node.RefreshTransactions);
            Assert.True(config.Node.CompactBlocksHighBandwidth);
            Assert.False(config.Node.DsProofsEnabled);
        }

        [Fact]
        public void TestDefaultTestnet4Settings() {
            var config = Knuth.Config.Settings.GetDefault(NetworkType.Testnet4);
            Assert.Equal(0U, config.Chain.Cores);
            Assert.True(config.Chain.Priority);
            Assert.Equal(0.1, config.Chain.ByteFeeSatoshis, 2);
            Assert.Equal(100.0, config.Chain.SigopFeeSatoshis);
            Assert.Equal(500UL, config.Chain.MinimumOutputSatoshis);
            Assert.Equal(24U, config.Chain.NotifyLimitHours);
            Assert.Equal(256U, config.Chain.ReorganizationLimit);
            Assert.Equal(18, config.Chain.Checkpoints.Count);
            Assert.Equal(0UL, config.Chain.Checkpoints[0].Height);
            Assert.Equal("000000001dd410c49a788668ce26751718cc797474d3152a5fc073dd44fd9f7b", Binary.ByteArrayToHexString(config.Chain.Checkpoints[0].Hash));
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
            Assert.True(config.Chain.BchGauss);
            Assert.True(config.Chain.BchDescartes);
            Assert.True(config.Chain.BchLobachevski);
            Assert.Equal(1747310400UL, config.Chain.GaloisActivationTime);
            Assert.Equal(1778846400UL, config.Chain.LeibnizActivationTime);
            Assert.Equal(60UL * 60, config.Chain.AsertHalfLife); //one hour
            // ------------------------------------------------------------------------------------
            Assert.Equal("blockchain", config.Database.Directory);
            Assert.Equal(Config.DbMode.Normal, config.Database.DbMode);
            Assert.Equal(100U, config.Database.ReorgPoolLimit);
            Assert.Equal(20UL * 1024 * 1024 * 1024, config.Database.DbMaxSize);
            Assert.True(config.Database.SafeMode);
            Assert.Equal(0U, config.Database.CacheCapacity);
            // ------------------------------------------------------------------------------------
            Assert.Equal(0U, config.Network.Threads);
            Assert.Equal(70015U, config.Network.ProtocolMaximum);
            Assert.Equal(31402U, config.Network.ProtocolMinimum);
            Assert.Equal(1U, config.Network.Services);
            Assert.Equal(0U, config.Network.InvalidServices);
            Assert.True(config.Network.RelayTransactions);
            Assert.False(config.Network.ValidateChecksum);
            Assert.Equal(2950346722U, config.Network.Identifier);
            Assert.Equal(28333U, config.Network.InboundPort);
            Assert.Equal(0U, config.Network.InboundConnections);
            Assert.Equal(8U, config.Network.OutboundConnections);
            Assert.Equal(0U, config.Network.ManualAttemptLimit);
            Assert.Equal(5U, config.Network.ConnectBatchSize);
            Assert.Equal(5U, config.Network.ConnectTimeoutSeconds);
            Assert.Equal(6000U, config.Network.ChannelHandshakeSeconds);
            Assert.Equal(5U, config.Network.ChannelHeartbeatMinutes);
            Assert.Equal(10U, config.Network.ChannelInactivityMinutes);
            Assert.Equal(60U, config.Network.ChannelExpirationMinutes);
            Assert.Equal(30U, config.Network.ChannelGerminationSeconds);
            Assert.Equal(1000U, config.Network.HostPoolCapacity);
            Assert.Equal("hosts.cache", config.Network.HostsFile);
            // Assert.Equal(config.Network.Self.Ip, "0.0.0.0");
            Assert.Equal(0U, config.Network.Self.Port);
            Assert.Empty(config.Network.Blacklist);
            Assert.Empty(config.Network.Peers);
            Assert.Equal(3, config.Network.Seeds.Count);
            Assert.Equal("", config.Network.Seeds[0].Scheme);
            Assert.Equal("testnet4-seed-bch.bitcoinforks.org", config.Network.Seeds[0].Host);
            Assert.Equal(28333U, config.Network.Seeds[0].Port);
            Assert.Equal("debug.log", config.Network.DebugFile);
            Assert.Equal("error.log", config.Network.ErrorFile);
            Assert.Equal("archive", config.Network.ArchiveDirectory);
            Assert.Equal(0U, config.Network.RotationSize);
            Assert.Equal(0U, config.Network.MinimumFreeSpace);
            Assert.Equal(0U, config.Network.MaximumArchiveSize);
            Assert.Equal(0U, config.Network.MaximumArchiveFiles);
            // Assert.Equal(config.Network.StatisticsServer.Ip, "0.0.0.0");
            Assert.Equal(0U, config.Network.StatisticsServer.Port);
            Assert.False(config.Network.Verbose);
            Assert.True(config.Network.UseIpV6);
            Assert.Single(config.Network.UserAgentBlacklist);
            Assert.Equal("/Bitcoin SV:", config.Network.UserAgentBlacklist[0]);
            // ------------------------------------------------------------------------------------
            Assert.Equal(0U, config.Node.SyncPeers);
            Assert.Equal(5U, config.Node.SyncTimeoutSeconds);
            Assert.Equal(60U, config.Node.BlockLatencySeconds);
            Assert.True(config.Node.RefreshTransactions);
            Assert.True(config.Node.CompactBlocksHighBandwidth);
            Assert.False(config.Node.DsProofsEnabled);
        }
    }
}
