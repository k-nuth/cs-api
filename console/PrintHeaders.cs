// // Copyright (c) 2016-2020 Knuth Project developers.
// // Distributed under the MIT software license, see the accompanying
// // file COPYING or http://www.opensource.org/licenses/mit-license.php.

// using System;
// using System.Threading.Tasks;
// using Knuth;

// namespace console
// {
//     public class Program {
//         static async Task Main(string[] args) {
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
            
//             // var config = Knuth.Config.Settings.GetFromFile("/home/fernando/kth-exec/kth-bch-mainnet-pruned.cfg");
//             var config = Knuth.Config.Settings.GetFromFile("/Users/fernando/dev/kth/cs-api/console/kth-bch-mainnet-pruned.cfg");

//             if ( ! config.Ok) {
//                 Console.WriteLine(config.ErrorMessage);
//                 return;
//             }

//             Console.WriteLine();
//             Console.WriteLine();
//             Console.WriteLine();
//             Console.WriteLine("DB Directory: {0}", config.Result.Database.Directory);
//             Console.WriteLine();
//             Console.WriteLine();
//             Console.WriteLine();

//             Console.WriteLine("Launching node...");

//             using (var node = new Knuth.Node(config.Result, true)) {
//                 var res = await node.LaunchAsync();
//                 Console.WriteLine(res);
//                 Console.WriteLine("Knuth node has been launched.");

//                 var heightRes = await node.Chain.GetLastHeightAsync();
//                 Console.WriteLine($"Current height in local copy: {heightRes.Result}");

//                 for (ulong i = 0; i < heightRes.Result; ++i) {
//                     var headerRes = await node.Chain.GetBlockHeaderByHeightAsync(i);
//                 }

//             }
//         }
//     }
// }