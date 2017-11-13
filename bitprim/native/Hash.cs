using System;
using System.Runtime.InteropServices;

namespace BitprimCs.Native
{

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size=32), Serializable]
    public struct hash_t
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] hash;
    };

}