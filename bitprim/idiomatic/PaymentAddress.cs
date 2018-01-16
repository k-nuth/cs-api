using System;
using Bitprim.Native;

namespace Bitprim
{

    /// <summary>
    /// Represents a Bitcoin wallet address.
    /// </summary>
    public class PaymentAddress : IDisposable
    {
        private IntPtr nativeInstance_;

        /// <summary>
        /// Create an address from its hex string representation.
        /// </summary>
        /// <param name="hexString"></param>
        public PaymentAddress(string hexString)
        {
            nativeInstance_ = PaymentAddressNative.chain_payment_address_construct_from_string(hexString);
        }

        ~PaymentAddress()
        {
            Dispose(false);
        }

        /// <summary>
        /// Address version.
        /// </summary>
        public byte Version
        {
            get
            {
                return PaymentAddressNative.chain_payment_address_version(nativeInstance_);
            }
        }

        /// <summary>
        /// Human readable representation.
        /// </summary>
        public string Encoded
        {
            get
            {
                return new NativeString(PaymentAddressNative.chain_payment_address_encoded(nativeInstance_)).ToString();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal PaymentAddress(IntPtr nativeInstance)
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

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //Release managed resources and call Dispose for member variables
            }
            //Release unmanaged resources
            //Logger.Log("Destroying payment address " + nativeInstance_.ToString("X"));
            PaymentAddressNative.chain_payment_address_destruct(nativeInstance_);
            //Logger.Log("Payment address " + nativeInstance_.ToString("X") + " destroyed!");
        }
    }

}