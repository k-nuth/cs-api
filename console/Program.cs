using System;
using System.Threading.Tasks;
using Knuth;
using Serilog;

namespace console
{
    public class Program
    {
        private static bool running_;

        static void Main(string[] args)
        {
            InternalMain(args).GetAwaiter().GetResult();
        }

        static async Task InternalMain(string[] args)
        {
            var log = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.ColoredConsole(outputTemplate: "{Timestamp:HH:mm} [{Level}] {Message}{NewLine}{Exception}")
                .CreateLogger();
                
            Log.Logger = log;

            try
            {
                Console.CancelKeyPress += OnSigInterrupt;
                Log.Information("Initializing...");
                using (var executor = new Executor("node.cfg"))
                {
                    var result = await executor.InitAndRunAsync();
                    if (result != 0)
                    {
                        throw new ApplicationException("Executor::InitAndRunAsync failed; error code: " + result);
                    }
                    executor.SubscribeToBlockChain(OnBlockArrived);
                    Log.Information("Synchronizing local copy of the blockchain...");
                    running_ = true;
                    while (running_)
                    {
                        var lastHeight = await executor.Chain.FetchLastHeightAsync();
                        Log.Information("Current height in local copy: " + lastHeight.Result);
                        await Task.Delay(5000);
                    }
                    Log.Information("Stopping node...");
                    executor.Stop();
                    Log.Information("Shutting down node...");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex,"Error detected");
            }
            Log.CloseAndFlush();
            Log.Information("Node shutdown OK!");
        }

        private static void OnSigInterrupt(object sender, ConsoleCancelEventArgs args)
        {
            args.Cancel = true;
            running_ = false;
        }

        private static bool OnBlockArrived(ErrorCode errorCode, UInt64 u, BlockList incoming, BlockList outgoing)
        {
            Log.Information("Block received!");
            return true;
        }
    }
}