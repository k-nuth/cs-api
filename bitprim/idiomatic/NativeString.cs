using System;
using System.Runtime.InteropServices;
using Bitprim.Native;

namespace Bitprim
{
    /// <summary>
    /// RAII wrapper for native strings. Guarantees that even if an exception
    /// is thrown, platform_free will be used to release the native memory.
    /// Also, it prevents the user from forgetting to call platform_free.
    /// </summary>
    internal class NativeString : IDisposable{
        private IntPtr nativePtr_;

        public NativeString(IntPtr nativePtr){
            nativePtr_ = nativePtr;
        }

        ~NativeString()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public override string ToString() => Marshal.PtrToStringAnsi(nativePtr_);

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