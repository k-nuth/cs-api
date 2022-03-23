// // Copyright (c) 2016-2022 Knuth Project developers.
// // Distributed under the MIT software license, see the accompanying
// // file COPYING or http://www.opensource.org/licenses/mit-license.php.

// using System;
// using System.Threading.Tasks;
// using Knuth;
// using Serilog;

// namespace console
// {
//     public class Program {
//         // private static bool running_;

//         static async Task DoSomething(Node node)  {
//             var result = await node.LaunchAsync();
//             if (result != ErrorCode.Success) {
//                 Log.Error($"Node::LaunchAsync failed; error code: {result}");
//                 return;
//                 // throw new ApplicationException($"Node::LaunchAsync failed; error code: {result}");
//             }
//         }

//         static async Task Main(string[] args)  {
//             // string configFile = "node.cfg";
//             string configFile = "node_throttling.cfg";

//             var log = new LoggerConfiguration()
//                 .MinimumLevel.Debug()
//                 .WriteTo.ColoredConsole(outputTemplate: "{Timestamp:HH:mm} [{Level}] {Message}{NewLine}{Exception}")
//                 .CreateLogger();

//             Log.Logger = log;

//             try {
//                 Log.Information("Initializing...");
//                 using (var node = new Knuth.Node(configFile)) {
//                     await DoSomething(node);
//                 }
//             } catch (Exception ex) {
//                 Log.Error(ex, "Error detected");
//             }
//             Log.Information("Node shutdown OK!");
//             Log.CloseAndFlush();
//         }
//     }
// }