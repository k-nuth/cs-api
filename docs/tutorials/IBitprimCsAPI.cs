using Bitprim;
using System;

namespace bitprim.tutorials
{
    public interface IBitprimCsAPI
    {
        IBlock GetBlockByHeight(UInt64 height);

        ITransaction GetTransactionByHash(string txHash);

        UInt64 GetCurrentBlockchainHeight();
    }
}