using System;
using Bitprim.Native;

namespace Bitprim
{
    /// <summary>
    /// Represents a transaction script.
    /// </summary>
    public class Script : IDisposable
    {
        private bool ownsNativeObject_;
        private IntPtr nativeInstance_;

        ~Script()
        {
            Dispose(false);
        }

        /// <summary>
        /// All script bytes are valid under some circumstance (e.g. coinbase).
        /// </summary>
        public bool IsValid
        {
            get
            {
                return ScriptNative.chain_script_is_valid(nativeInstance_) != 0;
            }
        }

        /// <summary>
        /// Script validity is independent of individual operation validity. 
        /// Operations are considered invalid if there is a trailing invalid/default op
        /// or if a push op has a size mismatch.
        /// </summary>
        public bool OperationsAreValid
        {
            get
            {
                return ScriptNative.chain_script_is_valid_operations(nativeInstance_) != 0;
            }
        }

        /// <summary>
        /// Translate operations in the script to a string.
        /// </summary>
        /// <param name="activeForks"> Tells which rule is active. </param>
        /// <returns> Human readable script. </returns>
        public string ToString(UInt32 activeForks)
        {
            return ScriptNative.chain_script_to_string(nativeInstance_, activeForks);
        }

        /// <summary>
        /// Size in bytes.
        /// </summary>
        public UIntPtr SatoshiContentSize
        {
            get
            {
                return ScriptNative.chain_script_satoshi_content_size(nativeInstance_);
            }
        }

        /// <summary>
        /// Count the sigops in the embedded script using BIP16 rules.
        /// </summary>
        /// <param name="prevOutScript"> Reference to previous output script. </param>
        /// <returns> Embedded sigops count. </returns>
        public UIntPtr GetEmbeddedSigOps(Script prevOutScript)
        {
            return ScriptNative.chain_script_embedded_sigops(nativeInstance_, prevOutScript.nativeInstance_);
        }

        /// <summary>
        /// Amount of signature operations in the script.
        /// </summary>
        /// <param name="embedded"> Iif true, consider this an embedded script. </param>
        /// <returns> Sigops count. </returns>
        public UIntPtr GetSigOps(bool embedded)
        {
            return ScriptNative.chain_script_sigops(nativeInstance_, embedded ? 1 : 0);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal Script(IntPtr nativeInstance, bool ownsNativeObject = true)
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
                //Logger.Log("Destroying script " + nativeInstance_.ToString("X") + " ...");
                ScriptNative.chain_script_destruct(nativeInstance_);
                //Logger.Log("Script " + nativeInstance_.ToString("X") + " destroyed!");
            }
        }
    }

}