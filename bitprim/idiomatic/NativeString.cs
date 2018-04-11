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
    internal class NativeString : NativeBuffer{

        public NativeString(IntPtr nativePtr) : base(nativePtr){
        }

        public override string ToString() => Marshal.PtrToStringAnsi(NativePtr);

    }
}