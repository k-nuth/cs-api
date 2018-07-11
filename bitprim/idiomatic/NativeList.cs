using System;
using System.Collections;
using System.Collections.Generic;

namespace Bitprim
{
    /// <summary>
    /// Abstract class to represents lists for native objects
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class NativeList<T> : IEnumerable<T>, IDisposable
    {
        private IntPtr nativeInstance_;

        protected NativeList()
        {
            nativeInstance_ = CreateNativeList();
        }

        protected NativeList(IntPtr nativeInstance)
        {
            nativeInstance_ = nativeInstance;
        }

        /// <summary>
        /// Enumerator of T
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (uint i = 0; i < GetCount(); i++)
            {
                yield return GetNthNativeElement(i);
            }
        }

        ~NativeList()
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

        protected abstract IntPtr CreateNativeList();
        protected abstract T GetNthNativeElement(uint n);
        protected abstract uint GetCount();
        protected abstract void AddElement(T element);
        protected abstract void DestroyNativeList();

        /// <summary>
        /// Allow access elements by index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[uint index] => GetNthNativeElement(index);

        /// <summary>
        /// Returns the count of elements
        /// </summary>
        public uint Count => GetCount();

        /// <summary>
        /// Adds an element to the list
        /// </summary>
        /// <param name="element"></param>
        public void Add(T element)
        {
            AddElement(element);
        }

        internal IntPtr NativeInstance => nativeInstance_;
    }
}