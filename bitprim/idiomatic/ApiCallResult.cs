using System;

namespace Bitprim
{
    /// <summary>
    /// Represents a result of calling a native API method
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
    /// Represents a disposable result of calling an API method. It's necessary to call Dispose when the result is not needed anymore
    /// </summary>
    /// <typeparam name="TResultData">The Result's type</typeparam>
    public sealed class DisposableApiCallResult<TResultData> : ApiCallResult<TResultData>, IDisposable where TResultData : IDisposable
    {
        /// <summary>
        /// Dispose method for resource cleanup
        /// </summary>
        public void Dispose()
        {
            Result.Dispose();
        }
    }

    /// <summary>
    /// Contains block information
    /// </summary>
    /// <typeparam name="TBlockData">Specific block data type</typeparam>
    public sealed class GetBlockDataResult<TBlockData> : IDisposable where TBlockData : IDisposable
    {
        /// <summary>
        /// Return the block's data
        /// </summary>
        public TBlockData BlockData { get; set; }
        
        /// <summary>
        /// The block's height
        /// </summary>
        public UInt64 BlockHeight;

        /// <summary>
        /// Dispose method for resource cleanup
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
        public GetBlockDataResult<IHeader> Header { get; set; }
        
        /// <summary>
        /// List of transaction hashes
        /// </summary>
        public INativeList<byte[]> TransactionHashes { get; set; }
        
        /// <summary>
        /// Serialized block size in bytes
        /// </summary>
        public UInt64 SerializedBlockSize { get; set; }

        /// <summary>
        /// Dispose method for resource cleanup
        /// </summary>
        public void Dispose()
        {
            Header.Dispose();
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
        public ITransaction Tx { get; set; }
        
        /// <summary>
        /// Transaction position as a block height - index pair
        /// </summary>
        public GetTxPositionResult TxPosition { get; set; }

        /// <summary>
        /// Dispose method for resource cleanup
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