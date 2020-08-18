// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Threading.Tasks;
using Knuth;
using Serilog;

namespace console
{
    public class Program
    {
        private static bool running_;


        static async Task Main(string[] args) {
            using (var node = new Knuth.Node("node.cfg")) {
                await node.LaunchAsync();
                Console.WriteLine("Knuth node has been launched");
                node.SubscribeToBlockChain(OnBlockArrived);

                var height = await node.Chain.GetLastHeightAsync();
                Console.WriteLine("Current height in local copy: " + height.Result);

                if (node.Chain.IsStale) {
                    Log.Information("Chain is doing IBD");
                }

                await DiscoverThePowerOfKnuth(node);
                Console.WriteLine("Shutting down node...");
            }
        }

        private static bool OnBlockArrived(ErrorCode errorCode, UInt64 u, BlockList incoming, BlockList outgoing) {
            Console.WriteLine($"{incoming.Count} blocks arrived!");
            return true;
        }
    }
}