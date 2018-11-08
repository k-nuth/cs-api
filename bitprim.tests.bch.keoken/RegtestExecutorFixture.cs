using System;
using System.IO;
using Bitprim.Keoken;

namespace Bitprim.Tests.Bch.Keoken
{
    public class RegtestExecutorFixture : IDisposable
    {
        private readonly Executor exec_;
        private readonly KeokenMemoryState state_ = new KeokenMemoryState();

        public RegtestExecutorFixture()
        {
            exec_ = new Executor("config/regtest.cfg");
            int initChainOk = exec_.InitAndRunAsync().GetAwaiter().GetResult();
            if (initChainOk != 0)
            {
                throw new InvalidOperationException("Executor::InitAndRunAsync failed, check log");
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
                        if (errCode != ErrorCode.Success && errCode != ErrorCode.DuplicateBlock)
                        {
                            throw new Exception("Error loading blocks:" + errCode);
                        }
                    }
                }
            }

            
            DelegatedState.SetDelegatedState(state_);
            exec_.KeokenManager.ConfigureState();
            exec_.KeokenManager.InitializeFromBlockchain();
        }

        public Executor Executor => exec_;

        public IKeokenState State => state_;

        public void Dispose()
        {
            state_.Dispose();
            exec_.Stop();
            exec_.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}