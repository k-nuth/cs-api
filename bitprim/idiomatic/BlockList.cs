using System;
using Bitprim.Native;

namespace Bitprim
{
    /// <summary>
    /// Represents a list of blocks
    /// </summary>
    public class BlockList : NativeList<Block>
    {
        protected override IntPtr CreateNativeList()
        {
            return BlockListNative.chain_block_list_construct_default();
        }

        protected override Block GetNthNativeElement(uint n)
        {
            return new Block(BlockListNative.chain_block_list_nth(NativeInstance, (UInt64)n), false);
        }

        protected override uint GetCount()
        {
            return (uint) BlockListNative.chain_block_list_count(NativeInstance);
        }

        protected override void AddElement(Block element)
        {
            BlockListNative.chain_block_list_push_back(NativeInstance, element.NativeInstance);
        }

        protected override void DestroyNativeList()
        {
            //Logger.Log("Destroying block list " + NativeInstance.ToString("X"));
            BlockListNative.chain_block_list_destruct(NativeInstance);
        }

        internal BlockList(IntPtr nativeInstance) : base(nativeInstance)
        {
        }
    }

}