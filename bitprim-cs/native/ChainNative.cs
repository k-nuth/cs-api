using System;
using System.Runtime.InteropServices;

namespace BitprimCs.Native
{

public static class ChainNative
{
    // Chain-----------------------------------------------------------------------------------

    //typedef void (*block_height_fetch_handler_t)(chain_t, void*, int, UInt64 /*size_t*/ h);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void BlockHeightFetchHandler(IntPtr chain, IntPtr context, int error, UInt64 height);

    //typedef void (*last_height_fetch_handler_t)(chain_t, void*, int, UInt64 /*size_t*/ h);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void LastHeightFetchHandler(IntPtr chain, IntPtr context, int error, UIntPtr height);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_block_height(IntPtr chain, IntPtr context, byte[] hash, BlockHeightFetchHandler handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_last_height(IntPtr chain, IntPtr context, LastHeightFetchHandler handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int chain_get_block_height(IntPtr chain, byte[] hash, ref UInt64 height);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int chain_get_last_height(IntPtr chain, ref UInt64 height);

    // Block-----------------------------------------------------------------------------------

    //typedef void (*block_fetch_handler_t)(chain_t, void*, int, block_t block, uint64_t /*size_t*/ h);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void BlockFetchHandler(IntPtr chain, IntPtr context, IntPtr block, int error, UInt64 height);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_block_by_hash(IntPtr chain, IntPtr context, byte[] hash, BlockFetchHandler handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_block_by_height(IntPtr chain, IntPtr context, UInt64 height, BlockFetchHandler handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int chain_get_block_by_hash(IntPtr chain, byte[] hash, ref IntPtr out_block, ref UInt64 out_height);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int chain_get_block_by_height(IntPtr chain, UInt64 height, ref IntPtr out_block, ref UInt64 out_height);

    // Block header------------------------------------------------------------------------------
    //typedef void (*block_header_fetch_handler_t)(chain_t, void*, int, header_t header, UInt64 /*size_t*/ h);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void BlockHeaderFetchHandler(IntPtr chain, IntPtr context, int error, IntPtr header, UInt64 height);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_block_header_by_hash(IntPtr chain, IntPtr context, byte[] hash, BlockHeaderFetchHandler handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_block_header_by_height(IntPtr chain, IntPtr context, UInt64 height, BlockHeaderFetchHandler handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int chain_get_block_header_by_hash(IntPtr chain, byte[] hash, ref IntPtr out_header, ref UInt64 out_height);
    
    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int chain_get_block_header_by_height(IntPtr chain, UInt64 height, ref IntPtr out_header, ref UInt64 out_height);

    // Merkle block-----------------------------------------------------------------------------

    //typedef void (*merkle_block_fetch_handler_t)(chain_t, void*, int, merkle_block_t block, uint64_t /*size_t*/ h);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void MerkleBlockFetchHandler(IntPtr chain, IntPtr context, int error, IntPtr block, UInt64 h);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_merkle_block_by_hash(IntPtr chain, IntPtr context, byte[] hash, MerkleBlockFetchHandler handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_merkle_block_by_height(IntPtr chain, IntPtr context, UInt64 height, MerkleBlockFetchHandler handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int chain_get_merkle_block_by_hash(IntPtr chain, byte[] hash, ref IntPtr out_block, ref UInt64 out_height);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int chain_get_merkle_block_by_height(IntPtr chain, UInt64 height, ref IntPtr out_block, ref UInt64 out_height);

    // Compact block -------------------------------------------------------------------------

    //typedef void (*compact_block_fetch_handler_t)(chain_t, void*, int, compact_block_t block, uint64_t /*size_t*/ h);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void CompactBlockFetchHandler(IntPtr chain, IntPtr context, int error, IntPtr block, UInt64 height);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_compact_block_by_hash(IntPtr chain, IntPtr context, byte[] hash, CompactBlockFetchHandler handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_compact_block_by_height(IntPtr chain, IntPtr context, UInt64 height, CompactBlockFetchHandler handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int chain_get_compact_block_by_hash(IntPtr chain, byte[] hash, ref IntPtr out_block, ref UInt64 out_height);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int chain_get_compact_block_by_height(IntPtr chain, UInt64 height, ref IntPtr out_block, ref UInt64 out_height);

    // Transaction

    //typedef void (*transaction_fetch_handler_t)(chain_t, void*, int, transaction_t transaction, uint64_t /*size_t*/ i, uint64_t /*size_t*/ h);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TransactionFetchHandler(IntPtr chain, IntPtr context, int error, IntPtr transaction, UInt64 i, UInt64 h);

    //typedef void (*transaction_index_fetch_handler_t)(chain_t, void*, int, uint64_t /*size_t*/ position, uint64_t /*size_t*/ height);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TransactionIndexFetchHandler(IntPtr chain, IntPtr context, int error, UInt64 position, UInt64 height);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_transaction(IntPtr chain, IntPtr context, byte[] hash, int require_confirmed, TransactionFetchHandler handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_transaction_position(IntPtr chain, IntPtr context, byte[] hash, int require_confirmed, TransactionIndexFetchHandler handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int chain_get_transaction(IntPtr chain, byte[] hash, int require_confirmed, ref IntPtr out_transaction, ref UInt64 out_height, ref UInt64 out_index);
    
    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int chain_get_transaction_position(IntPtr chain, byte[] hash, int require_confirmed, ref UInt64 out_position, ref UInt64 out_height);

    // Spend ---------------------------------------------------------------------------------------------

    //typedef void (*spend_fetch_handler_t)(chain_t, void*, int, input_point_t input_point);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SpendFetchHandler(IntPtr chain, IntPtr context, int error, IntPtr inputPoint);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_spend(IntPtr chain, IntPtr context, IntPtr op, SpendFetchHandler handler);

    // History -------------------------------------------------------------------------------------------

    //typedef void (*history_fetch_handler_t)(chain_t, void*, int, history_compact_list_t history);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void HistoryFetchHandler(IntPtr chain, IntPtr context, int error, IntPtr history);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_history(IntPtr chain, IntPtr context, IntPtr address, UInt64 limit, UInt64 from_height, HistoryFetchHandler handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int chain_get_history(IntPtr chain, IntPtr address, UInt64 limit, UInt64 from_height, ref IntPtr out_history);

    // Stealth ---------------------------------------------------------------------

    //typedef void (*stealth_fetch_handler_t)(chain_t chain, void*, int, stealth_compact_list_t stealth);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void StealthFetchHandler(IntPtr chain, IntPtr context, int error, IntPtr stealth);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_stealth(IntPtr chain, IntPtr context, IntPtr filter, UInt64 fromHeight, StealthFetchHandler handler);

    // Block indexes ---------------------------------------------------------------

    //typedef void (*block_locator_fetch_handler_t)(chain_t, void*, int, get_headers_ptr_t);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void BlockLocatorFetchHandler(IntPtr chain, IntPtr context, int error, IntPtr getHeaders);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_block_locator(IntPtr chain, IntPtr context, IntPtr heights, BlockLocatorFetchHandler handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int chain_get_block_locator(IntPtr chain, IntPtr heights, ref IntPtr outHeaders);

    // Subscribers -----------------------------------------------------------------

    //typedef int (*reorganize_handler_t)(chain_t, void*, int, uint64_t /*size_t*/, block_list_t, block_list_t);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void ReorganizeHandler(IntPtr chain, IntPtr context, int error, UInt64 u, IntPtr blockList, IntPtr blockList2);

    //typedef int (*transaction_handler_t)(chain_t, void*, int, transaction_t);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TransactionHandler(IntPtr chain, IntPtr context, int error, IntPtr transaction);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_subscribe_blockchain(IntPtr chain, IntPtr context, ReorganizeHandler handler);


    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_subscribe_transaction(IntPtr chain, IntPtr context, TransactionHandler handler);


    // Organizers.
    //-------------------------------------------------------------------------

    //typedef void (*result_handler_t)(chain_t, void*, int);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void ResultHandler(IntPtr chain, IntPtr context, int error);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_organize_block(IntPtr chain, IntPtr context, IntPtr block, ResultHandler handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int chain_organize_block_sync(IntPtr chain, IntPtr block);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_organize_transaction(IntPtr chain, IntPtr context, IntPtr transaction, ResultHandler handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int chain_organize_transaction_sync(IntPtr chain, IntPtr transaction);


    // Misc ------------------------------------------------
    //typedef void (*validate_tx_handler_t)(chain_t, void*, int, char const* message);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void ValidateTxHandler(IntPtr chain, IntPtr context, int error, [MarshalAs(UnmanagedType.LPStr)]string message);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern IntPtr hex_to_tx([MarshalAs(UnmanagedType.LPStr)]string txHex);


    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_validate_tx(IntPtr chain, IntPtr context, IntPtr transaction, ValidateTxHandler handler);

}

}