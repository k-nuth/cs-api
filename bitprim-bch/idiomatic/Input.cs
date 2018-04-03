using System;
using Bitprim.Native;

namespace Bitprim
{

    /// <summary>
    /// Represents a Transaction input.
    /// </summary>
    public class Input : IDisposable
    {
        private bool ownsNativeObject_;
        private IntPtr nativeInstance_;

        /// <summary>
        /// Create an empty input.
        /// </summary>
        public Input()
        {
            nativeInstance_ = InputNative.chain_input_construct_default();
            ownsNativeObject_ = true;
        }

        /// <summary>
        /// Create an input from a previous output, a script and a sequence number.
        /// </summary>
        /// <param name="previousOutput"> The output to spend. </param>
        /// <param name="script"> Input script. </param>
        /// <param name="sequence"> Zero-based, indexes this input in the input set. </param>
        public Input(Output previousOutput, Script script, UInt32 sequence)
        {
            nativeInstance_ = InputNative.chain_input_construct(previousOutput.NativeInstance, script.NativeInstance, sequence);
            ownsNativeObject_ = true;
        }

        ~Input()
        {
            Dispose(false);
        }

        /// <summary>
        /// Returns 1 iif sequence is equal to max_sequence.
        /// </summary>
        public bool IsFinal
        {
            get
            {
                return InputNative.chain_input_is_final(nativeInstance_) != 0;
            }
        }

        /// <summary>
        /// Returns false if and only if previous outputs or input script are invalid.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return InputNative.chain_input_is_valid(nativeInstance_) != 0;
            }
        }

        /// <summary>
        /// Returns a reference to the previous output, as an OutputPoint: a transaction hash and index pair.
        /// </summary>
        public OutputPoint PreviousOutput
        {
            get
            {
                return new OutputPoint(InputNative.chain_input_previous_output(nativeInstance_), false);
            }
        }

        /// <summary>
        /// The input's script.
        /// </summary>
        public Script Script
        {
            get
            {
                return new Script(InputNative.chain_input_script(nativeInstance_), false);
            }
        }

        /// <summary>
        /// Zero-based index for the input in the transaction's input set.
        /// </summary>
        public UInt32 Sequence
        {
            get
            {
                return InputNative.chain_input_sequence(nativeInstance_);
            }
        }

        /// <summary>
        /// Input size in bytes.
        /// </summary>
        /// <param name="wire"> Iif true, consider 4 extra bytes from wire field. </param>
        /// <returns> Size in bytes. </returns>
        public UInt64 GetSerializedSize(bool wire)
        {
            return (UInt64)InputNative.chain_input_serialized_size(nativeInstance_, wire ? 1 : 0);
        }

        /// <summary>
        /// Total amount of sigops (signature operations) in the input script.
        /// </summary>
        /// <param name="bip16Active"> Iif true, count BIP 16 active sig ops</param>
        /// <returns> Sigops count. </returns>
        public UInt64 GetSignatureOperationsCount(bool bip16Active)
        {
            return (UInt64)InputNative.chain_input_signature_operations(nativeInstance_, bip16Active ? 1 : 0);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal Input(IntPtr nativeInstance, bool ownsNativeObject = true)
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
                //Logger.Log("Destroying input " + nativeInstance_.ToString("X") + " ...");
                InputNative.chain_input_destruct(nativeInstance_);
                //Logger.Log("Input " + nativeInstance_.ToString("X") + " destroyed!");
            }
        }
    }

}