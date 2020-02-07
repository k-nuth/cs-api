using System;
using System.Runtime.InteropServices;

namespace Knuth.Native
{
    internal static class OutputPointNative
    {
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr chain_output_point_construct();

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern IntPtr chain_output_point_construct_from_hash_index(hash_t hash, UInt32 index);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern UInt32 chain_output_point_get_index(IntPtr output);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_output_point_destruct(IntPtr op);

        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern void chain_output_point_get_hash_out(IntPtr op, ref hash_t hash);
    }
}