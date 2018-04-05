using Bitprim;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace bitprim.console
{
    public class Program
    {
        private static bool running_;

        static void Main(string[] args)
        {
            try
            {
                Console.CancelKeyPress += new ConsoleCancelEventHandler(OnSigInterrupt);
                Console.WriteLine("Initializing...");
                var stdOut = new FileStream("stdout.txt", FileMode.Create);
                var stdOutWriter = new StreamWriter(stdOut);
                Console.SetOut(stdOutWriter);
                var stdErr = new FileStream("stderr.txt", FileMode.Create);
                var stdErrWriter = new StreamWriter(stdErr);
                Console.SetError(stdErrWriter);
                Console.OpenStandardError();
                var executor = new Executor("", stdOut, stdErr);
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
                    var lastHeight = executor.Chain.GetLastHeight();
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