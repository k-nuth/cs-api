using System;
using System.Runtime.InteropServices;

namespace Bitprim.Native
{
    internal static class Platform
    {
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void platform_free(IntPtr nativePtr);
    }

}