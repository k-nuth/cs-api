using BitprimCs.Native;
using System;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace BitprimCs
{

public class Chain
{
    private IntPtr nativeInstance_;

    public void FetchBlockHeaderByHeight(UInt64 height, Action<int, Header> handler)
    {
        GCHandle handlerHandle = GCHandle.Alloc(handler);
        IntPtr handlerPtr = (IntPtr) handlerHandle;
        ChainNative.chain_fetch_block_header_by_height(nativeInstance_, handlerPtr, height, BlockHeaderByHeightFetchHandler);
    }
    
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

    private static void BlockHeaderByHeightFetchHandler(IntPtr chain, IntPtr context, int error, IntPtr header, UInt64 height)
    {
        GCHandle handlerHandle = (GCHandle) context;
        Action<int, Header> handler = (handlerHandle.Target as Action<int, Header>);
        handler(error, new Header(header));
        handlerHandle.Free();
    }

    private static void LastHeightFetchHandler(IntPtr chain, IntPtr context, int error, UIntPtr height)
    {
        GCHandle handlerHandle = (GCHandle) context;
        Action<int, UInt64> handler = (handlerHandle.Target as Action<int, UInt64>);
        handler(error, (UInt64) height);
        handlerHandle.Free();
    }
}

}