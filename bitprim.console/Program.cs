using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bitprim;
using Serilog;

namespace bitprim.console
{
    public class Program
    {
        static void Main(string[] args)
        {
            InternalMain(args).GetAwaiter().GetResult();
        }

        static async Task WaitForBlock(Executor executor, UInt64 blockNumber)
        {
            Log.Information("Waiting for block " + blockNumber);
            bool keepWaiting = true;
            while ( keepWaiting )
            {
                var lastHeight = await executor.Chain.FetchLastHeightAsync();
                Log.Information("Current height in local copy: " + lastHeight.Result);
                keepWaiting = lastHeight.Result < blockNumber;
                if( keepWaiting )
                {
                    await Task.Delay(5000);
                }
            }
            Log.Information("Block " + blockNumber + " arrived/found!");
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
                Log.Information("Initializing...");
                using (var executor = new Executor(""))
                {
                    var result = await executor.InitAndRunAsync();
                    if (result != 0)
                    {
                        throw new ApplicationException("Executor::InitAndRunAsync failed; error code: " + result);
                    }
                    //executor.SubscribeToBlockChain(OnBlockArrived);
                    await WaitForBlock(executor, 1260000);
                    Log.Information("Stopping node...");
                    await Task.Delay(5000);
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

        private static bool OnBlockArrived(ErrorCode errorCode, UInt64 u, BlockList incoming, BlockList outgoing)
        {
            Log.Information("Block received!");
            return true;
        }
    }
}