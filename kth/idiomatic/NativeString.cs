// Copyright (c) 2016-2022 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Runtime.InteropServices;

namespace Knuth
{
    /// <summary>
    /// RAII wrapper for native strings. Guarantees that even if an exception
    /// is thrown, platform_free will be used to release the native memory.
    /// Also, it prevents the user from forgetting to call platform_free.
    /// </summary>
    internal class NativeString : NativeBuffer {

        public NativeString(IntPtr nativePtr)
            : base(nativePtr)
        {}

        public override string ToString() => Marshal.PtrToStringAnsi(NativePtr);

    }
}