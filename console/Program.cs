// // Copyright (c) 2016-2020 Knuth Project developers.
// // Distributed under the MIT software license, see the accompanying
// // file COPYING or http://www.opensource.org/licenses/mit-license.php.

// using System;
// using System.Threading.Tasks;
// using Knuth;
// using Serilog;

// namespace console
// {
//     public class Program
//     {
//         private static bool running_;

//         static async Task DoSomething(Node node) {
//             var result = await node.LaunchAsync();
//             if (result != ErrorCode.Success) {
//                 Log.Error($"Node::LaunchAsync failed; error code: {result}");
//                 return;
//             }

//             node.SubscribeBlockNotifications(OnBlockArrived);
//             Log.Information("Synchronizing local copy of the blockchain...");
//             running_ = true;
//             while (running_) {
//                 var lastHeight = await node.Chain.GetLastHeightAsync();

//                 if (node.Chain.IsStale) {
//                     Log.Information("Chain is doing IBD");
//                 }

//                 Log.Information("Current height in local copy: " + lastHeight.Result);
//                 await Task.Delay(5000);

//                 // return;
//             }
//         } 
        

//         static async Task Main(string[] args) {
//             // string configFile = "node.cfg";
//             string configFile = "/home/fernando/dev/kth/cs-api/tests/bch/config/invalid.cfg";
            

//             var log = new LoggerConfiguration()
//                 .MinimumLevel.Debug()
//                 .WriteTo.ColoredConsole(outputTemplate: "{Timestamp:HH:mm} [{Level}] {Message}{NewLine}{Exception}")
//                 .CreateLogger();
                
//             Log.Logger = log;

//             try {
//                 Console.CancelKeyPress += OnSigInterrupt;
//                 Log.Information("Initializing...");
//                 using (var node = new Knuth.Node(configFile)) {
//                     await DoSomething(node);
//                     Log.Information("Shutting down node...");
//                 }
//             } catch (Exception ex) {
//                 Log.Error(ex,"Error detected");
//             }
//             Log.CloseAndFlush();
//             Log.Information("Node shutdown OK!");
//         }
//         private static bool OnBlockArrived(ErrorCode errorCode, UInt64 u, BlockList incoming, BlockList outgoing) {
//             Log.Information("Block received!");
//             return true;
//         }

//         private static void OnSigInterrupt(object sender, ConsoleCancelEventArgs args) {
//             Log.Information("Stop signal detected.");
//             args.Cancel = true;
//             running_ = false;
//         }
//     }
// }