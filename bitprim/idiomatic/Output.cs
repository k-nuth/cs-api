using System;
using Bitprim.Native;

namespace Bitprim
{

    /// <summary>
    /// Represents one of the outputs of a Transaction.
    /// </summary>
    public class Output : IDisposable
    {
        private bool ownsNativeObject_;
        private IntPtr nativeInstance_;

        /// <summary>
        /// Create an empty output.
        /// </summary>
        public Output()
        {
            nativeInstance_ = OutputNative.chain_output_construct_default();
            ownsNativeObject_ = true;
        }

        /// <summary>
        /// Create an output from a value and a script.
        /// </summary>
        /// <param name="value"> In Satoshis. </param>
        /// <param name="script"> Output script. </param>
        public Output(UInt64 value, Script script)
        {
            nativeInstance_ = OutputNative.chain_output_construct(value, script.NativeInstance);
        }

        ~Output()
        {
            Dispose(false);
        }

        /// <summary>
        /// Returns false if and only if output is not found.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return OutputNative.chain_output_is_valid(nativeInstance_) != 0;
            }
        }

        public PaymentAddress PaymentAddress(bool useTestnetRules)
        {
            return new PaymentAddress(OutputNative.chain_output_payment_address(nativeInstance_, useTestnetRules? 1:0));
        }

        /// <summary>
        /// Output script.
        /// </summary>
        public Script Script
        {
            get
            {
                return new Script(OutputNative.chain_output_script(nativeInstance_), false);
            }
        }

        /// <summary>
        /// Spend, in Satoshis.
        /// </summary>
        public UInt64 Value
        {
            get
            {
                return OutputNative.chain_output_value(nativeInstance_);
            }
        }

        /// <summary>
        /// The amount of signature operations in the output script.
        /// </summary>
        public UInt64 SignatureOperationCount
        {
            get
            {
                return (UInt64)OutputNative.chain_output_signature_operations(nativeInstance_);
            }
        }

        /// <summary>
        /// Output size in bytes.
        /// </summary>
        /// <param name="wire"> If true, size will include size of 'uint32' for storing spender height. </param>
        /// <returns> Size in bytes. </returns>
        public UInt64 GetSerializedSize(bool wire)
        {
            return (UInt64)OutputNative.chain_output_serialized_size(nativeInstance_, wire ? 1 : 0);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal Output(IntPtr nativeInstance, bool ownsNativeObject = true)
        {
            nativeInstance_ = nativeInstance;
            ownsNativeObject_ = ownsNativeObject;
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
            if(ownsNativeObject_)
            {
                //Logger.Log("Destroying output " + nativeInstance_.ToString("X") + " ...");
                OutputNative.chain_output_destruct(nativeInstance_);
                //Logger.Log("Output " + nativeInstance_.ToString("X") + " destroyed!");
            }
        }
    }

}