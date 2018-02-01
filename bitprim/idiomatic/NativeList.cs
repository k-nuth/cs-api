using System;
using System.Collections;

namespace Bitprim
{

    public abstract class NativeList<T> : IDisposable
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

        ~NativeList()
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

        abstract public IntPtr CreateNativeList();
        abstract public T GetNthNativeElement(int n);
        abstract public uint GetCount();
        abstract public void AddElement(T element);
        abstract public void DestroyNativeList();

        public IEnumerator GetEnumerator()
        {
            return new NativeListEnumerator<T>(this);
        }

        public T this[int index]
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

    public class NativeListEnumerator<T> : IEnumerator
    {
        private int counter_;
        private NativeList<T> nativeList_;

        public bool MoveNext()
        {
            counter_++;
            return counter_ != nativeList_.Count;
        }

        public object Current
        {
            get
            {
                return nativeList_[counter_];
            }
        }

        public void Reset()
        {
            counter_ = -1;
        }

        internal NativeListEnumerator(NativeList<T> nativeList)
        {
            nativeList_ = nativeList;
            Reset();
        }
    }

}