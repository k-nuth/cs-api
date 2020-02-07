using System;
using System.Runtime.InteropServices;

namespace Knuth.Native
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 32), Serializable]
    public struct hash_t
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] hash;
    }
}