using System;
using Bitprim.Native;

namespace Bitprim
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
        public PaymentAddress(string hexString)
        {
            nativeInstance_ = PaymentAddressNative.wallet_payment_address_construct_from_string(hexString);
            ownsNativeObject_ = true;
        }

        internal PaymentAddress(IntPtr nativeInstance)
        {
            nativeInstance_ = nativeInstance;
            ownsNativeObject_ = false;
        }

        ~PaymentAddress()
        {
            Dispose(false);
        }

        /// <summary>
        /// Returns true iif this is a valid Base58 address.
        /// </summary>
        public bool IsValid => PaymentAddressNative.wallet_payment_address_is_valid(nativeInstance_) != 0;

        /// <summary>
        /// Address version.
        /// </summary>
        public byte Version => PaymentAddressNative.wallet_payment_address_version(nativeInstance_);

        /// <summary>
        /// Human readable representation.
        /// </summary>
        public string Encoded
        {
            get
            {
                using ( var addressString = new NativeString(PaymentAddressNative.wallet_payment_address_encoded(nativeInstance_)) )
                {
                    return addressString.ToString();
                }
            }
        }

        #if BCH

        /// <summary>
        /// (Only for BCH) The native node only handles legacy addresses; this method
        /// converts them to the CashAddr format, using bchtest: prefix for testnet and bitcoincash: prefix
        /// for mainnet.
        /// </summary>
        public string ToCashAddr()
        {
            return SharpCashAddr.Converter.LegacyAddrToCashAddr(Encoded, out bool isP2PKH, out bool isMainnet);
        }

        /// <summary>
        /// (Only for BCH) Utility function for legacy-to-cashaddr conversion. 
        /// </summary>
        public static string LegacyAddressToCashAddress(string legacyAddr)
        {
            return SharpCashAddr.Converter.LegacyAddrToCashAddr(legacyAddr, out bool isP2PKH, out bool isMainnet);
        }

        /// <summary>
        /// (Only for BCH) Utility function for cashaddr-to-legacy conversion. 
        /// </summary>
        public static string AddressToLegacyAddress(string cashAddr)
        {
            return SharpCashAddr.Converter.CashAddrToLegacyAddr(cashAddr, out bool isP2PKH, out bool isMainnet);
        }

        #endif

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal IntPtr NativeInstance => nativeInstance_;

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //Release managed resources and call Dispose for member variables
            }
            //Release unmanaged resources
            if (ownsNativeObject_)
            {
                PaymentAddressNative.wallet_payment_address_destruct(nativeInstance_);
            }
        }
    }

}