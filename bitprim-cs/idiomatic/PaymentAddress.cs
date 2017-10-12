using System;
using BitprimCs.Native;

namespace BitprimCs
{

public class PaymentAddress : IDisposable
{
    private IntPtr nativeInstance_;

    public PaymentAddress(string hexString)
    {
        nativeInstance_ = PaymentAddressNative.chain_payment_address_construct_from_string(hexString);
    }

    ~PaymentAddress()
    {
        Dispose(false);
    }

    public byte Version
    {
        get
        {
            return PaymentAddressNative.chain_payment_address_version(nativeInstance_);
        }
    }

    public string Encoded
    {
        get
        {
            return PaymentAddressNative.chain_payment_address_encoded(nativeInstance_);
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
        PaymentAddressNative.chain_payment_address_destruct(nativeInstance_);
    }
}

}