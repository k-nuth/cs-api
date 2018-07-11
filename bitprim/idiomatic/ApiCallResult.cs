using System;

namespace Bitprim
{
    /// <summary>
    /// Represent a result of calling an API method
    /// </summary>
    /// <typeparam name="TResultData">The Result's type</typeparam>
    public class ApiCallResult<TResultData>
    {
        /// <summary>
        /// Status code returned by native api
        /// </summary>
        public ErrorCode ErrorCode { get; set; }
        
        /// <summary>
        /// The result of an API Method
        /// </summary>
        public TResultData Result { get; set; }
    }

    /// <summary>
    /// Represent a disposable result of calling an API method. It's necessary to call Dispose when the result is not needed anymore
    /// </summary>
    /// <typeparam name="TResultData">The Result's type</typeparam>
    public sealed class DisposableApiCallResult<TResultData> : ApiCallResult<TResultData>, IDisposable where TResultData : IDisposable
    {
        /// <summary>
        /// Dipose method for cleaning resources
        /// </summary>
        public void Dispose()
        {
            Result.Dispose();
        }
    }

    /// <summary>
    /// Contains data for a block
    /// </summary>
    /// <typeparam name="TBlockData">Type of the data</typeparam>
    public sealed class GetBlockDataResult<TBlockData> : IDisposable where TBlockData : IDisposable
    {
        /// <summary>
        /// Return the blocks' data
        /// </summary>
        public TBlockData BlockData { get; set; }
        
        /// <summary>
        /// The height of the block
        /// </summary>
        public UInt64 BlockHeight;

        /// <summary>
        /// Dipose method for cleaning resources
        /// </summary>
        public void Dispose()
        {
            BlockData.Dispose();
        }
    }

    /// <summary>
    /// Return type for FetchBlockHeaderByHashTxSizesAsync
    /// </summary>
    public sealed class GetBlockHeaderByHashTxSizeResult : IDisposable
    {
        /// <summary>
        /// Block Data
        /// </summary>
        public GetBlockDataResult<Header> Block { get; set; }
        
        /// <summary>
        /// List of transaction's hashes
        /// </summary>
        public HashList TransactionHashes { get; set; }
        
        /// <summary>
        /// Size of a serialized block
        /// </summary>
        public UInt64 SerializedBlockSize { get; set; }

        /// <summary>
        /// Dipose method for cleaning resources
        /// </summary>
        public void Dispose()
        {
            Block.Dispose();
            TransactionHashes.Dispose();
        }
    }

    /// <summary>
    /// Return type for FetchBlockByHeightHashTimestampAsync
    /// </summary>
    public class GetBlockHashTimestampResult
    {
        /// <summary>
        /// Block's hash 
        /// </summary>
        public byte[] BlockHash { get; set; }
        
        /// <summary>
        /// Block's TimeStamp
        /// </summary>
        public DateTime BlockTimestamp { get; set; }
    }

 
    /// <summary>
    /// Return type for FetchTransactionAsync
    /// </summary>
    public class GetTxDataResult : IDisposable
    {
        /// <summary>
        /// The requested transaction
        /// </summary>
        public Transaction Tx { get; set; }
        
        /// <summary>
        /// Block information
        /// </summary>
        public GetTxPositionResult TxPosition { get; set; }

        /// <summary>
        /// Dipose method for cleaning resources
        /// </summary>
        public void Dispose()
        {
            Tx.Dispose();
        }
    }
    
    /// <summary>
    /// Type used by GetTxDataResult. Has block information 
    /// </summary>
    public struct GetTxPositionResult
    {
        /// <summary>
        /// Transaction index inside a block
        /// </summary>
        public UInt64 Index { get; set; }
        
        /// <summary>
        /// Block's Height
        /// </summary>
        public UInt64 BlockHeight { get; set; }
    }

}