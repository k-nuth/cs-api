// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System.Runtime.InteropServices;

namespace Knuth
{
    internal static class Constants
    {
        internal const string KTH_C_LIBRARY = "libkth-c-api";

#if _NOT_WINDOWS
        internal const CharSet KTH_OS_CHARSET = CharSet.Ansi;
#else
        internal const CharSet KTH_OS_CHARSET = CharSet.Unicode;
#endif        
    }
}