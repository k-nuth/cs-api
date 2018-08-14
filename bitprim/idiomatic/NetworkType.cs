namespace Bitprim
{
    /// <summary>
    /// Represents the network type (Mainnet, Testnet, Regtest)
    /// </summary>
    public enum NetworkType
    {
        /// <summary>
        /// Default value
        /// </summary>
        None = 0,
        /// <summary>
        /// Main network
        /// </summary>
        Mainnet = 1,
        /// <summary>
        /// Test network
        /// </summary>
        Testnet = 2,
        /// <summary>
        /// Regression test network
        /// </summary>
        Regtest = 3
    }
}