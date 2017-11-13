using System;
using Bitprim.Native;
using System.Collections;

namespace Bitprim
{

    public class OutputList : IDisposable
    {

        private IntPtr nativeInstance_;

        public OutputList()
        {
            nativeInstance_ = OutputListNative.chain_output_list_construct_default();
        }

        internal OutputList(IntPtr nativeInstance)
        {
            nativeInstance_ = nativeInstance;
        }

        ~OutputList()
        {
            Dispose(false);
        }

        public IEnumerator GetEnumerator()
        {
            return new OutputListEnumerator(nativeInstance_);
        }

        public uint Count
        {
            get
            {
                return (uint)OutputListNative.chain_output_list_count(nativeInstance_);
            }
        }

        public void Add(Output output)
        {
            OutputListNative.chain_output_list_push_back(nativeInstance_, output.NativeInstance);
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
            OutputListNative.chain_output_list_destruct(nativeInstance_);
        }

        internal IntPtr NativeInstance
        {
            get
            {
                return nativeInstance_;
            }
        }
    }

    public class OutputListEnumerator : IEnumerator
    {
        private uint counter_;
        private IntPtr nativeCollection_;

        public OutputListEnumerator(IntPtr nativeCollection)
        {
            nativeCollection_ = nativeCollection;
            counter_ = 0;
        }

        public bool MoveNext()
        {
            counter_++;
            return counter_ != (uint)InputListNative.chain_input_list_count(nativeCollection_);
        }

        public object Current
        {
            get
            {
                return new Input(InputListNative.chain_input_list_nth(nativeCollection_, (UIntPtr)counter_));
            }
        }

        public void Reset()
        {
            counter_ = 0;
        }
    }

}