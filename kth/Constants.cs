// Copyright (c) 2016-2024 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System.Runtime.InteropServices;

namespace Knuth
{
    internal static class Constants
    {
        internal const string KTH_C_LIBRARY = "libkth-c-api";

        // internal const CharSet KTH_OS_CHARSET = CharSet.Auto;
// #if _NOT_WINDOWS
//         internal const CharSet KTH_OS_CHARSET = CharSet.Ansi;
//         internal const UnmanagedType KTH_STR_PTR = UnmanagedType.LPStr;
// #else
//         internal const CharSet KTH_OS_CHARSET = CharSet.Unicode;
//         internal const UnmanagedType KTH_STR_PTR = UnmanagedType.LPWStr;
// #endif
    }
}