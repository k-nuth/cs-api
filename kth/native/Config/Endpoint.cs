// Copyright (c) 2016-2021 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Runtime.InteropServices;

namespace Knuth.Native.Config
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Endpoint {
        [MarshalAs(UnmanagedType.LPStr)]
        public string scheme;
        [MarshalAs(UnmanagedType.LPStr)]
        public string host;

        // public IntPtr scheme;
        // public IntPtr host;
        public UInt16 port;
    }

    public static class EndpointNative
    {
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr kth_config_endpoint_allocate_n(UInt64 n);
    }
}
