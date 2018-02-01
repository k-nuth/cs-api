using System;
using System.Runtime.InteropServices;

namespace Bitprim.Native
{

public static class ChainNative
{
    // Chain-----------------------------------------------------------------------------------

    //typedef void (*block_height_fetch_handler_t)(chain_t, void*, error_code_t, UInt64 /*size_t*/ h);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FetchBlockHeightHandler(IntPtr chain, IntPtr context, ErrorCode error, UInt64 height);

    //typedef void (*last_height_fetch_handler_t)(chain_t, void*, error_code_t, UInt64 /*size_t*/ h);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FetchLastHeightHandler(IntPtr chain, IntPtr context, ErrorCode error, UIntPtr height);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_block_height(IntPtr chain, IntPtr context, hash_t hash, FetchBlockHeightHandler handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern ErrorCode chain_get_block_height(IntPtr chain, hash_t hash, ref UInt64 height);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_last_height(IntPtr chain, IntPtr context, FetchLastHeightHandler handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern ErrorCode chain_get_last_height(IntPtr chain, ref UInt64 height);

    // Block-----------------------------------------------------------------------------------

    //typedef void (*block_fetch_handler_t)(chain_t, void*, error_code_t, block_t block, uint64_t /*size_t*/ h);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FetchBlockHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr block, UInt64 height);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_block_by_hash(IntPtr chain, IntPtr context, hash_t hash, FetchBlockHandler handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern ErrorCode chain_get_block_by_hash(IntPtr chain, hash_t hash, ref IntPtr out_block, ref UInt64 out_height);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_block_by_height(IntPtr chain, IntPtr context, UInt64 height, FetchBlockHandler handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern ErrorCode chain_get_block_by_height(IntPtr chain, UInt64 height, ref IntPtr out_block, ref UInt64 out_height);

    // Block header------------------------------------------------------------------------------
    //typedef void (*block_header_fetch_handler_t)(chain_t, void*, error_code_t, header_t header, UInt64 /*size_t*/ h);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FetchBlockHeaderHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr header, UInt64 height);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_block_header_by_hash(IntPtr chain, IntPtr context, hash_t hash, FetchBlockHeaderHandler handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern ErrorCode chain_get_block_header_by_hash(IntPtr chain, hash_t hash, ref IntPtr out_header, ref UInt64 out_height);
    

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_block_header_by_height(IntPtr chain, IntPtr context, UInt64 height, FetchBlockHeaderHandler handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern ErrorCode chain_get_block_header_by_height(IntPtr chain, UInt64 height, ref IntPtr out_header, ref UInt64 out_height);

    // Merkle block-----------------------------------------------------------------------------

    //typedef void (*merkle_block_fetch_handler_t)(chain_t, void*, error_code_t, merkle_block_t block, uint64_t /*size_t*/ h);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void MerkleBlockFetchHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr block, UInt64 h);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_merkle_block_by_hash(IntPtr chain, IntPtr context, hash_t hash, MerkleBlockFetchHandler handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_merkle_block_by_height(IntPtr chain, IntPtr context, UInt64 height, MerkleBlockFetchHandler handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern ErrorCode chain_get_merkle_block_by_hash(IntPtr chain, hash_t hash, ref IntPtr out_block, ref UInt64 out_height);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern ErrorCode chain_get_merkle_block_by_height(IntPtr chain, UInt64 height, ref IntPtr out_block, ref UInt64 out_height);

    // Compact block -------------------------------------------------------------------------

    //typedef void (*compact_block_fetch_handler_t)(chain_t, void*, error_code_t, compact_block_t block, uint64_t /*size_t*/ h);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FetchCompactBlockHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr block, UInt64 height);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_compact_block_by_hash(IntPtr chain, IntPtr context, hash_t hash, FetchCompactBlockHandler handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern ErrorCode chain_get_compact_block_by_hash(IntPtr chain, hash_t hash, ref IntPtr out_block, ref UInt64 out_height);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_compact_block_by_height(IntPtr chain, IntPtr context, UInt64 height, FetchCompactBlockHandler handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern ErrorCode chain_get_compact_block_by_height(IntPtr chain, UInt64 height, ref IntPtr out_block, ref UInt64 out_height);

    // Transaction

    //typedef void (*transaction_fetch_handler_t)(chain_t, void*, error_code_t, transaction_t transaction, uint64_t /*size_t*/ i, uint64_t /*size_t*/ h);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FetchTransactionHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr transaction, UInt64 i, UInt64 h);

    //typedef void (*transaction_index_fetch_handler_t)(chain_t, void*, error_code_t, uint64_t /*size_t*/ position, uint64_t /*size_t*/ height);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FetchTransactionPositionHandler(IntPtr chain, IntPtr context, ErrorCode error, UInt64 position, UInt64 height);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_transaction(IntPtr chain, IntPtr context, hash_t hash, int require_confirmed, FetchTransactionHandler handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern ErrorCode chain_get_transaction(IntPtr chain, hash_t hash, int require_confirmed, ref IntPtr out_transaction, ref UInt64 out_height, ref UInt64 out_index);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_transaction_position(IntPtr chain, IntPtr context, hash_t hash, int require_confirmed, FetchTransactionPositionHandler handler);
   
    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern ErrorCode chain_get_transaction_position(IntPtr chain, hash_t hash, int require_confirmed, ref UInt64 out_position, ref UInt64 out_height);

    // Spend ---------------------------------------------------------------------------------------------

    //typedef void (*spend_fetch_handler_t)(chain_t, void*, error_code_t, input_point_t input_point);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FetchSpendHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr inputPoint);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_spend(IntPtr chain, IntPtr context, IntPtr op, FetchSpendHandler handler);

    // History -------------------------------------------------------------------------------------------

    //typedef void (*history_fetch_handler_t)(chain_t, void*, error_code_t, history_compact_list_t history);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FetchHistoryHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr history);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_history(IntPtr chain, IntPtr context, IntPtr address, UInt64 limit, UInt64 from_height, FetchHistoryHandler handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern ErrorCode chain_get_history(IntPtr chain, IntPtr address, UInt64 limit, UInt64 from_height, ref IntPtr out_history);

    // Stealth ---------------------------------------------------------------------

    //typedef void (*stealth_fetch_handler_t)(chain_t chain, void*, error_code_t, stealth_compact_list_t stealth);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FetchStealthHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr stealth);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_stealth(IntPtr chain, IntPtr context, IntPtr filter, UInt64 fromHeight, FetchStealthHandler handler);

    // Block indexes ---------------------------------------------------------------

    //typedef void (*block_locator_fetch_handler_t)(chain_t, void*, error_code_t, get_headers_ptr_t);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void BlockLocatorFetchHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr getHeaders);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_block_locator(IntPtr chain, IntPtr context, IntPtr heights, BlockLocatorFetchHandler handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern ErrorCode chain_get_block_locator(IntPtr chain, IntPtr heights, ref IntPtr outHeaders);

    // Subscribers -----------------------------------------------------------------

    //typedef int (*reorganize_handler_t)(chain_t, void*, error_code_t, uint64_t /*size_t*/, block_list_t, block_list_t);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void ReorganizeHandler(IntPtr chain, IntPtr context, ErrorCode error, UInt64 u, IntPtr blockList, IntPtr blockList2);

    //typedef int (*transaction_handler_t)(chain_t, void*, error_code_t, transaction_t);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TransactionHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr transaction);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_subscribe_blockchain(IntPtr chain, IntPtr context, ReorganizeHandler handler);


    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_subscribe_transaction(IntPtr chain, IntPtr context, TransactionHandler handler);


    // Organizers.
    //-------------------------------------------------------------------------

    //typedef void (*result_handler_t)(chain_t, void*, error_code_t);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void ResultHandler(IntPtr chain, IntPtr context, ErrorCode error);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_organize_block(IntPtr chain, IntPtr context, IntPtr block, ResultHandler handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern ErrorCode chain_organize_block_sync(IntPtr chain, IntPtr block);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_organize_transaction(IntPtr chain, IntPtr context, IntPtr transaction, ResultHandler handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern ErrorCode chain_organize_transaction_sync(IntPtr chain, IntPtr transaction);


    // Misc ------------------------------------------------
    //typedef void (*validate_tx_handler_t)(chain_t, void*, error_code_t, char const* message);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void ValidateTxHandler(IntPtr chain, IntPtr context, ErrorCode error, [MarshalAs(UnmanagedType.LPStr)]string message);

    [DllImport(Constants.BITPRIM_C_LIBRARY)] //TODO This is a transaction constructor, move to transaction.h
    public static extern IntPtr hex_to_tx([MarshalAs(UnmanagedType.LPStr)]string txHex);


    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_validate_tx(IntPtr chain, IntPtr context, IntPtr transaction, ValidateTxHandler handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int /* bool */ chain_is_stale(IntPtr chain);

}

}