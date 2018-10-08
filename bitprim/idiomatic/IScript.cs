using System;

namespace Bitprim
{
    public interface IScript : IDisposable
    {
        /// <summary>
        /// All script bytes are valid under some circumstance (e.g. coinbase).
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// Script validity is independent of individual operation validity. 
        /// Operations are considered invalid if there is a trailing invalid/default op
        /// or if a push op has a size mismatch.
        /// </summary>
        bool OperationsAreValid{ get; }

        /// <summary>
        /// Raw script data
        /// </summary>
        /// <param name="prefix"> Tells whether to include script size in data </param>
        /// <returns> Byte array with script data </returns>
        byte[] ToData(bool prefix);

        /// <summary>
        /// Translate operations in the script to a string.
        /// </summary>
        /// <param name="activeForks"> Tells which rule is active. </param>
        /// <returns> Human readable script. </returns>
        string ToString(UInt32 activeForks);

        /// <summary>
        /// Script type
        /// </summary>
        string Type { get; }

        /// <summary>
        /// Size in bytes.
        /// </summary>
        UInt64 SatoshiContentSize { get; }

        /// <summary>
        /// Count the sigops in the embedded script using BIP16 rules.
        /// </summary>
        /// <param name="prevOutScript"> Reference to previous output script. </param>
        /// <returns> Embedded sigops count. </returns>
        UInt64 GetEmbeddedSigOps(Script prevOutScript);

        /// <summary>
        /// Amount of signature operations in the script.
        /// </summary>
        /// <param name="embedded"> Iif true, consider this an embedded script. </param>
        /// <returns> Sigops count. </returns>
        UInt64 GetSigOps(bool embedded);

    }
}