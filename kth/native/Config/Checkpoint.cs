// Copyright (c) 2016-2024 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Runtime.InteropServices;

namespace Knuth.Native.Config
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Checkpoint {
        public hash_t hash;
        public UInt64 height;

    }

    public static class CheckpointNative
    {
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr kth_config_checkpoint_allocate_n(UInt64 n);
    }
}
