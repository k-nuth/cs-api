using System;
using Bitprim.Native;
using System.Collections;

namespace Bitprim
{

    public class HashList : NativeList<byte[]>
    {
        public override IntPtr CreateNativeList()
        {
            return HashListNative.chain_hash_list_construct_default();
        }

        public override byte[] GetNthNativeElement(int n)
        {
            return HashListNative.chain_hash_list_nth(NativeInstance, (UIntPtr)n);
        }

        public override uint GetCount()
        {
            return (uint) HashListNative.chain_hash_list_count(NativeInstance);
        }

        public override void AddElement(byte[] element)
        {
            HashListNative.chain_hash_list_push_back(NativeInstance, element);
        }

        public override void DestroyNativeList()
        {
            Logger.Log("Destroying block " + NativeInstance.ToString("X"));
            HashListNative.chain_hash_list_destruct(NativeInstance);
        }

        internal HashList(IntPtr nativeInstance) : base(nativeInstance)
        {            
        }
    }
    

}