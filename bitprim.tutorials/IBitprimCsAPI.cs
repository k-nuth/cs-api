using Bitprim;
using System;

namespace bitprim.tutorials
{
    public interface IBitprimCsAPI
    {
        Block GetBlockByHeight(UInt64 height);

        Transaction GetTransactionByHash(string txHash);

        UInt64 GetCurrentBlockchainHeight();
    }
}