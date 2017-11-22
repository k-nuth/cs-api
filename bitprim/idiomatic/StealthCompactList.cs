using System;
using Bitprim.Native;
using System.Collections;

namespace Bitprim
{

    public class StealthCompactList : IDisposable
    {

        private IntPtr nativeInstance_;

        ~StealthCompactList()
        {
            Dispose(false);
        }

        public IEnumerator GetEnumerator()
        {
            return new StealthCompactListEnumerator(nativeInstance_);
        }

        public uint Count
        {
            get
            {
                return (uint)StealthCompactListNative.stealth_compact_list_count(nativeInstance_);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal StealthCompactList(IntPtr nativeInstance)
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
            StealthCompactListNative.stealth_compact_list_destruct(nativeInstance_);
        }
    }

    public class StealthCompactListEnumerator : IEnumerator
    {
        private UInt64 counter_;
        private IntPtr nativeCollection_;

        public StealthCompactListEnumerator(IntPtr nativeCollection)
        {
            nativeCollection_ = nativeCollection;
            counter_ = 0;
        }

        public bool MoveNext()
        {
            counter_++;
            return counter_ != (uint)StealthCompactListNative.stealth_compact_list_count(nativeCollection_);
        }

        public object Current
        {
            get
            {
                return StealthCompactListNative.stealth_compact_list_nth(nativeCollection_, counter_);
            }
        }

        public void Reset()
        {
            counter_ = 0;
        }

    }

}