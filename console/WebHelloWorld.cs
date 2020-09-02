// // Copyright (c) 2016-2020 Knuth Project developers.
// // Distributed under the MIT software license, see the accompanying
// // file COPYING or http://www.opensource.org/licenses/mit-license.php.

// using System;
// using System.IO;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Configuration;
// using Microsoft.AspNetCore.Hosting;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Http;

// using Knuth;

// namespace SSESample {
//     public class Startup {
//         public void ConfigureServices(IServiceCollection services) {
//             services.AddDirectoryBrowser();
//             services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//             services.AddMvcCore();
//         }

//         public void Configure(IApplicationBuilder app, IHostingEnvironment host) {
//             app.UseFileServer(new FileServerOptions {EnableDirectoryBrowsing = true});

//             var node = Program.Node;

//             app.Use(async (context, next) => {
//                 if (context.Request.Path.ToString().Equals("/sse")) {
//                     var response = context.Response;
//                     response.Headers.Add("Content-Type", "text/event-stream");

//                     for (var i = 0; true; ++i) {
//                         var height = await node.Chain.GetLastHeightAsync();
//                         await response.WriteAsync($"data:{height.Result}|{DateTime.Now}\r\r");
//                         response.Body.Flush();
//                         await Task.Delay(1 * 1000);
//                     }
//                 }

//                 await next.Invoke();
//             });

//             app.UseDeveloperExceptionPage();
//             app.UseMvc();
//         }
//     }

//     public class Program {
//         public static Node Node;

//         public static async Task Main(string[] args) {
//             // var wwwroot = Path.Combine(Directory.GetCurrentDirectory(), "console", "wwwroot");
//             var wwwroot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
//             // Console.WriteLine($"wwwroot: {wwwroot}");

//             using (var node = new Knuth.Node("node.cfg")) {
//                 Node = node;
//                 await node.LaunchAsync();
//                 Console.WriteLine("Knuth node has been launched.");

//                 var host = new WebHostBuilder()
//                     .UseKestrel()
//                     .UseContentRoot(Directory.GetCurrentDirectory())
//                     .UseWebRoot(wwwroot)
//                     .UseStartup<Startup>()
//                     .Build();

//                 host.Run();
//             }
//             Console.WriteLine("Good bye!.");
//         }
//     }
// }






// // using System;
// // using System.Threading.Tasks;

// // using Microsoft.AspNetCore.Builder;
// // using Microsoft.AspNetCore.Http;
// // using Microsoft.AspNetCore.Hosting;

// // using Knuth;

// // // dotnet add package Microsoft.AspNetCore.Server.Kestrel
// // // dotnet add package Microsoft.AspNetCore.Mvc
// // // dotnet add package Microsoft.AspNetCore.StaticFiles
// // // dotnet add package Microsoft.AspNetCore.Diagnostics
// // // dotnet add package Microsoft.Extensions.Configuration



// // namespace console
// // {
// //     public class Program {

// //         static async Task Main(string[] args) {
// //             // var host = new WebHostBuilder()
// //             //     .UseKestrel()
// //             //     .Configure(app => app.Run(context => context.Response.WriteAsync("Hello World!")))
// //             //     .Build();

// //             var host = new WebHostBuilder()
// //                 .UseKestrel()
// //                 .Configure(app => app.Run(context => {
// //                     string html = @"<body>
// //     <script type='text/javascript'>

// //         var source = new EventSource('sse');

// //         source.onmessage = function (event) {
// //             console.log('onmessage: ' + event.data);
// //         };

// //         source.onopen = function(event) {
// //             console.log('onopen');
// //         };

// //         source.onerror = function(event) {
// //             console.log('onerror');
// //         }

// //     </script>
// // </body>";

// //                     return context.Response.WriteAsync(html);
// //                 }))
// //                 .Build();

// //             host.Run();            
// //         }
// //         // static async Task Main(string[] args) {
// //         //     using (var node = new Knuth.Node("node.cfg")) {
// //         //         await node.LaunchAsync();
// //         //         Console.WriteLine("Knuth node has been launched.");
// //         //         node.SubscribeBlockNotifications(OnBlockArrived);

// //         //         var height = await node.Chain.GetLastHeightAsync();
// //         //         Console.WriteLine($"Current height in local copy: {height.Result}");

// //         //         if (node.Chain.IsStale) {
// //         //             Console.WriteLine("Knuth is doing IBD.");
// //         //         }

// //         //         await DiscoverThePowerOfKnuth(node);
// //         //     }
// //         // }

// //         private static bool OnBlockArrived(ErrorCode errorCode, UInt64 height, BlockList incoming, BlockList outgoing) {
// //             Console.WriteLine($"{incoming.Count} blocks arrived!");
// //             return true;
// //         }
// //         public static async Task DiscoverThePowerOfKnuth(Node node) {

// //         }
// //     }
// // }