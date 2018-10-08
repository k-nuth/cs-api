using Bitprim.Native;
using System;

namespace Bitprim
{
    /// <summary>
    /// Represents an unspent output.
    /// </summary>
    public class Utxo : IUtxo
    {
        private IntPtr nativeInstance_;

        /// <summary>
        /// Output destination address.
        /// </summary>
        public string Address
        {
            get
            {
                using ( NativeString scriptString = new NativeString(UtxoNative.chain_utxo_get_address(nativeInstance_)) )
                {
                    return scriptString.ToString();
                }
            }
        }

        /// <summary>
        /// Output parent tx hash in 32 byte array format.
        /// </summary>
        public byte[] TxHash
        {
            get
            {
                var managedHash = new hash_t();
                UtxoNative.chain_utxo_get_tx_hash_out(nativeInstance_, ref managedHash);
                return managedHash.hash;
            }
        }

        /// <summary>
        /// Output index inside its parent transaction.
        /// </summary>
        public UInt32 Index
        {
            get
            {
                return UtxoNative.chain_utxo_get_index(nativeInstance_);
            }
        }

        /// <summary>
        /// Output amount, in coin units.
        /// </summary>
        public UInt64 Amount
        {
            get
            {
                return UtxoNative.chain_utxo_get_amount(nativeInstance_);
            }
        }

        /// <summary>
        /// Output script.
        /// </summary>
        public IScript Script
        {
            get
            {
                return new Script( UtxoNative.chain_utxo_get_script(nativeInstance_), false );
            }
        }

        internal Utxo(IntPtr nativeInstance)
        {
            nativeInstance_ = nativeInstance;
        }

    }
                                                                                                                                                                            }
