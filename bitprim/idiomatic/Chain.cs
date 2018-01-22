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
        private IntPtr nativeInstance_;

        #region Chain

        /// <summary>
        /// Given a block hash, it queries the chain asynchronously for the block's height.
        /// Return right away and uses a callback to return the result.
        /// </summary>
        /// <param name="blockHash"> 32-byte array representation of the block hash.
        ///    Identifies it univocally.
        /// </param>
        /// <param name="handler"> Callback which will be invoked when the block height is found. </param>
        public void FetchBlockHeight(byte[] blockHash, Action<ErrorCode, UInt64> handler)
        {
            var managedHash = new hash_t
            {
                hash = blockHash
            };
            IntPtr contextPtr = CreateContext(handler, managedHash);
            ChainNative.chain_fetch_block_height(nativeInstance_, contextPtr, managedHash, FetchBlockHeightHandler);
        }

        /// <summary>
        /// Given a block hash, it queries the chain asynchronously for the block's height.
        /// Blocks until block height is retrieved.
        /// </summary>
        /// <param name="blockHash">  32-byte array representation of the block hash.
        ///    Identifies it univocally. </param>
        /// <returns> The block height </returns>
        public Tuple<ErrorCode, UInt64> GetBlockHeight(byte[] blockHash)
        {
            UInt64 height = 0;
            var managedHash = new hash_t
            {
                hash = blockHash
            };
            ErrorCode result = ChainNative.chain_get_block_height(nativeInstance_, managedHash, ref height);
            return new Tuple<ErrorCode, UInt64>(result, height);
        }

        /// <summary>
        /// Gets the height of the highest block in the local copy of the blockchain, asynchronously.
        /// </summary>
        /// <param name="handler"> Callback which will be called once the last height is retrieved. </param>
        public void FetchLastHeight(Action<ErrorCode, UInt64> handler)
        {
            GCHandle handlerHandle = GCHandle.Alloc(handler);
            IntPtr handlerPtr = (IntPtr)handlerHandle;
            ChainNative.chain_fetch_last_height(nativeInstance_, handlerPtr, FetchLastHeightHandler);
        }

        /// <summary>
        /// Gets the height of the highest block in the local copy of the blockchain, synchronously.
        /// It blocks until height is retrieved.
        /// </summary>
        /// <returns> Error code (0 = success) and height </returns>
        public Tuple<ErrorCode, UInt64> GetLastHeight()
        {
            UInt64 height = 0;
            ErrorCode result = ChainNative.chain_get_last_height(nativeInstance_, ref height);
            return new Tuple<ErrorCode, UInt64>(result, height);
        }

        #endregion //Chain

        #region Block

        /// <summary>
        /// Given a block hash, retrieve the full block it identifies, asynchronously.
        /// </summary>
        /// <param name="blockHash"> 32 bytes of the block hash </param>
        /// <param name="handler"> Callback which will be called when the block is retrieved. </param>
        public void FetchBlockByHash(byte[] blockHash, Action<ErrorCode, Block> handler)
        {
            var managedHash = new hash_t
            {
                hash = blockHash
            };
            IntPtr contextPtr = CreateContext(handler, managedHash);
            ChainNative.chain_fetch_block_by_hash(nativeInstance_, contextPtr, managedHash, FetchBlockByHashHandler);
        }

        /// <summary>
        /// Given a block hash, get the full block it identifies, synchronously.
        /// </summary>
        /// <param name="blockHash"> 32 bytes of the block hash </param>
        /// <returns> Error code and full block </returns>
        public Tuple<ErrorCode, Block, UInt64> GetBlockByHash(byte[] blockHash)
        {
            IntPtr block = IntPtr.Zero;
            UInt64 height = 0;
            var managedHash = new hash_t
            {
                hash = blockHash
            };
            ErrorCode result = ChainNative.chain_get_block_by_hash(nativeInstance_, managedHash, ref block, ref height);
            return new Tuple<ErrorCode, Block, UInt64>(result, new Block(block), height);
        }

        /// <summary>
        /// Given a block height, retrieve the full block it identifies, asynchronously.
        /// </summary>
        /// <param name="height"> Block height </param>
        /// <param name="handler"> Callback which will be called when the block is retrieved. </param>
        public void FetchBlockByHeight(UInt64 height, Action<ErrorCode, Block> handler)
        {
            GCHandle handlerHandle = GCHandle.Alloc(handler);
            IntPtr handlerPtr = (IntPtr)handlerHandle;
            ChainNative.chain_fetch_block_by_height(nativeInstance_, handlerPtr, height, FetchBlockByHeightHandler);
        }

        /// <summary>
        /// Given a block height, get the full block it identifies, synchronously.
        /// </summary>
        /// <param name="height"> Block height </param>
        /// <returns> Error code and full block </returns>
        public Tuple<ErrorCode, Block, UInt64> GetBlockByHeight(UInt64 height)
        {
            IntPtr block = IntPtr.Zero;
            UInt64 actualHeight = 0; //Should always match input height
            ErrorCode result = ChainNative.chain_get_block_by_height(nativeInstance_, height, ref block, ref actualHeight);
            return new Tuple<ErrorCode, Block, UInt64>(result, new Block(block), actualHeight);
        }

        #endregion //Block

        #region Block header

        /// <summary>
        /// Given a block hash, get the header from the block it identifies, asynchronously.
        /// </summary>
        /// <param name="blockHash"> 32 bytes of the block hash </param>
        /// <param name="handler"> Callback which will be called when the header is retrieved </param>
        public void FetchBlockHeaderByHash(byte[] blockHash, Action<ErrorCode, Header> handler)
        {
            var managedHash = new hash_t
            {
                hash = blockHash
            };
            IntPtr contextPtr = CreateContext(handler, managedHash);
            ChainNative.chain_fetch_block_header_by_hash(nativeInstance_, contextPtr, managedHash, FetchBlockHeaderByHashHandler);
        }

        /// <summary>
        /// Given a block hash, get the header from the block it identifies, synchronously.
        /// </summary>
        /// <param name="blockHash"> 32 bytes of the block hash </param>
        /// <returns> Error code, full block header and block height </returns>
        public Tuple<ErrorCode, Header, UInt64> GetBlockHeaderByHash(byte[] blockHash)
        {
            IntPtr header = IntPtr.Zero;
            UInt64 height = 0;
            var managedHash = new hash_t
            {
                hash = blockHash
            };
            ErrorCode result = ChainNative.chain_get_block_header_by_hash(nativeInstance_, managedHash, ref header, ref height);
            return new Tuple<ErrorCode, Header, UInt64>(result, new Header(header), height);
        }

        /// <summary>
        /// Given a block height, get the header from the block it identifies, asynchronously.
        /// </summary>
        /// <param name="height"> Block height </param>
        /// <param name="handler"> Callback which will be invoked when the block header is retrieved </param>
        public void FetchBlockHeaderByHeight(UInt64 height, Action<ErrorCode, Header> handler)
        {
            GCHandle handlerHandle = GCHandle.Alloc(handler);
            IntPtr handlerPtr = (IntPtr)handlerHandle;
            ChainNative.chain_fetch_block_header_by_height(nativeInstance_, handlerPtr, height, FetchBlockHeaderbyHeightHandler);
        }

        /// <summary>
        /// Given a block height, get the header from the block it identifies, synchronously.
        /// </summary>
        /// <param name="height"> Block height </param>
        /// <returns> Error code, full block header, and height </returns>
        public Tuple<ErrorCode, Header, UInt64> GetBlockHeaderByHeight(UInt64 height)
        {
            IntPtr header = IntPtr.Zero;
            UInt64 actualHeight = 0; //Should always match input height
            ErrorCode result = ChainNative.chain_get_block_header_by_height(nativeInstance_, height, ref header, ref actualHeight);
            return new Tuple<ErrorCode, Header, UInt64>(result, new Header(header), actualHeight);
        }

        #endregion //Block header

        #region Merkle Block

        /// <summary>
        /// Given a block hash, get the merkle block from the block it identifies, asynchronously.
        /// </summary>
        /// <param name="blockHash"> 32 bytes of the block hash </param>
        /// <param name="handler"> Callback which will be invoked when the Merkle block is retrieved </param>
        public void FetchMerkleBlockByHash(byte[] blockHash, Action<ErrorCode, MerkleBlock, UInt64> handler)
        {
            var managedHash = new hash_t
            {
                hash = blockHash
            };
            IntPtr contextPtr = CreateContext(handler, managedHash);
            ChainNative.chain_fetch_merkle_block_by_hash(nativeInstance_, contextPtr, managedHash, FetchMerkleBlockByHashHandler);
        }

        /// <summary>
        /// Given a block hash, get the merkle block from the block it identifies, synchronously.
        /// </summary>
        /// <param name="blockHash"> 32 bytes of the block hash </param>
        /// <returns> Error code, full Merkle block and height </returns>
        public Tuple<ErrorCode, MerkleBlock, UInt64> GetMerkleBlockByHash(byte[] blockHash)
        {
            IntPtr merkleBlock = IntPtr.Zero;
            UInt64 height = 0;
            var managedHash = new hash_t
            {
                hash = blockHash
            };
            ErrorCode result = ChainNative.chain_get_merkle_block_by_hash(nativeInstance_, managedHash, ref merkleBlock, ref height);
            return new Tuple<ErrorCode, MerkleBlock, UInt64>(result, new MerkleBlock(merkleBlock), height);
        }

        /// <summary>
        /// Given a block height, get the merkle block from the block it identifies, asynchronously.
        /// </summary>
        /// <param name="height"> Desired block height </param>
        /// <param name="handler"> Callback which will be invoked when the Merkle block is retrieved </param>
        public void FetchMerkleBlockByHeight(UInt64 height, Action<ErrorCode, MerkleBlock, UInt64> handler)
        {
            GCHandle handlerHandle = GCHandle.Alloc(handler);
            IntPtr handlerPtr = (IntPtr)handlerHandle;
            ChainNative.chain_fetch_merkle_block_by_height(nativeInstance_, handlerPtr, height, FetchMerkleBlockByHeightHandler);
        }

        /// <summary>
        /// Given a block height, get the merkle block from the block it identifies, synchronously.
        /// </summary>
        /// <param name="height"> Desired block height </param>
        /// <returns> Error code, full Merkle block and height </returns>
        public Tuple<ErrorCode, MerkleBlock, UInt64> GetMerkleBlockByHeight(UInt64 height)
        {
            IntPtr merkleBlock = IntPtr.Zero;
            UInt64 actualHeight = 0; //Should always match input height
            ErrorCode result = ChainNative.chain_get_merkle_block_by_height(nativeInstance_, height, ref merkleBlock, ref actualHeight);
            return new Tuple<ErrorCode, MerkleBlock, UInt64>(result, new MerkleBlock(merkleBlock), actualHeight);
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
            ChainNative.chain_fetch_compact_block_by_hash(nativeInstance_, contextPtr, managedHash, FetchCompactBlockByHashHandler);
        }

        /// <summary>
        /// Given a block hash, get the compact block from the block it identifies, synchronously.
        /// </summary>
        /// <param name="blockHash"> 32 bytes of the block hash </param>
        /// <returns> Error code, full compact block and height </returns>
        public Tuple<ErrorCode, CompactBlock, UInt64> GetCompactBlockByHash(byte[] blockHash)
        {
            IntPtr compactBlock = IntPtr.Zero;
            UInt64 height = 0;
            var managedHash = new hash_t
            {
                hash = blockHash
            };
            ErrorCode result = ChainNative.chain_get_compact_block_by_hash(nativeInstance_, managedHash, ref compactBlock, ref height);
            return new Tuple<ErrorCode, CompactBlock, UInt64>(result, new CompactBlock(compactBlock), height);
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
            ChainNative.chain_fetch_compact_block_by_height(nativeInstance_, handlerPtr, height, FetchCompactBlockByHeightHandler);
        }

        /// <summary>
        /// Given a block height, get the compact block from the block it identifies, synchronously.
        /// </summary>
        /// <param name="height"> Desired block height </param>
        /// <returns> Error code, full compact block and height </returns>
        public Tuple<ErrorCode, CompactBlock, UInt64> GetCompactBlockByHeight(UInt64 height)
        {
            IntPtr compactBlock = IntPtr.Zero;
            UInt64 actualHeight = 0; //Should always match input height
            ErrorCode result = ChainNative.chain_get_compact_block_by_height(nativeInstance_, height, ref compactBlock, ref actualHeight);
            return new Tuple<ErrorCode, CompactBlock, UInt64>(result, new CompactBlock(compactBlock), actualHeight);
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
            ChainNative.chain_fetch_transaction(nativeInstance_, contextPtr, managedHash, requireConfirmed ? 1 : 0, FetchTransactionByHashHandler);
        }

        /// <summary>
        /// Get a transaction by its hash, synchronously.
        /// </summary>
        /// <param name="txHash"> 32 bytes of transaction hash </param>
        /// <param name="requireConfirmed"> True iif the transaction must belong to a block </param>
        /// <returns> Error code, full transaction, index inside block and height </returns>
        public Tuple<ErrorCode, Transaction, UInt64, UInt64> GetTransaction(byte[] txHash, bool requireConfirmed)
        {
            IntPtr transaction = IntPtr.Zero;
            UInt64 index = 0;
            UInt64 height = 0;
            var managedHash = new hash_t
            {
                hash = txHash
            };
            ErrorCode result = ChainNative.chain_get_transaction(nativeInstance_, managedHash, requireConfirmed ? 1 : 0, ref transaction, ref index, ref height);
            return new Tuple<ErrorCode, Transaction, UInt64, UInt64>(result, new Transaction(transaction), index, height);
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
            ChainNative.chain_fetch_transaction_position(nativeInstance_, contextPtr, managedHash, requireConfirmed ? 1 : 0, FetchTransactionPositionHandler);
        }

        /// <summary>
        /// Given a transaction hash, it fetches the height and position inside the block, synchronously.
        /// </summary>
        /// <param name="txHash"> 32 bytes of transaction hash </param>
        /// <param name="requireConfirmed"> True iif the transaction must belong to a block </param>
        /// <returns> Error code, index in block (zero based) and block height </returns>
        public Tuple<ErrorCode, UInt64, UInt64> GetTransactionPosition(byte[] txHash, bool requireConfirmed)
        {
            UInt64 index = 0;
            UInt64 height = 0;
            var managedHash = new hash_t
            {
                hash = txHash
            };
            ErrorCode result = ChainNative.chain_get_transaction_position(nativeInstance_, managedHash, requireConfirmed ? 1 : 0, ref index, ref height);
            return new Tuple<ErrorCode, UInt64, UInt64>(result, index, height);
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
            ChainNative.chain_fetch_spend(nativeInstance_, contextPtr, outputPoint.NativeInstance, FetchSpendHandler);
        }

        /// <summary>
        /// Get the transaction input which spends the indicated output, synchronously.
        /// </summary>
        /// <param name="outputPoint"> Tx hash and index pair where the output was spent. </param>
        /// <returns> Error code and output point </returns>
        public Tuple<ErrorCode, Point> GetSpend(OutputPoint outputPoint)
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
            return new Tuple<ErrorCode, Point>(error, point);
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
            ChainNative.chain_fetch_history(nativeInstance_, handlerPtr, address.NativeInstance, limit, fromHeight, FetchHistoryHandler);
        }

        /// <summary>
        /// Get a list of output points, values, and spends for a given payment address (synchronously)
        /// </summary>
        /// <param name="address"> Bitcoin payment address to search </param>
        /// <param name="limit"> Maximum amount of results to fetch </param>
        /// <param name="fromHeight"> Starting point to search for transactions </param>
        /// <returns> Error code, HistoryCompactList </returns>
        public Tuple<ErrorCode, HistoryCompactList> GetHistory(PaymentAddress address, UInt64 limit, UInt64 fromHeight)
        {
            IntPtr history = IntPtr.Zero;
            ErrorCode result = ChainNative.chain_get_history(nativeInstance_, address.NativeInstance, limit, fromHeight, ref history);
            return new Tuple<ErrorCode, HistoryCompactList>(result, new HistoryCompactList(history));
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
            ChainNative.chain_fetch_stealth(nativeInstance_, contextPtr, filter.NativeInstance, fromHeight, FetchStealthHandler);
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
            ChainNative.chain_fetch_block_locator(nativeInstance_, handlerPtr, indexes.NativeInstance, FetchBlockLocatorHandler);
        }

        /// <summary>
        /// Given a list of indexes, fetch a header reader for them, synchronously
        /// </summary>
        /// <param name="indexes"> Block indexes </param>
        /// <returns> Error code, HeaderReader </returns>
        public Tuple<ErrorCode, HeaderReader> GetBlockLocator(BlockIndexList indexes)
        {
            IntPtr headerReader = IntPtr.Zero;
            ErrorCode result = ChainNative.chain_get_block_locator(nativeInstance_, indexes.NativeInstance, ref headerReader);
            return new Tuple<ErrorCode, HeaderReader>(result, new HeaderReader(headerReader));
        }

        #endregion //Block indexes

        #region Subscribers

        /// <summary>
        /// Be notified (called back) when the local copy of the blockchain is reorganized.
        /// </summary>
        /// <param name="handler"> Callback which will be called when blocks are added or removed.
        /// The callback returns 3 parameters:
        ///     - Height (UInt64): The chain height at which reorganization takes place
        ///     - Incoming (Blocklist): Incoming blocks (added to the blockchain).
        ///     - Outgoing (Blocklist): Outgoing blocks (removed from the blockchain).
        /// </param>
        public void SubscribeToBlockChain(Action<UInt64, BlockList, BlockList> handler)
        {
            GCHandle handlerHandle = GCHandle.Alloc(handler);
            IntPtr handlerPtr = (IntPtr)handlerHandle;
            ChainNative.chain_subscribe_blockchain(nativeInstance_, handlerPtr, ReorganizeHandler);
        }

        /// <summary>
        /// Be notified (called back) when the local copy of the blockchain is updated at the transaction level.
        /// </summary>
        /// <param name="handler"> Callback which will be called when a transaction is added. </param>
        public void SubscribeToTransaction(Action<UInt64, BlockList, BlockList> handler)
        {
            GCHandle handlerHandle = GCHandle.Alloc(handler);
            IntPtr handlerPtr = (IntPtr)handlerHandle;
            ChainNative.chain_subscribe_transaction(nativeInstance_, handlerPtr, TransactionHandler);
        }

        #endregion //Subscribers

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
            ChainNative.chain_organize_block(nativeInstance_, handlerPtr, block.NativeInstance, ResultHandler);
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
            ChainNative.chain_organize_transaction(nativeInstance_, handlerPtr, transaction.NativeInstance, ResultHandler);
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
            ChainNative.chain_validate_tx(nativeInstance_, handlerPtr, transaction.NativeInstance, ValidateTransactionHandler);
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

        private static void FetchBlockHeaderByHashHandler(IntPtr chain, IntPtr contextPtr, ErrorCode error, IntPtr header, UInt64 height)
        {
            GCHandle contextHandle = (GCHandle)contextPtr;
            Tuple<Action<ErrorCode, Header>, hash_t> context = (contextHandle.Target as Tuple<Action<ErrorCode, Header>, hash_t>);
            Action<ErrorCode, Header> handler = context.Item1;
            handler(error, new Header(header));
            contextHandle.Free();
        }

        private static void FetchBlockHeaderbyHeightHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr header, UInt64 height)
        {
            GCHandle handlerHandle = (GCHandle)context;
            Action<ErrorCode, Header> handler = (handlerHandle.Target as Action<ErrorCode, Header>);
            handler(error, new Header(header));
            handlerHandle.Free();
        }

        private static void FetchBlockByHashHandler(IntPtr chain, IntPtr contextPtr, ErrorCode error, IntPtr block, UInt64 height)
        {
            GCHandle contextHandle = (GCHandle)contextPtr;
            Tuple<Action<ErrorCode, Block>, hash_t> context = (contextHandle.Target as Tuple<Action<ErrorCode, Block>, hash_t>);
            Action<ErrorCode, Block> handler = context.Item1;
            handler(error, new Block(block));
            contextHandle.Free();
        }

        private static void FetchBlockByHeightHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr block, UInt64 height)
        {
            GCHandle handlerHandle = (GCHandle)context;
            Action<ErrorCode, Block> handler = (handlerHandle.Target as Action<ErrorCode, Block>);
            handler(error, new Block(block));
            handlerHandle.Free();
        }

        private static void FetchBlockHeightHandler(IntPtr chain, IntPtr contextPtr, ErrorCode error, UInt64 height)
        {
            GCHandle contextHandle = (GCHandle)contextPtr;
            Tuple<Action<ErrorCode, UInt64>, hash_t> context = (contextHandle.Target as Tuple<Action<ErrorCode, UInt64>, hash_t>);
            Action<ErrorCode, UInt64> handler = context.Item1;
            handler(error, height);
            contextHandle.Free();
        }

        private static void FetchBlockLocatorHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr headerReader)
        {
            GCHandle handlerHandle = (GCHandle)context;
            Action<ErrorCode, HeaderReader> handler = (handlerHandle.Target as Action<ErrorCode, HeaderReader>);
            handler(error, new HeaderReader(headerReader));
            handlerHandle.Free();
        }

        private static void FetchCompactBlockByHashHandler(IntPtr chain, IntPtr contextPtr, ErrorCode error, IntPtr compactBlock, UInt64 height)
        {
            GCHandle contextHandle = (GCHandle)contextPtr;
            Tuple<Action<ErrorCode, CompactBlock>, hash_t> context = (contextHandle.Target as Tuple<Action<ErrorCode, CompactBlock>, hash_t>);
            Action<ErrorCode, CompactBlock> handler = context.Item1;
            handler(error, new CompactBlock(compactBlock));
            contextHandle.Free();
        }

        private static void FetchCompactBlockByHeightHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr compactBlock, UInt64 height)
        {
            GCHandle handlerHandle = (GCHandle)context;
            Action<ErrorCode, CompactBlock> handler = (handlerHandle.Target as Action<ErrorCode, CompactBlock>);
            handler(error, new CompactBlock(compactBlock));
            handlerHandle.Free();
        }

        private static void FetchHistoryHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr history)
        {
            GCHandle handlerHandle = (GCHandle)context;
            Action<ErrorCode, HistoryCompactList> handler = (handlerHandle.Target as Action<ErrorCode, HistoryCompactList>);
            handler(error, new HistoryCompactList(history));
            handlerHandle.Free();
        }

        private static void FetchLastHeightHandler(IntPtr chain, IntPtr context, ErrorCode error, UIntPtr height)
        {
            GCHandle handlerHandle = (GCHandle)context;
            Action<ErrorCode, UInt64> handler = (handlerHandle.Target as Action<ErrorCode, UInt64>);
            handler(error, (UInt64)height);
            handlerHandle.Free();
        }

        private static void FetchMerkleBlockByHashHandler(IntPtr chain, IntPtr contextPtr, ErrorCode error, IntPtr merkleBlock, UInt64 height)
        {
            GCHandle contextHandle = (GCHandle)contextPtr;
            Tuple<Action<ErrorCode, MerkleBlock, UInt64>, hash_t> context = (contextHandle.Target as Tuple<Action<ErrorCode, MerkleBlock, UInt64>, hash_t>);
            Action<ErrorCode, MerkleBlock, UInt64> handler = context.Item1;
            handler(error, new MerkleBlock(merkleBlock), height);
            contextHandle.Free();
        }

        private static void FetchMerkleBlockByHeightHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr merkleBlock, UInt64 height)
        {
            GCHandle handlerHandle = (GCHandle)context;
            Action<ErrorCode, MerkleBlock, UInt64> handler = (handlerHandle.Target as Action<ErrorCode, MerkleBlock, UInt64>);
            handler(error, new MerkleBlock(merkleBlock), height);
            handlerHandle.Free();
        }

        private static void FetchSpendHandler(IntPtr chain, IntPtr contextPtr, ErrorCode error, IntPtr inputPoint)
        {
            GCHandle contextHandle = (GCHandle)contextPtr;
            Tuple<Action<ErrorCode, Point>, OutputPoint> context = (contextHandle.Target as Tuple<Action<ErrorCode, Point>, OutputPoint>);
            Action<ErrorCode, Point> handler = context.Item1;
            handler(error, new Point(inputPoint));
            contextHandle.Free();
        }

        private static void FetchStealthHandler(IntPtr chain, IntPtr contextPtr, ErrorCode error, IntPtr stealth)
        {
            GCHandle contextHandle = (GCHandle)contextPtr;
            Tuple<Action<ErrorCode, StealthCompactList>, Binary> context = (contextHandle.Target as Tuple<Action<ErrorCode, StealthCompactList>, Binary>);
            Action<ErrorCode, StealthCompactList> handler = context.Item1;
            handler(error, new StealthCompactList(stealth));
            contextHandle.Free();
        }

        private static void FetchTransactionByHashHandler(IntPtr chain, IntPtr contextPtr, ErrorCode error, IntPtr transaction, UInt64 index, UInt64 height)
        {
            GCHandle contextHandle = (GCHandle)contextPtr;
            Tuple<Action<ErrorCode, Transaction, UInt64, UInt64>, hash_t> context = (contextHandle.Target as Tuple<Action<ErrorCode, Transaction, UInt64, UInt64>, hash_t>);
            Action<ErrorCode, Transaction, UInt64, UInt64> handler = context.Item1;
            handler(error, new Transaction(transaction), index, height);
            contextHandle.Free();
        }

        private static void FetchTransactionByHeightHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr transaction, UInt64 index, UInt64 height)
        {
            GCHandle handlerHandle = (GCHandle)context;
            Action<ErrorCode, Transaction, UInt64, UInt64> handler = (handlerHandle.Target as Action<ErrorCode, Transaction, UInt64, UInt64>);
            handler(error, new Transaction(transaction), index, height);
            handlerHandle.Free();
        }

        private static void FetchTransactionPositionHandler(IntPtr chain, IntPtr contextPtr, ErrorCode error, UInt64 index, UInt64 height)
        {
            GCHandle contextHandle = (GCHandle)contextPtr;
            Tuple<Action<ErrorCode, UInt64, UInt64>, hash_t> context = (contextHandle.Target as Tuple<Action<ErrorCode, UInt64, UInt64>, hash_t>);
            Action<ErrorCode, UInt64, UInt64> handler = context.Item1;
            handler(error, index, height);
            contextHandle.Free();
        }

        private static void ReorganizeHandler(IntPtr chain, IntPtr context, ErrorCode error, UInt64 u, IntPtr blockList, IntPtr blockList2)
        {
            GCHandle handlerHandle = (GCHandle)context;
            Action<ErrorCode, UInt64, BlockList, BlockList> handler = (handlerHandle.Target as Action<ErrorCode, UInt64, BlockList, BlockList>);
            handler(error, u, new BlockList(blockList), new BlockList(blockList2));
            handlerHandle.Free();
        }

        private static void ResultHandler(IntPtr chain, IntPtr context, ErrorCode error)
        {
            GCHandle handlerHandle = (GCHandle)context;
            Action<ErrorCode> handler = (handlerHandle.Target as Action<ErrorCode>);
            handler(error);
            handlerHandle.Free();
        }

        private static void TransactionHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr transaction)
        {
            GCHandle handlerHandle = (GCHandle)context;
            Action<ErrorCode, Transaction> handler = (handlerHandle.Target as Action<ErrorCode, Transaction>);
            handler(error, new Transaction(transaction));
            handlerHandle.Free();
        }

        private static void ValidateTransactionHandler(IntPtr chain, IntPtr context, ErrorCode error, string message)
        {
            GCHandle handlerHandle = (GCHandle)context;
            Action<ErrorCode, string> handler = (handlerHandle.Target as Action<ErrorCode, string>);
            handler(error, message);
            handlerHandle.Free();
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
    }

}