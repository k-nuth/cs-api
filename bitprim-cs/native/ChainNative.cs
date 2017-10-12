using System;
using System.Runtime.InteropServices;

namespace BitprimCs.Native
{

public static class ChainNative
{
    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_last_height(IntPtr chain, void* ctx, last_height_fetch_handler_t handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int chain_get_last_height(IntPtr chain, uint64_t /*size_t*/* height);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_block_height(IntPtr chain, void* ctx, hash_t hash, block_height_fetch_handler_t handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int chain_get_block_height(IntPtr chain, hash_t hash, uint64_t /*size_t*/* height);


    // Block Header ---------------------------------------------------------------------
    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_block_header_by_height(IntPtr chain, void* ctx, uint64_t /*size_t*/ height, block_header_fetch_handler_t handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int chain_get_block_header_by_height(IntPtr chain, uint64_t /*size_t*/ height, header_t* out_header, uint64_t /*size_t*/* out_height);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_block_header_by_hash(IntPtr chain, void* ctx, hash_t hash, block_header_fetch_handler_t handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int chain_get_block_header_by_hash(IntPtr chain, hash_t hash, header_t* out_header, uint64_t /*size_t*/* out_height);


    // Block ---------------------------------------------------------------------
    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_block_by_height(IntPtr chain, void* ctx, uint64_t /*size_t*/ height, block_fetch_handler_t handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int chain_get_block_by_height(IntPtr chain, uint64_t /*size_t*/ height, block_t* out_block, uint64_t /*size_t*/* out_height);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_block_by_hash(IntPtr chain, void* ctx, hash_t hash, block_fetch_handler_t handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int chain_get_block_by_hash(IntPtr chain, hash_t hash, block_t* out_block, uint64_t /*size_t*/* out_height);


    // Merkle Block ---------------------------------------------------------------------
    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_merkle_block_by_height(IntPtr chain, void* ctx, uint64_t /*size_t*/ height, merkle_block_fetch_handler_t handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int chain_get_merkle_block_by_height(IntPtr chain, uint64_t /*size_t*/ height, merkle_block_t* out_block, uint64_t /*size_t*/* out_height);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_merkle_block_by_hash(IntPtr chain, void* ctx, hash_t hash, merkle_block_fetch_handler_t handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int chain_get_merkle_block_by_hash(IntPtr chain, hash_t hash, merkle_block_t* out_block, uint64_t /*size_t*/* out_height);


    // Compact Block ---------------------------------------------------------------------
    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_compact_block_by_height(IntPtr chain, void* ctx, uint64_t /*size_t*/ height, compact_block_fetch_handler_t handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int chain_get_compact_block_by_height(IntPtr chain, uint64_t /*size_t*/ height, compact_block_t* out_block, uint64_t /*size_t*/* out_height);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_compact_block_by_hash(IntPtr chain, void* ctx, hash_t hash, compact_block_fetch_handler_t handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int chain_get_compact_block_by_hash(IntPtr chain, hash_t hash, compact_block_t* out_block, uint64_t /*size_t*/* out_height);

    // Transaction ---------------------------------------------------------------------
    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_transaction(IntPtr chain, void* ctx, hash_t hash, int require_confirmed, transaction_fetch_handler_t handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int chain_get_transaction(IntPtr chain, hash_t hash, int require_confirmed, transaction_t* out_transaction, uint64_t /*size_t*/* out_height, uint64_t /*size_t*/* out_index);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_transaction_position(IntPtr chain, void* ctx, hash_t hash, int require_confirmed, transaction_index_fetch_handler_t handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int chain_get_transaction_position(IntPtr chain, hash_t hash, int require_confirmed, uint64_t /*size_t*/* out_position, uint64_t /*size_t*/* out_height);


    // Output  ---------------------------------------------------------------------
    //Note: Removed on 3.3.0
    // [DllImport(Constants.BITPRIM_C_LIBRARY)]
    // void chain_fetch_output(IntPtr chain, void* ctx, hash_t hash, uint32_t index, int require_confirmed, output_fetch_handler_t handler);

    // [DllImport(Constants.BITPRIM_C_LIBRARY)]
    // int chain_get_output(IntPtr chain, hash_t hash, uint32_t index, int require_confirmed, output_t* out_output);

    // Spend ---------------------------------------------------------------------
    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_spend(IntPtr chain, void* ctx, output_point_t op, spend_fetch_handler_t handler);

    // History ---------------------------------------------------------------------
    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_history(IntPtr chain, void* ctx, payment_address_t address, uint64_t /*size_t*/ limit, uint64_t /*size_t*/ from_height, history_fetch_handler_t handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int chain_get_history(IntPtr chain, payment_address_t address, uint64_t /*size_t*/ limit, uint64_t /*size_t*/ from_height, history_compact_list_t* out_history);


    // Stealth ---------------------------------------------------------------------
    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_stealth(IntPtr chain, void* ctx, binary_t filter, uint64_t from_height, stealth_fetch_handler_t handler);

    //[DllImport(Constants.BITPRIM_C_LIBRARY)]
    //void chain_fetch_stealth(const binary& filter, uint64_t /*size_t*/ from_height, stealth_fetch_handler handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_fetch_block_locator(IntPtr chain, void* ctx, block_indexes_t heights, block_locator_fetch_handler_t handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int chain_get_block_locator(IntPtr chain, block_indexes_t heights, get_headers_ptr_t* out_headers);


    // ------------------------------------------------------------------
    //virtual void fetch_block_locator(const chain::block::indexes& heights, block_locator_fetch_handler handler) const = 0;
    //virtual void fetch_locator_block_hashes(get_blocks_const_ptr locator, const hash_digest& threshold, size_t limit, inventory_fetch_handler handler) const = 0;
    //virtual void fetch_locator_block_headers(get_headers_const_ptr locator, const hash_digest& threshold, size_t limit, locator_block_headers_fetch_handler handler) const = 0;
    //
    // Transaction Pool.
    //-------------------------------------------------------------------------
    //
    //virtual void fetch_template(merkle_block_fetch_handler handler) const = 0;
    //virtual void fetch_mempool(size_t count_limit, uint64_t minimum_fee, inventory_fetch_handler handler) const = 0;
    //
    // Filters.
    //-------------------------------------------------------------------------
    //
    //virtual void filter_blocks(get_data_ptr message, result_handler handler) const = 0;
    //virtual void filter_transactions(get_data_ptr message, result_handler handler) const = 0;
    // ------------------------------------------------------------------



    // Subscribers.
    //-------------------------------------------------------------------------

    //virtual void subscribe_blockchain(reorganize_handler&& handler) = 0;
    //virtual void subscribe_transaction(transaction_handler&& handler) = 0;


    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_subscribe_blockchain(IntPtr chain, void* ctx, reorganize_handler_t handler);


    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_subscribe_transaction(IntPtr chain, void* ctx, transaction_handler_t handler);


    // Organizers.
    //-------------------------------------------------------------------------

    //virtual void organize(block_const_ptr block, result_handler handler) = 0;
    //virtual void organize(transaction_const_ptr tx, result_handler handler) = 0;

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_organize_block(IntPtr chain, void* ctx, block_t block, result_handler_t handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int chain_organize_block_sync(IntPtr chain, block_t block);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_organize_transaction(IntPtr chain, void* ctx, transaction_t transaction, result_handler_t handler);

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern int chain_organize_transaction_sync(IntPtr chain, transaction_t transaction);



    // ------------------------------------------------

    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern transaction_t hex_to_tx(char const* tx_hex);


    [DllImport(Constants.BITPRIM_C_LIBRARY)]
    public static extern void chain_validate_tx(IntPtr chain, void* ctx, transaction_t tx, validate_tx_handler_t handler);
}

}