using System;
using Bitprim.Native;

namespace Bitprim
{
    /// <summary>
    /// Represents a list of blocks
    /// </summary>
    public class BlockList : NativeReadableWritableList<Block>
    {
        protected override IntPtr CreateNativeList()
        {
            return BlockListNative.chain_block_list_construct_default();
        }

        protected override Block GetNthNativeElement(UInt64 n)
        {
            return new Block(BlockListNative.chain_block_list_nth(NativeInstance, n), false);
        }

        protected override UInt64 GetCount()
        {
            return BlockListNative.chain_block_list_count(NativeInstance);
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