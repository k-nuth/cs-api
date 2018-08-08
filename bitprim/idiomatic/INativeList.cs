using System;
using System.Collections;
using System.Collections.Generic;

namespace Bitprim
{
    public interface INativeList<T> :  IEnumerable<T>, IDisposable
    {
        T this[UInt64 index] { get; }
    }
}