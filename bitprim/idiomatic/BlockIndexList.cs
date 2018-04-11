using System;
using Bitprim.Native;
using System.Collections;

namespace Bitprim
{

    public class BlockIndexList : NativeList<uint>
    {
        public override IntPtr CreateNativeList()
        {
            return BlockIndexesNative.chain_block_indexes_construct_default();
        }

        public override uint GetNthNativeElement(int n)
        {
            return (uint) BlockIndexesNative.chain_block_indexes_nth(NativeInstance, (UIntPtr)n);
        }

        public override uint GetCount()
        {
            return (uint) BlockIndexesNative.chain_block_indexes_count(NativeInstance);
        }

        public override void AddElement(uint element)
        {
            BlockIndexesNative.chain_block_indexes_push_back(NativeInstance, (UIntPtr) element);
        }

        public override void DestroyNativeList()
        {
            Logger.Log("Destroying block index list " + NativeInstance.ToString("X"));
            BlockIndexesNative.chain_block_indexes_destruct(NativeInstance);
        }

        internal BlockIndexList(IntPtr nativeInstance) : base(nativeInstance)
        {            
        }
    }

}
