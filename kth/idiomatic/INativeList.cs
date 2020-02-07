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

    public class ManagedReadableList<T> : INativeList<T>
    {
        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public T this[ulong index] => throw new NotImplementedException();

        public ulong Count { get; }
        public IntPtr NativeInstance { get; }
    }
}