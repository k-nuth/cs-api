// Copyright (c) 2016-2021 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Threading.Tasks;
using Knuth;

namespace console
{
    public class Program {

        public static void TestNative() {

            var nodeSettings = Knuth.Native.Config.NodeSettingsNative.kth_config_node_settings_default(NetworkType.Mainnet);
            Console.WriteLine($"sync_peers:                     {nodeSettings.sync_peers}");
            Console.WriteLine($"sync_timeout_seconds:           {nodeSettings.sync_timeout_seconds}");
            Console.WriteLine($"block_latency_seconds:          {nodeSettings.block_latency_seconds}");
            Console.WriteLine($"refresh_transactions:           {nodeSettings.refresh_transactions}");
            Console.WriteLine($"compact_blocks_high_bandwidth:  {nodeSettings.compact_blocks_high_bandwidth}");

            var blockchainSettings = Knuth.Native.Config.BlockchainSettingsNative.kth_config_blockchain_settings_default(NetworkType.Mainnet);
            Console.WriteLine($"cores:                    {blockchainSettings.cores}");
            Console.WriteLine($"priority:                 {blockchainSettings.priority}");
            Console.WriteLine($"byte_fee_satoshis:        {blockchainSettings.byte_fee_satoshis}");
            Console.WriteLine($"sigop_fee_satoshis:       {blockchainSettings.sigop_fee_satoshis}");
            Console.WriteLine($"minimum_output_satoshis:  {blockchainSettings.minimum_output_satoshis}");
            Console.WriteLine($"notify_limit_hours:       {blockchainSettings.notify_limit_hours}");
            Console.WriteLine($"reorganization_limit:     {blockchainSettings.reorganization_limit}");
            Console.WriteLine($"checkpoint_count:         {blockchainSettings.checkpoint_count}");
            Console.WriteLine($"checkpoints:              {blockchainSettings.checkpoints}");
            Console.WriteLine($"fix_checkpoints:         {blockchainSettings.fix_checkpoints}");
            Console.WriteLine($"allow_collisions:         {blockchainSettings.allow_collisions}");
            Console.WriteLine($"easy_blocks:              {blockchainSettings.easy_blocks}");
            Console.WriteLine($"retarget:                 {blockchainSettings.retarget}");
            Console.WriteLine($"bip16:                    {blockchainSettings.bip16}");
            Console.WriteLine($"bip30:                    {blockchainSettings.bip30}");
            Console.WriteLine($"bip34:                    {blockchainSettings.bip34}");
            Console.WriteLine($"bip66:                    {blockchainSettings.bip66}");
            Console.WriteLine($"bip65:                    {blockchainSettings.bip65}");
            Console.WriteLine($"bip90:                    {blockchainSettings.bip90}");
            Console.WriteLine($"bip68:                    {blockchainSettings.bip68}");
            Console.WriteLine($"bip112:                   {blockchainSettings.bip112}");
            Console.WriteLine($"bip113:                   {blockchainSettings.bip113}");
            Console.WriteLine($"bch_uahf:                 {blockchainSettings.bch_uahf}");
            Console.WriteLine($"bch_daa_cw144:            {blockchainSettings.bch_daa_cw144}");
            Console.WriteLine($"bch_pythagoras:             {blockchainSettings.bch_pythagoras}");
            Console.WriteLine($"bch_euclid:     {blockchainSettings.bch_euclid}");
            Console.WriteLine($"bch_pisano:           {blockchainSettings.bch_pisano}");
            Console.WriteLine($"bch_mersenne:             {blockchainSettings.bch_mersenne}");
            Console.WriteLine($"bch_fermat:               {blockchainSettings.bch_fermat}");
            Console.WriteLine($"bch_euler:                {blockchainSettings.bch_euler}");
            // Console.WriteLine($"euler_activation_time:    {blockchainSettings.euler_activation_time}");
            // Console.WriteLine($"bch_gauss:                {blockchainSettings.bch_gauss}");
            // Console.WriteLine($"gauss_activation_time:    {blockchainSettings.gauss_activation_time}");
            Console.WriteLine($"asert_half_life:          {blockchainSettings.asert_half_life}");

            var dbSettings = Knuth.Native.Config.DatabaseSettingsNative.kth_config_database_settings_default(NetworkType.Mainnet);
            Console.WriteLine($"directory:                {dbSettings.directory}");
            Console.WriteLine($"flush_writes:             {dbSettings.flush_writes}");
            Console.WriteLine($"file_growth_rate:         {dbSettings.file_growth_rate}");
            Console.WriteLine($"index_start_height:       {dbSettings.index_start_height}");
            Console.WriteLine($"reorg_pool_limit:         {dbSettings.reorg_pool_limit}");
            Console.WriteLine($"db_max_size:              {dbSettings.db_max_size}");
            Console.WriteLine($"safe_mode:                {dbSettings.safe_mode}");
            Console.WriteLine($"cache_capacity:           {dbSettings.cache_capacity}");
        }

        public static void TestsFromFile() {
            var okSettings = Knuth.Config.Settings.GetFromFile("/home/fernando/dev/kth/cs-api/console/node.cfg");
            if (okSettings.Ok) {
                Console.WriteLine(okSettings.Result.Chain.Checkpoints.Count);
            }

            var nonExistingFile = Knuth.Config.Settings.GetFromFile("/home/fernando/dev/kth/cs-api/console/asdasd.cfg");
            if ( ! nonExistingFile.Ok) {
                Console.WriteLine(nonExistingFile.ErrorMessage);
            }

            var fileWithError = Knuth.Config.Settings.GetFromFile("/home/fernando/dev/kth/cs-api/console/node_error.cfg");
            if ( ! fileWithError.Ok) {
                Console.WriteLine(fileWithError.ErrorMessage);
            }
        }

        public static void Main(string[] args) {

            // TestsFromFile();

            var settings = Knuth.Config.Settings.GetDefault(NetworkType.Mainnet);
            Console.WriteLine(settings.Chain.Checkpoints.Count);
            Console.WriteLine(settings.Network.Blacklist.Count);
            Console.WriteLine(settings.Network.Peers.Count);
            Console.WriteLine(settings.Network.Seeds.Count);
            Console.WriteLine(settings.Network.DebugFile);
            Console.WriteLine(settings.Network.ErrorFile);
            Console.WriteLine(settings.Network.UserAgentBlacklist.Count);

            // var nativeBlk = settings.Chain.ToNative();
            // var nativeNet = settings.Network.ToNative();
            // Knuth.Native.Config.NetworkSettingsNative.kth_config_network_settings_test_something(nativeNet);

            // settings.Network.UserAgentBlacklist.Add("/Bitcoin ABC:");
            // var nativeNet2 = settings.Network.ToNative();
            // Knuth.Native.Config.NetworkSettingsNative.kth_config_network_settings_test_something(nativeNet2);

            TestNative();
        }
    }
}