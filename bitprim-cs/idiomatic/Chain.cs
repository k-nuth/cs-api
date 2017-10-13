using System;
using System.Runtime.InteropServices;

namespace BitprimCs.Native
{

public class Chain
{
    private IntPtr nativeInstance_;

    internal Chain(IntPtr nativeInstance)
    {
        nativeInstance_ = nativeInstance;
    }
}

}