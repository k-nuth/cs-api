using Bitprim;
using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace bitprim.ibd
{
    public class Program
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Initializing...");
                IntPtr stdOutHandle = GetStdHandle(-11);
                IntPtr stdErrHandle = GetStdHandle(-12);
                var executor = new Executor(Configuration.GetSetting("ConfigFile"), stdOutHandle, stdErrHandle);
                executor.InitChain();
                executor.RunWait();
                Console.WriteLine("Synchronizing local copy of the blockchain...");
                while (true)
                {
                    var lastHeight = executor.Chain.GetLastHeight();
                    Console.WriteLine("Current height in local copy: " + lastHeight);
                    Thread.Sleep(60000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex);
            }
        }
    }
}
