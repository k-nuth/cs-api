// Copyright (c) 2016-2022 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using Knuth;
using System;
using System.Threading.Tasks;

namespace Knuth
{
    /// <summary>
    /// Blockchain abstract interface.
    /// </summary>
    public interface IChain
    {
        /// <summary>
        /// Returns true if and only if the blockchain is synchronized (i.e. at current network top height)
        /// </summary>
        bool IsStale { get; }

        /// <summary>
        /// Get mempool transactions (unconfirmed) from a specific address.
        /// </summary>
        /// <param name="address"> Address to search. </param>
        /// <param name="useTestnetRules"> Tells whether we are in testnet or not. </param>
        INativeList<ITransaction> GetMempoolTransactions(INativeList<PaymentAddress> address, bool useTestnetRules);

        /// <summary>
        /// Given a block height, retrieve only block hash and timestamp, asynchronously.
        /// </summary>
        /// <param name="height"> Block height </param>
        Task<ApiCallResult<GetBlockHashTimestampResult>> GetBlockByHeightHashTimestampAsync(UInt64 height);

        /// <summary>
        /// Given a transaction hash, it fetches the height and position inside the block, asynchronously.
        /// </summary>
        /// <param name="txHash"> 32 bytes of transaction hash </param>
        /// <param name="requireConfirmed"> True iif the transaction must belong to a block </param>
        Task<ApiCallResult<GetTxPositionResult>> GetTransactionPositionAsync(byte[] txHash, bool requireConfirmed);

        /// <summary>
        /// Get the transaction input which spends the indicated output, asynchronously.
        /// </summary>
        /// <param name="outputPoint"> Tx hash and index pair where the output was spent. </param>
        Task<ApiCallResult<IPoint>> GetSpendAsync(OutputPoint outputPoint);

        /// <summary>
        /// Determine if a transaction is valid for submission to the blockchain.
        /// </summary>
        /// <param name="transaction"> Transaction to validate </param>
        Task<ApiCallResult<string>> ValidateTransactionAsync(Transaction transaction);

        /// <summary>
        /// Given a block hash, it queries the chain asynchronously for the block's height.
        /// Return right away and uses a callback to return the result.
        /// </summary>
        /// <param name="blockHash"> 32-byte array representation of the block hash.
        ///    Identifies it univocally.
        /// </param>
        Task<ApiCallResult<UInt64>> GetBlockHeightAsync(byte[] blockHash);

        /// <summary>
        /// Gets the height of the highest block in the local copy of the blockchain, asynchronously.
        /// </summary>
        Task<ApiCallResult<UInt64>> GetLastHeightAsync();

        /// <summary>
        /// Given a block hash, retrieve the full block it identifies, asynchronously.
        /// </summary>
        /// <param name="blockHash"> 32 bytes of the block hash </param>
        Task<DisposableApiCallResult<GetBlockDataResult<IBlock>>> GetBlockByHashAsync(byte[] blockHash);

        /// <summary>
        /// Given a block height, retrieve the full block it identifies, asynchronously.
        /// </summary>
        /// <param name="height"> Block height </param>
        Task<DisposableApiCallResult<GetBlockDataResult<IBlock>>> GetBlockByHeightAsync(UInt64 height);

        /// <summary>
        /// Given a block hash, get the header from the block it identifies, asynchronously.
        /// </summary>
        /// <param name="blockHash"> 32 bytes of the block hash </param>
        Task<DisposableApiCallResult<GetBlockDataResult<IHeader>>> GetBlockHeaderByHashAsync(byte[] blockHash);

        /// <summary>
        /// Given a block height, get the header from the block it identifies, asynchronously.
        /// </summary>
        /// <param name="height"> Block height </param>
        Task<DisposableApiCallResult<GetBlockDataResult<IHeader>>> GetBlockHeaderByHeightAsync(UInt64 height);

        /// <summary>
        /// Given a block hash, get the merkle block from the block it identifies, asynchronously.
        /// </summary>
        /// <param name="blockHash"> 32 bytes of the block hash </param>
        Task<DisposableApiCallResult<GetBlockDataResult<IMerkleBlock>>> GetMerkleBlockByHashAsync(byte[] blockHash);

        /// <summary>
        /// Given a block height, get the merkle block from the block it identifies, asynchronously.
        /// </summary>
        /// <param name="height"> Desired block height </param>
        Task<DisposableApiCallResult<GetBlockDataResult<IMerkleBlock>>> GetMerkleBlockByHeightAsync(UInt64 height);

        /// <summary>
        /// Given a block hash, retrieve block header, tx hashes and serialized block size, asynchronously.
        /// </summary>
        /// <param name="blockHash"> 32 bytes of the block hash </param>
        /// <returns> Tx hashes and serialized block size. Dispose result. </returns>
        Task<DisposableApiCallResult<GetBlockHeaderByHashTxSizeResult>> GetBlockHeaderByHashTxSizesAsync(byte[] blockHash);

        /// <summary>
        /// Get a transaction by its hash, asynchronously.
        /// </summary>
        /// <param name="txHash"> 32 bytes of transaction hash </param>
        /// <param name="requireConfirmed"> True if the transaction must belong to a block </param>
        Task<DisposableApiCallResult<GetTxDataResult>> GetTransactionAsync(byte[] txHash, bool requireConfirmed);

        /// <summary>
        /// Get a list of tx ids for a given payment address (asynchronously). Duplicates are already filtered out.
        /// </summary>
        /// <param name="address"> Bitcoin payment address to search </param>
        /// <param name="limit"> Maximum amount of results to fetch </param>
        /// <param name="fromHeight"> Starting point to search for transactions </param>
        Task<DisposableApiCallResult<INativeList<byte[]>>> GetConfirmedTransactionsAsync(PaymentAddress address, UInt64 limit, UInt64 fromHeight);

        /// <summary>
        /// Get a list of output points, values, and spends for a given payment address (asynchronously)
        /// </summary>
        /// <param name="address"> Bitcoin payment address to search </param>
        /// <param name="limit"> Maximum amount of results to fetch </param>
        /// <param name="fromHeight"> Starting point to search for transactions </param>
        Task<DisposableApiCallResult<INativeList<IHistoryCompact>>> GetHistoryAsync(PaymentAddress address, UInt64 limit, UInt64 fromHeight);

        /// <summary>
        /// Get metadata on potential payment transactions by stealth filter. Given a filter and a
        /// height in the chain, it queries the chain for transactions matching the given filter.
        /// </summary>
        /// <param name="filter"> Must be at least 8 bits in length. example "10101010" </param>
        /// <param name="fromHeight"> Starting height in the chain to search for transactions </param>
        //Task<DisposableApiCallResult<INativeList<IStealthCompact>>> GetStealthAsync(Binary filter, UInt64 fromHeight);

        /// <summary>
        /// Given a block, organize it (async).
        /// </summary>
        /// <param name="block"> The block to organize </param>
        Task<ErrorCode> OrganizeBlockAsync(Block block);

        /// <summary>
        /// Add new transaction to blockchain. It will be validated, so it might get rejected.
        /// Confirmation time might depende on miner fees.
        /// </summary>
        /// <param name="transaction"> Transaction to add. </param>
        /// <returns> ErrorCode with operation result. See ErrorCode enumeration. </returns>
        Task<ErrorCode> OrganizeTransactionAsync(Transaction transaction);

    }
}