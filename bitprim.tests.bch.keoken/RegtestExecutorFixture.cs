using System;
using System.IO;

namespace Bitprim.Tests.Bch.Keoken
{
    public class RegtestExecutorFixture : IDisposable
    {
        private readonly Executor exec_;

        public RegtestExecutorFixture()
        {
            exec_ = new Executor("config/regtest.cfg");
            int initChainOk = exec_.InitAndRunAsync().GetAwaiter().GetResult();
            if (initChainOk != 0)
            {
                throw new InvalidOperationException("Executor::InitChain failed, check log");
            }

            //Add mined blocks containing Keoken Transactions
            using (var sr = new StreamReader("config/blocks.hex")) 
            {
                while (sr.Peek() >= 0)
                {
                    string hex = sr.ReadLine();
                    using (var b = new Block(1, hex))
                    {
                        ErrorCode errCode = Executor.Chain.OrganizeBlockAsync(b).Result;
                        if (errCode != ErrorCode.Success)
                        {
                            throw new Exception("Error loading blocks:" + errCode.ToString());
                        }
                    }
                }
            }
           
            exec_.KeokenManager.InitializeFromBlockchain();

        }

        public Executor Executor => exec_;

        public void Dispose()
        {
            exec_.Stop();
            exec_.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}