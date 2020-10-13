// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Threading.Tasks;
using Knuth;

namespace console
{
    public class Program {
        static async Task Main(string[] args) {
            
            // var config = Knuth.Config.Settings.GetFromFile("/home/fernando/dev/kth/cs-api/console/node.cfg");
            var config = Knuth.Config.Settings.GetFromFile("/Users/fernando/dev/kth/cs-api/console/node_macos.cfg");
            // var config = Knuth.Config.Settings.GetFromFile("C:\\development\\kth\\cs-api\\console\\node_win.cfg");
            // var config = Knuth.Config.Settings.GetFromFile("C:\\development\\kth\\cs-api\\tests\\bch\\config\\non_existing_file.cfg");
            // var config = Knuth.Config.Settings.GetFromFile("C:\\development\\kth\\cs-api\\tests\\bch\\config\\invalid.cfg");

            if ( ! config.Ok) {
                Console.WriteLine(config.ErrorMessage);
                return;
            }

            Console.WriteLine("Starting demo");

#if KTH_CS_CURRENCY_BCH
            Console.WriteLine("Currency: BCH");
#else
            Console.WriteLine("Currency: other");
#endif

            // var config = Knuth.Config.Settings.GetDefault(NetworkType.Mainnet);

            Console.WriteLine("Launching node...");

            using (var node = new Knuth.Node(config.Result, true)) {
            // using (var node = new Knuth.Node(config, true)) {
                var res = await node.LaunchAsync();
                Console.WriteLine(res);

                // Console.WriteLine("Knuth node has been launched.");
                // node.SubscribeBlockNotifications(OnBlockArrived);

                var height = await node.Chain.GetLastHeightAsync();
                Console.WriteLine($"Current height in local copy: {height.Result}");

                // if (node.Chain.IsStale) {
                //     Console.WriteLine("Knuth is doing IBD.");
                // }

                // await DiscoverThePowerOfKnuth(node);
            }
        }

        private static bool OnBlockArrived(ErrorCode errorCode, UInt64 height, BlockList incoming, BlockList outgoing) {
            Console.WriteLine($"{incoming.Count} blocks arrived!");
            return true;
        }
        public static async Task DiscoverThePowerOfKnuth(Node node) {

        }
    }
}