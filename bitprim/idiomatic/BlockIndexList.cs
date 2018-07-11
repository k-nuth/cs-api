using System;
using Bitprim.Native;

namespace Bitprim
{
    /*/// <summary>
    /// Represent a list of block's indexes
    /// </summary>
    public class BlockIndexList : NativeList<uint>
    {
        protected override IntPtr CreateNativeList()
        {
            return BlockIndexesNative.chain_block_indexes_construct_default();
        }

        protected override uint GetNthNativeElement(uint n)
        {
            return (uint) BlockIndexesNative.chain_block_indexes_nth(NativeInstance, (UIntPtr)n);
        }

        protected override uint GetCount()
        {
            return (uint) BlockIndexesNative.chain_block_indexes_count(NativeInstance);
        }

        protected override void AddElement(uint element)
        {
            BlockIndexesNative.chain_block_indexes_push_back(NativeInstance, (UIntPtr) element);
        }

        protected override void DestroyNativeList()
        {
            //Logger.Log("Destroying block index list " + NativeInstance.ToString("X"));
            BlockIndexesNative.chain_block_indexes_destruct(NativeInstance);
        }

        internal BlockIndexList(IntPtr nativeInstance) : base(nativeInstance)
        {            
        }

        public BlockIndexList()
        {
            
        }
    }*/

}
