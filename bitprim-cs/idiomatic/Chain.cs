using BitprimCs.Native;
using System;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace BitprimCs
{

public class Chain
{
    private IntPtr nativeInstance_;

    #region Chain

    public void FetchBlockHeight(byte[] blockHash, Action<int, UInt64> handler)
    {
        var managedHash = new hash_t
        {
            hash = blockHash
        };
        IntPtr contextPtr = CreateContext(handler, managedHash);
        ChainNative.chain_fetch_block_height(nativeInstance_, contextPtr, managedHash, FetchBlockHeightHandler);
    }

    public Tuple<int, UInt64> GetBlockHeight(byte[] blockHash)
    {
        UInt64 height = 0;
        var managedHash = new hash_t
        {
            hash = blockHash
        };        
        int result = ChainNative.chain_get_block_height(nativeInstance_, managedHash, ref height);
        return new Tuple<int, UInt64>(result, height);
    }

    public void FetchLastHeight(Action<int, UInt64> handler)
    {
        GCHandle handlerHandle = GCHandle.Alloc(handler);
        IntPtr handlerPtr = (IntPtr) handlerHandle;
        ChainNative.chain_fetch_last_height(nativeInstance_, handlerPtr, FetchLastHeightHandler);
    }

    public Tuple<int, UInt64> GetLastHeight()
    {
        UInt64 height = 0;
        int result = ChainNative.chain_get_last_height(nativeInstance_, ref height);
        return new Tuple<int, UInt64>(result, height);
    }

    #endregion //Chain

    #region Block

    public void FetchBlockByHash(byte[] blockHash, Action<int, Block> handler)
    {
        var managedHash = new hash_t
        {
            hash = blockHash
        };
        IntPtr contextPtr = CreateContext(handler, managedHash);
        ChainNative.chain_fetch_block_by_hash(nativeInstance_, contextPtr, managedHash, FetchBlockByHashHandler);
    }

    public Tuple<int, Block, UInt64> GetBlockByHash(byte[] blockHash)
    {
        IntPtr block = IntPtr.Zero;
        UInt64 height = 0;
        var managedHash = new hash_t
        {
            hash = blockHash
        };
        int result = ChainNative.chain_get_block_by_hash(nativeInstance_, managedHash, ref block, ref height);
        return new Tuple<int, Block, UInt64>(result, new Block(block), height);
    }

    public void FetchBlockByHeight(UInt64 height, Action<int, Block> handler)
    {
        GCHandle handlerHandle = GCHandle.Alloc(handler);
        IntPtr handlerPtr = (IntPtr) handlerHandle;
        ChainNative.chain_fetch_block_by_height(nativeInstance_, handlerPtr, height, FetchBlockByHeightHandler);
    }

    public Tuple<int, Block, UInt64> GetBlockByHeight(UInt64 height)
    {
        IntPtr block = IntPtr.Zero;
        UInt64 actualHeight = 0; //Should always match input height
        int result = ChainNative.chain_get_block_by_height(nativeInstance_, height, ref block, ref actualHeight);
        return new Tuple<int, Block, UInt64>(result, new Block(block), actualHeight);
    }

    #endregion //Block

    #region Block header

    public void FetchBlockHeaderByHash(byte[] blockHash, Action<int, Header> handler)
    {
        var managedHash = new hash_t
        {
            hash = blockHash
        };
        IntPtr contextPtr = CreateContext(handler, managedHash);
        ChainNative.chain_fetch_block_header_by_hash(nativeInstance_, contextPtr, managedHash, FetchBlockHeaderByHashHandler);
    }

    public Tuple<int, Header, UInt64> GetBlockHeaderByHash(byte[] blockHash)
    {
        IntPtr header = IntPtr.Zero;
        UInt64 height = 0;
        var managedHash = new hash_t
        {
            hash = blockHash
        };
        int result = ChainNative.chain_get_block_header_by_hash(nativeInstance_, managedHash, ref header, ref height);
        return new Tuple<int, Header, UInt64>(result, new Header(header), height);
    }

    public void FetchBlockHeaderByHeight(UInt64 height, Action<int, Header> handler)
    {
        GCHandle handlerHandle = GCHandle.Alloc(handler);
        IntPtr handlerPtr = (IntPtr) handlerHandle;
        ChainNative.chain_fetch_block_header_by_height(nativeInstance_, handlerPtr, height, FetchBlockHeaderbyHeightHandler);
    }

    public Tuple<int, Header, UInt64> GetBlockHeaderByHeight(UInt64 height)
    {
        IntPtr header = IntPtr.Zero;
        UInt64 actualHeight = 0; //Should always match input height
        int result = ChainNative.chain_get_block_header_by_height(nativeInstance_, height, ref header, ref actualHeight);
        return new Tuple<int, Header, UInt64>(result, new Header(header), actualHeight);
    }

    #endregion //Block header

    #region Merkle Block

    public void FetchMerkleBlockByHash(byte[] blockHash, Action<int, MerkleBlock, UInt64> handler)
    {
        var managedHash = new hash_t
        {
            hash = blockHash
        };
        IntPtr contextPtr = CreateContext(handler, managedHash);
        ChainNative.chain_fetch_merkle_block_by_hash(nativeInstance_, contextPtr, managedHash, FetchMerkleBlockByHashHandler);
    }

    public Tuple<int, MerkleBlock, UInt64> GetMerkleBlockByHash(byte[] blockHash)
    {
        IntPtr merkleBlock = IntPtr.Zero;
        UInt64 height = 0;
        var managedHash = new hash_t
        {
            hash = blockHash
        };
        int result = ChainNative.chain_get_merkle_block_by_hash(nativeInstance_, managedHash, ref merkleBlock, ref height);
        return new Tuple<int, MerkleBlock, UInt64>(result, new MerkleBlock(merkleBlock), height);
    }

    public void FetchMerkleBlockByHeight(UInt64 height, Action<int, MerkleBlock> handler)
    {
        GCHandle handlerHandle = GCHandle.Alloc(handler);
        IntPtr handlerPtr = (IntPtr) handlerHandle;
        ChainNative.chain_fetch_merkle_block_by_height(nativeInstance_, handlerPtr, height, FetchMerkleBlockByHeightHandler);
    }

    public Tuple<int, MerkleBlock, UInt64> GetMerkleBlockByHeight(UInt64 height)
    {
        IntPtr merkleBlock = IntPtr.Zero;
        UInt64 actualHeight = 0; //Should always match input height
        int result = ChainNative.chain_get_merkle_block_by_height(nativeInstance_, height, ref merkleBlock, ref actualHeight);
        return new Tuple<int, MerkleBlock, UInt64>(result, new MerkleBlock(merkleBlock), actualHeight);
    }

    #endregion //Merkle Block

    #region Compact block

    public void FetchCompactBlockByHash(byte[] blockHash, Action<int, CompactBlock> handler)
    {
        var managedHash = new hash_t
        {
            hash = blockHash
        };
        IntPtr contextPtr = CreateContext(handler, managedHash);
        ChainNative.chain_fetch_compact_block_by_hash(nativeInstance_, contextPtr, managedHash, FetchCompactBlockByHashHandler);
    }

    public Tuple<int, CompactBlock, UInt64> GetCompactBlockByHash(byte[] blockHash)
    {
        IntPtr compactBlock = IntPtr.Zero;
        UInt64 height = 0;
        var managedHash = new hash_t
        {
            hash = blockHash
        };
        int result = ChainNative.chain_get_compact_block_by_hash(nativeInstance_, managedHash, ref compactBlock, ref height);
        return new Tuple<int, CompactBlock, UInt64>(result, new CompactBlock(compactBlock), height);
    }

    public void FetchCompactBlockByHeight(UInt64 height, Action<int, CompactBlock> handler)
    {
        GCHandle handlerHandle = GCHandle.Alloc(handler);
        IntPtr handlerPtr = (IntPtr) handlerHandle;
        ChainNative.chain_fetch_compact_block_by_height(nativeInstance_, handlerPtr, height, FetchCompactBlockByHeightHandler);
    }

    public Tuple<int, CompactBlock, UInt64> GetCompactBlockByHeight(UInt64 height)
    {
        IntPtr compactBlock = IntPtr.Zero;
        UInt64 actualHeight = 0; //Should always match input height
        int result = ChainNative.chain_get_compact_block_by_height(nativeInstance_, height, ref compactBlock, ref actualHeight);
        return new Tuple<int, CompactBlock, UInt64>(result, new CompactBlock(compactBlock), actualHeight);
    }

    #endregion //Compact block

    #region Transaction

    public void FetchTransaction(byte[] txHash, bool requireConfirmed, Action<int, Transaction, UInt64, UInt64> handler)
    {
        var managedHash = new hash_t
        {
            hash = txHash
        };
        IntPtr contextPtr = CreateContext(handler, managedHash);
        ChainNative.chain_fetch_transaction(nativeInstance_, contextPtr, managedHash, requireConfirmed? 1:0, FetchTransactionByHashHandler);
    }

    public Tuple<int, Transaction, UInt64, UInt64> GetTransaction(byte[] txHash, bool requireConfirmed)
    {
        IntPtr transaction = IntPtr.Zero;
        UInt64 index = 0;
        UInt64 height = 0;
        var managedHash = new hash_t
        {
            hash = txHash
        };
        int result = ChainNative.chain_get_transaction(nativeInstance_, managedHash, requireConfirmed? 1:0, ref transaction, ref index, ref height);
        return new Tuple<int, Transaction, UInt64, UInt64>(result, new Transaction(transaction), index, height);
    }

    public void FetchTransactionPosition(byte[] txHash, bool requireConfirmed, Action<int, UInt64, UInt64> handler)
    {
        var managedHash = new hash_t
        {
            hash = txHash
        };
        IntPtr contextPtr = CreateContext(handler, managedHash);
        ChainNative.chain_fetch_transaction_position(nativeInstance_, contextPtr, managedHash, requireConfirmed? 1:0, FetchTransactionPositionHandler);
    }

    public Tuple<int, UInt64, UInt64> GetTransactionPosition(byte[] txHash, bool requireConfirmed)
    {
        UInt64 index = 0;
        UInt64 height = 0;
        var managedHash = new hash_t
        {
            hash = txHash
        };
        int result = ChainNative.chain_get_transaction_position(nativeInstance_, managedHash, requireConfirmed? 1:0, ref index, ref height);
        return new Tuple<int, UInt64, UInt64>(result, index, height);
    }

    #endregion //Transaction

    #region Spend

    public void FetchSpend(OutputPoint outputPoint, Action<int, Point> handler)
    {
        IntPtr contextPtr = CreateContext(handler, outputPoint);
        ChainNative.chain_fetch_spend(nativeInstance_, contextPtr, outputPoint.NativeInstance, FetchSpendHandler);
    }

    #endregion //Spend

    #region History

    public void FetchHistory(PaymentAddress address, UInt64 limit, UInt64 fromHeight, Action<int, HistoryCompactList> handler)
    {
        GCHandle handlerHandle = GCHandle.Alloc(handler);
        IntPtr handlerPtr = (IntPtr) handlerHandle;
        ChainNative.chain_fetch_history(nativeInstance_, handlerPtr, address.NativeInstance, limit, fromHeight, FetchHistoryHandler);
    }

    public Tuple<int, HistoryCompactList> GetHistory(PaymentAddress address, UInt64 limit, UInt64 fromHeight)
    {
        IntPtr history = IntPtr.Zero;
        int result = ChainNative.chain_get_history(nativeInstance_, address.NativeInstance, limit, fromHeight, ref history);
        return new Tuple<int, HistoryCompactList>(result, new HistoryCompactList(history));
    }

    #endregion //History

    #region Stealth

    public void FetchStealth(Binary filter, UInt64 fromHeight, Action<int, StealthCompactList> handler)
    {
        GCHandle handlerHandle = GCHandle.Alloc(handler);
        IntPtr handlerPtr = (IntPtr) handlerHandle;
        ChainNative.chain_fetch_stealth(nativeInstance_, handlerPtr, filter.NativeInstance, fromHeight, FetchStealthHandler);
    }

    #endregion //Stealth

    #region Block indexes

    public void FetchBlockLocator(BlockIndexCollection indexes, Action<int, HeaderReader> handler)
    {
        GCHandle handlerHandle = GCHandle.Alloc(handler);
        IntPtr handlerPtr = (IntPtr) handlerHandle;
        ChainNative.chain_fetch_block_locator(nativeInstance_, handlerPtr, indexes.NativeInstance, FetchBlockLocatorHandler);
    }

    public Tuple<int, HeaderReader> GetBlockLocator(BlockIndexCollection indexes)
    {
        IntPtr headerReader = IntPtr.Zero;
        int result = ChainNative.chain_get_block_locator(nativeInstance_, indexes.NativeInstance, ref headerReader);
        return new Tuple<int, HeaderReader>(result, new HeaderReader(headerReader));
    }

    #endregion //Block indexes

    #region Subscribers

    public void SubscribeToBlockChain(Action<UInt64, BlockList, BlockList> handler)
    {
        GCHandle handlerHandle = GCHandle.Alloc(handler);
        IntPtr handlerPtr = (IntPtr) handlerHandle;
        ChainNative.chain_subscribe_blockchain(nativeInstance_, handlerPtr, ReorganizeHandler);
    }

    public void SubscribeToTransaction(Action<UInt64, BlockList, BlockList> handler)
    {
        GCHandle handlerHandle = GCHandle.Alloc(handler);
        IntPtr handlerPtr = (IntPtr) handlerHandle;
        ChainNative.chain_subscribe_transaction(nativeInstance_, handlerPtr, TransactionHandler);
    }

    #endregion //Subscribers

    #region Organizers

    public void OrganizeBlock(Block block, Action<int> handler)
    {
        GCHandle handlerHandle = GCHandle.Alloc(handler);
        IntPtr handlerPtr = (IntPtr) handlerHandle;
        ChainNative.chain_organize_block(nativeInstance_, handlerPtr, block.NativeInstance, ResultHandler);
    }

    public int OrganizeBlockSync(Block block)
    {
        return ChainNative.chain_organize_block_sync(nativeInstance_, block.NativeInstance);
    }

    public void OrganizeTransaction(Transaction transaction, Action<int> handler)
    {
        GCHandle handlerHandle = GCHandle.Alloc(handler);
        IntPtr handlerPtr = (IntPtr) handlerHandle;
        ChainNative.chain_organize_transaction(nativeInstance_, handlerPtr, transaction.NativeInstance, ResultHandler);
    }

    public int OrganizeTransactionSync(Transaction transaction)
    {
        return ChainNative.chain_organize_transaction_sync(nativeInstance_, transaction.NativeInstance);
    }

    #endregion //Organizers

    #region Misc

    public void ValidateTransaction(Transaction transaction, Action handler)
    {
        GCHandle handlerHandle = GCHandle.Alloc(handler);
        IntPtr handlerPtr = (IntPtr) handlerHandle;
        ChainNative.chain_validate_tx(nativeInstance_, handlerPtr, transaction.NativeInstance, ValidateTransactionHandler);
    }

    #endregion //Misc

    internal Chain(IntPtr nativeInstance)
    {
        nativeInstance_ = nativeInstance;
    }

    private static void FetchBlockHeaderByHashHandler(IntPtr chain, IntPtr contextPtr, int error, IntPtr header, UInt64 height)
    {
        GCHandle contextHandle = (GCHandle) contextPtr;
        Tuple<Action<int, Header>, hash_t> context = (contextHandle.Target as Tuple<Action<int, Header>, hash_t>);
        Action<int, Header> handler = context.Item1;
        handler(error, new Header(header));
        contextHandle.Free();
    }

    private static void FetchBlockHeaderbyHeightHandler(IntPtr chain, IntPtr context, int error, IntPtr header, UInt64 height)
    {
        GCHandle handlerHandle = (GCHandle) context;
        Action<int, Header> handler = (handlerHandle.Target as Action<int, Header>);
        handler(error, new Header(header));
        handlerHandle.Free();
    }

    private static void FetchBlockByHashHandler(IntPtr chain, IntPtr contextPtr, int error, IntPtr block, UInt64 height)
    {
        GCHandle contextHandle = (GCHandle) contextPtr;
        Tuple<Action<int, Block>, hash_t> context = (contextHandle.Target as Tuple<Action<int, Block>, hash_t>);
        Action<int, Block> handler = context.Item1;
        handler(error, new Block(block));
        contextHandle.Free();
    }

    private static void FetchBlockByHeightHandler(IntPtr chain, IntPtr context, int error, IntPtr block, UInt64 height)
    {
        GCHandle handlerHandle = (GCHandle) context;
        Action<int, Block> handler = (handlerHandle.Target as Action<int, Block>);
        handler(error, new Block(block));
        handlerHandle.Free();
    }

    private static void FetchBlockHeightHandler(IntPtr chain, IntPtr contextPtr, int error, UInt64 height)
    {
        GCHandle contextHandle = (GCHandle) contextPtr;
        Tuple<Action<int, UInt64>, hash_t> context = (contextHandle.Target as Tuple<Action<int, UInt64>, hash_t>);
        Action<int, UInt64> handler = context.Item1;
        handler(error, height);
        contextHandle.Free();
    }

    private static void FetchBlockLocatorHandler(IntPtr chain, IntPtr context, int error, IntPtr headerReader)
    {
        GCHandle handlerHandle = (GCHandle) context;
        Action<int, HeaderReader> handler = (handlerHandle.Target as Action<int, HeaderReader>);
        handler(error, new HeaderReader(headerReader));
        handlerHandle.Free();
    }

    private static void FetchCompactBlockByHashHandler(IntPtr chain, IntPtr contextPtr, int error, IntPtr compactBlock, UInt64 height)
    {
        GCHandle contextHandle = (GCHandle) contextPtr;
        Tuple<Action<int, CompactBlock>, hash_t> context = (contextHandle.Target as Tuple<Action<int, CompactBlock>, hash_t>);
        Action<int, CompactBlock> handler = context.Item1;
        handler(error, new CompactBlock(compactBlock));
        contextHandle.Free();
    }

    private static void FetchCompactBlockByHeightHandler(IntPtr chain, IntPtr context, int error, IntPtr compactBlock, UInt64 height)
    {
        GCHandle handlerHandle = (GCHandle) context;
        Action<int, CompactBlock> handler = (handlerHandle.Target as Action<int, CompactBlock>);
        handler(error, new CompactBlock(compactBlock));
        handlerHandle.Free();
    }

    private static void FetchHistoryHandler(IntPtr chain, IntPtr context, int error, IntPtr history)
    {
        GCHandle handlerHandle = (GCHandle) context;
        Action<int, HistoryCompactList> handler = (handlerHandle.Target as Action<int, HistoryCompactList>);
        handler(error, new HistoryCompactList(history));
        handlerHandle.Free();
    }

    private static void FetchLastHeightHandler(IntPtr chain, IntPtr context, int error, UIntPtr height)
    {
        GCHandle handlerHandle = (GCHandle) context;
        Action<int, UInt64> handler = (handlerHandle.Target as Action<int, UInt64>);
        handler(error, (UInt64) height);
        handlerHandle.Free();
    }

    private static void FetchMerkleBlockByHashHandler(IntPtr chain, IntPtr contextPtr, int error, IntPtr merkleBlock, UInt64 height)
    {
        GCHandle contextHandle = (GCHandle) contextPtr;
        Tuple<Action<int, MerkleBlock, UInt64>, hash_t> context = (contextHandle.Target as Tuple<Action<int, MerkleBlock, UInt64>, hash_t>);
        Action<int, MerkleBlock, UInt64> handler = context.Item1;
        handler(error, new MerkleBlock(merkleBlock), height);
        contextHandle.Free();
    }

    private static void FetchMerkleBlockByHeightHandler(IntPtr chain, IntPtr context, int error, IntPtr merkleBlock, UInt64 height)
    {
        GCHandle handlerHandle = (GCHandle) context;
        Action<int, MerkleBlock> handler = (handlerHandle.Target as Action<int, MerkleBlock>);
        handler(error, new MerkleBlock(merkleBlock));
        handlerHandle.Free();
    }

    private static void FetchSpendHandler(IntPtr chain, IntPtr contextPtr, int error, IntPtr inputPoint)
    {
        GCHandle contextHandle = (GCHandle) contextPtr;
        Tuple<Action<int, Point>, OutputPoint> context = (contextHandle.Target as Tuple<Action<int, Point>, OutputPoint>);
        Action<int, Point> handler = context.Item1;
        handler(error, new Point(inputPoint));
        contextHandle.Free();
    }

    private static void FetchStealthHandler(IntPtr chain, IntPtr context, int error, IntPtr stealth)
    {
        GCHandle handlerHandle = (GCHandle) context;
        Action<int, StealthCompactList> handler = (handlerHandle.Target as Action<int, StealthCompactList>);
        handler(error, new StealthCompactList(stealth));
        handlerHandle.Free();
    }

    private static void FetchTransactionByHashHandler(IntPtr chain, IntPtr contextPtr, int error, IntPtr transaction, UInt64 index, UInt64 height)
    {
        GCHandle contextHandle = (GCHandle) contextPtr;
        Tuple<Action<int, Transaction, UInt64, UInt64>, hash_t> context = (contextHandle.Target as Tuple<Action<int, Transaction, UInt64, UInt64>, hash_t>);
        Action<int, Transaction, UInt64, UInt64> handler = context.Item1;
        handler(error, new Transaction(transaction), index, height);
        contextHandle.Free();
    }

    private static void FetchTransactionByHeightHandler(IntPtr chain, IntPtr context, int error, IntPtr transaction, UInt64 index, UInt64 height)
    {
        GCHandle handlerHandle = (GCHandle) context;
        Action<int, Transaction, UInt64, UInt64> handler = (handlerHandle.Target as Action<int, Transaction, UInt64, UInt64>);
        handler(error, new Transaction(transaction), index, height);
        handlerHandle.Free();
    }

    private static void FetchTransactionPositionHandler(IntPtr chain, IntPtr contextPtr, int error, UInt64 index, UInt64 height)
    {
        GCHandle contextHandle = (GCHandle) contextPtr;
        Tuple<Action<int, UInt64, UInt64>, hash_t> context = (contextHandle.Target as Tuple<Action<int, UInt64, UInt64>, hash_t>);
        Action<int, UInt64, UInt64> handler = context.Item1;
        handler(error, index, height);
        contextHandle.Free();
    }

    private static void ReorganizeHandler(IntPtr chain, IntPtr context, int error, UInt64 u, IntPtr blockList, IntPtr blockList2)
    {
        GCHandle handlerHandle = (GCHandle) context;
        Action<int, UInt64, BlockList, BlockList> handler = (handlerHandle.Target as Action<int, UInt64, BlockList, BlockList>);
        handler(error, u, new BlockList(blockList), new BlockList(blockList2));
        handlerHandle.Free();
    }

    private static void ResultHandler(IntPtr chain, IntPtr context, int error)
    {
        GCHandle handlerHandle = (GCHandle) context;
        Action<int> handler = (handlerHandle.Target as Action<int>);
        handler(error);
        handlerHandle.Free();
    }

    private static void TransactionHandler(IntPtr chain, IntPtr context, int error, IntPtr transaction)
    {
        GCHandle handlerHandle = (GCHandle) context;
        Action<int, Transaction> handler = (handlerHandle.Target as Action<int, Transaction>);
        handler(error, new Transaction(transaction));
        handlerHandle.Free();
    }

    private static void ValidateTransactionHandler(IntPtr chain, IntPtr context, int error, string message)
    {
        GCHandle handlerHandle = (GCHandle) context;
        Action<int, string> handler = (handlerHandle.Target as Action<int, string>);
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
        return (IntPtr) contextHandle;
    }
}

}