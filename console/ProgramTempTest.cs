// // Copyright (c) 2016-2024 Knuth Project developers.
// // Distributed under the MIT software license, see the accompanying
// // file COPYING or http://www.opensource.org/licenses/mit-license.php.

// using System;
// using System.Threading.Tasks;
// using Knuth;
// using Serilog;

// namespace console {
//     public class Program {

//         private static async Task WaitUntilBlock(Node node, UInt64 desiredHeight) {
//             ErrorCode error = 0;
//             UInt64 height = 0;
//             while (error == 0 && height < desiredHeight) {
//                 Console.WriteLine($"---> height: {height} desiredHeight: {desiredHeight}");
//                 var errorAndHeight = await node.Chain.GetLastHeightAsync();

//                 error = errorAndHeight.ErrorCode;
//                 height = errorAndHeight.Result;

//                 if (height < desiredHeight) {
//                     await Task.Delay(10000);
//                 }
//             }
//             Console.WriteLine($"---> height: {height} desiredHeight: {desiredHeight}");
//         }

//         static async Task DoSomething(Node node) {
//             const int FIRST_NON_COINBASE_BLOCK_HEIGHT = 170;

//             var result = await node.LaunchAsync();
//             if (result != ErrorCode.Success) {
//                 Log.Error($"Node::LaunchAsync failed; error code: {result}");
//                 return;
//             }

//             await WaitUntilBlock(node, FIRST_NON_COINBASE_BLOCK_HEIGHT);
//             string txHashHexStr = "f4184fc596403b9d638783cf57adfe4c75c605f6356fbc91338530e9831e9e16";
//             byte[] hash = Binary.HexStringToByteArray(txHashHexStr);

//             using (var ret = await node.Chain.GetTransactionAsync(hash, true)) {

//                 var x1 = ret.Result;
//                 var x2 = ret.Result.Tx;
//                 var x3 = ret.Result.Tx.SignatureOperations;

//                 Console.WriteLine($"ret.Result.Tx.SignatureOperations: {ret.Result.Tx.SignatureOperations}");

//                 // CheckFirstNonCoinbaseTxFromHeight170(ret.Result.Tx, txHashHexStr);
//             }
//         }

//         static async Task Main(string[] args) {
//             // string configFile = "node.cfg";
//             string configFile = "/home/fernando/dev/kth/cs-api/console/node.cfg";
//             // string configFile = "/home/fernando/dev/kth/cs-api/tests/bch/config/invalid.cfg";

//             var log = new LoggerConfiguration()
//                 .MinimumLevel.Debug()
//                 .WriteTo.ColoredConsole(outputTemplate: "{Timestamp:HH:mm} [{Level}] {Message}{NewLine}{Exception}")
//                 .CreateLogger();

//             Log.Logger = log;

//             try {
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
//     }
// }