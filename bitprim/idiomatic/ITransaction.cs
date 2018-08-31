using System;

namespace Bitprim
{
    public interface ITransaction : IDisposable
    {
        /// <summary>
        /// Returns true if and only if this is a coinbase transaction (i.e. generates new coins).
        /// </summary>
        bool IsCoinbase { get; }

        /// <summary>
        /// Returns true if and only if the transaction is locked and
        /// every input is final, false otherwise.
        /// </summary>
        bool IsLocktimeConflict { get; }

        /// <summary>
        /// Returns true if and only if at least one of the previous outputs is invalid,
        /// false otherwise.
        /// </summary>
        bool IsMissingPreviousOutputs { get; }

        /// <summary>
        /// Return true if and only if the transaction is not coinbase and
        /// has a null previous output, false otherwise.
        /// </summary>
        bool IsNullNonCoinbase { get; }

        /// <summary>
        /// Returns true if the transaction is coinbase and
        /// has an invalid script size on its first input.
        /// </summary>
        bool IsOversizeCoinbase { get; }

        /// <summary>
        /// Returns true if transaction is not a coinbase, and
        /// the sum of its outputs is higher than the sum of its inputs,
        /// false otherwise.
        /// </summary>
        bool IsOverspent { get; }

        /// <summary>
        /// Returns true if and only if this transaction is valid according to the protocol.
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// Transaction hash in 32 byte array format.
        /// </summary>
        byte[] Hash { get; }

        /// <summary>
        /// A list with all the transaction inputs.
        /// </summary>
        InputList Inputs { get; }

        /// <summary>
        /// A list with all the transaction outputs.
        /// </summary>
        OutputList Outputs { get; }

        /// <summary>
        /// Transaction locktime.
        /// </summary>
        UInt32 Locktime { get; }

        /// <summary>
        /// Transaction protocol version.
        /// </summary>
        UInt32 Version { get; set; }

        /// <summary>
        /// Fees to pay to the winning miner.
        /// </summary>
        UInt64 Fees { get; }

        /// <summary>
        /// Amount of signature operations in the transaction.
        /// </summary>
        UInt64 SignatureOperations { get; }

        /// <summary>
        /// Sum of every input value in the transaction.
        /// </summary>
        UInt64 TotalInputValue { get; }

        /// <summary>
        /// Sum of every output value in the transaction.
        /// </summary>
        UInt64 TotalOutputValue { get; }

        /// <summary>
        /// 32 bytes transaction hash + 4 bytes signature hash type
        /// </summary>
        /// <param name="sigHashType"> Sighash type. </param>
        /// <returns> Hash and sighash type. </returns>
        byte[] GetHashBySigHashType(UInt32 sigHashType);

        /// <summary>
        /// Returns true if at least one of the previous outputs was already spent,
        /// false otherwise.
        /// </summary>
        /// <param name="includeUnconfirmed"> Iif true, consider unconfirmed transactions. </param>
        /// <returns> True if and only if transaction is double spend. </returns>
        bool IsDoubleSpend(bool includeUnconfirmed);

        /// <summary>
        /// Returns true if and only if the transaction is final, 
        /// false otherwise.
        /// </summary>
        /// <param name="blockHeight"></param>
        /// <param name="blockTime"></param>
        /// <returns></returns>
        bool IsFinal(UInt64 blockHeight, UInt32 blockTime);

        /// <summary>
        /// Returns true if and only if at least one of the inputs is not mature,
        /// false otherwise.
        /// </summary>
        /// <param name="targetHeight"></param>
        /// <returns></returns>
        bool IsImmature(UInt64 targetHeight);

        /// <summary>
        /// Raw transaction data.
        /// </summary>
        /// <param name="wire">Iif true, include data size at the beginning.</param>
        /// <returns>Byte array with transaction data.</returns>
        byte[] ToData(bool wire);

        /// <summary>
        /// Transaction size in bytes.
        /// </summary>
        /// <param name="wire"> If and only if true, size will
        /// include size of 'uint32' for storing spender output height </param>
        /// <returns> Size in bytes. </returns>
        UInt64 GetSerializedSize(bool wire = true);

        /// <summary>
        /// Amount of signature operations in the transactions.
        /// </summary>
        /// <param name="bip16Active"> True if and only if BIP16 is active, false otherwise. </param>
        /// <returns></returns>
        UInt64 GetSignatureOperationsBip16Active(bool bip16Active);
    }
}