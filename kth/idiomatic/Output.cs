// Copyright (c) 2016-2021 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using Knuth.Native;

namespace Knuth
{

    /// <summary>
    /// Represents one of the outputs of a Transaction.
    /// </summary>
    public class Output : IDisposable
    {
        private bool ownsNativeObject_;
        private IntPtr nativeInstance_;
        private Script script_;

        /// <summary>
        /// Create an empty output.
        /// </summary>
        public Output() {
            nativeInstance_ = OutputNative.kth_chain_output_construct_default();
            ownsNativeObject_ = true;
            script_ = new Script(OutputNative.kth_chain_output_script(nativeInstance_), false);
        }

        /// <summary>
        /// Create an output from a value and a script.
        /// </summary>
        /// <param name="value"> In Satoshis. </param>
        /// <param name="script"> Output script. </param>
        public Output(UInt64 value, Script script) {
            nativeInstance_ = OutputNative.kth_chain_output_construct(value, script.NativeInstance);
            ownsNativeObject_ = true;
            script_ = new Script(OutputNative.kth_chain_output_script(nativeInstance_), false);
        }

        ~Output() {
            Dispose(false);
        }

        /// <summary>
        /// Returns false if and only if output is not found.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return OutputNative.kth_chain_output_is_valid(nativeInstance_) != 0;
            }
        }

        public PaymentAddress PaymentAddress(bool useTestnetRules) {
            return new PaymentAddress(OutputNative.kth_chain_output_payment_address(nativeInstance_, useTestnetRules? 1:0));
        }

        /// <summary>
        /// Output script.
        /// </summary>
        public Script Script
        {
            get
            {
                return script_;
            }
        }

        /// <summary>
        /// Spend, in Satoshis.
        /// </summary>
        public UInt64 Value
        {
            get
            {
                return OutputNative.kth_chain_output_value(nativeInstance_);
            }
        }

        /// <summary>
        /// The amount of signature operations in the output script.
        /// </summary>
        public UInt64 SignatureOperations {
            get {
                return (UInt64)OutputNative.kth_chain_output_signature_operations(nativeInstance_);
            }
        }

        /// <summary>
        /// Output size in bytes.
        /// </summary>
        /// <param name="wire"> If true, size will include size of 'uint32' for storing spender height. </param>
        /// <returns> Size in bytes. </returns>
        public UInt64 GetSerializedSize(bool wire) {
            return (UInt64)OutputNative.kth_chain_output_serialized_size(nativeInstance_, Helper.BoolToC(wire));
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal Output(IntPtr nativeInstance, bool ownsNativeObject = true) {
            nativeInstance_ = nativeInstance;
            ownsNativeObject_ = ownsNativeObject;
            script_ = new Script(OutputNative.kth_chain_output_script(nativeInstance_), false);
        }

        internal IntPtr NativeInstance
        {
            get
            {
                return nativeInstance_;
            }
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                //Release managed resources and call Dispose for member variables
            }
            //Release unmanaged resources
            if (ownsNativeObject_) {
                //Logger.Log("Destroying output " + nativeInstance_.ToString("X") + " ...");
                OutputNative.kth_chain_output_destruct(nativeInstance_);
                //Logger.Log("Output " + nativeInstance_.ToString("X") + " destroyed!");
            }
        }
    }

}