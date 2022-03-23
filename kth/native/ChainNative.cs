// Copyright (c) 2016-2022 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Runtime.InteropServices;

namespace Knuth.Native
{
    internal static class ChainNative
    {
        // Chain-----------------------------------------------------------------------------------

        //typedef void (*block_height_fetch_handler_t)(chain_t, void*, error_code_t, UInt64 /*size_t*/ h);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GetBlockHeightHandler(IntPtr chain, IntPtr context, ErrorCode error, UInt64 height);

        //typedef void (*last_height_fetch_handler_t)(chain_t, void*, error_code_t, UInt64 /*size_t*/ h);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GetLastHeightHandler(IntPtr chain, IntPtr context, ErrorCode error, UInt64 height);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_chain_async_block_height(IntPtr chain, IntPtr context, hash_t hash, GetBlockHeightHandler handler);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_chain_async_last_height(IntPtr chain, IntPtr context, GetLastHeightHandler handler);

        // Block-----------------------------------------------------------------------------------

        //typedef void (*block_fetch_handler_t)(chain_t, void*, error_code_t, block_t block, uint64_t /*size_t*/ h);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GetBlockHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr block, UInt64 height);

        //typedef void (*block_hash_timestamp_fetch_handler_t)(chain_t, void*, error_code_t, hash_t, uint32_t, uint64_t /*size_t*/);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GetBlockHeightTimestampHandler(IntPtr chain, IntPtr context, ErrorCode error, hash_t blockHash, UInt32 timestamp, UInt64 height);

        //typedef void (*block_header_txs_size_fetch_handler_t)(chain_t, void*, error_code_t, header_t, uint64_t /*size_t*/, hash_list_t, uint64_t);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GetBlockHeaderByHashTxsSizeHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr block_header, UInt64 block_height, IntPtr tx_hashes, UInt64 block_serialized_size);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_chain_async_block_by_hash(IntPtr chain, IntPtr context, hash_t hash, GetBlockHandler handler);


        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_chain_async_block_header_by_hash_txs_size(IntPtr chain, IntPtr ctx, hash_t hash, GetBlockHeaderByHashTxsSizeHandler handler);


        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_chain_async_block_by_height(IntPtr chain, IntPtr context, UInt64 height, GetBlockHandler handler);


        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_chain_async_block_by_height_timestamp(IntPtr chain, IntPtr context, UInt64 height, GetBlockHeightTimestampHandler handler);



        // Block header------------------------------------------------------------------------------
        //typedef void (*block_header_fetch_handler_t)(chain_t, void*, error_code_t, header_t header, UInt64 /*size_t*/ h);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GetBlockHeaderHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr header, UInt64 height);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_chain_async_block_header_by_hash(IntPtr chain, IntPtr context, hash_t hash, GetBlockHeaderHandler handler);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_chain_async_block_header_by_height(IntPtr chain, IntPtr context, UInt64 height, GetBlockHeaderHandler handler);


        // Merkle block-----------------------------------------------------------------------------

        //typedef void (*merkle_block_fetch_handler_t)(chain_t, void*, error_code_t, merkle_block_t block, uint64_t /*size_t*/ h);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void MerkleBlockGetHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr block, UInt64 h);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_chain_async_merkle_block_by_hash(IntPtr chain, IntPtr context, hash_t hash, MerkleBlockGetHandler handler);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_chain_async_merkle_block_by_height(IntPtr chain, IntPtr context, UInt64 height, MerkleBlockGetHandler handler);


        // Compact block -------------------------------------------------------------------------

        //typedef void (*compact_block_fetch_handler_t)(chain_t, void*, error_code_t, compact_block_t block, uint64_t /*size_t*/ h);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GetCompactBlockHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr block, UInt64 height);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_chain_async_compact_block_by_hash(IntPtr chain, IntPtr context, hash_t hash, GetCompactBlockHandler handler);


        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_chain_async_compact_block_by_height(IntPtr chain, IntPtr context, UInt64 height, GetCompactBlockHandler handler);


        // Transaction

        //typedef void (*transaction_fetch_handler_t)(chain_t, void*, error_code_t, transaction_t transaction, uint64_t /*size_t*/ i, uint64_t /*size_t*/ h);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GetTransactionHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr transaction, UInt64 i, UInt64 h);

        //typedef void (*transaction_index_fetch_handler_t)(chain_t, void*, error_code_t, uint64_t /*size_t*/ position, uint64_t /*size_t*/ height);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GetTransactionPositionHandler(IntPtr chain, IntPtr context, ErrorCode error, UInt64 position, UInt64 height);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_chain_async_transaction(IntPtr chain, IntPtr context, hash_t hash, int require_confirmed, GetTransactionHandler handler);


        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_chain_async_transaction_position(IntPtr chain, IntPtr context, hash_t hash, int require_confirmed, GetTransactionPositionHandler handler);


        // Spend ---------------------------------------------------------------------------------------------

        //typedef void (*spend_fetch_handler_t)(chain_t, void*, error_code_t, input_point_t input_point);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GetSpendHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr inputPoint);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_chain_async_spend(IntPtr chain, IntPtr context, IntPtr op, GetSpendHandler handler);

        // History -------------------------------------------------------------------------------------------

        //typedef void (*history_fetch_handler_t)(chain_t, void*, error_code_t, history_compact_list_t history);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GetHistoryHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr history);

        //typedef void (*history_fetch_handler_t)(chain_t, void*, error_code_t, hash_list_t history);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GetTransactionsHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr txns);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_chain_async_history(IntPtr chain, IntPtr context, IntPtr address, UInt64 limit, UInt64 from_height, GetHistoryHandler handler);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_chain_async_confirmed_transactions(IntPtr chain, IntPtr context, IntPtr address, UInt64 limit, UInt64 from_height, GetTransactionsHandler handler);


        // Stealth ---------------------------------------------------------------------

        //typedef void (*stealth_fetch_handler_t)(chain_t chain, void*, error_code_t, kth_chain_stealth_compact_list_t stealth);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GetStealthHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr stealth);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_chain_async_stealth(IntPtr chain, IntPtr context, IntPtr filter, UInt64 fromHeight, GetStealthHandler handler);

        // Block indexes ---------------------------------------------------------------

        //typedef void (*block_locator_fetch_handler_t)(chain_t, void*, error_code_t, get_headers_ptr_t);
        //[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        //public delegate void BlockLocatorGetHandler(IntPtr chain, IntPtr context, ErrorCode error, IntPtr getHeaders);

        //Comented in CINT
        //[DllImport(Constants.KTH_C_LIBRARY)]
        //public static extern void kth_chain_async_block_locator(IntPtr chain, IntPtr context, IntPtr heights, BlockLocatorGetHandler handler);


        // Organizers.
        //-------------------------------------------------------------------------

        //typedef void (*result_handler_t)(chain_t, void*, error_code_t);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ResultHandler(IntPtr chain, IntPtr context, ErrorCode error);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_chain_async_organize_block(IntPtr chain, IntPtr context, IntPtr block, ResultHandler handler);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_chain_async_organize_transaction(IntPtr chain, IntPtr context, IntPtr transaction, ResultHandler handler);

        // Mempool.
        //-------------------------------------------------------------------------
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr kth_chain_sync_mempool_transactions(IntPtr chain, IntPtr address, int /*bool*/ use_testnet_rules);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr kth_chain_sync_mempool_transactions_from_wallets(IntPtr chain, IntPtr addresses, int /*bool*/ use_testnet_rules);

        // Misc ------------------------------------------------
        //typedef void (*validate_tx_handler_t)(chain_t, void*, error_code_t, char const* message);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ValidateTxHandler(IntPtr chain, IntPtr context, ErrorCode error, [MarshalAs(UnmanagedType.LPStr)]string message);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void kth_chain_transaction_validate(IntPtr chain, IntPtr context, IntPtr transaction, ValidateTxHandler handler);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern int /* bool */ kth_chain_is_stale(IntPtr chain);

    }
}