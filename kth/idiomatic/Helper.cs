// Copyright (c) 2016-2021 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Knuth {
    public static class Helper {
        public static int BoolToC(bool x) {
            return x ? 1 : 0;
        }

//         public static string PtrToString(IntPtr ptr) {
// #if _NOT_WINDOWS
//             var str = Marshal.PtrToStringAnsi(ptr);
// #else
//             var str = Marshal.PtrToStringUni(ptr);
// #endif        
//             return str;
//         }

//         public static IntPtr StringToPtr(string s) {
// #if _NOT_WINDOWS
//             var str = Marshal.StringToHGlobalAnsi(s);
// #else
//             var str = Marshal.StringToHGlobalUni(s);
// #endif        
//             return str;
//         }

        public static IList<TIdiomatic> ArrayOfPointersToManaged<TIdiomatic, TNative>(
            IntPtr ptr, UInt64 count, Func<TNative, TIdiomatic> converter) {
            var res = new List<TIdiomatic>();
            res.Capacity = (int)count;
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

        public static IList<string> ArrayOfStringsToManaged(IntPtr ptr, UInt64 count) {
            var res = new List<string>();
            if (count == 0) {
                return res;
            }

            var ptrArray = new IntPtr[count];
            Marshal.Copy(ptr, ptrArray, 0, (int)count);
            res.Capacity = (int)count;
            for (int i = 0; i < (int)count; ++i) {
                var str = Marshal.PtrToStringAnsi(ptrArray[i]);
                res.Add(str);
                // Marshal.FreeCoTaskMem(ptrArray[i]);
            }
            // Marshal.FreeCoTaskMem(ptr);
            return res;     
        }


        public static IntPtr ListToNative<TNative, TIdiomatic>(IList<TIdiomatic> list, 
            Func<UInt64, IntPtr> allocator, Func<TIdiomatic, TNative> converter, ref UInt64 outCount) 
        {
            outCount = (UInt64)list.Count;
            var buffer = allocator((UInt64)list.Count);

            int nativeStructSize = Marshal.SizeOf<TNative>();

            for (int i = 0; i < list.Count; ++i) {
                var mem = buffer.ToInt64() + nativeStructSize * i;
                var ptr = new IntPtr(mem);
                var native = converter(list[i]);
                Marshal.StructureToPtr(native, ptr, false);
            }

            return buffer;
        }

        public static IntPtr StringListToNative(IList<string> list, ref UInt64 outCount) {
            outCount = (UInt64)list.Count;
            var buffer = Native.Platform.kth_platform_allocate_array_of_strings((UInt64)list.Count);
            for (int i = 0; i < list.Count; ++i) {
                var str = list[i];
                Native.Platform.kth_platform_allocate_and_copy_string_at(buffer, (UInt64)i, str);
            }
            return buffer;
        }
    }
}