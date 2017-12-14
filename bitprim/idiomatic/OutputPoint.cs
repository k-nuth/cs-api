using System;
using Bitprim.Native;

namespace Bitprim
{

    /// <summary>
    /// Transaction hash and index pair representing one of the transaction outputs.
    /// </summary>
    public class OutputPoint : IDisposable
    {
        private IntPtr nativeInstance_;

        /// <summary>
        /// Create an empty output point.
        /// </summary>
        public OutputPoint()
        {
            nativeInstance_ = OutputPointNative.output_point_construct();
        }

        /// <summary>
        /// Create an output point from a hash and index pair.
        /// </summary>
        /// <param name="pointHash"></param>
        /// <param name="index"></param>
        public OutputPoint(byte[] pointHash, UInt32 index)
        {
            var managedHash = new hash_t
            {
                hash = pointHash
            };
            nativeInstance_ = OutputPointNative.output_point_construct_from_hash_index(managedHash, index);
        }

        ~OutputPoint()
        {
            Dispose(false);
        }

        /// <summary>
        /// Transaction hash in 32 byte array format.
        /// </summary>
        public byte[] Hash
        {
            get
            {
                var managedHash = new hash_t();
                OutputPointNative.output_point_get_hash_out(nativeInstance_, ref managedHash);
                return managedHash.hash;
            }
        }

        /// <summary>
        /// Transaction index (zero-based).
        /// </summary>
        public UInt32 Index
        {
            get
            {
                return OutputPointNative.output_point_get_index(nativeInstance_);
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
            Logger.Log("Destroying output point " + nativeInstance_.ToString("X"));
            OutputPointNative.output_point_destruct(nativeInstance_);
        }
    }

}