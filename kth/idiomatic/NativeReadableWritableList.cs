// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Collections;
using System.Collections.Generic;

namespace Knuth
{
    /// <summary>
    /// Abstract class to represent lists of native objects
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class NativeReadableWritableList<T> : NativeReadableList<T>
    {
        protected NativeReadableWritableList()
        {
            nativeInstance_ = CreateNativeList();
        }

        protected NativeReadableWritableList(IntPtr nativeInstance):base(nativeInstance)
        {
            
        }
        
        protected abstract IntPtr CreateNativeList();
        protected abstract void AddElement(T element);

        /// <summary>
        /// Element to add; it is added at the end of the list
        /// </summary>
        /// <param name="element"></param>
        public void Add(T element)
        {
            AddElement(element);
        }
    }
}