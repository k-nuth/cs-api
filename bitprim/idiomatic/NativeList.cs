using System;
using System.Collections;
using System.Collections.Generic;

namespace Bitprim
{
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

        public IEnumerator<T> GetEnumerator()  
        {
            for (uint i = 0; i < GetCount() ; i++)
            {
                yield return this[i];
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

        public abstract IntPtr CreateNativeList();
        public abstract T GetNthNativeElement(uint n);
        public abstract uint GetCount();
        public abstract void AddElement(T element);
        public abstract void DestroyNativeList();

     
        public T this[uint index]
        {
            get
            {
                return GetNthNativeElement(index);
            }
        }

        public uint Count
        {
            get
            {
                return GetCount();
            }
        }

        public void Add(T element)
        {
            AddElement(element);
        }

        internal IntPtr NativeInstance
        {
            get
            {
                return nativeInstance_;
            }
        }
    }
}