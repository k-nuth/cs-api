using System;
using BitprimCs.Native;

namespace BitprimCs
{

public class StealthCompact : IDisposable
{
    private IntPtr nativeInstance_;

   ~StealthCompact()
    {
        Dispose(false);
    }

    public byte[] EphemeralPublicKeyHash
    {
        get
        {
            return StealthCompactNative.stealth_compact_get_ephemeral_public_key_hash(nativeInstance_);
        }
    }

    public byte[] PublicKeyHash
    {
        get
        {
            return StealthCompactNative.stealth_compact_get_public_key_hash(nativeInstance_);
        }
    }

    public byte[] TransactionHash
    {
        get
        {
            return StealthCompactNative.stealth_compact_get_transaction_hash(nativeInstance_);
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            //Release managed resources and call Dispose for member variables
        }   
        //Release unmanaged resources
        StealthCompactNative.stealth_compact_destruct(nativeInstance_);
    }

    internal StealthCompact(IntPtr nativeInstance)
    {
        nativeInstance_ = nativeInstance;
    }
}

}