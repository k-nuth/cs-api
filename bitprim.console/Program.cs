using System;
using System.Threading;
using System.Threading.Tasks;
using Bitprim;

namespace bitprim.console
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
            try
            {
                Console.CancelKeyPress += new ConsoleCancelEventHandler(OnSigInterrupt);
                Console.WriteLine("Initializing...");
                var executor = new Executor("");
                bool ok = executor.InitChain();
                if (!ok)
                {
                    throw new ApplicationException("Executor::InitChain failed; check log");
                }
                int result = executor.RunWait();
                if (result != 0)
                {
                    throw new ApplicationException("Executor::RunWait failed; error code: " + result);
                }
                executor.SubscribeToBlockChain(OnBlockArrived);
                Console.WriteLine("Synchronizing local copy of the blockchain...");
                running_ = true;
                while (running_)
                {
                    var lastHeight = await executor.Chain.FetchLastHeightAsync();
                    Console.WriteLine("Current height in local copy: " + lastHeight);
                    Thread.Sleep(5000);
                }
                Console.WriteLine("Stopping node...");
                executor.Stop();
                Console.WriteLine("Disposing node...");
                executor.Dispose();
                Console.WriteLine("Node stopped!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex);
            }
        }

        private static void OnSigInterrupt(object sender, ConsoleCancelEventArgs args)
        {
            args.Cancel = true;
            running_ = false;
        }

        private static bool OnBlockArrived(ErrorCode errorCode, UInt64 u, BlockList incoming, BlockList outgoing)
        {
            Console.WriteLine("Block received!");
            return true;
        }
    }
}