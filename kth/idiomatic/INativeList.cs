// Copyright (c) 2016-2024 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Collections;
using System.Collections.Generic;

namespace Knuth
{
    public interface INativeList<out T> :  IEnumerable<T>, IDisposable
    {
        T this[UInt64 index] { get; }

        UInt64 Count { get; }

        IntPtr NativeInstance { get; }
    }

    public class ManagedReadableList<T> : INativeList<T> {
        public IEnumerator<T> GetEnumerator() {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public void Dispose() {
            throw new NotImplementedException();
        }

        public T this[ulong index] => throw new NotImplementedException();

        public ulong Count { get; }
        public IntPtr NativeInstance { get; }
    }
}