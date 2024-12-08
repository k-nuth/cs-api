// // Copyright (c) 2016-2024 Knuth Project developers.
// // Distributed under the MIT software license, see the accompanying
// // file COPYING or http://www.opensource.org/licenses/mit-license.php.

// using System;
// using System.Threading.Tasks;
// using Knuth;

// namespace console
// {
//     public class Program {
//         private async static Task WaitUntilBlock(Knuth.Chain chain, UInt64 desiredHeight) {
//             ErrorCode error = 0;
//             UInt64 height = 0;
//             while (error == 0 && height < desiredHeight) {
//                 Console.WriteLine($"---> height: {height} desiredHeight: {desiredHeight}");
//                 var errorAndHeight = await chain.GetLastHeightAsync();
//                 height = errorAndHeight.Result;

//                 if (height < desiredHeight) {
//                     await Task.Delay(1000);
//                 }
//             }
//             Console.WriteLine($"---> height: {height} desiredHeight: {desiredHeight}");
//         }

//         static async Task Main(string[] args) {
//             const int blockHeight = 170;

//             var buildConfig = Knuth.LibConfig.Get();
//             Console.WriteLine($"LogLibrary:          {buildConfig.LogLibrary}");
//             Console.WriteLine($"UseLibmdbx:          {buildConfig.UseLibmdbx}");
//             Console.WriteLine($"Version:             {buildConfig.Version}");
//             Console.WriteLine($"MicroarchitectureId: {buildConfig.MicroarchitectureId}");
//             Console.WriteLine($"Currency:            {buildConfig.Currency}");
//             Console.WriteLine($"Mempool:             {buildConfig.Mempool}");
//             Console.WriteLine($"DbMode:              {buildConfig.DbMode}");
//             Console.WriteLine($"DbReadonly:          {buildConfig.DbReadonly}");
//             Console.WriteLine($"DebugMode:           {buildConfig.DebugMode}");

//             var config = Knuth.Config.Settings.GetDefault(NetworkType.Mainnet);

//             Console.WriteLine();
//             Console.WriteLine();
//             Console.WriteLine();
//             Console.WriteLine("DB Directory: {0}", config.Database.Directory);
//             Console.WriteLine();
//             Console.WriteLine();
//             Console.WriteLine();

//             Console.WriteLine("Launching node...");

//             using (var node = new Knuth.Node(config, false)) {
//                 var res = await node.LaunchAsync();
//                 Console.WriteLine(res);
//                 Console.WriteLine("Knuth node has been launched.");

//                 await WaitUntilBlock(node.Chain, blockHeight);

//                 var heightRes = await node.Chain.GetLastHeightAsync();
//                 Console.WriteLine($"Current height in local copy: {heightRes.Result}");

//                 for (ulong i = 0; i < blockHeight; ++i) {
//                     var blockRes = await node.Chain.GetBlockByHeightAsync(i);
//                     var data = blockRes.Result.BlockData.ToData(true);
//                     var hexStr = Binary.ByteArrayToHexString(data);
//                     Console.WriteLine(hexStr);
//                 }
//             }
//         }
//     }
// }


// // 4a5e1e4baab89f3a32518a88c31bc87f618f76673e2cc77ab2127b7afdeda33b

// // 00000000ac5f1df16b2b704c8a578d0bbaf74d385cde12c11ee50455f3c438ef4c3fbcf649b6de611feae06279a60939e028a8d65c10b73071a6f16719274855feb0fd8a67044143000000012a05f20001ffffffff736b6e616220726f662074756f6c69616220646e6f63657320666f206b6e697262206e6f20726f6c6c65636e61684320393030322f6e614a2f33302073656d6954206568544504011d00ffff044dffffffff00000000000000000000000000000000000000000000000000000000000000000100000001017c2bac1d1d00ffff495fab294a5e1e4baab89f3a32518a88c31bc87f618f76673e2cc77ab2127b7afdeda33b000000000000000000000000000000000000000000000000000000000000000000000001