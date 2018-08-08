using System;
using System.Collections;
using System.Collections.Generic;

namespace Bitprim
{
    /// <summary>
    /// Abstract class to represent read-only lists of native objects
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class NativeReadOnlyList<T> : IEnumerable<T>, IDisposable
    {
        protected IntPtr nativeInstance_;

        protected NativeReadOnlyList()
        {

        }

        protected NativeReadOnlyList(IntPtr nativeInstance)
        {
            nativeInstance_ = nativeInstance;
        }

        /// <summary>
        /// Enumerator of T
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (UInt64 i = 0; i < GetCount(); i++)
            {
                yield return GetNthNativeElement(i);
            }
        }

        ~NativeReadOnlyList()
        {
            Dispose(false);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //Release managed resources and call Dispose for member variables
            }
            //Release unmanaged resources
            DestroyNativeList();
        }

        
        protected abstract T GetNthNativeElement(UInt64 n);
        protected abstract UInt64 GetCount();
        protected abstract void DestroyNativeList();

        /// <summary>
        /// Allow element random access (by index)
        /// </summary>
        /// <param name="index">Zero-based index</param>
        /// <returns></returns>
        public T this[UInt64 index] => GetNthNativeElement(index);

        /// <summary>
        /// Returns element count
        /// </summary>
        public UInt64 Count => GetCount();

        internal IntPtr NativeInstance => nativeInstance_;
    }
}