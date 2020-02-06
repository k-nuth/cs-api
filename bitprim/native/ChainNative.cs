using System;
using System.Runtime.InteropServices;

namespace Bitprim.Native
{

    internal static class ChainNative
    {
        // Chain-----------------------------------------------------------------------------------

        //typedef void (*block_height_fetch_handler_t)(chain_t, void*, error_code_t, UInt64 /*size_t*/ h);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void FetchBlockHeightHandler(IntPtr chain, IntPtr context, ErrorCode error, UInt64 height);

        //typedef void (*last_height_fetch_handler_t)(chain_t, void*, error_code_t, UInt64 /*size_t*/ h);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void FetchLastHeightHandler(IntPtr chain, IntPtr context, ErrorCode error, UInt64 height);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_fetch_block_height(IntPtr chain, IntPtr context, hash_t hash, FetchBlockHeightHandler handler);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_fetch_last_height(IntPtr chain, IntPtr context, FetchLastHeightHandler handler);
        
        // Block-----------------------------------------------------------------------------------

        //typedef void (*block_fetch_handler_t)(chain_t, void*, error_code_t, block_t block, uint64_t /*size_t*/ h);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void FetchBlockHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr block, UInt64 height);

        //typedef void (*block_hash_timestamp_fetch_handler_t)(chain_t, void*, error_code_t, hash_t, uint32_t, uint64_t /*size_t*/);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void FetchBlockHeightTimestampHandler(IntPtr chain, IntPtr context, ErrorCode error, hash_t blockHash, UInt32 timestamp, UInt64 height);

        //typedef void (*block_header_txs_size_fetch_handler_t)(chain_t, void*, error_code_t, header_t, uint64_t /*size_t*/, hash_list_t, uint64_t);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void FetchBlockHeaderByHashTxsSizeHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr block_header, UInt64 block_height, IntPtr tx_hashes, UInt64 block_serialized_size);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_fetch_block_by_hash(IntPtr chain, IntPtr context, hash_t hash, FetchBlockHandler handler);

        
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_fetch_block_header_by_hash_txs_size(IntPtr chain, IntPtr ctx, hash_t hash, FetchBlockHeaderByHashTxsSizeHandler handler);

        
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_fetch_block_by_height(IntPtr chain, IntPtr context, UInt64 height, FetchBlockHandler handler);

        
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_fetch_block_by_height_timestamp(IntPtr chain, IntPtr context, UInt64 height, FetchBlockHeightTimestampHandler handler);

        
        
        // Block header------------------------------------------------------------------------------
        //typedef void (*block_header_fetch_handler_t)(chain_t, void*, error_code_t, header_t header, UInt64 /*size_t*/ h);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void FetchBlockHeaderHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr header, UInt64 height);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_fetch_block_header_by_hash(IntPtr chain, IntPtr context, hash_t hash, FetchBlockHeaderHandler handler);
      
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_fetch_block_header_by_height(IntPtr chain, IntPtr context, UInt64 height, FetchBlockHeaderHandler handler);

        
        // Merkle block-----------------------------------------------------------------------------

        //typedef void (*merkle_block_fetch_handler_t)(chain_t, void*, error_code_t, merkle_block_t block, uint64_t /*size_t*/ h);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void MerkleBlockFetchHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr block, UInt64 h);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_fetch_merkle_block_by_hash(IntPtr chain, IntPtr context, hash_t hash, MerkleBlockFetchHandler handler);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_fetch_merkle_block_by_height(IntPtr chain, IntPtr context, UInt64 height, MerkleBlockFetchHandler handler);

        
        // Compact block -------------------------------------------------------------------------

        //typedef void (*compact_block_fetch_handler_t)(chain_t, void*, error_code_t, compact_block_t block, uint64_t /*size_t*/ h);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void FetchCompactBlockHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr block, UInt64 height);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_fetch_compact_block_by_hash(IntPtr chain, IntPtr context, hash_t hash, FetchCompactBlockHandler handler);

        
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_fetch_compact_block_by_height(IntPtr chain, IntPtr context, UInt64 height, FetchCompactBlockHandler handler);

        
        // Transaction

        //typedef void (*transaction_fetch_handler_t)(chain_t, void*, error_code_t, transaction_t transaction, uint64_t /*size_t*/ i, uint64_t /*size_t*/ h);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void FetchTransactionHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr transaction, UInt64 i, UInt64 h);

        //typedef void (*transaction_index_fetch_handler_t)(chain_t, void*, error_code_t, uint64_t /*size_t*/ position, uint64_t /*size_t*/ height);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void FetchTransactionPositionHandler(IntPtr chain, IntPtr context, ErrorCode error, UInt64 position, UInt64 height);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_fetch_transaction(IntPtr chain, IntPtr context, hash_t hash, int require_confirmed, FetchTransactionHandler handler);

        
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_fetch_transaction_position(IntPtr chain, IntPtr context, hash_t hash, int require_confirmed, FetchTransactionPositionHandler handler);

        
        // Spend ---------------------------------------------------------------------------------------------

        //typedef void (*spend_fetch_handler_t)(chain_t, void*, error_code_t, input_point_t input_point);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void FetchSpendHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr inputPoint);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_fetch_spend(IntPtr chain, IntPtr context, IntPtr op, FetchSpendHandler handler);

        // History -------------------------------------------------------------------------------------------

        //typedef void (*history_fetch_handler_t)(chain_t, void*, error_code_t, history_compact_list_t history);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void FetchHistoryHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr history);

        //typedef void (*history_fetch_handler_t)(chain_t, void*, error_code_t, hash_list_t history);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void FetchTransactionsHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr txns);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_fetch_history(IntPtr chain, IntPtr context, IntPtr address, UInt64 limit, UInt64 from_height, FetchHistoryHandler handler);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_fetch_confirmed_transactions(IntPtr chain, IntPtr context, IntPtr address, UInt64 limit, UInt64 from_height, FetchTransactionsHandler handler);

        
        // Stealth ---------------------------------------------------------------------

        //typedef void (*stealth_fetch_handler_t)(chain_t chain, void*, error_code_t, stealth_compact_list_t stealth);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void FetchStealthHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr stealth);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_fetch_stealth(IntPtr chain, IntPtr context, IntPtr filter, UInt64 fromHeight, FetchStealthHandler handler);

        // Block indexes ---------------------------------------------------------------

        //typedef void (*block_locator_fetch_handler_t)(chain_t, void*, error_code_t, get_headers_ptr_t);
        //[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        //public delegate void BlockLocatorFetchHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr getHeaders);

        //Comented in CINT
        //[DllImport(Constants.KTH_C_LIBRARY)]
        //public static extern void chain_fetch_block_locator(IntPtr chain, IntPtr context, IntPtr heights, BlockLocatorFetchHandler handler);

        
        // Organizers.
        //-------------------------------------------------------------------------

        //typedef void (*result_handler_t)(chain_t, void*, error_code_t);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ResultHandler(IntPtr chain, IntPtr context, ErrorCode error);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_organize_block(IntPtr chain, IntPtr context, IntPtr block, ResultHandler handler);
 
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_organize_transaction(IntPtr chain, IntPtr context, IntPtr transaction, ResultHandler handler);

        // Mempool.
        //-------------------------------------------------------------------------
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr chain_get_mempool_transactions(IntPtr chain, IntPtr address, int /*bool*/ use_testnet_rules);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr chain_get_mempool_transactions_from_wallets(IntPtr chain, IntPtr addresses, int /*bool*/ use_testnet_rules);

        // Misc ------------------------------------------------
        //typedef void (*validate_tx_handler_t)(chain_t, void*, error_code_t, char const* message);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ValidateTxHandler(IntPtr chain, IntPtr context, ErrorCode error, [MarshalAs(UnmanagedType.LPStr)]string message);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_validate_tx(IntPtr chain, IntPtr context, IntPtr transaction, ValidateTxHandler handler);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern int /* bool */ chain_is_stale(IntPtr chain);

    }
}