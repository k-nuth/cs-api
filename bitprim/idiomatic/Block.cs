using Bitprim.Native;
using System;
using System.Runtime.InteropServices;

namespace Bitprim
{

    /// <summary>
    /// Represents a full Bitcoin blockchain block.
    /// </summary>
    public class Block : IDisposable
    {
        private IntPtr nativeInstance_;

        ~Block()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //Release managed resources and call Dispose for member variables
            }
            //Release unmanaged resources
            BlockNative.chain_block_destruct(nativeInstance_);
        }

        /// <summary>
        /// Returns true iif all transactions in the block have a unique hash (i.e. no duplicates)
        /// </summary>
        public bool IsDistinctTransactionSet
        {
            get
            {
                return BlockNative.chain_block_is_distinct_transaction_set(nativeInstance_) != 0;
            }
        }

        /// <summary>
        /// Returns true iif there is more than one coinbase transaction in the block.
        /// </summary>
        public bool IsExtraCoinbase
        {
            get
            {
                return BlockNative.chain_block_is_extra_coinbases(nativeInstance_) != 0;
            }
        }

        /// <summary>
        /// Returns true iif there is at least one double-spent transaction in this block
        /// </summary>
        public bool IsInternalDoubleSpend
        {
            get
            {
                return BlockNative.chain_block_is_internal_double_spend(nativeInstance_) != 0;
            }
        }

        /// <summary>
        /// Returns true iif the block is valid
        /// </summary>
        public bool IsValid
        {
            get
            {
                return BlockNative.chain_block_is_valid(nativeInstance_) != 0;
            }
        }

        /// <summary>
        /// Returns true iif the generated Merkle root equals the header's Merkle root.
        /// </summary>
        public bool IsValidMerkleRoot
        {
            get
            {
                return BlockNative.chain_block_is_valid_merkle_root(nativeInstance_) != 0;
            }
        }

        /// <summary>
        /// The block's hash as a 32 byte array.
        /// </summary>
        public byte[] Hash
        {
            get
            {
                return BlockNative.chain_block_hash(nativeInstance_);
            }
        }

        /// <summary>
        /// The block's Merkle root, as a 32 byte array.
        /// </summary>
        public byte[] MerkleRoot
        {
            get
            {
                return BlockNative.chain_block_generate_merkle_root(nativeInstance_);
            }
        }

        /// <summary>
        /// The block's header
        /// </summary>
        public Header Header
        {
            get
            {
                return new Header(BlockNative.chain_block_header(nativeInstance_));
            }
        }

        /// <summary>
        /// Miner fees included in the block's coinbase transaction.
        /// </summary>
        public UInt64 Fees
        {
            get
            {
                return BlockNative.chain_block_fees(nativeInstance_);
            }
        }

        /// <summary>
        /// Sum of coinbase outputs.
        /// </summary>
        public UInt64 Claim
        {
            get
            {
                return BlockNative.chain_block_claim(nativeInstance_);
            }
        }

        /// <summary>
        /// Amount of signature operations in the block.
        /// </summary>
        public UIntPtr SignatureOperationsCount
        {
            get
            {
                return BlockNative.chain_block_signature_operations(nativeInstance_);
            }
        }

        /// <summary>
        /// The total amount of transactions that the block contains.
        /// </summary>
        public UIntPtr TransactionCount
        {
            get
            {
                return BlockNative.chain_block_transaction_count(nativeInstance_);
            }
        }

        /// <summary>
        /// Returns true iif every transaction in the block is final or not.
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        public bool IsFinal(UIntPtr height)
        {
            return BlockNative.chain_block_is_final(nativeInstance_, height) != 0;
        }

        /// <summary>
        /// Given a block height, return true iif its coinbase claim is not higher than the deserved reward.
        /// </summary>
        /// <param name="height">The height which identifies the block to examine</param>
        /// <returns> True iif 1 if coinbase claim is not higher than the deserved reward. </returns>
        public bool IsValidCoinbaseClaim(UIntPtr height)
        {
            return BlockNative.chain_block_is_valid_coinbase_claim(nativeInstance_, height) != 0;
        }

        /// <summary>
        /// Returns true iif the block's coinbase script is valid.
        /// </summary>
        /// <param name="height"> The block's height. Identifies it univocally. </param>
        /// <returns>True iif the block's coinbase script is valid.</returns>
        public bool IsValidCoinbaseScript(UIntPtr height)
        {
            return BlockNative.chain_block_is_valid_coinbase_script(nativeInstance_, height) != 0;
        }

        /// <summary>
        /// The block subsidy. It's the same value for all blocks.
        /// </summary>
        /// <param name="height"> The block's height. It identifies it univocally. </param>
        /// <returns> UInt64 representation of the block subsidy </returns>
        public static UInt64 GetSubsidy(UIntPtr height)
        {
            return BlockNative.chain_block_subsidy(height);
        }

        /// <summary>
        /// Given a position in the block, returns the corresponding transaction.
        /// </summary>
        /// <param name="n"> Zero-based index </param>
        /// <returns> Full transaction object </returns>
        public Transaction GetNthTransaction(UIntPtr n)
        {
            return new Transaction(BlockNative.chain_block_transaction_nth(nativeInstance_, n));
        }

        /// <summary>
        /// Reward = Subsidy + Fees, for the block at the given height.
        /// </summary>
        /// <param name="height"> Block height in the chain; identifies it univocally. </param>
        /// <returns> UInt64 representation of the block's reward. </returns>
        public UInt64 GetBlockReward(UIntPtr height)
        {
            return BlockNative.chain_block_reward(nativeInstance_, height);
        }

        /// <summary>
        /// Block size in bytes.
        /// </summary>
        /// <param name="version"> Protocol version. </param>
        /// <returns> UInt64 representation of the block size in bytes. </returns>
        public UIntPtr GetSerializedSize(UInt32 version)
        {
            return BlockNative.chain_block_serialized_size(nativeInstance_, version);
        }

        /// <summary>
        /// Amount of signature operations in the block.
        /// </summary>
        /// <param name="bip16Active"> Iif true, count bip16 active operations. </param>
        /// <returns> The amount of signature operations in this block </returns>
        public UIntPtr GetSignatureOperationsCount(bool bip16Active)
        {
            return BlockNative.chain_block_signature_operations_bip16_active
            (
                nativeInstance_, bip16Active ? 1 : 0
            );
        }

        /// <summary>
        /// The sum of all inputs of all transactions in the block.
        /// </summary>
        /// <param name="withCoinbase">Iif true, consider coinbase transactions. </param>
        /// <returns> UInt64 representation of the sum </returns>
        public UIntPtr GetTotalInputs(bool withCoinbase)
        {
            return BlockNative.chain_block_total_inputs
            (
                nativeInstance_, withCoinbase ? 1 : 0
            );
        }

        internal Block(IntPtr nativeInstance)
        {
            nativeInstance_ = nativeInstance;
        }

        internal IntPtr NativeInstance
        {
            get
            {
                return nativeInstance_;
            }
        }

    }

}