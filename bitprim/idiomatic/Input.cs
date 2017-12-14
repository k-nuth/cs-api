using System;
using Bitprim.Native;

namespace Bitprim
{

    /// <summary>
    /// Represents a Transaction input.
    /// </summary>
    public class Input : IDisposable
    {
        private IntPtr nativeInstance_;

        /// <summary>
        /// Create an empty input.
        /// </summary>
        public Input()
        {
            nativeInstance_ = InputNative.chain_input_construct_default();
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
        /// Returns the previous output, with its transaction hash and index.
        /// </summary>
        public Output PreviousOutput
        {
            get
            {
                return new Output(InputNative.chain_input_previous_output(nativeInstance_));
            }
        }

        /// <summary>
        /// The input's script.
        /// </summary>
        public Script Script
        {
            get
            {
                return new Script(InputNative.chain_input_script(nativeInstance_));
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
        public UIntPtr GetSerializedSize(bool wire)
        {
            return InputNative.chain_input_serialized_size(nativeInstance_, wire ? 1 : 0);
        }

        /// <summary>
        /// Total amount of sigops (signature operations) in the input script.
        /// </summary>
        /// <param name="bip16Active"> Iif true, count BIP 16 active sig ops</param>
        /// <returns> Sigops count. </returns>
        public UIntPtr GetSignatureOperationsCount(bool bip16Active)
        {
            return InputNative.chain_input_signature_operations(nativeInstance_, bip16Active ? 1 : 0);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal Input(IntPtr nativeInstance)
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
            Console.WriteLine("Destroying input " + nativeInstance_.ToString("X"));
            InputNative.chain_input_destruct(nativeInstance_);
        }
    }

}