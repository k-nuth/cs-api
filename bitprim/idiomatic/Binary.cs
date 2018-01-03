using System;
using System.Text;
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
        /// Convert byte array to hex tring
        /// </summary>
        /// <param name="ba">Byte array</param>
        /// <returns>HEx string representation, with as many characters as bytes</returns>
        public static string ByteArrayToHexString(byte[] ba)
        {
            StringBuilder hexString = new StringBuilder(ba.Length * 2);
            for(int i=ba.Length-1; i>=0; i--)
            {
                hexString.AppendFormat("{0:x2}", ba[i]);
            }
            return hexString.ToString();
        }

        /// <summary>
        /// Convert hex string to byte array
        /// </summary>
        /// <param name="hex">Hex string</param>
        /// <returns>ASCII byte array</returns>
        public static byte[] HexStringToByteArray(string hex)
        {
            int numberChars = hex.Length;
            byte[] bytes = new byte[numberChars / 2];
            for (int i = 0; i < numberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            Array.Reverse(bytes);
            return bytes;
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