using Bitprim.Native;
using System;

namespace Bitprim
{

    /// <summary>
    /// Represents a full Bitcoin blockchain block.
    /// </summary>
    public class Block : IBlock
    {
        private readonly bool ownsNativeObject_;
        private readonly Header header_;
        private readonly IntPtr nativeInstance_;

        public Block (UInt32 version, string hexString)
        {
            //the raw block is already reversed
            byte[] array = Binary.HexStringToByteArray(hexString,false);
            nativeInstance_ = BlockNative.chain_block_factory_from_data(version,array,(UInt64)array.Length);
            header_ = new Header(BlockNative.chain_block_header(nativeInstance_), false);
            ownsNativeObject_ = true;
        }

        internal Block(IntPtr nativeInstance, bool ownsNativeObject = true)
        {
            nativeInstance_ = nativeInstance;
            ownsNativeObject_ = ownsNativeObject;
            header_ = new Header(BlockNative.chain_block_header(nativeInstance_), false);
        }


        ~Block()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Returns true if and only if all transactions in the block have a unique hash (i.e. no duplicates)
        /// </summary>
        public bool IsDistinctTransactionSet => BlockNative.chain_block_is_distinct_transaction_set(nativeInstance_) != 0;

        /// <summary>
        /// Returns true if and only if there is more than one coinbase transaction in the block.
        /// </summary>
        public bool IsExtraCoinbase => BlockNative.chain_block_is_extra_coinbases(nativeInstance_) != 0;

        /// <summary>
        /// Returns true if and only if there is at least one double-spent transaction in this block
        /// </summary>
        public bool IsInternalDoubleSpend => BlockNative.chain_block_is_internal_double_spend(nativeInstance_) != 0;

        /// <summary>
        /// Returns true if and only if the block is valid
        /// </summary>
        public bool IsValid => BlockNative.chain_block_is_valid(nativeInstance_) != 0;

        /// <summary>
        /// Returns true if and only if the generated Merkle root equals the header's Merkle root.
        /// </summary>
        public bool IsValidMerkleRoot => BlockNative.chain_block_is_valid_merkle_root(nativeInstance_) != 0;

        /// <summary>
        /// The block's hash as a 32 byte array.
        /// </summary>
        public byte[] Hash
        {
            get
            {
                var managedHash = new hash_t();
                BlockNative.chain_block_hash_out(nativeInstance_, ref managedHash);
                return managedHash.hash;
            }
        }

        /// <summary>
        /// The block's Merkle root, as a 32 byte array.
        /// </summary>
        public byte[] MerkleRoot
        {
            get
            {
                var managedHash = new hash_t();
                BlockNative.chain_block_generate_merkle_root_out(nativeInstance_, ref managedHash);
                return managedHash.hash;
            }
        }

        /// <summary>
        /// The block's header
        /// </summary>
        public IHeader Header => header_;

        /// <summary>
        /// Amount of work done to mine the block
        /// </summary>
        public string Proof
        {
            get
            {
                using ( NativeString proofStr = new NativeString(BlockNative.chain_block_proof(nativeInstance_)) )
                {
                    return proofStr.ToString();
                }
            }
        }

        /// <summary>
        /// Miner fees included in the block's coinbase transaction.
        /// </summary>
        public UInt64 Fees => BlockNative.chain_block_fees(nativeInstance_);

        /// <summary>
        /// Sum of coinbase outputs.
        /// </summary>
        public UInt64 Claim => BlockNative.chain_block_claim(nativeInstance_);

        /// <summary>
        /// Amount of signature operations in the block.
        /// </summary>
        public UInt64 SignatureOperationsCount => BlockNative.chain_block_signature_operations(nativeInstance_);

        /// <summary>
        /// The total amount of transactions that the block contains.
        /// </summary>
        public UInt64 TransactionCount => BlockNative.chain_block_transaction_count(nativeInstance_);

        /// <summary>
        /// Returns true if and only if every transaction in the block is final or not.
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        public bool IsFinal(UInt64 height)
        {
            return BlockNative.chain_block_is_final(nativeInstance_, (UIntPtr)height) != 0;
        }

        /// <summary>
        /// Given a block height, return true if and only if its coinbase claim is not higher than the deserved reward.
        /// </summary>
        /// <param name="height">The height which identifies the block to examine</param>
        /// <returns> True if and only if 1 if coinbase claim is not higher than the deserved reward. </returns>
        public bool IsValidCoinbaseClaim(UInt64 height)
        {
            return BlockNative.chain_block_is_valid_coinbase_claim(nativeInstance_, (UIntPtr)height) != 0;
        }

        /// <summary>
        /// Returns true if and only if the block's coinbase script is valid.
        /// </summary>
        /// <param name="height"> The block's height. Identifies it univocally. </param>
        /// <returns>True if and only if the block's coinbase script is valid.</returns>
        public bool IsValidCoinbaseScript(UInt64 height)
        {
            return BlockNative.chain_block_is_valid_coinbase_script(nativeInstance_, (UIntPtr)height) != 0;
        }

        /// <summary>
        /// Raw block data.
        /// </summary>
        /// <param name="wire">if and only if true, include data size at the beginning.</param>
        /// <returns>Byte array with block data.</returns>
        public byte[] ToData(bool wire)
        {
            int blockSize = 0;
            var blockData = new NativeBuffer(BlockNative.chain_block_to_data(nativeInstance_, wire? 1:0, ref blockSize));
            return blockData.CopyToManagedArray(blockSize);
        }

        /// <summary>
        /// Given a position in the block, returns the corresponding transaction.
        /// </summary>
        /// <param name="n"> Zero-based index </param>
        /// <returns> Full transaction object </returns>
        public ITransaction GetNthTransaction(UInt64 n)
        {
            return new Transaction(BlockNative.chain_block_transaction_nth(nativeInstance_, (UIntPtr)n), false);
        }

        /// <summary>
        /// The block subsidy. It's the same value for all blocks.
        /// </summary>
        /// <param name="height"> The block's height. It identifies it univocally. </param>
        /// <returns> UInt64 representation of the block subsidy </returns>
        public static UInt64 GetSubsidy(UInt64 height)
        {
            return BlockNative.chain_block_subsidy((UIntPtr)height);
        }

        /// <summary>
        /// Reward = Subsidy + Fees, for the block at the given height.
        /// </summary>
        /// <param name="height"> Block height in the chain; identifies it univocally. </param>
        /// <returns> UInt64 representation of the block's reward. </returns>
        public UInt64 GetBlockReward(UInt64 height)
        {
            return BlockNative.chain_block_reward(nativeInstance_, (UIntPtr)height);
        }

        /// <summary>
        /// Block size in bytes.
        /// </summary>
        /// <param name="version"> Protocol version. </param>
        /// <returns> UInt64 representation of the block size in bytes. </returns>
        public UInt64 GetSerializedSize(UInt32 version)
        {
            return BlockNative.chain_block_serialized_size(nativeInstance_, version);
        }

        /// <summary>
        /// Amount of signature operations in the block.
        /// </summary>
        /// <param name="bip16Active"> If and only if true, count bip16 active operations. </param>
        /// <returns> The amount of signature operations in this block </returns>
        public UInt64 GetSignatureOperationsCount(bool bip16Active)
        {
            return BlockNative.chain_block_signature_operations_bip16_active
            (
                nativeInstance_, bip16Active ? 1 : 0
            );
        }

        /// <summary>
        /// The sum of all inputs of all transactions in the block.
        /// </summary>
        /// <param name="withCoinbase">If and only if true, consider coinbase transactions. </param>
        /// <returns> UInt64 representation of the sum </returns>
        public UInt64 GetTotalInputs(bool withCoinbase)
        {
            return BlockNative.chain_block_total_inputs
            (
                nativeInstance_, withCoinbase ? 1 : 0
            );
        }

        
        internal IntPtr NativeInstance => nativeInstance_;

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //Release managed resources and call Dispose for member variables
                Header.Dispose();
            }
            //Release unmanaged resources
            if(ownsNativeObject_)
            {
                BlockNative.chain_block_destruct(nativeInstance_);
            }
        }
    }
}