// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Knuth {
    public static class Helper {
        public static int BoolToC(bool x) {
            return x ? 1 : 0;
        }

        public static IList<TIdiomatic> ArrayOfPointersToManaged<TIdiomatic, TNative>(
            IntPtr ptr, UInt64 count,
            Func<TNative, TIdiomatic> converter
        ) 
            // where TNative : new()
        {
            var res = new List<TIdiomatic>();
            // int nativeStructSize = Marshal.SizeOf(new TNative());
            int nativeStructSize = Marshal.SizeOf<TNative>();
            for (int i = 0; i < (int)count; ++i) {
                var mem = ptr.ToInt64() + nativeStructSize * i;
                IntPtr targetPtr = new IntPtr(mem);
                var nativeElement = (TNative)Marshal.PtrToStructure(targetPtr, typeof(TNative));
                var managedElement = converter(nativeElement);
                res.Add(managedElement);
            }
            return res;     
        }
    }
}