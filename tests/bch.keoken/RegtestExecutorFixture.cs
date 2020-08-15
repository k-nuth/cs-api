// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.IO;
using Knuth.Keoken;

namespace Tests.Bch.Keoken
{
    public class RegtestNodeFixture : IDisposable
    {
        private readonly Node exec_;
        private readonly KeokenMemoryState state_ = new KeokenMemoryState();

        public RegtestNodeFixture()
        {
            exec_ = new Node("config/regtest.cfg");
            int initChainOk = exec_.InitAndRunAsync().GetAwaiter().GetResult();
            if (initChainOk != 0)
            {
                throw new InvalidOperationException("Node::InitAndRunAsync failed, check log");
            }

            //Add mined blocks containing Keoken Transactions
            using (var sr = new StreamReader("config/blocks.hex")) 
            {
                while (sr.Peek() >= 0)
                {
                    string hex = sr.ReadLine();
                    using (var b = new Block(1, hex))
                    {
                        ErrorCode errCode = Node.Chain.OrganizeBlockAsync(b).Result;
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

        public Node Node => exec_;

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