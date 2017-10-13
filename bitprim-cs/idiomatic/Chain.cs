using System;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace BitprimCs.Native
{

public class Chain
{
    private IntPtr nativeInstance_;

    public void FetchLastHeight(Action<int, UInt64> handler)
    {
        GCHandle handlerHandle = GCHandle.Alloc(handler);
        IntPtr handlerPtr = (IntPtr) handlerHandle;
        ChainNative.chain_fetch_last_height(nativeInstance_, handlerPtr, LastHeightFetchHandler);
    }

    internal Chain(IntPtr nativeInstance)
    {
        nativeInstance_ = nativeInstance;
    }

    private void LastHeightFetchHandler(IntPtr chain, IntPtr context, int error, UInt64 height)
    {
        GCHandle handlerHandle = (GCHandle) context;
        Action<int, UInt64> handler = (handlerHandle.Target as Action<int, UInt64>);
        handler(error, height);
        handlerHandle.Free();
    }
}

}