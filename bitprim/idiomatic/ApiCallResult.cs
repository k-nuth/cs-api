using System;

namespace Bitprim
{
    public class ApiCallResult<TResultData>
    {
        public Bitprim.ErrorCode ErrorCode { get; set; }
        public TResultData Result { get; set; }
    }

    public class GetBlockByHashTxSizeResult
    {
        public GetBlockDataResult<Block> Block { get; set; }
        public Bitprim.HashList TransactionHashes { get; set; }
        public UInt64 SerializedBlockSize { get; set; }
    }

    public class GetBlockHashTimestampResult
    {
        public byte[] BlockHash { get; set; }
        public DateTime BlockTimestamp { get; set; }
    }

    public class GetBlockDataResult<TBlockData>
    {
        public TBlockData BlockData { get; set; }
        public UInt64 BlockHeight;
    }

    public class GetTxDataResult
    {
        public Transaction Tx { get; set; }
        public GetTxPositionResult TxPosition { get; set; }
    }

    public class GetTxPositionResult
    {
        public UInt64 Index { get; set; }
        public UInt64 BlockHeight { get; set; }
    }

}