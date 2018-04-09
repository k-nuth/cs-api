using System;

namespace Bitprim
{
    public class ApiCallResult<TResultData>
    {
        public Bitprim.ErrorCode ErrorCode { get; set; }
        public TResultData Result { get; set; }
    }

    public sealed class DisposableApiCallResult<TResultData> : IDisposable where TResultData : IDisposable
    {
        public Bitprim.ErrorCode ErrorCode { get; set; }
        public TResultData Result { get; set; }

        public void Dispose()
        {
            Result.Dispose();
        }
    }

    public sealed class GetBlockByHashTxSizeResult : IDisposable
    {
        public GetBlockDataResult<Block> Block { get; set; }
        public Bitprim.HashList TransactionHashes { get; set; }
        public UInt64 SerializedBlockSize { get; set; }

        public void Dispose()
        {
            Block.Dispose();
            TransactionHashes.Dispose();
        }
    }

    public class GetBlockHashTimestampResult
    {
        public byte[] BlockHash { get; set; }
        public DateTime BlockTimestamp { get; set; }
    }

    public sealed class GetBlockDataResult<TBlockData> : IDisposable where TBlockData : IDisposable
    {
        public TBlockData BlockData { get; set; }
        public UInt64 BlockHeight;

        public void Dispose()
        {
            BlockData.Dispose();
        }
    }

    public class GetTxDataResult : IDisposable
    {
        public Transaction Tx { get; set; }
        public GetTxPositionResult TxPosition { get; set; }

        public void Dispose()
        {
            Tx.Dispose();
        }
    }

    public struct GetTxPositionResult
    {
        public UInt64 Index { get; set; }
        public UInt64 BlockHeight { get; set; }
    }

}