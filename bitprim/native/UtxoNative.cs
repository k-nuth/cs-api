using System;
using System.Runtime.InteropServices;

namespace Bitprim.Native
{
    internal static class UtxoNative
    {
        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern IntPtr chain_utxo_get_address(IntPtr utxo);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern void chain_utxo_get_tx_hash_out(IntPtr utxo, ref hash_t out_hash);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern UInt32 chain_utxo_get_index(IntPtr utxo);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern UInt64 chain_utxo_get_amount(IntPtr utxo);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern IntPtr chain_utxo_get_script(IntPtr utxo);

        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern UInt64 chain_utxo_get_block_height(IntPtr utxo);
    }
}