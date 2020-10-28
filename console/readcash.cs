// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Threading.Tasks;
using System.Text.Json;
using Knuth;

namespace HelloKnuth {
    public class Program {
        private static bool running_;

        static async Task Main(string[] args) {
            Console.CancelKeyPress += OnSigInterrupt;

            var config = Knuth.Config.Settings.GetDefault(NetworkType.Mainnet);
            config.Database.DbMaxSize /= 1024;
            config.Network.Threads = 1;
            config.Chain.Cores = 1;
            // Console.WriteLine($"Config: {JsonSerializer.Serialize(config)}");

            using (var node = new Knuth.Node(config, true)) {
                Console.WriteLine("Lauching Knuth node...");
                var res = await node.LaunchAsync();
                if (res != ErrorCode.Success) {
                    Console.WriteLine("Error launching the node...");
                    return;
                }
                
                running_ = true;
                Console.WriteLine("Knuth node has been launched.");

                var height = await node.Chain.GetLastHeightAsync();
                Console.WriteLine($"Current height in local copy: {height.Result}");

                if (await ComeBackAfterTheBCHHardFork(node)) {
                    Console.WriteLine("Bitcoin Cash has been created!");
                }
                Console.WriteLine("Closing the node...");
            }
            Console.WriteLine("Good bye!");
        }

        private static async Task<bool> ComeBackAfterTheBCHHardFork(Node node) {
            UInt64 hfHeight = 478559;
            while (running_) {
                var res = await node.Chain.GetLastHeightAsync();
                if (res.Result >= hfHeight) return true;
                await Task.Delay(10000);
            }
            return false;
        }

        private static void OnSigInterrupt(object sender, ConsoleCancelEventArgs args) {
            Console.WriteLine("Stop signal detected.");
            args.Cancel = true;
            running_ = false;
        }
    }
}