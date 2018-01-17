using System;
using System.Runtime.InteropServices;
using Bitprim.Native;

namespace Bitprim
{
    /// <summary>
    /// RAII wrapper for native memory. Guarantees that even if an exception
    /// is thrown, platform_free will be used to release it.
    /// Also, it prevents the user from forgetting to call platform_free.
    /// </summary>
    internal class NativeBuffer : IDisposable{
        private IntPtr nativePtr_;

        public NativeBuffer(IntPtr nativePtr){
            nativePtr_ = nativePtr;
        }

        ~NativeBuffer()
        {
            Dispose(false);
        }

        public byte[] CopyToManagedArray(int arraySize)
        {
            byte[] managedArray = new byte[arraySize];
            Marshal.Copy(nativePtr_, managedArray, 0, arraySize);
            return managedArray;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected IntPtr NativePtr
        {
            get
            {
                return nativePtr_;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //Release managed resources and call Dispose for member variables
            }
            //Release unmanaged resources
            Platform.platform_free(nativePtr_);
        }
    }
}