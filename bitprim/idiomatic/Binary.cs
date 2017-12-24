using System;
using Bitprim.Native;

namespace Bitprim
{

    /// <summary>
    /// Represents a binary filter.
    /// </summary>
    public class Binary : IDisposable
    {

        private IntPtr nativeInstance_;

        /// <summary>
        /// Create an empty binary object.
        /// </summary>
        public Binary()
        {
            nativeInstance_ = BinaryNative.binary_construct();
        }

        /// <summary>
        /// Creates a binary filter from a binary string.
        /// </summary>
        /// <param name="hexString">  Binary string. Example: '10111010101011011111000000001101' </param>
        public Binary(string hexString)
        {
            nativeInstance_ = BinaryNative.binary_construct_string(hexString);
        }

        /// <summary>
        /// Creates a binary filter from an int array.
        /// </summary>
        /// <param name="bitsSize"> Elements size </param>
        /// <param name="blocks"> Filter representation. Example: '[186,173,240,13]'. </param>
        /// <param name="n"> Array length in amount of elements. </param>
        public Binary(UInt64 bitsSize, byte[] blocks, UInt64 n)
        {
            nativeInstance_ = BinaryNative.binary_construct_blocks((UIntPtr)bitsSize, blocks, (UIntPtr)n);
        }

        ~Binary()
        {
            Dispose(false);
        }

        /// <summary>
        /// Filter representation as binary string.
        /// </summary>
        public string Encoded
        {
            get
            {
                return BinaryNative.binary_encoded(nativeInstance_);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
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
            //Logger.Log("Destroying binary " + nativeInstance_.ToString("X") + " ...");
            BinaryNative.binary_destruct(nativeInstance_);
            //Logger.Log("Binary " + nativeInstance_.ToString("X") + " destroyed!");
        }

    }

}