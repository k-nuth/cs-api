using System;
using Bitprim.Native;
using System.Collections;

namespace Bitprim
{

    /// <summary>
    /// A collection of Bitcoin hashes (for blocks or transactions).
    /// </summary>
    public class HashList : IDisposable
    {

        private IntPtr nativeInstance_;

        /// <summary>
        /// Create an empty list.
        /// </summary>
        public HashList()
        {
            nativeInstance_ = HashListNative.chain_hash_list_construct_default();
        }

        ~HashList()
        {
            Dispose(false);
        }

        /// <summary>
        /// Needed to iterate collection using foreach
        /// </summary>
        /// <returns> Collection enumerator. </returns>
        public IEnumerator GetEnumerator()
        {
            return new HashListEnumerator(nativeInstance_);
        }

        /// <summary>
        /// Hash count
        /// </summary>
        /// <returns>Count</returns>
        public uint Count
        {
            get
            {
                return (uint)HashListNative.chain_hash_list_count(nativeInstance_);
            }
        }

        /// <summary>
        /// Add a hash to collection
        /// </summary>
        /// <param name="hash">Hash to add</param>
        public void Add(byte[] hash)
        {
            HashListNative.chain_hash_list_push_back(nativeInstance_, hash);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal HashList(IntPtr nativeInstance)
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
            HashListNative.chain_hash_list_destruct(nativeInstance_);
        }
    }

    /// <summary>
    /// For enumerator pattern
    /// </summary>
    public class HashListEnumerator : IEnumerator
    {
        private uint counter_;
        private IntPtr nativeCollection_;

        /// <summary>
        /// Advance enumerator to next element
        /// </summary>
        /// <returns>True if and only if enumerator moved to next element</returns>
        public bool MoveNext()
        {
            counter_++;
            return counter_ != (uint)HashListNative.chain_hash_list_count(nativeCollection_);
        }

        /// <summary>
        /// Return current element
        /// </summary>
        /// <returns>Reference to current element, as object</returns>
        public object Current
        {
            get
            {
                return HashListNative.chain_hash_list_nth(nativeCollection_, (UIntPtr)counter_);
            }
        }

        /// <summary>
        /// Go back to collection's first element
        /// </summary>
        public void Reset()
        {
            counter_ = 0;
        }

        internal HashListEnumerator(IntPtr nativeCollection)
        {
            nativeCollection_ = nativeCollection;
            counter_ = 0;
        }
    }

}