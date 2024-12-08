// // Copyright (c) 2016-2024 Knuth Project developers.
// // Distributed under the MIT software license, see the accompanying
// // file COPYING or http://www.opensource.org/licenses/mit-license.php.

// using System;
// using System.Threading.Tasks;
// using Knuth;

// namespace console
// {
//     public class Program {
//         static async Task Main(string[] args) {
//             var config = Knuth.Config.Settings.GetDefault(NetworkType.Mainnet);
//             config.Node.DsProofsEnabled = true;
//             using (var node = new Knuth.Node(config, true)) {
//                 await node.LaunchAsync();
//                 Console.WriteLine("Knuth node has been launched.");

//                 node.SubscribeDsProofNotifications((err, dsp) => {
//                     Console.WriteLine($"ATTENTION: a Double-Spend attempt has been received!");
//                     return true;
//                 });

//                 var height = await node.Chain.GetLastHeightAsync();
//                 Console.WriteLine($"Current height in local copy: {height.Result}");

//                 if (node.Chain.IsStale) {
//                     Console.WriteLine("Knuth is doing IBD.");
//                 }

//                 await DiscoverThePowerOfKnuth(node);
//             }
//         }

//         public static async Task DiscoverThePowerOfKnuth(Node node) {

//         }
//     }
// }