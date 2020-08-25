// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;

namespace Knuth.Tests {
    public class NodeFixture : IDisposable
    {
        public NodeFixture() {
            Node = new Node("config/mainnet.cfg");
            int initChainOk = Node.LaunchAsync().GetAwaiter().GetResult();
            if (initChainOk != 0) {
                throw new InvalidOperationException("Node::LaunchAsync failed, check log");
            }
        }

        public Node Node { get; }
     
        private void ReleaseUnmanagedResources() {
            Node.Stop();
            Node.Dispose();
        }

        protected virtual void Dispose(bool disposing) {
            ReleaseUnmanagedResources();
            if (disposing) {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~NodeFixture() {
            Dispose(false);
        }
    }
}