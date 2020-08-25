// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;

namespace Knuth
{
    public interface IBlock : IDisposable
    {
        /// <summary>
        /// Returns true if and only if all transactions in the block have a unique hash (i.e. no duplicates)
        /// </summary>
        bool IsDistinctTransactionSet { get; }

        /// <summary>
        /// Returns true if and only if there is more than one coinbase transaction in the block.
        /// </summary>
        bool IsExtraCoinbase { get; }

        /// <summary>
        /// Returns true if and only if there is at least one double-spent transaction in this block
        /// </summary>
        bool IsInternalDoubleSpend { get; }

        /// <summary>
        /// Returns true if and only if the block is valid
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// Returns true if and only if the generated Merkle root equals the header's Merkle root.
        /// </summary>
        bool IsValidMerkleRoot { get; }

        /// <summary>
        /// The block's hash as a 32 byte array.
        /// </summary>
        byte[] Hash { get; }

        /// <summary>
        /// The block's Merkle root, as a 32 byte array.
        /// </summary>
        byte[] MerkleRoot { get; }

        /// <summary>
        /// The block's header.
        /// </summary>
        IHeader Header { get; }

        /// <summary>
        /// Amount of work done to mine the block
        /// </summary>
        string Proof { get; }

        /// <summary>
        /// Miner fees included in the block's coinbase transaction.
        /// </summary>
        UInt64 Fees { get; }

        /// <summary>
        /// Sum of coinbase outputs.
        /// </summary>
        UInt64 Claim { get; }

        /// <summary>
        /// Amount of signature operations in the block.
        /// </summary>
        UInt64 SignatureOperations { get; }

        /// <summary>
        /// The total amount of transactions that the block contains.
        /// </summary>
        UInt64 TransactionCount { get; }

        /// <summary>
        /// Returns true if and only if every transaction in the block is final or not.
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        bool IsFinal(UInt64 height);

        /// <summary>
        /// Given a block height, return true if and only if its coinbase claim is not higher than the deserved reward.
        /// </summary>
        /// <param name="height">The height which identifies the block to examine</param>
        /// <returns> True if and only if 1 if coinbase claim is not higher than the deserved reward. </returns>
        bool IsValidCoinbaseClaim(UInt64 height);

        /// <summary>
        /// Returns true if and only if the block's coinbase script is valid.
        /// </summary>
        /// <param name="height"> The block's height. Identifies it univocally. </param>
        /// <returns>True if and only if the block's coinbase script is valid.</returns>
        bool IsValidCoinbaseScript(UInt64 height);

        /// <summary>
        /// Raw block data.
        /// </summary>
        /// <param name="wire">if and only if true, include data size at the beginning.</param>
        /// <returns>Byte array with block data.</returns>
        byte[] ToData(bool wire);

        /// <summary>
        /// Given a position in the block, returns the corresponding transaction.
        /// </summary>
        /// <param name="n"> Zero-based index </param>
        /// <returns> Full transaction object </returns>
        ITransaction GetNthTransaction(UInt64 n);

        /// <summary>
        /// Reward = Subsidy + Fees, for the block at the given height.
        /// </summary>
        /// <param name="height"> Block height in the chain; identifies it univocally. </param>
        /// <returns> UInt64 representation of the block's reward. </returns>
        UInt64 GetBlockReward(UInt64 height);

        /// <summary>
        /// Block size in bytes.
        /// </summary>
        /// <param name="version"> Protocol version. </param>
        /// <returns> UInt64 representation of the block size in bytes. </returns>
        UInt64 GetSerializedSize(UInt32 version);

        /// <summary>
        /// Amount of signature operations in the block.
        /// </summary>
        /// <param name="bip16Active"> If and only if true, count bip16 active operations. </param>
        /// <returns> The amount of signature operations in this block </returns>
        UInt64 GetSignatureOperations(bool bip16Active);

        /// <summary>
        /// The sum of all inputs of all transactions in the block.
        /// </summary>
        /// <param name="withCoinbase">If and only if true, consider coinbase transactions. </param>
        /// <returns> UInt64 representation of the sum </returns>
        UInt64 GetTotalInputs(bool withCoinbase);
    }
}