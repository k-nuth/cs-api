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
        GCHandle handlerHandle = GCHandle.Alloc(handler);
        IntPtr handlerPtr = (IntPtr) handlerHandle;
        ChainNative.chain_fetch_block_height(nativeInstance_, handlerPtr, blockHash, FetchBlockHeightHandler);
    }

    public Tuple<int, UInt64> GetBlockHeight(byte[] blockHash)
    {
        UInt64 height = 0;
        int result = ChainNative.chain_get_block_height(nativeInstance_, blockHash, ref height);
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

    public void FetchBlockByHash(byte[] hash, Action<int, Block> handler)
    {
        GCHandle handlerHandle = GCHandle.Alloc(handler);
        IntPtr handlerPtr = (IntPtr) handlerHandle;
        ChainNative.chain_fetch_block_by_hash(nativeInstance_, handlerPtr, hash, FetchBlockHandler);
    }

    public Tuple<int, Block, UInt64> GetBlockByHash(byte[] hash)
    {
        IntPtr block = IntPtr.Zero;
        UInt64 height = 0;
        int result = ChainNative.chain_get_block_by_hash(nativeInstance_, hash, ref block, ref height);
        return new Tuple<int, Block, UInt64>(result, new Block(block), height);
    }

    public void FetchBlockByHeight(UInt64 height, Action<int, Block> handler)
    {
        GCHandle handlerHandle = GCHandle.Alloc(handler);
        IntPtr handlerPtr = (IntPtr) handlerHandle;
        ChainNative.chain_fetch_block_by_height(nativeInstance_, handlerPtr, height, FetchBlockHandler);
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

    public void FetchBlockHeaderByHash(byte[] hash, Action<int, Header> handler)
    {
        GCHandle handlerHandle = GCHandle.Alloc(handler);
        IntPtr handlerPtr = (IntPtr) handlerHandle;
        ChainNative.chain_fetch_block_header_by_hash(nativeInstance_, handlerPtr, hash, FetchBlockHeaderHandler);
    }

    public Tuple<int, Header, UInt64> GetBlockHeaderByHash(byte[] hash)
    {
        IntPtr header = IntPtr.Zero;
        UInt64 height = 0;
        int result = ChainNative.chain_get_block_header_by_hash(nativeInstance_, hash, ref header, ref height);
        return new Tuple<int, Header, UInt64>(result, new Header(header), height);
    }

    public void FetchBlockHeaderByHeight(UInt64 height, Action<int, Header> handler)
    {
        GCHandle handlerHandle = GCHandle.Alloc(handler);
        IntPtr handlerPtr = (IntPtr) handlerHandle;
        ChainNative.chain_fetch_block_header_by_height(nativeInstance_, handlerPtr, height, FetchBlockHeaderHandler);
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

    public void FetchMerkleBlockByHash(byte[] hash, Action<int, MerkleBlock> handler)
    {
        GCHandle handlerHandle = GCHandle.Alloc(handler);
        IntPtr handlerPtr = (IntPtr) handlerHandle;
        ChainNative.chain_fetch_merkle_block_by_hash(nativeInstance_, handlerPtr, hash, FetchMerkleBlockHandler);
    }

    public Tuple<int, MerkleBlock, UInt64> GetMerkleBlockByHash(byte[] hash)
    {
        IntPtr merkleBlock = IntPtr.Zero;
        UInt64 height = 0;
        int result = ChainNative.chain_get_merkle_block_by_hash(nativeInstance_, hash, ref merkleBlock, ref height);
        return new Tuple<int, MerkleBlock, UInt64>(result, new MerkleBlock(merkleBlock), height);
    }

    public void FetchMerkleBlockByHeight(UInt64 height, Action<int, MerkleBlock> handler)
    {
        GCHandle handlerHandle = GCHandle.Alloc(handler);
        IntPtr handlerPtr = (IntPtr) handlerHandle;
        ChainNative.chain_fetch_merkle_block_by_height(nativeInstance_, handlerPtr, height, FetchMerkleBlockHandler);
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

    public void FetchCompactBlockByHash(byte[] hash, Action<int, CompactBlock> handler)
    {
        GCHandle handlerHandle = GCHandle.Alloc(handler);
        IntPtr handlerPtr = (IntPtr) handlerHandle;
        ChainNative.chain_fetch_compact_block_by_hash(nativeInstance_, handlerPtr, hash, FetchCompactBlockHandler);
    }

    public Tuple<int, CompactBlock, UInt64> GetCompactBlockByHash(byte[] hash)
    {
        IntPtr compactBlock = IntPtr.Zero;
        UInt64 height = 0;
        int result = ChainNative.chain_get_compact_block_by_hash(nativeInstance_, hash, ref compactBlock, ref height);
        return new Tuple<int, CompactBlock, UInt64>(result, new CompactBlock(compactBlock), height);
    }

    public void FetchCompactBlockByHeight(UInt64 height, Action<int, CompactBlock> handler)
    {
        GCHandle handlerHandle = GCHandle.Alloc(handler);
        IntPtr handlerPtr = (IntPtr) handlerHandle;
        ChainNative.chain_fetch_compact_block_by_height(nativeInstance_, handlerPtr, height, FetchCompactBlockHandler);
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

    public void FetchTransaction(byte[] hash, bool requireConfirmed, Action<int, Transaction, UInt64, UInt64> handler)
    {
        GCHandle handlerHandle = GCHandle.Alloc(handler);
        IntPtr handlerPtr = (IntPtr) handlerHandle;
        ChainNative.chain_fetch_transaction(nativeInstance_, handlerPtr, hash, requireConfirmed? 1:0, FetchTransactionHandler);
    }

    public Tuple<int, Transaction, UInt64, UInt64> GetTransaction(byte[] hash, bool requireConfirmed)
    {
        IntPtr transaction = IntPtr.Zero;
        UInt64 index = 0;
        UInt64 height = 0;
        int result = ChainNative.chain_get_transaction(nativeInstance_, hash, requireConfirmed? 1:0, ref transaction, ref index, ref height);
        return new Tuple<int, Transaction, UInt64, UInt64>(result, new Transaction(transaction), index, height);
    }

    public void FetchTransactionPosition(byte[] hash, bool requireConfirmed, Action<int, UInt64, UInt64> handler)
    {
        GCHandle handlerHandle = GCHandle.Alloc(handler);
        IntPtr handlerPtr = (IntPtr) handlerHandle;
        ChainNative.chain_fetch_transaction_position(nativeInstance_, handlerPtr, hash, requireConfirmed? 1:0, FetchTransactionPositionHandler);
    }

    public Tuple<int, UInt64, UInt64> GetTransactionPosition(byte[] hash, bool requireConfirmed)
    {
        UInt64 index = 0;
        UInt64 height = 0;
        int result = ChainNative.chain_get_transaction_position(nativeInstance_, hash, requireConfirmed? 1:0, ref index, ref height);
        return new Tuple<int, UInt64, UInt64>(result, index, height);
    }

    #endregion //Transaction

    #region Spend

    public void FetchSpend(OutputPoint outputPoint, Action<int, Point> handler)
    {
        GCHandle handlerHandle = GCHandle.Alloc(handler);
        IntPtr handlerPtr = (IntPtr) handlerHandle;
        ChainNative.chain_fetch_spend(nativeInstance_, handlerPtr, outputPoint.NativeInstance, FetchSpendHandler);
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

    private static void FetchBlockHeaderHandler(IntPtr chain, IntPtr context, int error, IntPtr header, UInt64 height)
    {
        GCHandle handlerHandle = (GCHandle) context;
        Action<int, Header> handler = (handlerHandle.Target as Action<int, Header>);
        handler(error, new Header(header));
        handlerHandle.Free();
    }

    private static void FetchBlockHandler(IntPtr chain, IntPtr context, int error, IntPtr block, UInt64 height)
    {
        GCHandle handlerHandle = (GCHandle) context;
        Action<int, Block> handler = (handlerHandle.Target as Action<int, Block>);
        handler(error, new Block(block));
        handlerHandle.Free();
    }

    private static void FetchBlockHeaderByHeightHandler(IntPtr chain, IntPtr context, int error, IntPtr header, UInt64 height)
    {
        GCHandle handlerHandle = (GCHandle) context;
        Action<int, Header> handler = (handlerHandle.Target as Action<int, Header>);
        handler(error, new Header(header));
        handlerHandle.Free();
    }

    private static void FetchBlockHeightHandler(IntPtr chain, IntPtr context, int error, UInt64 height)
    {
        GCHandle handlerHandle = (GCHandle) context;
        Action<int, UInt64> handler = (handlerHandle.Target as Action<int, UInt64>);
        handler(error, height);
        handlerHandle.Free();
    }

    private static void FetchBlockLocatorHandler(IntPtr chain, IntPtr context, int error, IntPtr headerReader)
    {
        GCHandle handlerHandle = (GCHandle) context;
        Action<int, HeaderReader> handler = (handlerHandle.Target as Action<int, HeaderReader>);
        handler(error, new HeaderReader(headerReader));
        handlerHandle.Free();
    }

    private static void FetchCompactBlockHandler(IntPtr chain, IntPtr context, int error, IntPtr compactBlock, UInt64 height)
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

    private static void FetchMerkleBlockHandler(IntPtr chain, IntPtr context, int error, IntPtr merkleBlock, UInt64 height)
    {
        GCHandle handlerHandle = (GCHandle) context;
        Action<int, MerkleBlock> handler = (handlerHandle.Target as Action<int, MerkleBlock>);
        handler(error, new MerkleBlock(merkleBlock));
        handlerHandle.Free();
    }

    private static void FetchSpendHandler(IntPtr chain, IntPtr context, int error, IntPtr inputPoint)
    {
        GCHandle handlerHandle = (GCHandle) context;
        Action<int, Point> handler = (handlerHandle.Target as Action<int, Point>);
        handler(error, new Point(inputPoint));
        handlerHandle.Free();
    }

    private static void FetchStealthHandler(IntPtr chain, IntPtr context, int error, IntPtr stealth)
    {
        GCHandle handlerHandle = (GCHandle) context;
        Action<int, StealthCompactList> handler = (handlerHandle.Target as Action<int, StealthCompactList>);
        handler(error, new StealthCompactList(stealth));
        handlerHandle.Free();
    }

    private static void FetchTransactionHandler(IntPtr chain, IntPtr context, int error, IntPtr transaction, UInt64 index, UInt64 height)
    {
        GCHandle handlerHandle = (GCHandle) context;
        Action<int, Transaction, UInt64, UInt64> handler = (handlerHandle.Target as Action<int, Transaction, UInt64, UInt64>);
        handler(error, new Transaction(transaction), index, height);
        handlerHandle.Free();
    }

    private static void FetchTransactionPositionHandler(IntPtr chain, IntPtr context, int error, UInt64 index, UInt64 height)
    {
        GCHandle handlerHandle = (GCHandle) context;
        Action<int, UInt64, UInt64> handler = (handlerHandle.Target as Action<int, UInt64, UInt64>);
        handler(error, index, height);
        handlerHandle.Free();
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
}

}