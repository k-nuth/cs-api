// Copyright (c) 2016-2022 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Runtime.InteropServices;

namespace Knuth.Native.Config
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Authority {
        [MarshalAs(UnmanagedType.LPStr)]
        public string ip;

        // public IntPtr ip;
        public UInt16 port;
    }

    public static class AuthorityNative
    {
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr kth_config_authority_allocate_n(UInt64 n);
    }
}
