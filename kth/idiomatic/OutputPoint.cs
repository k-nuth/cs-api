using System;
using Bitprim.Native;

namespace Bitprim
{
    /// <summary>
    /// Transaction hash and index pair representing one of the transaction outputs.
    /// </summary>
    public class OutputPoint : IDisposable
    {
        private readonly bool ownsNativeObject_;

        /// <summary>
        /// Create an empty output point.
        /// </summary>
        public OutputPoint()
        {
            NativeInstance = OutputPointNative.chain_output_point_construct();
            ownsNativeObject_ = true;
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
            NativeInstance = OutputPointNative.chain_output_point_construct_from_hash_index(managedHash, index);
            ownsNativeObject_ = true;
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
                OutputPointNative.chain_output_point_get_hash_out(NativeInstance, ref managedHash);
                return managedHash.hash;
            }
        }

        /// <summary>
        /// Transaction index (zero-based).
        /// </summary>
        public UInt32 Index => OutputPointNative.chain_output_point_get_index(NativeInstance);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal OutputPoint(IntPtr nativeInstance, bool ownsNativeObject = false)
        {
            NativeInstance = nativeInstance;
            ownsNativeObject_ = ownsNativeObject;
        }

        internal IntPtr NativeInstance { get; }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //Release managed resources and call Dispose for member variables
            }
            //Release unmanaged resources
            if(ownsNativeObject_)
            {
                OutputPointNative.chain_output_point_destruct(NativeInstance);
            }
        }
    }

}