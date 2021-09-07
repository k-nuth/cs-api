// // Copyright (c) 2016-2021 Knuth Project developers.
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
//             Console.WriteLine($"Microarchitecture:   {buildConfig.Microarchitecture}");
//             Console.WriteLine($"MicroarchitectureId: {buildConfig.MicroarchitectureId}");
//             Console.WriteLine($"Currency:            {buildConfig.Currency}");
//             Console.WriteLine($"Mempool:             {buildConfig.Mempool}");
//             Console.WriteLine($"DbMode:              {buildConfig.DbMode}");
//             Console.WriteLine($"DbReadonly:          {buildConfig.DbReadonly}");
//             Console.WriteLine($"DebugMode:           {buildConfig.DebugMode}");
            
//             var config = Knuth.Config.Settings.GetDefault(NetworkType.Mainnet);

//             Console.WriteLine("DB Directory: {0}", config.Database.Directory);
//             Console.WriteLine("Launching node...");

//             using (var node = new Knuth.Node(config, false)) {
//                 var res = await node.LaunchAsync(StartModules.All);
//                 Console.WriteLine(res);
//                 Console.WriteLine("Knuth node has been launched.");

//                 await WaitUntilBlock(node.Chain, blockHeight);

//                 var heightRes = await node.Chain.GetLastHeightAsync();
//                 Console.WriteLine($"Current height in local copy: {heightRes.Result}");

//                 for (ulong i = 1; i <= blockHeight; ++i) {
//                     var blockRes = await node.Chain.GetBlockByHeightAsync(i);
//                     var data = blockRes.Result.BlockData.ToData(true);
//                     // Console.WriteLine(data);
//                     var hexStr = Binary.ByteArrayToHexString(data, true);
//                     Console.WriteLine(hexStr);

//                     using (var block = new Block(1, hexStr)) {
//                         var valid = block.IsValid;
//                         // Console.WriteLine(valid);
//                         if ( ! valid) {
//                             throw new Exception();
//                         }

//                         var hash1 = Binary.ByteArrayToHexString(blockRes.Result.BlockData.Hash);
//                         // Console.WriteLine(hash1);
//                         var hash2 = Binary.ByteArrayToHexString(block.Hash);
//                         // Console.WriteLine(hash2);
//                         // Console.WriteLine(hash1 == hash2);


//                         if (hash1 != hash2) {
//                             throw new Exception();
//                         }
//                     }                    
//                 }

//                 heightRes = await node.Chain.GetLastHeightAsync();
//                 Console.WriteLine($"Current height in local copy: {heightRes.Result}");
//             }
//         }
//     }
// }

