using Bitprim.Native;
using System;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Threading;

namespace Bitprim
{
    /// <summary>
    /// Represents the Bitcoin blockchain; meant to offer its different interfaces (query, mining, network)
    /// </summary>
    public class Chain
    {
        public delegate void FetchBlockByHeightHashTimestampHandler(ErrorCode errorCode, byte[] blockHash, DateTime blockDate, UInt64 blockHeight);
        private delegate void FetchBlockHeaderByHashTxsSizeHandler(ErrorCode errorCode, Header blockHeader, UInt64 blockHeight, HashList txHashes, UInt64 serializedBlockSize);

        private IntPtr nativeInstance_;

        #region Chain

        
        /// <summary>
        /// Given a block hash, it queries the chain asynchronously for the block's height.
        /// Return right away and uses a callback to return the result.
        /// </summary>
        /// <param name="blockHash"> 32-byte array representation of the block hash.
        ///    Identifies it univocally.
        /// </param>
        public async Task<ApiCallResult<ulong>> FetchBlockHeightAsync(byte[] blockHash)
        {
            return await TaskHelper.ToTask(() =>
            {
                ApiCallResult<ulong> ret = null;
                FetchBlockHeight(blockHash, (code, height) =>
                {
                    ret = new ApiCallResult<ulong>
                    {
                        ErrorCode = code, 
                        Result = height
                    };
                });
                return ret;
            });
        }


        /// <summary>
        /// Given a block hash, it queries the chain asynchronously for the block's height.
        /// Return right away and uses a callback to return the result.
        /// </summary>
        /// <param name="blockHash"> 32-byte array representation of the block hash.
        ///    Identifies it univocally.
        /// </param>
        /// <param name="handler"> Callback which will be invoked when the block height is found. </param>
        private void FetchBlockHeight(byte[] blockHash, Action<ErrorCode, UInt64> handler)
        {
            var managedHash = new hash_t
            {
                hash = blockHash
            };
            IntPtr contextPtr = CreateContext(handler, managedHash);
            ChainNative.chain_fetch_block_height(nativeInstance_, contextPtr, managedHash, FetchBlockHeightInternalHandler);
        }

        /// <summary>
        /// Given a block hash, it queries the chain asynchronously for the block's height.
        /// Blocks until block height is retrieved.
        /// </summary>
        /// <param name="blockHash">  32-byte array representation of the block hash.
        ///    Identifies it univocally. </param>
        /// <returns> The block height </returns>
        private ApiCallResult<UInt64> GetBlockHeight(byte[] blockHash)
        {
            UInt64 height = 0;
            var managedHash = new hash_t
            {
                hash = blockHash
            };
            ErrorCode result = ChainNative.chain_get_block_height(nativeInstance_, managedHash, ref height);
            return new ApiCallResult<UInt64>{ ErrorCode = result, Result = height };
        }

        /// <summary>
        /// Gets the height of the highest block in the local copy of the blockchain, asynchronously.
        /// </summary>
        public async Task<ApiCallResult<ulong>> FetchLastHeightAsync()
        {
            return await TaskHelper.ToTask(() =>
            {
                ApiCallResult<ulong> ret = null;
                FetchLastHeight((code, height) =>
                {
                    ret = new ApiCallResult<ulong>
                    {
                        ErrorCode = code, 
                        Result = height
                    };
                });
                return ret;
            });
        }

        /// <summary>
        /// Gets the height of the highest block in the local copy of the blockchain, asynchronously.
        /// </summary>
        /// <param name="handler"> Callback which will be called once the last height is retrieved. </param>
        private void FetchLastHeight(Action<ErrorCode, UInt64> handler)
        {
            GCHandle handlerHandle = GCHandle.Alloc(handler);
            IntPtr handlerPtr = (IntPtr)handlerHandle;
            ChainNative.chain_fetch_last_height(nativeInstance_, handlerPtr, FetchLastHeightInternalHandler);
        }

        /// <summary>
        /// Gets the height of the highest block in the local copy of the blockchain, synchronously.
        /// It blocks until height is retrieved.
        /// </summary>
        /// <returns> Error code (0 = success) and height </returns>
        private ApiCallResult<UInt64> GetLastHeight()
        {
            UInt64 height = 0;
            ErrorCode result = ChainNative.chain_get_last_height(nativeInstance_, ref height);
            return new ApiCallResult<UInt64>{ ErrorCode = result, Result = height };
        }

        #endregion //Chain

        #region Block

        /// <summary>
        /// Given a block hash, retrieve the full block it identifies, asynchronously.
        /// </summary>
        /// <param name="blockHash"> 32 bytes of the block hash </param>
        public async Task<DisposableApiCallResult<Block>> FetchBlockByHashAsync(byte[] blockHash)
        {
            return await TaskHelper.ToTask(() =>
            {
                DisposableApiCallResult<Block> ret = null;
                FetchBlockByHash(blockHash, (code, block) =>
                    {
                        ret = new DisposableApiCallResult<Block> {ErrorCode = code, Result = block};
                    });
                return ret;
            });
        }


        /// <summary>
        /// Given a block hash, retrieve the full block it identifies, asynchronously.
        /// </summary>
        /// <param name="blockHash"> 32 bytes of the block hash </param>
        /// <param name="handler"> Callback which will be called when the block is retrieved. </param>
        private void FetchBlockByHash(byte[] blockHash, Action<ErrorCode, Block> handler)
        {
            var managedHash = new hash_t
            {
                hash = blockHash
            };
            IntPtr contextPtr = CreateContext(handler, managedHash);
            ChainNative.chain_fetch_block_by_hash(nativeInstance_, contextPtr, managedHash, FetchBlockByHashInternalHandler);
        }


        /// <summary>
        /// Given a block hash, get the full block it identifies, synchronously.
        /// </summary>
        /// <param name="blockHash"> 32 bytes of the block hash </param>
        /// <returns> Error code and full block </returns>
        private DisposableApiCallResult<GetBlockDataResult<Block>> GetBlockByHash(byte[] blockHash)
        {
            IntPtr block = IntPtr.Zero;
            UInt64 height = 0;
            var managedHash = new hash_t
            {
                hash = blockHash
            };
            ErrorCode result = ChainNative.chain_get_block_by_hash(nativeInstance_, managedHash, ref block, ref height);
            return new DisposableApiCallResult<GetBlockDataResult<Block>>
            {
                ErrorCode = result,
                Result = new GetBlockDataResult<Block>{ BlockData = new Block(block), BlockHeight = height }
            };
        }


        /// <summary>
        /// Given a block hash, retrieve block header, tx hashes and serialized block size, asynchronously.
        /// </summary>
        /// <param name="blockHash"> 32 bytes of the block hash </param>
        public async Task<DisposableApiCallResult<GetBlockHeaderByHashTxSizeResult>> FetchBlockHeaderByHashTxSizesAsync(byte[] blockHash)
        {
            return await TaskHelper.ToTask(() =>
            {
                DisposableApiCallResult<GetBlockHeaderByHashTxSizeResult> ret = null;
                FetchBlockHeaderByHashTxSizes(blockHash, (errorCode, header, height, hashes, size) =>
                {
                    ret = new DisposableApiCallResult<GetBlockHeaderByHashTxSizeResult>
                    {
                        ErrorCode = errorCode,
                        Result = new GetBlockHeaderByHashTxSizeResult
                        {
                            Block = new GetBlockDataResult<Header> {BlockData = header, BlockHeight = height},
                            TransactionHashes = hashes,
                            SerializedBlockSize = size
                        }
                    };
                });
                return ret;
            });
        }

        /// <summary>
        /// Given a block hash, retrieve block header, tx hashes and serialized block size, asynchronously.
        /// </summary>
        /// <param name="blockHash"> 32 bytes of the block hash </param>
        /// <param name="handler"> Callback which will be called when the data is retrieved. </param>
        private void FetchBlockHeaderByHashTxSizes(byte[] blockHash, FetchBlockHeaderByHashTxsSizeHandler handler)
        {
            var managedHash = new hash_t
            {
                hash = blockHash
            };
            IntPtr contextPtr = CreateContext(handler, managedHash);
            ChainNative.chain_fetch_block_header_by_hash_txs_size(nativeInstance_, contextPtr, managedHash, FetchBlockHeaderByHashTxsSizeInternalHandler);
        }

        /// <summary>
        /// Given a block hash, retrieve block header, tx hashes and serialized block size, synchronously.
        /// </summary>
        /// <param name="blockHash"> 32 bytes of the block hash. </param>
        /// <returns> Error code, block, block height, tx hashes, serialized block size. </returns>
        private DisposableApiCallResult<GetBlockHeaderByHashTxSizeResult> GetBlockHeaderByHashTxSizes(byte[] blockHash)
        {
            var managedHash = new hash_t
            {
                hash = blockHash
            };
            IntPtr blockHeader = IntPtr.Zero;
            UInt64 blockHeight = 0;
            IntPtr txHashes = IntPtr.Zero;
            UInt64 serializedBlockSize = 0;
            ErrorCode result = ChainNative.chain_get_block_header_by_hash_txs_size
            (
                nativeInstance_, managedHash, ref blockHeader,
                ref blockHeight, ref txHashes, ref serializedBlockSize
            );
            return result == ErrorCode.Success?
                new DisposableApiCallResult<GetBlockHeaderByHashTxSizeResult>{ ErrorCode = result, Result = new GetBlockHeaderByHashTxSizeResult
                    {
                        Block = new GetBlockDataResult<Header>{ BlockData =  new Header(blockHeader), BlockHeight = blockHeight },
                        TransactionHashes = new HashList(txHashes),
                        SerializedBlockSize = serializedBlockSize
                    }}:
                new DisposableApiCallResult<GetBlockHeaderByHashTxSizeResult>{ ErrorCode = result, Result = null };
        }


        /// <summary>
        /// Given a block height, retrieve the full block it identifies, asynchronously.
        /// </summary>
        /// <param name="height"> Block height </param>
        public async Task<DisposableApiCallResult<Block>> FetchBlockByHeightAsync(ulong height)
        {
            return await TaskHelper.ToTask(() =>
            {
                DisposableApiCallResult<Block> ret = null;
                FetchBlockByHeight(height, (code, block) =>
                    {
                        ret = new DisposableApiCallResult<Block> {ErrorCode = code, Result = block};
                    });
                return ret;
            });
            
        }


        /// <summary>
        /// Given a block height, retrieve the full block it identifies, asynchronously.
        /// </summary>
        /// <param name="height"> Block height </param>
        /// <param name="handler"> Callback which will be called when the block is retrieved. </param>
        private void FetchBlockByHeight(UInt64 height, Action<ErrorCode, Block> handler)
        {
            GCHandle handlerHandle = GCHandle.Alloc(handler);
            IntPtr handlerPtr = (IntPtr)handlerHandle;
            ChainNative.chain_fetch_block_by_height(nativeInstance_, handlerPtr, height, FetchBlockByHeightInternalHandler);
        }

        /// <summary>
        /// Given a block height, get the full block it identifies, synchronously.
        /// </summary>
        /// <param name="height"> Block height </param>
        /// <returns> Error code and full block </returns>
        private DisposableApiCallResult<GetBlockDataResult<Block>> GetBlockByHeight(UInt64 height)
        {
            IntPtr block = IntPtr.Zero;
            UInt64 actualHeight = 0; //Should always match input height
            ErrorCode result = ChainNative.chain_get_block_by_height(nativeInstance_, height, ref block, ref actualHeight);
            return new DisposableApiCallResult<GetBlockDataResult<Block>>
            {
                ErrorCode = result,
                Result = new GetBlockDataResult<Block>{ BlockData = new Block(block), BlockHeight = actualHeight }
            };
        }


        /// <summary>
        /// Given a block height, retrieve only block hash and timestamp, asynchronously.
        /// </summary>
        /// <param name="height"> Block height </param>
        public async Task<ApiCallResult<GetBlockHashTimestampResult>> FetchBlockByHeightHashTimestampAsync(ulong height)
        {
            return await TaskHelper.ToTask(() =>
            {
                ApiCallResult<GetBlockHashTimestampResult> ret = null;

                FetchBlockByHeightHashTimestamp(height, (errorCode, hash, date, blockHeight) =>
                {
                    ret = new ApiCallResult<GetBlockHashTimestampResult>
                    {
                        ErrorCode = errorCode,
                        Result = new GetBlockHashTimestampResult
                        {
                            BlockHash = hash,
                            BlockTimestamp = date
                        }
                    };
                });

                return ret;
            });
        }



        /// <summary>
        /// Given a block height, retrieve only block hash and timestamp, asynchronously.
        /// </summary>
        /// <param name="height"> Block height </param>
        /// <param name="handler"> Callback which will be called when the block data is retrieved. </param>
        private void FetchBlockByHeightHashTimestamp(UInt64 height, FetchBlockByHeightHashTimestampHandler handler)
        {
            GCHandle handlerHandle = GCHandle.Alloc(handler);
            IntPtr handlerPtr = (IntPtr)handlerHandle;
            ChainNative.chain_fetch_block_by_height_timestamp(nativeInstance_, handlerPtr, height, FetchBlockByHeightHashTimestampInternalHandler);
        }

        /// <summary>
        /// Given a block height, retrieve only its block hash and timestamp, synchronously.
        /// </summary>
        /// <param name="height"> Block height </param>
        /// <returns> Error code, block hash and block timestamp. </returns>
        private ApiCallResult<GetBlockHashTimestampResult> GetBlockByHeightHashTimestamp(UInt64 height)
        {
            var blockHash = new hash_t();
            UInt32 blockTimestamp = 0;
            UInt64 blockHeight = 0;
            ErrorCode result = ChainNative.chain_get_block_by_height_timestamp(nativeInstance_, height, ref blockHash, ref blockTimestamp, ref blockHeight);
            return new ApiCallResult<GetBlockHashTimestampResult>
            {
                ErrorCode = result,
                Result = new GetBlockHashTimestampResult
                {
                    BlockHash = blockHash.hash,
                    BlockTimestamp = DateTimeOffset.FromUnixTimeSeconds(blockTimestamp).UtcDateTime
                }
            };
        }

        

        /// <summary>
        /// Given a block height, get just the block hash, synchronously.
        /// </summary>
        /// <param name="height"> Block height. </param>
        /// <returns> Error code and block hash. </returns>
        private ApiCallResult<byte[]> GetBlockHash(UInt64 height)
        {
            var blockHash = new hash_t();
            ErrorCode result = ChainNative.chain_get_block_hash(nativeInstance_, height, ref blockHash);
            return result == ErrorCode.Success?
                new ApiCallResult<byte[]>{ ErrorCode = result, Result = blockHash.hash }:
                new ApiCallResult<byte[]>{ ErrorCode = result, Result = null };
        }

        #endregion //Block

        #region Block header


        /// <summary>
        /// Given a block hash, get the header from the block it identifies, asynchronously.
        /// </summary>
        /// <param name="blockHash"> 32 bytes of the block hash </param>
        public async Task<DisposableApiCallResult<Header>> FetchBlockHeaderByHashAsync(byte[] blockHash)
        {
            return await TaskHelper.ToTask(() =>
            {
                DisposableApiCallResult<Header> ret = null;

                FetchBlockHeaderByHash(blockHash, (code, header) =>
                {
                    ret = new DisposableApiCallResult<Header>
                    {
                        ErrorCode = code,
                        Result = header
                    };
                });

                return ret;
            });
        }

        /// <summary>
        /// Given a block hash, get the header from the block it identifies, asynchronously.
        /// </summary>
        /// <param name="blockHash"> 32 bytes of the block hash </param>
        /// <param name="handler"> Callback which will be called when the header is retrieved </param>
        private void FetchBlockHeaderByHash(byte[] blockHash, Action<ErrorCode, Header> handler)
        {
            var managedHash = new hash_t
            {
                hash = blockHash
            };
            IntPtr contextPtr = CreateContext(handler, managedHash);
            ChainNative.chain_fetch_block_header_by_hash(nativeInstance_, contextPtr, managedHash, FetchBlockHeaderByHashInternalHandler);
        }

        /// <summary>
        /// Given a block hash, get the header from the block it identifies, synchronously.
        /// </summary>
        /// <param name="blockHash"> 32 bytes of the block hash </param>
        /// <returns> Error code, full block header and block height </returns>
        private DisposableApiCallResult<GetBlockDataResult<Header>> GetBlockHeaderByHash(byte[] blockHash)
        {
            IntPtr header = IntPtr.Zero;
            UInt64 height = 0;
            var managedHash = new hash_t
            {
                hash = blockHash
            };
            ErrorCode result = ChainNative.chain_get_block_header_by_hash(nativeInstance_, managedHash, ref header, ref height);
            return new DisposableApiCallResult<GetBlockDataResult<Header>>
            {
                ErrorCode = result,
                Result = new GetBlockDataResult<Header>{ BlockData = new Header(header), BlockHeight = height }
            };
        }
        

        /// <summary>
        /// Given a block height, get the header from the block it identifies, asynchronously.
        /// </summary>
        /// <param name="height"> Block height </param>
        public async Task<DisposableApiCallResult<GetBlockDataResult<Header>>> FetchBlockHeaderByHeightAsync(UInt64 height)
        {
            return await TaskHelper.ToTask(() =>
            {
                DisposableApiCallResult<GetBlockDataResult<Header>> ret = null;

                FetchBlockHeaderByHeight(height, (code, header) =>
                {
                    ret = new DisposableApiCallResult<GetBlockDataResult<Header>>
                    {
                        ErrorCode = code,
                        Result = new GetBlockDataResult<Header>
                        {
                            BlockData = header, BlockHeight = height
                        }
                    };
                });

                return ret;
            });
        }



        /// <summary>
        /// Given a block height, get the header from the block it identifies, asynchronously.
        /// </summary>
        /// <param name="height"> Block height </param>
        /// <param name="handler"> Callback which will be invoked when the block header is retrieved </param>
        private void FetchBlockHeaderByHeight(UInt64 height, Action<ErrorCode, Header> handler)
        {
            GCHandle handlerHandle = GCHandle.Alloc(handler);
            IntPtr handlerPtr = (IntPtr)handlerHandle;
            ChainNative.chain_fetch_block_header_by_height(nativeInstance_, handlerPtr, height, FetchBlockHeaderbyHeightInternalHandler);
        }

        /// <summary>
        /// Given a block height, get the header from the block it identifies, synchronously.
        /// </summary>
        /// <param name="height"> Block height </param>
        /// <returns> Error code, full block header, and height </returns>
        private DisposableApiCallResult<GetBlockDataResult<Header>> GetBlockHeaderByHeight(UInt64 height)
        {
            IntPtr header = IntPtr.Zero;
            UInt64 actualHeight = 0; //Should always match input height
            ErrorCode result = ChainNative.chain_get_block_header_by_height(nativeInstance_, height, ref header, ref actualHeight);
            return new DisposableApiCallResult<GetBlockDataResult<Header>>
            {
                ErrorCode = result,
                Result = new GetBlockDataResult<Header>{ BlockData = new Header(header), BlockHeight = actualHeight }
            };
        }

        #endregion //Block header

        #region Merkle Block

        /// <summary>
        /// Given a block hash, get the merkle block from the block it identifies, asynchronously.
        /// </summary>
        /// <param name="blockHash"> 32 bytes of the block hash </param>
        public async Task<DisposableApiCallResult<GetBlockDataResult<MerkleBlock>>> FetchMerkleBlockByHashAsync(byte[] blockHash)
        {
            return await TaskHelper.ToTask(() =>
            {
                DisposableApiCallResult<GetBlockDataResult<MerkleBlock>> ret = null;

                FetchMerkleBlockByHash(blockHash, (code, merkleBlock, height) =>
                {
                    ret = new DisposableApiCallResult<GetBlockDataResult<MerkleBlock>>
                    {
                        ErrorCode = code,
                        Result = new GetBlockDataResult<MerkleBlock>
                        {
                            BlockData = merkleBlock,
                            BlockHeight = height
                        }
                    };
                });

                return ret;
            });
        }

        /// <summary>
        /// Given a block hash, get the merkle block from the block it identifies, asynchronously.
        /// </summary>
        /// <param name="blockHash"> 32 bytes of the block hash </param>
        /// <param name="handler"> Callback which will be invoked when the Merkle block is retrieved </param>
        private void FetchMerkleBlockByHash(byte[] blockHash, Action<ErrorCode, MerkleBlock, UInt64> handler)
        {
            var managedHash = new hash_t
            {
                hash = blockHash
            };
            IntPtr contextPtr = CreateContext(handler, managedHash);
            ChainNative.chain_fetch_merkle_block_by_hash(nativeInstance_, contextPtr, managedHash, FetchMerkleBlockByHashInternalHandler);
        }

        /// <summary>
        /// Given a block hash, get the merkle block from the block it identifies, synchronously.
        /// </summary>
        /// <param name="blockHash"> 32 bytes of the block hash </param>
        /// <returns> Error code, full Merkle block and height </returns>
        private DisposableApiCallResult<GetBlockDataResult<MerkleBlock>> GetMerkleBlockByHash(byte[] blockHash)
        {
            IntPtr merkleBlock = IntPtr.Zero;
            UInt64 height = 0;
            var managedHash = new hash_t
            {
                hash = blockHash
            };
            ErrorCode result = ChainNative.chain_get_merkle_block_by_hash(nativeInstance_, managedHash, ref merkleBlock, ref height);
            return new DisposableApiCallResult<GetBlockDataResult<MerkleBlock>>
            {
                ErrorCode = result,
                Result = new GetBlockDataResult<MerkleBlock>{ BlockData = new MerkleBlock(merkleBlock), BlockHeight = height }
            };
        }

        /// <summary>
        /// Given a block height, get the merkle block from the block it identifies, asynchronously.
        /// </summary>
        /// <param name="height"> Desired block height </param>
        public async Task<DisposableApiCallResult<GetBlockDataResult<MerkleBlock>>> FetchMerkleBlockByHeightAsync(UInt64 height)
        {
            return await TaskHelper.ToTask(() =>
            {
                DisposableApiCallResult<GetBlockDataResult<MerkleBlock>> ret = null;

                FetchMerkleBlockByHeight(height, (code, merkleBlock, actualHeight) =>
                {
                    ret = new DisposableApiCallResult<GetBlockDataResult<MerkleBlock>>
                    {
                        ErrorCode = code,
                        Result = new GetBlockDataResult<MerkleBlock>
                        {
                            BlockData = merkleBlock,
                            BlockHeight = actualHeight
                        }
                    };
                });

                return ret;
            });
        }



        /// <summary>
        /// Given a block height, get the merkle block from the block it identifies, asynchronously.
        /// </summary>
        /// <param name="height"> Desired block height </param>
        /// <param name="handler"> Callback which will be invoked when the Merkle block is retrieved </param>
        private void FetchMerkleBlockByHeight(UInt64 height, Action<ErrorCode, MerkleBlock, UInt64> handler)
        {
            GCHandle handlerHandle = GCHandle.Alloc(handler);
            IntPtr handlerPtr = (IntPtr)handlerHandle;
            ChainNative.chain_fetch_merkle_block_by_height(nativeInstance_, handlerPtr, height, FetchMerkleBlockByHeightInternalHandler);
        }

        /// <summary>
        /// Given a block height, get the merkle block from the block it identifies, synchronously.
        /// </summary>
        /// <param name="height"> Desired block height </param>
        /// <returns> Error code, full Merkle block and height </returns>
        private DisposableApiCallResult<GetBlockDataResult<MerkleBlock>> GetMerkleBlockByHeight(UInt64 height)
        {
            IntPtr merkleBlock = IntPtr.Zero;
            UInt64 actualHeight = 0; //Should always match input height
            ErrorCode result = ChainNative.chain_get_merkle_block_by_height(nativeInstance_, height, ref merkleBlock, ref actualHeight);
            return new DisposableApiCallResult<GetBlockDataResult<MerkleBlock>>
            {
                ErrorCode = result,
                Result = new GetBlockDataResult<MerkleBlock>{ BlockData = new MerkleBlock(merkleBlock), BlockHeight = actualHeight }
            };
        }

        #endregion //Merkle Block

        #region Compact block

        /// <summary>
        /// Given a block hash, get the compact block from the block it identifies, asynchronously.
        /// </summary>
        /// <param name="blockHash"> 32 bytes of the block hash </param>
        /// <param name="handler"> Callback which will be invoked when the compact block is retrieved </param>
        public void FetchCompactBlockByHash(byte[] blockHash, Action<ErrorCode, CompactBlock> handler)
        {
            var managedHash = new hash_t
            {
                hash = blockHash
            };
            IntPtr contextPtr = CreateContext(handler, managedHash);
            ChainNative.chain_fetch_compact_block_by_hash(nativeInstance_, contextPtr, managedHash, FetchCompactBlockByHashInternalHandler);
        }

        /// <summary>
        /// Given a block hash, get the compact block from the block it identifies, synchronously.
        /// </summary>
        /// <param name="blockHash"> 32 bytes of the block hash </param>
        /// <returns> Error code, full compact block and height </returns>
        public DisposableApiCallResult<GetBlockDataResult<CompactBlock>> GetCompactBlockByHash(byte[] blockHash)
        {
            IntPtr compactBlock = IntPtr.Zero;
            UInt64 height = 0;
            var managedHash = new hash_t
            {
                hash = blockHash
            };
            ErrorCode errorCode = ChainNative.chain_get_compact_block_by_hash(nativeInstance_, managedHash, ref compactBlock, ref height);
            return new DisposableApiCallResult<GetBlockDataResult<CompactBlock>>
            {
                ErrorCode = errorCode,
                Result = new GetBlockDataResult<CompactBlock>{ BlockData = new CompactBlock(compactBlock), BlockHeight = height }
            };
        }

        /// <summary>
        /// Given a block height, get the compact block from the block it identifies, asynchronously.
        /// </summary>
        /// <param name="height"> Desired block height </param>
        /// <param name="handler"> Callback which will be invoked when the compact block is retrieved </param>
        public void FetchCompactBlockByHeight(UInt64 height, Action<ErrorCode, CompactBlock> handler)
        {
            GCHandle handlerHandle = GCHandle.Alloc(handler);
            IntPtr handlerPtr = (IntPtr)handlerHandle;
            ChainNative.chain_fetch_compact_block_by_height(nativeInstance_, handlerPtr, height, FetchCompactBlockByHeightInternalHandler);
        }

        /// <summary>
        /// Given a block height, get the compact block from the block it identifies, synchronously.
        /// </summary>
        /// <param name="height"> Desired block height </param>
        /// <returns> Error code, full compact block and height </returns>
        public DisposableApiCallResult<GetBlockDataResult<CompactBlock>> GetCompactBlockByHeight(UInt64 height)
        {
            IntPtr compactBlock = IntPtr.Zero;
            UInt64 actualHeight = 0; //Should always match input height
            ErrorCode errorCode = ChainNative.chain_get_compact_block_by_height(nativeInstance_, height, ref compactBlock, ref actualHeight);
            return new DisposableApiCallResult<GetBlockDataResult<CompactBlock>>
            {
                ErrorCode = errorCode,
                Result = new GetBlockDataResult<CompactBlock>{ BlockData = new CompactBlock(compactBlock), BlockHeight = actualHeight }
            };
        }

        #endregion //Compact block

        #region Transaction

        /// <summary>
        /// Get a transaction by its hash, asynchronously.
        /// </summary>
        /// <param name="txHash"> 32 bytes of transaction hash </param>
        /// <param name="requireConfirmed"> True iif the transaction must belong to a block </param>
        /// <param name="handler"> Callback which will be invoked when the transaction is retrieved </param>
        public void FetchTransaction(byte[] txHash, bool requireConfirmed, Action<ErrorCode, Transaction, UInt64, UInt64> handler)
        {
            var managedHash = new hash_t
            {
                hash = txHash
            };
            IntPtr contextPtr = CreateContext(handler, managedHash);
            ChainNative.chain_fetch_transaction(nativeInstance_, contextPtr, managedHash, requireConfirmed ? 1 : 0, FetchTransactionByHashInternalHandler);
        }

        /// <summary>
        /// Get a transaction by its hash, synchronously.
        /// </summary>
        /// <param name="txHash"> 32 bytes of transaction hash </param>
        /// <param name="requireConfirmed"> True iif the transaction must belong to a block </param>
        /// <returns> Error code, full transaction, index inside block and height </returns>
        public DisposableApiCallResult<GetTxDataResult> GetTransaction(byte[] txHash, bool requireConfirmed)
        {
            IntPtr transaction = IntPtr.Zero;
            UInt64 index = 0;
            UInt64 height = 0;
            var managedHash = new hash_t
            {
                hash = txHash
            };
            ErrorCode errorCode = ChainNative.chain_get_transaction(nativeInstance_, managedHash, requireConfirmed ? 1 : 0, ref transaction, ref index, ref height);
            return new DisposableApiCallResult<GetTxDataResult>
            {
                ErrorCode = errorCode,
                Result = new GetTxDataResult
                {
                     Tx = new Transaction(transaction),
                     TxPosition = new GetTxPositionResult{ Index = index, BlockHeight = height }
                }
            };
        }

        /// <summary>
        /// Given a transaction hash, it fetches the height and position inside the block, asynchronously.
        /// </summary>
        /// <param name="txHash"> 32 bytes of transaction hash </param>
        /// <param name="requireConfirmed"> True iif the transaction must belong to a block </param>
        /// <param name="handler"> Callback which will be invoked when the transaction position is retrieved </param>
        public void FetchTransactionPosition(byte[] txHash, bool requireConfirmed, Action<ErrorCode, UInt64, UInt64> handler)
        {
            var managedHash = new hash_t
            {
                hash = txHash
            };
            IntPtr contextPtr = CreateContext(handler, managedHash);
            ChainNative.chain_fetch_transaction_position(nativeInstance_, contextPtr, managedHash, requireConfirmed ? 1 : 0, FetchTransactionPositionInternalHandler);
        }

        /// <summary>
        /// Given a transaction hash, it fetches the height and position inside the block, synchronously.
        /// </summary>
        /// <param name="txHash"> 32 bytes of transaction hash </param>
        /// <param name="requireConfirmed"> True iif the transaction must belong to a block </param>
        /// <returns> Error code, index in block (zero based) and block height </returns>
        public ApiCallResult<GetTxPositionResult> GetTransactionPosition(byte[] txHash, bool requireConfirmed)
        {
            UInt64 index = 0;
            UInt64 height = 0;
            var managedHash = new hash_t
            {
                hash = txHash
            };
            ErrorCode errorCode = ChainNative.chain_get_transaction_position(nativeInstance_, managedHash, requireConfirmed ? 1 : 0, ref index, ref height);
            return new ApiCallResult<GetTxPositionResult>
            {
                ErrorCode = errorCode,
                Result = new GetTxPositionResult{ Index = index, BlockHeight = height }
            };
        }

        #endregion //Transaction

        #region Spend

        /// <summary>
        /// Fetch the transaction input which spends the indicated output, asynchronously.
        /// </summary>
        /// <param name="outputPoint"> Tx hash and index pair where the output was spent. </param>
        /// <param name="handler"> Callback which will be called when spend is retrieved </param>
        public void FetchSpend(OutputPoint outputPoint, Action<ErrorCode, Point> handler)
        {
            IntPtr contextPtr = CreateContext(handler, outputPoint);
            ChainNative.chain_fetch_spend(nativeInstance_, contextPtr, outputPoint.NativeInstance, FetchSpendInternalHandler);
        }

        /// <summary>
        /// Get the transaction input which spends the indicated output, synchronously.
        /// </summary>
        /// <param name="outputPoint"> Tx hash and index pair where the output was spent. </param>
        /// <returns> Error code and output point </returns>
        public ApiCallResult<Point> GetSpend(OutputPoint outputPoint)
        {
            //TODO When node-cint wraps a get function for this, call that instead
            var handlerDone = new AutoResetEvent(false);
            ErrorCode error = 0;
            Point point = null;
            Action<ErrorCode, Point> handler = delegate(ErrorCode theError, Point thePoint)
            {
                error = theError;
                point = thePoint;
                handlerDone.Set();
            };
            FetchSpend(outputPoint, handler);
            handlerDone.WaitOne();
            return new ApiCallResult<Point>{ ErrorCode = error, Result = point };
        }

        #endregion //Spend

        #region History

        /// <summary>
        /// Get a list of output points, values, and spends for a given payment address (asynchronously)
        /// </summary>
        /// <param name="address"> Bitcoin payment address to search </param>
        /// <param name="limit"> Maximum amount of results to fetch </param>
        /// <param name="fromHeight"> Starting point to search for transactions </param>
        /// <param name="handler"> Callback which will be called when the history is retrieved </param>
        public void FetchHistory(PaymentAddress address, UInt64 limit, UInt64 fromHeight, Action<ErrorCode, HistoryCompactList> handler)
        {
            GCHandle handlerHandle = GCHandle.Alloc(handler);
            IntPtr handlerPtr = (IntPtr)handlerHandle;
            ChainNative.chain_fetch_history(nativeInstance_, handlerPtr, address.NativeInstance, limit, fromHeight, FetchHistoryInternalHandler);
        }

        /// <summary>
        /// Get a list of output points, values, and spends for a given payment address (synchronously)
        /// </summary>
        /// <param name="address"> Bitcoin payment address to search </param>
        /// <param name="limit"> Maximum amount of results to fetch </param>
        /// <param name="fromHeight"> Starting point to search for transactions </param>
        /// <returns> Error code, HistoryCompactList </returns>
        public DisposableApiCallResult<HistoryCompactList> GetHistory(PaymentAddress address, UInt64 limit, UInt64 fromHeight)
        {
            IntPtr history = IntPtr.Zero;
            ErrorCode errorCode = ChainNative.chain_get_history(nativeInstance_, address.NativeInstance, limit, fromHeight, ref history);
            return new DisposableApiCallResult<HistoryCompactList>{ ErrorCode = errorCode, Result = new HistoryCompactList(history) };
        }

        #endregion //History

        #region Stealth

        /// <summary>
        /// Get metadata on potential payment transactions by stealth filter. Given a filter and a
        /// height in the chain, it queries the chain for transactions matching the given filter.
        /// </summary>
        /// <param name="filter"> Must be at least 8 bits in length. example "10101010" </param>
        /// <param name="fromHeight"> Starting height in the chain to search for transactions </param>
        /// <param name="handler"> Callback which will be called when the stealth list is retrieved </param>
        public void FetchStealth(Binary filter, UInt64 fromHeight, Action<ErrorCode, StealthCompactList> handler)
        {
            IntPtr contextPtr = CreateContext(handler, filter);
            ChainNative.chain_fetch_stealth(nativeInstance_, contextPtr, filter.NativeInstance, fromHeight, FetchStealthInternalHandler);
        }

        #endregion //Stealth

        #region Block indexes

        /// <summary>
        /// Given a list of indexes, fetch a header reader for them, asynchronously
        /// </summary>
        /// <param name="indexes"> Block indexes </param>
        /// <param name="handler"> Callback which will called when the reader is retrieved </param>
        public void FetchBlockLocator(BlockIndexList indexes, Action<ErrorCode, HeaderReader> handler)
        {
            GCHandle handlerHandle = GCHandle.Alloc(handler);
            IntPtr handlerPtr = (IntPtr)handlerHandle;
            ChainNative.chain_fetch_block_locator(nativeInstance_, handlerPtr, indexes.NativeInstance, FetchBlockLocatorInternalHandler);
        }

        /// <summary>
        /// Given a list of indexes, fetch a header reader for them, synchronously
        /// </summary>
        /// <param name="indexes"> Block indexes </param>
        /// <returns> Error code, HeaderReader </returns>
        public DisposableApiCallResult<HeaderReader> GetBlockLocator(BlockIndexList indexes)
        {
            IntPtr headerReader = IntPtr.Zero;
            ErrorCode errorCode = ChainNative.chain_get_block_locator(nativeInstance_, indexes.NativeInstance, ref headerReader);
            return new DisposableApiCallResult<HeaderReader>{ ErrorCode = errorCode, Result = new HeaderReader(headerReader) };
        }

        #endregion //Block indexes

        #region Organizers

        /// <summary>
        /// Given a block, organize it (async).
        /// </summary>
        /// <param name="block"> The block to organize </param>
        /// <param name="handler"> Callback which will be called when organization is complete. </param>
        public void OrganizeBlock(Block block, Action<int> handler)
        {
            GCHandle handlerHandle = GCHandle.Alloc(handler);
            IntPtr handlerPtr = (IntPtr)handlerHandle;
            ChainNative.chain_organize_block(nativeInstance_, handlerPtr, block.NativeInstance, ResultInternalHandler);
        }

        /// <summary>
        /// Given a block, organize it (sync).
        /// </summary>
        /// <param name="block"> The block to organize. </param>
        /// <returns> Error code </returns>
        public ErrorCode OrganizeBlockSync(Block block)
        {
            return ChainNative.chain_organize_block_sync(nativeInstance_, block.NativeInstance);
        }

        /// <summary>
        /// Given a transaction, organize it (async).
        /// </summary>
        /// <param name="transaction"> The transaction to organize. </param>
        /// <param name="handler"> Callback which will be called when organization is complete. </param>
        public void OrganizeTransaction(Transaction transaction, Action<int> handler)
        {
            GCHandle handlerHandle = GCHandle.Alloc(handler);
            IntPtr handlerPtr = (IntPtr)handlerHandle;
            ChainNative.chain_organize_transaction(nativeInstance_, handlerPtr, transaction.NativeInstance, ResultInternalHandler);
        }

        /// <summary>
        /// Given a transaction, organize it (sync)
        /// </summary>
        /// <param name="transaction"> The transaction to organize </param>
        /// <returns> Error code </returns>
        public ErrorCode OrganizeTransactionSync(Transaction transaction)
        {
            return ChainNative.chain_organize_transaction_sync(nativeInstance_, transaction.NativeInstance);
        }

        #endregion //Organizers

        #region Misc

        /// <summary>
        /// Determine if a transaction is valid for submission to the blockchain.
        /// </summary>
        /// <param name="transaction"> Transaction to validate </param>
        /// <param name="handler"> Callback which will be called when validation is complete. </param>
        public void ValidateTransaction(Transaction transaction, Action handler)
        {
            GCHandle handlerHandle = GCHandle.Alloc(handler);
            IntPtr handlerPtr = (IntPtr)handlerHandle;
            ChainNative.chain_validate_tx(nativeInstance_, handlerPtr, transaction.NativeInstance, ValidateTransactionInternalHandler);
        }

        /// <summary>
        /// Determine if the node is synchronized (i.e. has the latest copy of the blockchain/is at top height)
        /// Criterion: no nodes from the last 48 hs.
        /// </summary>
        public bool IsStale
        {
            get
            {
                return ChainNative.chain_is_stale(nativeInstance_) != 0;
            }
        }

        #endregion //Misc

        internal Chain(IntPtr nativeInstance)
        {
            nativeInstance_ = nativeInstance;
        }

        internal IntPtr NativeInstance
        {
            get
            {
                return nativeInstance_;
            }
        }

        private IntPtr CreateContext<C, P>(C callback, P parameters)
        {
            // Both the callback and its parameters need to hold garbage collection off until
            // the callback is called, so a GCHandle is taken for an object containing both of them:
            // that is the context
            var context = new Tuple<C, P>(callback, parameters);
            GCHandle contextHandle = GCHandle.Alloc(context);
            return (IntPtr)contextHandle;
        }

        private static void FetchBlockByHashInternalHandler(IntPtr chain, IntPtr contextPtr, ErrorCode error,
                                                            IntPtr block, UInt64 height)
        {
            GCHandle contextHandle = (GCHandle)contextPtr;
            try
            {
                var context = (contextHandle.Target as Tuple<Action<ErrorCode, Block>, hash_t>);
                Action<ErrorCode, Block> handler = context.Item1;
                handler(error, new Block(block));
            }
            finally
            {
                contextHandle.Free();
            }
        }

        private static void FetchBlockHeaderByHashTxsSizeInternalHandler(IntPtr chain, IntPtr contextPtr, ErrorCode error,
                                                                         IntPtr blockHeader, UInt64 blockHeight, IntPtr txHashes,
                                                                         UInt64 blockSerializedSize)
        {
            GCHandle contextHandle = (GCHandle)contextPtr;
            try
            {
                var context = (contextHandle.Target as Tuple<FetchBlockHeaderByHashTxsSizeHandler, hash_t>);
                FetchBlockHeaderByHashTxsSizeHandler handler = context.Item1;
                handler(error, new Header(blockHeader), blockHeight, new HashList(txHashes), blockSerializedSize);
            }
            finally
            {
                contextHandle.Free();
            }
        }

        private static void FetchBlockByHeightHashTimestampInternalHandler(IntPtr chain, IntPtr context, ErrorCode error,
                                                                           hash_t blockHash, UInt32 timestamp, UInt64 height)
        {
            GCHandle handlerHandle = (GCHandle)context;
            try
            {
                var handler = (handlerHandle.Target as FetchBlockByHeightHashTimestampHandler);
                //Copy native memory before it goes out of scope
                byte[] blockHashCopy = new byte[blockHash.hash.Length];
                blockHash.hash.CopyTo(blockHashCopy, 0);
                //Convert Unix timestamp to date
                DateTime blockDate = DateTimeOffset.FromUnixTimeSeconds(timestamp).UtcDateTime;
                handler(error, blockHashCopy, blockDate, height);
            }
            finally
            {
                handlerHandle.Free();
            }
        }

        private static void FetchBlockByHeightInternalHandler(IntPtr chain, IntPtr context, ErrorCode error,
                                                              IntPtr block, UInt64 height)
        {
            GCHandle handlerHandle = (GCHandle)context;
            try
            {
                Action<ErrorCode, Block> handler = (handlerHandle.Target as Action<ErrorCode, Block>);
                handler(error, new Block(block));
            }
            finally
            {
                handlerHandle.Free();
            }
        }

        private static void FetchBlockHeaderByHashInternalHandler(IntPtr chain, IntPtr contextPtr, ErrorCode error,
                                                                  IntPtr header, UInt64 height)
        {
            GCHandle contextHandle = (GCHandle)contextPtr;
            try
            {
                var context = (contextHandle.Target as Tuple<Action<ErrorCode, Header>, hash_t>);
                Action<ErrorCode, Header> handler = context.Item1;
                handler(error, new Header(header));
            }
            finally
            {
                contextHandle.Free();
            }
        }

        private static void FetchBlockHeaderbyHeightInternalHandler(IntPtr chain, IntPtr context, ErrorCode error,
                                                                    IntPtr header, UInt64 height)
        {
            GCHandle handlerHandle = (GCHandle)context;
            try
            {
                var handler = (handlerHandle.Target as Action<ErrorCode, Header>);
                handler(error, new Header(header));
            }
            finally
            {
                handlerHandle.Free();
            }
        }

        private static void FetchBlockHeightInternalHandler(IntPtr chain, IntPtr contextPtr, ErrorCode error,
                                                            UInt64 height)
        {
            GCHandle contextHandle = (GCHandle)contextPtr;
            try
            {
                var context = (contextHandle.Target as Tuple<Action<ErrorCode, UInt64>, hash_t>);
                Action<ErrorCode, UInt64> handler = context.Item1;
                handler(error, height);
            }
            finally
            {
                contextHandle.Free();
            }
        }

        private static void FetchBlockLocatorInternalHandler(IntPtr chain, IntPtr context, ErrorCode error,
                                                             IntPtr headerReader)
        {
            GCHandle handlerHandle = (GCHandle)context;
            try
            {
                var handler = (handlerHandle.Target as Action<ErrorCode, HeaderReader>);
                handler(error, new HeaderReader(headerReader));
            }
            finally
            {
                handlerHandle.Free();
            }
        }

        private static void FetchCompactBlockByHashInternalHandler(IntPtr chain, IntPtr contextPtr, ErrorCode error,
                                                                   IntPtr compactBlock, UInt64 height)
        {
            GCHandle contextHandle = (GCHandle)contextPtr;
            try
            {
                var context = (contextHandle.Target as Tuple<Action<ErrorCode, CompactBlock>, hash_t>);
                Action<ErrorCode, CompactBlock> handler = context.Item1;
                handler(error, new CompactBlock(compactBlock));
            }
            finally
            {
                contextHandle.Free();
            }
        }

        private static void FetchCompactBlockByHeightInternalHandler(IntPtr chain, IntPtr context, ErrorCode error,
                                                             IntPtr compactBlock, UInt64 height)
        {
            GCHandle handlerHandle = (GCHandle)context;
            try
            {
                var handler = (handlerHandle.Target as Action<ErrorCode, CompactBlock>);
                handler(error, new CompactBlock(compactBlock));
            }
            finally
            {
                handlerHandle.Free();
            }
        }

        private static void FetchHistoryInternalHandler(IntPtr chain, IntPtr context, ErrorCode error,
                                                                IntPtr history)
        {
            GCHandle handlerHandle = (GCHandle)context;
            try
            {
                var handler = (handlerHandle.Target as Action<ErrorCode, HistoryCompactList>);
                handler(error, new HistoryCompactList(history));
            }
            finally
            {
                handlerHandle.Free();
            }
        }

        private static void FetchLastHeightInternalHandler(IntPtr chain, IntPtr context, ErrorCode error,
                                                                   UIntPtr height)
        {
            GCHandle handlerHandle = (GCHandle)context;
            try
            {
                var handler = (handlerHandle.Target as Action<ErrorCode, UInt64>);
                handler(error, (UInt64)height);
            }
            finally
            {
                handlerHandle.Free();
            }
        }

        private static void FetchMerkleBlockByHashInternalHandler(IntPtr chain, IntPtr contextPtr, ErrorCode error,
                                                                  IntPtr merkleBlock, UInt64 height)
        {
            GCHandle contextHandle = (GCHandle)contextPtr;
            try
            {
                var context = (contextHandle.Target as Tuple<Action<ErrorCode, MerkleBlock, UInt64>, hash_t>);
                Action<ErrorCode, MerkleBlock, UInt64> handler = context.Item1;
                handler(error, new MerkleBlock(merkleBlock), height);
            }
            finally
            {
                contextHandle.Free();
            }
        }

        private static void FetchMerkleBlockByHeightInternalHandler(IntPtr chain, IntPtr context, ErrorCode error,
                                                                    IntPtr merkleBlock, UInt64 height)
        {
            GCHandle handlerHandle = (GCHandle)context;
            try
            {
                var handler = (handlerHandle.Target as Action<ErrorCode, MerkleBlock, UInt64>);
                handler(error, new MerkleBlock(merkleBlock), height);
            }
            finally
            {
                handlerHandle.Free();
            }
        }

        private static void FetchSpendInternalHandler(IntPtr chain, IntPtr contextPtr, ErrorCode error,
                                                      IntPtr inputPoint)
        {
            GCHandle contextHandle = (GCHandle)contextPtr;
            try
            {
                var context = (contextHandle.Target as Tuple<Action<ErrorCode, Point>, OutputPoint>);
                Action<ErrorCode, Point> handler = context.Item1;
                handler(error, new Point(inputPoint));
            }
            finally
            {
                contextHandle.Free();
            }
        }

        private static void FetchStealthInternalHandler(IntPtr chain, IntPtr contextPtr, ErrorCode error,
                                                        IntPtr stealth)
        {
            GCHandle contextHandle = (GCHandle)contextPtr;
            try
            {
                var context = (contextHandle.Target as Tuple<Action<ErrorCode, StealthCompactList>, Binary>);
                Action<ErrorCode, StealthCompactList> handler = context.Item1;
                handler(error, new StealthCompactList(stealth));
            }
            finally
            {
                contextHandle.Free();
            }
        }

        private static void FetchTransactionByHashInternalHandler(IntPtr chain, IntPtr contextPtr, ErrorCode error,
                                                                  IntPtr transaction, UInt64 index, UInt64 height)
        {
            GCHandle contextHandle = (GCHandle)contextPtr;
            try
            {
                var context = (contextHandle.Target as Tuple<Action<ErrorCode, Transaction, UInt64, UInt64>, hash_t>);
                Action<ErrorCode, Transaction, UInt64, UInt64> handler = context.Item1;
                handler(error, new Transaction(transaction), index, height);
            }
            finally
            {
                contextHandle.Free();
            }
        }

        private static void FetchTransactionByHeightInternalHandler(IntPtr chain, IntPtr context, ErrorCode error,
                                                                    IntPtr transaction, UInt64 index, UInt64 height)
        {
            GCHandle handlerHandle = (GCHandle)context;
            try
            {
                var handler = (handlerHandle.Target as Action<ErrorCode, Transaction, UInt64, UInt64>);
                handler(error, new Transaction(transaction), index, height);
            }
            finally
            {
                handlerHandle.Free();
            }
        }

        private static void FetchTransactionPositionInternalHandler(IntPtr chain, IntPtr contextPtr, ErrorCode error,
                                                                    UInt64 index, UInt64 height)
        {
            GCHandle contextHandle = (GCHandle)contextPtr;
            try
            {
                var context = (contextHandle.Target as Tuple<Action<ErrorCode, UInt64, UInt64>, hash_t>);
                Action<ErrorCode, UInt64, UInt64> handler = context.Item1;
                handler(error, index, height);
            }
            finally
            {
                contextHandle.Free();
            }
        }

        private static void ResultInternalHandler(IntPtr chain, IntPtr context, ErrorCode error)
        {
            GCHandle handlerHandle = (GCHandle)context;
            try
            {
                Action<ErrorCode> handler = (handlerHandle.Target as Action<ErrorCode>);
                handler(error);
            }
            finally
            {
                handlerHandle.Free();
            }
        }

        private static void ValidateTransactionInternalHandler(IntPtr chain, IntPtr context, ErrorCode error,
                                                               string message)
        {
            GCHandle handlerHandle = (GCHandle)context;
            try
            {
                var handler = (handlerHandle.Target as Action<ErrorCode, string>);
                handler(error, message);
            }
            finally
            {
                handlerHandle.Free();
            }
        }

    }

}