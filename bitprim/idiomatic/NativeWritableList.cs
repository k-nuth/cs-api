using System;
using System.Collections;
using System.Collections.Generic;

namespace Bitprim
{
    /// <summary>
    /// Abstract class to represent write-only lists of native objects
    /// </summary>
    public abstract class NativeWritableList<T> : INativeList<T>
    {
        protected IntPtr nativeInstance_;

        protected NativeWritableList()
        {
            nativeInstance_ = CreateNativeList();
        }

        ~NativeWritableList()
        {
            Dispose(false);
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

        public IntPtr NativeInstance => nativeInstance_;

        public ulong Count => throw new NotImplementedException();

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Element to add; it is added at the end of the list
        /// </summary>
        /// <param name="element"></param>
        public void Add(T element)
        {
            AddElement(element);
        }

        public T this[ulong index] => throw new NotImplementedException();

        protected abstract IntPtr CreateNativeList();

        protected abstract void AddElement(T element);

        protected abstract void DestroyNativeList();

    }
}