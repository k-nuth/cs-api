// Copyright (c) 2016-2021 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Runtime.InteropServices;
using Knuth.Native;

namespace Knuth
{
    /// <summary>
    /// RAII wrapper for native memory. Guarantees that even if an exception
    /// is thrown, platform_free will be used to release it.
    /// Also, it prevents the user from forgetting to call platform_free.
    /// </summary>
    internal class NativeBuffer : IDisposable {
        private IntPtr nativePtr_;
        public NativeBuffer(IntPtr nativePtr) {
            nativePtr_ = nativePtr;
        }

        ~NativeBuffer() {
            Dispose(false);
        }

        public byte[] CopyToManagedArray(int arraySize) {
            byte[] managedArray = new byte[arraySize];
            Marshal.Copy(nativePtr_, managedArray, 0, arraySize);
            return managedArray;
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected IntPtr NativePtr {
            get {return nativePtr_;}
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                //Release managed resources and call Dispose for member variables
            }
            //Release unmanaged resources
            Platform.kth_platform_free(nativePtr_);
        }
    }
}