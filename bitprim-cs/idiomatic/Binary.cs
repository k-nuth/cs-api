using System;

public class Binary : IDisposable{

    private IntPtr nativeInstance_;

    public Binary(){
        nativeInstance_ = BinaryNative.binary_construct();
    }

    public Binary(string hexString){
        nativeInstance_ = BinaryNative.binary_construct_string(hexString);
    }

    public Binary(UIntPtr bitsSize, byte[] blocks, UIntPtr n){
        nativeInstance_ = BinaryNative.binary_construct_blocks(bitsSize, blocks, n);
    }

    public string Encoded{
        get{
            return BinaryNative.binary_encoded(nativeInstance_);
        }
    }

    ~Binary()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing){
            //Release managed resources and call Dispose for member variables
        }   
        //Release unmanaged resources
        BinaryNative.binary_destruct(nativeInstance_);
    }
}