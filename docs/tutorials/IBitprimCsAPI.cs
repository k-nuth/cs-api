using Knuth;
using System;

namespace Knuth.Tutorials
{
    public interface IKnuthCsAPI
    {
        IBlock GetBlockByHeight(UInt64 height);

        ITransaction GetTransactionByHash(string txHash);

        UInt64 GetCurrentBlockchainHeight();
    }
}