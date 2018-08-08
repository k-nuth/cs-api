using Bitprim.Native;
using System;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Bitprim
{
    /// <summary>
    /// Represents the Bitcoin blockchain; meant to offer its different interfaces (query, mining, network)
    /// </summary>
    public class Chain : IChain
    {
        private delegate void FetchBlockByHeightHashTimestampHandler(ErrorCode errorCode, byte[] blockHash, DateTime blockDate, UInt64 blockHeight);
        private delegate void FetchBlockHeaderByHashTxsSizeHandler(ErrorCode errorCode, Header blockHeader, UInt64 blockHeight, HashList txHashes, UInt64 serializedBlockSize);

        private readonly IntPtr nativeInstance_;

        private readonly ChainNative.FetchBlockHandler internalFetchBlockHandler_;
        private readonly ChainNative.FetchBlockHandler internalFetchBlockHandlerByHash_;
        private readonly ChainNative.FetchBlockHeightHandler internalFetchBlockHeightHandler_;
        private readonly ChainNative.FetchLastHeightHandler internalFetchLastHeightHandler_;
        private readonly ChainNative.FetchBlockHeaderByHashTxsSizeHandler internalFetchBlockHeaderByHashTxsSizeHandler_;
        private readonly ChainNative.FetchBlockHeightTimestampHandler internalFetchBlockHeightTimestampHandler_;
        private readonly ChainNative.FetchBlockHeaderHandler internalFetchBlockHeaderHandler_;
        private readonly ChainNative.FetchBlockHeaderHandler internalFetchBlockHeaderHandlerByHash_;
        private readonly ChainNative.MerkleBlockFetchHandler internalMerkleBlockFetchHandler_;
        private readonly ChainNative.MerkleBlockFetchHandler internalMerkleBlockFetchHandlerByHash_;
        private readonly ChainNative.ValidateTxHandler internalValidateTxHandler_;
        private readonly ChainNative.ResultHandler internalResultHandler_;
        //private readonly ChainNative.BlockLocatorFetchHandler internalBlockLocatorFetchHandler_;
        private readonly ChainNative.FetchSpendHandler internalFetchSpendHandler_;
        private readonly ChainNative.FetchHistoryHandler internalFetchHistoryHandler_;
        private readonly ChainNative.FetchTransactionsHandler internalFetchTxnsHandler_;
        private readonly ChainNative.FetchStealthHandler internalFetchStealthHandler_;
        //private readonly ChainNative.FetchCompactBlockHandler internalFetchCompactBlockHandler_;
        private readonly ChainNative.FetchTransactionHandler internalFetchTransactionHandler_;
        private readonly ChainNative.FetchTransactionPositionHandler internalFetchTransactionPositionHandler_;

        internal Chain(IntPtr nativeInstance)
        {
            nativeInstance_ = nativeInstance;

            internalFetchBlockHandler_ = FetchBlockInternalHandler;
            internalFetchBlockHandlerByHash_ = FetchBlockByHashInternalHandler;
            internalFetchBlockHeightHandler_ = FetchBlockHeightInternalHandler;
            internalFetchLastHeightHandler_ = FetchLastHeightInternalHandler;
            internalFetchBlockHeaderByHashTxsSizeHandler_ = FetchBlockHeaderByHashTxsSizeInternalHandler;
            internalFetchBlockHeightTimestampHandler_ = FetchBlockByHeightHashTimestampInternalHandler;
            internalFetchBlockHeaderHandler_ = FetchBlockHeaderInternalHandler;
            internalFetchBlockHeaderHandlerByHash_ = FetchBlockHeaderByHashInternalHandler;
            internalMerkleBlockFetchHandler_ = FetchMerkleBlockInternalHandler;
            internalMerkleBlockFetchHandlerByHash_ = FetchMerkleBlockByHashInternalHandler;
            internalValidateTxHandler_ = ValidateTransactionInternalHandler;
            internalResultHandler_ = ResultInternalHandler;
            //internalBlockLocatorFetchHandler_ = FetchBlockLocatorInternalHandler;
            internalFetchSpendHandler_ = FetchSpendInternalHandler;
            internalFetchHistoryHandler_ = FetchHistoryInternalHandler;
            internalFetchTxnsHandler_ = FetchTransactionsInternalHandler;
            internalFetchStealthHandler_ = FetchStealthInternalHandler;
            //internalFetchCompactBlockHandler_ = FetchCompactBlockInternalHandler;
            internalFetchTransactionHandler_ = FetchTransactionByHashInternalHandler;
            internalFetchTransactionPositionHandler_ = FetchTransactionPositionInternalHandler;
        }

        internal IntPtr NativeInstance
        {
            get
            {
                return nativeInstance_;
            }
        }

        #region Chain

        /// <summary>
        /// Given a block hash, it queries the chain asynchronously for the block's height.
        /// Return right away and uses a callback to return the result.
        /// </summary>
        /// <param name="blockHash"> 32-byte array representation of the block hash.
        ///    Identifies it univocally.
        /// </param>
        public async Task<ApiCallResult<UInt64>> FetchBlockHeightAsync(byte[] blockHash)
        {
            return await TaskHelper.ToTask<ApiCallResult<UInt64>>(tcs =>
            {
                FetchBlockHeight(blockHash, (code, height) =>
                {
                    tcs.TrySetResult(new ApiCallResult<UInt64>
                    {
                        ErrorCode = code,
                        Result = height
                    });
                });
            });
        }

        private void FetchBlockHeight(byte[] blockHash, Action<ErrorCode, UInt64> handler)
        {
            var managedHash = new hash_t
            {
                hash = blockHash
            };
            IntPtr contextPtr = CreateContext(handler, managedHash);
            ChainNative.chain_fetch_block_height(nativeInstance_, contextPtr, managedHash, internalFetchBlockHeightHandler_);
        }


        /// <summary>
        /// Gets the height of the highest block in the local copy of the blockchain, asynchronously.
        /// </summary>
        public async Task<ApiCallResult<UInt64>> FetchLastHeightAsync()
        {
            return await TaskHelper.ToTask<ApiCallResult<UInt64>>(tcs =>
            {
                FetchLastHeight((code, height) =>
                {
                    tcs.TrySetResult(new ApiCallResult<UInt64>
                    {
                        ErrorCode = code,
                        Result = height
                    });
                });
            });
        }

        private void FetchLastHeight(Action<ErrorCode, UInt64> handler)
        {
            GCHandle handlerHandle = GCHandle.Alloc(handler);
            IntPtr handlerPtr = (IntPtr)handlerHandle;
            ChainNative.chain_fetch_last_height(nativeInstance_, handlerPtr, internalFetchLastHeightHandler_);
        }

        #endregion //Chain

        #region Block

        /// <summary>
        /// Given a block hash, retrieve the full block it identifies, asynchronously.
        /// </summary>
        /// <param name="blockHash"> 32 bytes of the block hash </param>
        public async Task<DisposableApiCallResult<GetBlockDataResult<IBlock>>> FetchBlockByHashAsync(byte[] blockHash)
        {
            return await TaskHelper.ToTask<DisposableApiCallResult<GetBlockDataResult<IBlock>>>(tcs =>
            {
                FetchBlockByHash(blockHash, (code, block, height) =>
                {
                    tcs.TrySetResult(new DisposableApiCallResult<GetBlockDataResult<IBlock>>
                    {
                        ErrorCode = code,
                        Result = new GetBlockDataResult<IBlock>
                        {
                            BlockData = block,
                            BlockHeight = height
                        }
                    });
                });
            });
        }

        private void FetchBlockByHash(byte[] blockHash, Action<ErrorCode, Block, UInt64> handler)
        {
            var managedHash = new hash_t
            {
                hash = blockHash
            };
            IntPtr contextPtr = CreateContext(handler, managedHash);
            ChainNative.chain_fetch_block_by_hash(nativeInstance_, contextPtr, managedHash, internalFetchBlockHandlerByHash_);
        }

        /// <summary>
        /// Given a block height, retrieve the full block it identifies, asynchronously.
        /// </summary>
        /// <param name="height"> Block height </param>
        public async Task<DisposableApiCallResult<GetBlockDataResult<IBlock>>> FetchBlockByHeightAsync(UInt64 height)
        {
            return await TaskHelper.ToTask<DisposableApiCallResult<GetBlockDataResult<IBlock>>>(tcs =>
            {
                FetchBlockByHeight(height, (code, block, blockHeight) =>
                {
                    tcs.TrySetResult(new DisposableApiCallResult<GetBlockDataResult<IBlock>>
                    {
                        ErrorCode = code,
                        Result = new GetBlockDataResult<IBlock>
                        {
                            BlockData = block,
                            BlockHeight = blockHeight
                        }
                    });

                });

            });

        }

        private void FetchBlockByHeight(UInt64 height, Action<ErrorCode, Block, UInt64> handler)
        {
            GCHandle handlerHandle = GCHandle.Alloc(handler);
            IntPtr handlerPtr = (IntPtr)handlerHandle;
            ChainNative.chain_fetch_block_by_height(nativeInstance_, handlerPtr, height, internalFetchBlockHandler_);
        }

        /// <summary>
        /// Given a block hash, retrieve block header, tx hashes and serialized block size, asynchronously.
        /// </summary>
        /// <param name="blockHash"> 32 bytes of the block hash </param>
        public async Task<DisposableApiCallResult<GetBlockHeaderByHashTxSizeResult>> FetchBlockHeaderByHashTxSizesAsync(byte[] blockHash)
        {
            return await TaskHelper.ToTask<DisposableApiCallResult<GetBlockHeaderByHashTxSizeResult>>(tcs =>
            {

                FetchBlockHeaderByHashTxSizes(blockHash, (errorCode, header, height, hashes, size) =>
                {
                    tcs.TrySetResult(new DisposableApiCallResult<GetBlockHeaderByHashTxSizeResult>
                    {
                        ErrorCode = errorCode,
                        Result = new GetBlockHeaderByHashTxSizeResult
                        {
                            Header = new GetBlockDataResult<IHeader> { BlockData = header, BlockHeight = height },
                            TransactionHashes = hashes,
                            SerializedBlockSize = size
                        }
                    });

                });

            });
        }

        private void FetchBlockHeaderByHashTxSizes(byte[] blockHash, FetchBlockHeaderByHashTxsSizeHandler handler)
        {
            var managedHash = new hash_t
            {
                hash = blockHash
            };
            IntPtr contextPtr = CreateContext(handler, managedHash);
            ChainNative.chain_fetch_block_header_by_hash_txs_size(nativeInstance_, contextPtr, managedHash, internalFetchBlockHeaderByHashTxsSizeHandler_);
        }

        /// <summary>
        /// Given a block height, retrieve only block hash and timestamp, asynchronously.
        /// </summary>
        /// <param name="height"> Block height </param>
        public async Task<ApiCallResult<GetBlockHashTimestampResult>> FetchBlockByHeightHashTimestampAsync(UInt64 height)
        {
            return await TaskHelper.ToTask<ApiCallResult<GetBlockHashTimestampResult>>(tcs =>
            {
                FetchBlockByHeightHashTimestamp(height, (errorCode, hash, date, blockHeight) =>
                {
                    tcs.TrySetResult(new ApiCallResult<GetBlockHashTimestampResult>
                    {
                        ErrorCode = errorCode,
                        Result = new GetBlockHashTimestampResult
                        {
                            BlockHash = hash,
                            BlockTimestamp = date
                        }
                    });

                });
            });
        }

        private void FetchBlockByHeightHashTimestamp(UInt64 height, FetchBlockByHeightHashTimestampHandler handler)
        {
            GCHandle handlerHandle = GCHandle.Alloc(handler);
            IntPtr handlerPtr = (IntPtr)handlerHandle;
            ChainNative.chain_fetch_block_by_height_timestamp(nativeInstance_, handlerPtr, height, internalFetchBlockHeightTimestampHandler_);
        }

        #endregion //Block

        #region Block header

        /// <summary>
        /// Given a block hash, get the header from the block it identifies, asynchronously.
        /// </summary>
        /// <param name="blockHash"> 32 bytes of the block hash </param>
        public async Task<DisposableApiCallResult<GetBlockDataResult<IHeader>>> FetchBlockHeaderByHashAsync(byte[] blockHash)
        {
            return await TaskHelper.ToTask<DisposableApiCallResult<GetBlockDataResult<IHeader>>>(tcs =>
            {
                FetchBlockHeaderByHash(blockHash, (code, header, height) =>
                {
                    tcs.TrySetResult(new DisposableApiCallResult<GetBlockDataResult<IHeader>>
                    {
                        ErrorCode = code,
                        Result = new GetBlockDataResult<IHeader>
                        {
                            BlockData = header,
                            BlockHeight = height
                        }
                    });

                });
            });
        }

        private void FetchBlockHeaderByHash(byte[] blockHash, Action<ErrorCode, Header, UInt64> handler)
        {
            var managedHash = new hash_t
            {
                hash = blockHash
            };
            IntPtr contextPtr = CreateContext(handler, managedHash);
            ChainNative.chain_fetch_block_header_by_hash(nativeInstance_, contextPtr, managedHash, internalFetchBlockHeaderHandlerByHash_);
        }


        /// <summary>
        /// Given a block height, get the header from the block it identifies, asynchronously.
        /// </summary>
        /// <param name="height"> Block height </param>
        public async Task<DisposableApiCallResult<GetBlockDataResult<IHeader>>> FetchBlockHeaderByHeightAsync(UInt64 height)
        {
            return await TaskHelper.ToTask<DisposableApiCallResult<GetBlockDataResult<IHeader>>>(tcs =>
            {
                FetchBlockHeaderByHeight(height, (code, header, blockHeight) =>
                {
                    tcs.TrySetResult(new DisposableApiCallResult<GetBlockDataResult<IHeader>>
                    {
                        ErrorCode = code,
                        Result = new GetBlockDataResult<IHeader>
                        {
                            BlockData = header,
                            BlockHeight = blockHeight
                        }
                    });
                });
            });
        }

        private void FetchBlockHeaderByHeight(UInt64 height, Action<ErrorCode, Header, UInt64> handler)
        {
            GCHandle handlerHandle = GCHandle.Alloc(handler);
            IntPtr handlerPtr = (IntPtr)handlerHandle;
            ChainNative.chain_fetch_block_header_by_height(nativeInstance_, handlerPtr, height, internalFetchBlockHeaderHandler_);
        }

        #endregion //Block header

        #region Merkle Block

        /// <summary>
        /// Given a block hash, get the merkle block from the block it identifies, asynchronously.
        /// </summary>
        /// <param name="blockHash"> 32 bytes of the block hash </param>
        public async Task<DisposableApiCallResult<GetBlockDataResult<IMerkleBlock>>> FetchMerkleBlockByHashAsync(byte[] blockHash)
        {
            return await TaskHelper.ToTask<DisposableApiCallResult<GetBlockDataResult<IMerkleBlock>>>(tcs =>
            {
                FetchMerkleBlockByHash(blockHash, (code, merkleBlock, height) =>
                {
                    tcs.SetResult(new DisposableApiCallResult<GetBlockDataResult<IMerkleBlock>>
                    {
                        ErrorCode = code,
                        Result = new GetBlockDataResult<IMerkleBlock>
                        {
                            BlockData = merkleBlock,
                            BlockHeight = height
                        }
                    });
                });

            });
        }

        private void FetchMerkleBlockByHash(byte[] blockHash, Action<ErrorCode, MerkleBlock, UInt64> handler)
        {
            var managedHash = new hash_t
            {
                hash = blockHash
            };
            IntPtr contextPtr = CreateContext(handler, managedHash);
            ChainNative.chain_fetch_merkle_block_by_hash(nativeInstance_, contextPtr, managedHash, internalMerkleBlockFetchHandlerByHash_);
        }

        /// <summary>
        /// Given a block height, get the merkle block from the block it identifies, asynchronously.
        /// </summary>
        /// <param name="height"> Desired block height </param>
        public async Task<DisposableApiCallResult<GetBlockDataResult<IMerkleBlock>>> FetchMerkleBlockByHeightAsync(UInt64 height)
        {
            return await TaskHelper.ToTask<DisposableApiCallResult<GetBlockDataResult<IMerkleBlock>>>(tcs =>
            {
                FetchMerkleBlockByHeight(height, (code, merkleBlock, actualHeight) =>
                {
                    tcs.TrySetResult(new DisposableApiCallResult<GetBlockDataResult<IMerkleBlock>>
                    {
                        ErrorCode = code,
                        Result = new GetBlockDataResult<IMerkleBlock>
                        {
                            BlockData = merkleBlock,
                            BlockHeight = actualHeight
                        }
                    });

                });


            });
        }

        private void FetchMerkleBlockByHeight(UInt64 height, Action<ErrorCode, MerkleBlock, UInt64> handler)
        {
            GCHandle handlerHandle = GCHandle.Alloc(handler);
            IntPtr handlerPtr = (IntPtr)handlerHandle;
            ChainNative.chain_fetch_merkle_block_by_height(nativeInstance_, handlerPtr, height, internalMerkleBlockFetchHandler_);
        }

        #endregion //Merkle Block

        #region Compact block
        /*
        /// <summary>
        /// Given a block hash, get the compact block from the block it identifies, asynchronously.
        /// </summary>
        /// <param name="blockHash"> 32 bytes of the block hash </param>
        public async Task<DisposableApiCallResult<GetBlockDataResult<CompactBlock>>> FetchCompactBlockByHashAsync(byte[] blockHash)
        {
            return await TaskHelper.ToTask<DisposableApiCallResult<GetBlockDataResult<CompactBlock>>>(tcs =>
            {
                FetchCompactBlockByHash(blockHash, (code, compactBlock, height) =>
                {
                    tcs.TrySetResult(new DisposableApiCallResult<GetBlockDataResult<CompactBlock>>
                    {
                        ErrorCode = code,
                        Result = new GetBlockDataResult<CompactBlock>
                        {
                            BlockData = compactBlock,
                            BlockHeight = height
                        }
                    });

                });

            });
        }

        /// <summary>
        /// Given a block hash, get the compact block from the block it identifies, asynchronously.
        /// </summary>
        /// <param name="blockHash"> 32 bytes of the block hash </param>
        /// <param name="handler"> Callback which will be invoked when the compact block is retrieved </param>
        private void FetchCompactBlockByHash(byte[] blockHash, Action<ErrorCode, CompactBlock, UInt64> handler)
        {
            var managedHash = new hash_t
            {
                hash = blockHash
            };
            IntPtr contextPtr = CreateContext(handler, managedHash);
            ChainNative.chain_fetch_compact_block_by_hash(nativeInstance_, contextPtr, managedHash, internalFetchCompactBlockHandler_);
        }*/

        /*
        /// <summary>
        /// Given a block height, get the compact block from the block it identifies, asynchronously.
        /// </summary>
        /// <param name="height"> Desired block height </param>
        public async Task<DisposableApiCallResult<GetBlockDataResult<CompactBlock>>> FetchCompactBlockByHeightAsync(UInt64 height)
        {
            return await TaskHelper.ToTask<DisposableApiCallResult<GetBlockDataResult<CompactBlock>>>(tcs =>
            {
                FetchCompactBlockByHeight(height, (code, compactBlock, blockHeight) =>
                {
                    tcs.TrySetResult(new DisposableApiCallResult<GetBlockDataResult<CompactBlock>>
                    {
                        ErrorCode = code,
                        Result = new GetBlockDataResult<CompactBlock>
                        {
                            BlockData = compactBlock,
                            BlockHeight = blockHeight
                        }
                    });

                });

            });
        }

        /// <summary>
        /// Given a block height, get the compact block from the block it identifies, asynchronously.
        /// </summary>
        /// <param name="height"> Desired block height </param>
        /// <param name="handler"> Callback which will be invoked when the compact block is retrieved </param>
        private void FetchCompactBlockByHeight(UInt64 height, Action<ErrorCode, CompactBlock, UInt64> handler)
        {
            GCHandle handlerHandle = GCHandle.Alloc(handler);
            IntPtr handlerPtr = (IntPtr)handlerHandle;
            ChainNative.chain_fetch_compact_block_by_height(nativeInstance_, handlerPtr, height, internalFetchCompactBlockHandler_);
        }
        */

        #endregion //Compact block

        #region Transaction

        /// <summary>
        /// Get a transaction by its hash, asynchronously.
        /// </summary>
        /// <param name="txHash"> 32 bytes of transaction hash </param>
        /// <param name="requireConfirmed"> True if the transaction must belong to a block </param>
        public async Task<DisposableApiCallResult<GetTxDataResult>> FetchTransactionAsync(byte[] txHash, bool requireConfirmed)
        {
            return await TaskHelper.ToTask<DisposableApiCallResult<GetTxDataResult>>(tcs =>
            {
                FetchTransaction(txHash, requireConfirmed, (code, transaction, index, height) =>
                {
                    tcs.TrySetResult(new DisposableApiCallResult<GetTxDataResult>
                    {
                        ErrorCode = code,
                        Result = new GetTxDataResult
                        {
                            Tx = transaction,
                            TxPosition = new GetTxPositionResult
                            {
                                Index = index,
                                BlockHeight = height
                            }
                        }
                    });

                });
            });
        }

        private void FetchTransaction(byte[] txHash, bool requireConfirmed, Action<ErrorCode, Transaction, UInt64, UInt64> handler)
        {
            var managedHash = new hash_t
            {
                hash = txHash
            };
            IntPtr contextPtr = CreateContext(handler, managedHash);
            ChainNative.chain_fetch_transaction(nativeInstance_, contextPtr, managedHash, requireConfirmed ? 1 : 0, internalFetchTransactionHandler_);
        }

        /// <summary>
        /// Given a transaction hash, it fetches the height and position inside the block, asynchronously.
        /// </summary>
        /// <param name="txHash"> 32 bytes of transaction hash </param>
        /// <param name="requireConfirmed"> True iif the transaction must belong to a block </param>
        public async Task<ApiCallResult<GetTxPositionResult>> FetchTransactionPositionAsync(byte[] txHash, bool requireConfirmed)
        {
            return await TaskHelper.ToTask<ApiCallResult<GetTxPositionResult>>(tcs =>
            {

                FetchTransactionPosition(txHash, requireConfirmed, (code, index, height) =>
                {
                    tcs.TrySetResult(new ApiCallResult<GetTxPositionResult>
                    {
                        ErrorCode = code,
                        Result = new GetTxPositionResult { Index = index, BlockHeight = height }
                    });

                });

            });
        }

        private void FetchTransactionPosition(byte[] txHash, bool requireConfirmed, Action<ErrorCode, UInt64, UInt64> handler)
        {
            var managedHash = new hash_t
            {
                hash = txHash
            };
            IntPtr contextPtr = CreateContext(handler, managedHash);
            ChainNative.chain_fetch_transaction_position(nativeInstance_, contextPtr, managedHash, requireConfirmed ? 1 : 0, internalFetchTransactionPositionHandler_);
        }

        #endregion //Transaction

        #region Spend

        /// <summary>
        /// Fetch the transaction input which spends the indicated output, asynchronously.
        /// </summary>
        /// <param name="outputPoint"> Tx hash and index pair where the output was spent. </param>
        public async Task<ApiCallResult<IPoint>> FetchSpendAsync(OutputPoint outputPoint)
        {
            return await TaskHelper.ToTask<ApiCallResult<IPoint>>(tcs =>
            {
                FetchSpend(outputPoint, (code, point) =>
                {
                    tcs.TrySetResult(new ApiCallResult<IPoint> { ErrorCode = code, Result = point });
                });
            });
        }

        private void FetchSpend(OutputPoint outputPoint, Action<ErrorCode, Point> handler)
        {
            IntPtr contextPtr = CreateContext(handler, outputPoint);
            ChainNative.chain_fetch_spend(nativeInstance_, contextPtr, outputPoint.NativeInstance, internalFetchSpendHandler_);
        }

        #endregion //Spend

        #region History

        /// <summary>
        /// Get a list of output points, values, and spends for a given payment address (asynchronously)
        /// </summary>
        /// <param name="address"> Bitcoin payment address to search </param>
        /// <param name="limit"> Maximum amount of results to fetch </param>
        /// <param name="fromHeight"> Starting point to search for transactions </param>
        public async Task<DisposableApiCallResult<INativeList<IHistoryCompact>>> FetchHistoryAsync(PaymentAddress address, UInt64 limit, UInt64 fromHeight)
        {
            return await TaskHelper.ToTask<DisposableApiCallResult<INativeList<IHistoryCompact>>>(tcs =>
            {
                FetchHistory(address, limit, fromHeight, (code, history) =>
                {
                    tcs.TrySetResult(new DisposableApiCallResult<INativeList<IHistoryCompact>>
                    {
                        ErrorCode = code,
                        Result = history
                    });
                    
                });   
            });
        }

        private void FetchHistory(PaymentAddress address, UInt64 limit, UInt64 fromHeight, Action<ErrorCode, HistoryCompactList> handler)
        {
            GCHandle handlerHandle = GCHandle.Alloc(handler);
            IntPtr handlerPtr = (IntPtr)handlerHandle;
            ChainNative.chain_fetch_history(nativeInstance_, handlerPtr, address.NativeInstance, limit, fromHeight, internalFetchHistoryHandler_);
        }

        /// <summary>
        /// Get a list of tx ids for a given payment address (asynchronously). Duplicates are already filtered out.
        /// </summary>
        /// <param name="address"> Bitcoin payment address to search </param>
        /// <param name="limit"> Maximum amount of results to fetch </param>
        /// <param name="fromHeight"> Starting point to search for transactions </param>
        public async Task<DisposableApiCallResult<INativeList<byte[]>>> FetchConfirmedTransactionsAsync(PaymentAddress address, UInt64 limit, UInt64 fromHeight)
        {
            return await TaskHelper.ToTask<DisposableApiCallResult<INativeList<byte[]>>>(tcs =>
            {
                FetchConfirmedTransactions(address, limit, fromHeight, (code, txns) =>
                {
                    tcs.TrySetResult(new DisposableApiCallResult<INativeList<byte[]>>
                    {
                        ErrorCode = code,
                        Result = txns
                    });
                });
            });
        }

        private void FetchConfirmedTransactions(PaymentAddress address, UInt64 limit, UInt64 fromHeight, Action<ErrorCode, HashList> handler)
        {
            GCHandle handlerHandle = GCHandle.Alloc(handler);
            IntPtr handlerPtr = (IntPtr)handlerHandle;
            ChainNative.chain_fetch_confirmed_transactions(nativeInstance_, handlerPtr, address.NativeInstance, limit, fromHeight, internalFetchTxnsHandler_);
        }

        #endregion //History

        #region Stealth

        /// <summary>
        /// Get metadata on potential payment transactions by stealth filter. Given a filter and a
        /// height in the chain, it queries the chain for transactions matching the given filter.
        /// </summary>
        /// <param name="filter"> Must be at least 8 bits in length. example "10101010" </param>
        /// <param name="fromHeight"> Starting height in the chain to search for transactions </param>
        public async Task<DisposableApiCallResult<INativeList<IStealthCompact>>> FetchStealthAsync(Binary filter, UInt64 fromHeight)
        {
            return await TaskHelper.ToTask<DisposableApiCallResult<INativeList<IStealthCompact>>>(tcs =>
            {
                
                FetchStealth(filter, fromHeight, (code, list) =>
                {
                    tcs.TrySetResult(new DisposableApiCallResult<INativeList<IStealthCompact>>()
                    {
                        ErrorCode = code,
                        Result = list
                    });
                    
                });
                
            });
        }

        private void FetchStealth(Binary filter, UInt64 fromHeight, Action<ErrorCode, StealthCompactList> handler)
        {
            IntPtr contextPtr = CreateContext(handler, filter);
            ChainNative.chain_fetch_stealth(nativeInstance_, contextPtr, filter.NativeInstance, fromHeight, internalFetchStealthHandler_);
        }

        #endregion //Stealth

        #region Block indexes

        /*/// <summary>
        /// Given a list of indexes, fetch a header reader for them, asynchronously
        /// </summary>
        /// <param name="indexes"> Block indexes </param>
        public async Task<DisposableApiCallResult<HeaderReader>> FetchBlockLocatorAsync(BlockIndexList indexes)
        {
            return await TaskHelper.ToTask<DisposableApiCallResult<HeaderReader>>(tcs =>
            {
                FetchBlockLocator(indexes, (code, headerReader) =>
                {
                    tcs.TrySetResult(new DisposableApiCallResult<HeaderReader>
                    {
                        ErrorCode = code,
                        Result = headerReader
                    });
                    
                });
                
            });
        }*/

       /* /// <summary>
        /// Given a list of indexes, fetch a header reader for them, asynchronously
        /// </summary>
        /// <param name="indexes"> Block indexes </param>
        /// <param name="handler"> Callback which will called when the reader is retrieved </param>
        private void FetchBlockLocator(BlockIndexList indexes, Action<ErrorCode, HeaderReader> handler)
        {
            GCHandle handlerHandle = GCHandle.Alloc(handler);
            IntPtr handlerPtr = (IntPtr)handlerHandle;
            ChainNative.chain_fetch_block_locator(nativeInstance_, handlerPtr, indexes.NativeInstance, internalBlockLocatorFetchHandler_);
        }*/

        #endregion //Block indexes

        #region Organizers

        /// <summary>
        /// Given a block, organize it (async).
        /// </summary>
        /// <param name="block"> The block to organize </param>
        public async Task<ErrorCode> OrganizeBlockAsync(Block block)
        {
            return await TaskHelper.ToTask<ErrorCode>(tcs =>
            {
                OrganizeBlock(block, errorCode =>
                {
                    tcs.TrySetResult(errorCode);      
                });
                
            });
        }

        private void OrganizeBlock(Block block, Action<ErrorCode> handler)
        {
            GCHandle handlerHandle = GCHandle.Alloc(handler);
            IntPtr handlerPtr = (IntPtr)handlerHandle;
            ChainNative.chain_organize_block(nativeInstance_, handlerPtr, block.NativeInstance, internalResultHandler_);
        }

        /// <summary>
        /// Given a transaction, organize it (async).
        /// </summary>
        /// <param name="transaction"> The transaction to organize. </param>
        public async Task<ErrorCode> OrganizeTransactionAsync(Transaction transaction)
        {
            return await TaskHelper.ToTask<ErrorCode>(tcs =>
            {
                OrganizeTransaction(transaction, errorCode =>
                {
                    tcs.TrySetResult(errorCode);      
                });
            });
        }

        private void OrganizeTransaction(Transaction transaction, Action<ErrorCode> handler)
        {
            GCHandle handlerHandle = GCHandle.Alloc(handler);
            IntPtr handlerPtr = (IntPtr)handlerHandle;
            ChainNative.chain_organize_transaction(nativeInstance_, handlerPtr, transaction.NativeInstance, internalResultHandler_);
        }

        #endregion //Organizers

        #region Misc

        /// <summary>
        /// Determine if a transaction is valid for submission to the blockchain.
        /// </summary>
        /// <param name="transaction"> Transaction to validate </param>
        public async Task<ApiCallResult<string>> ValidateTransactionAsync(Transaction transaction)
        {
            return await TaskHelper.ToTask<ApiCallResult<string>>(tcs =>
            {
                
                ValidateTransaction(transaction, (code, message) =>
                {
                    tcs.TrySetResult(new ApiCallResult<string>
                    {
                        ErrorCode = code,
                        Result = message
                    });
                    
                });
                
            });
        }

        private void ValidateTransaction(Transaction transaction, Action<ErrorCode, string> handler)
        {
            GCHandle handlerHandle = GCHandle.Alloc(handler);
            IntPtr handlerPtr = (IntPtr)handlerHandle;
            ChainNative.chain_validate_tx(nativeInstance_, handlerPtr, transaction.NativeInstance, internalValidateTxHandler_);
        }

        /// <summary>
        /// Determine if the node is synchronized (i.e. has the latest copy of the blockchain/is at top height)
        /// </summary>
        public bool IsStale
        {
            get
            {
                return ChainNative.chain_is_stale(nativeInstance_) != 0;
            }
        }

        #endregion //Misc

        #region Mempool

        public INativeList<IMempoolTransaction> GetMempoolTransactions(PaymentAddress address, bool useTestnetRules)
        {
            IntPtr txs = ChainNative.chain_get_mempool_transactions(nativeInstance_, address.NativeInstance, useTestnetRules? 1:0);
            return new MempoolTransactionList(txs);
        }

        #endregion //Mempool

        private IntPtr CreateContext<TC, TP>(TC callback, TP parameters)
        {
            // Both the callback and its parameters need to hold garbage collection off until
            // the callback is called, so a GCHandle is taken for an object containing both of them:
            // that is the context
            var context = new Tuple<TC, TP>(callback, parameters);
            var contextHandle = GCHandle.Alloc(context);
            return (IntPtr)contextHandle;
        }

        private static void FetchBlockByHashInternalHandler(IntPtr chain, IntPtr contextPtr, ErrorCode error, IntPtr block, UInt64 height)
        {
            GCHandle contextHandle = (GCHandle)contextPtr;
            try
            {
                var context = (contextHandle.Target as Tuple<Action<ErrorCode, Block, UInt64>, hash_t>);
                Action<ErrorCode, Block, UInt64> handler = context.Item1;
                handler(error, new Block(block), height);
            }
            finally
            {
                contextHandle.Free();
            }
        }

        private static void FetchBlockInternalHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr block, UInt64 height)
        {
            GCHandle handlerHandle = (GCHandle)context;
            try
            {
                Action<ErrorCode, Block, UInt64> handler = (handlerHandle.Target as Action<ErrorCode, Block, UInt64>);
                handler(error, new Block(block), height);
            }
            finally
            {
                handlerHandle.Free();
            }
        }

        private static void FetchBlockHeaderByHashTxsSizeInternalHandler(IntPtr chain, IntPtr contextPtr, ErrorCode error, IntPtr blockHeader, UInt64 blockHeight, IntPtr txHashes, UInt64 blockSerializedSize)
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

        private static void FetchBlockByHeightHashTimestampInternalHandler(IntPtr chain, IntPtr context, ErrorCode error, hash_t blockHash, UInt32 timestamp, UInt64 height)
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

        private static void FetchBlockHeaderByHashInternalHandler(IntPtr chain, IntPtr contextPtr, ErrorCode error,
            IntPtr header, UInt64 height)
        {
            GCHandle contextHandle = (GCHandle)contextPtr;
            try
            {
                var context = (contextHandle.Target as Tuple<Action<ErrorCode, Header, UInt64>, hash_t>);
                Action<ErrorCode, Header, UInt64> handler = context.Item1;
                handler(error, new Header(header), height);
            }
            finally
            {
                contextHandle.Free();
            }
        }

        private static void FetchBlockHeaderInternalHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr header, UInt64 height)
        {
            GCHandle handlerHandle = (GCHandle)context;
            try
            {
                var handler = (handlerHandle.Target as Action<ErrorCode, Header, UInt64>);
                handler(error, new Header(header), height);
            }
            finally
            {
                handlerHandle.Free();
            }
        }

        private static void FetchBlockHeightInternalHandler(IntPtr chain, IntPtr contextPtr, ErrorCode error, UInt64 height)
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
        /*
        private static void FetchBlockLocatorInternalHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr headerReader)
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
        }*/
        /*
        private static void FetchCompactBlockInternalHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr compactBlock, UInt64 height)
        {
            GCHandle handlerHandle = (GCHandle)context;
            try
            {
                var contexHandler = handlerHandle.Target as Tuple<Action<ErrorCode, CompactBlock, UInt64>,hash_t>;
                Action<ErrorCode, CompactBlock, UInt64> handler = contexHandler.Item1;
                handler(error, new CompactBlock(compactBlock), height);
            }
            finally
            {
                handlerHandle.Free();
            }
        }*/

        private static void FetchHistoryInternalHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr history)
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

        private static void FetchTransactionsInternalHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr txns)
        {
            GCHandle handlerHandle = (GCHandle)context;
            try
            {
                var handler = (handlerHandle.Target as Action<ErrorCode, HashList>);
                handler(error, new HashList(txns));
            }
            finally
            {
                handlerHandle.Free();
            }
        }

        private static void FetchLastHeightInternalHandler(IntPtr chain, IntPtr context, ErrorCode error, UInt64 height)
        {
            GCHandle handlerHandle = (GCHandle)context;
            try
            {
                var handler = (handlerHandle.Target as Action<ErrorCode, UInt64>);
                handler(error, height);
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

        private static void FetchMerkleBlockInternalHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr merkleBlock, UInt64 height)
        {
            GCHandle handlerHandle = (GCHandle)context;
            try
            {
                var handler = handlerHandle.Target as Action<ErrorCode, MerkleBlock, UInt64>;
                handler(error, new MerkleBlock(merkleBlock), height);
            }
            finally
            {
                handlerHandle.Free();
            }
        }

        private static void FetchSpendInternalHandler(IntPtr chain, IntPtr contextPtr, ErrorCode error, IntPtr inputPoint)
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

        private static void FetchStealthInternalHandler(IntPtr chain, IntPtr contextPtr, ErrorCode error, IntPtr stealth)
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

        private static void FetchTransactionByHashInternalHandler(IntPtr chain, IntPtr contextPtr, ErrorCode error, IntPtr transaction, UInt64 index, UInt64 height)
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

        private static void FetchTransactionPositionInternalHandler(IntPtr chain, IntPtr contextPtr, ErrorCode error, UInt64 index, UInt64 height)
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

        private static void ValidateTransactionInternalHandler(IntPtr chain, IntPtr context, ErrorCode error, string message)
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