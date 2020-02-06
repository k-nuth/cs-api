using System;
using System.Collections;
using System.Collections.Generic;

namespace Bitprim
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