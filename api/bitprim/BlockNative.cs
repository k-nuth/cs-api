using System;
using System.Runtime.InteropServices;

public static class BlockNative{

    [DllImport("bitprim-node-cint")]
    static extern byte[] header_hash(IntPtr header);

    [DllImport("bitprim-node-cint")]
    static extern byte[] header_merkle(IntPtr header);

    [DllImport("bitprim-node-cint")]
    static extern byte[] header_previous_block_hash(IntPtr header);

    [DllImport("bitprim-node-cint")]
    static extern int header_is_valid(IntPtr header);

    [DllImport("bitprim-node-cint")]
    static extern UInt32 header_bits(IntPtr header);

    [DllImport("bitprim-node-cint")]
    static extern UInt32 header_nonce(IntPtr header);

    [DllImport("bitprim-node-cint")]
    static extern UInt32 header_timestamp(IntPtr header);

    [DllImport("bitprim-node-cint")]
    static extern UInt32 header_version(IntPtr header);

    [DllImport("bitprim-node-cint")]
    static extern void header_destruct(IntPtr header);

    [DllImport("bitprim-node-cint")]
    static extern void header_set_bits(IntPtr header, UInt32 bits);

    [DllImport("bitprim-node-cint")]
    static extern void header_set_nonce(IntPtr header, UInt32 nonce);

    [DllImport("bitprim-node-cint")]
    static extern void header_set_timestamp(IntPtr header, UInt32 timestamp);

    [DllImport("bitprim-node-cint")]
    static extern void header_set_version(IntPtr header, UInt32 version);

}