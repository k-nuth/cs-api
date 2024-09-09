// Copyright (c) 2016-2022 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Runtime.InteropServices;
using Knuth.Native;

namespace Knuth
{
    /// <summary>
    /// Represents a Bitcoin wallet address.
    /// </summary>
    public class PaymentAddress : IDisposable
    {
        private readonly IntPtr nativeInstance_;
        private readonly bool ownsNativeObject_;

        /// <summary>
        /// Create an address from its hex string representation.
        /// </summary>
        /// <param name="hexString"></param>
        public PaymentAddress(string hexString) {
            nativeInstance_ = PaymentAddressNative.kth_wallet_payment_address_construct_from_string(hexString);
            ownsNativeObject_ = true;
        }

        internal PaymentAddress(IntPtr nativeInstance) {
            nativeInstance_ = nativeInstance;
            ownsNativeObject_ = false;
        }

        ~PaymentAddress() {
            Dispose(false);
        }

        /// <summary>
        /// Returns true iif this is a valid Base58 address.
        /// </summary>
        public bool IsValid => PaymentAddressNative.kth_wallet_payment_address_is_valid(nativeInstance_) != 0;

        /// <summary>
        /// Address version.
        /// </summary>
        public byte Version => PaymentAddressNative.kth_wallet_payment_address_version(nativeInstance_);

        /// <summary>
        /// Human readable representation in legacy BTC format.
        /// </summary>
        public string EncodeLegacy() {
            using (var addressString = new NativeString(PaymentAddressNative.kth_wallet_payment_address_encoded_legacy(nativeInstance_))) {
                return addressString.ToString();
            }
        }

#if KTH_CS_CURRENCY_BCH
        /// <summary>
        /// Human readable representation in CashAddr format.
        /// </summary>
        public string EncodeCashAddr(bool tokenAware) {
            using (var addressString = new NativeString(PaymentAddressNative.kth_wallet_payment_address_encoded_cashaddr(nativeInstance_, tokenAware ? 1 : 0))) {
                return addressString.ToString();
            }
        }

        /// <summary>
        /// (Only for BCH) The native node only handles legacy addresses; this method
        /// converts them to the CashAddr format, using bchtest: prefix for testnet and bitcoincash: prefix
        /// for mainnet.
        /// </summary>
        /// <param name="includePrefix"> If and only if true, include cashaddr prefix (bchtest/bitcoincash) </param>
        public string ToCashAddr(bool includePrefix) {
            return SharpCashAddr.Converter.LegacyAddrToCashAddr(EncodeLegacy(), includePrefix, out bool isP2PKH, out bool isMainnet);
        }

        /// <summary>
        /// (Only for BCH) Utility function for legacy-to-cashaddr conversion.
        /// </summary>
        /// <param name="includePrefix"> If and only if true, include cashaddr prefix (bchtest/bitcoincash) </param>
        public static string LegacyAddressToCashAddress(string legacyAddr, bool includePrefix) {
            return SharpCashAddr.Converter.LegacyAddrToCashAddr(legacyAddr, includePrefix, out bool isP2PKH, out bool isMainnet);
        }

        /// <summary>
        /// (Only for BCH) Utility function for cashaddr-to-legacy conversion.
        /// </summary>
        public static string CashAddressToLegacyAddress(string cashAddr) {
            return SharpCashAddr.Converter.CashAddrToLegacyAddr(cashAddr, out bool isP2PKH, out bool isMainnet);
        }
#endif

        /// <summary>
        /// Try to parse a hex string which represents a payment address.
        /// </summary>
        /// <param name="hex"> For BCH, it can be in cashaddr format, with or without prefix. </param>
        /// <param name="address"> If parsing fails (invalid address), this will be null; otherwise, it
        /// will contain a newly created PaymentAdress instance. </param>
        public static bool TryParse(string hex, out PaymentAddress address) {
            if (string.IsNullOrWhiteSpace(hex)) {
                address = null;
                return false;
            }
            address = new PaymentAddress(hex);
            if ( !address.IsValid ) {
                address.Dispose();
                address = null;
                return false;
            }
            return true;
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal IntPtr NativeInstance => nativeInstance_;

        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                //Release managed resources and call Dispose for member variables
            }
            //Release unmanaged resources
            if (ownsNativeObject_) {
                PaymentAddressNative.kth_wallet_payment_address_destruct(nativeInstance_);
            }
        }
    }

}