// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Collections;
using System.Collections.Generic;

namespace Knuth
{
    /// <summary>
    /// Abstract class to represent read-only lists of native objects
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class NativeReadableList<T> : INativeList<T>
    {
        protected IntPtr nativeInstance_;

        protected NativeReadableList()
        {

        }

        protected NativeReadableList(IntPtr nativeInstance)
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

        ~NativeReadableList()
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

        public IntPtr NativeInstance => nativeInstance_;

    }
}