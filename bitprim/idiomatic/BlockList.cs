using System;
using Bitprim.Native;

namespace Bitprim
{

    public class BlockList : NativeList<Block>
    {
        public override IntPtr CreateNativeList()
        {
            return BlockListNative.chain_block_list_construct_default();
        }

        public override Block GetNthNativeElement(int n)
        {
            return new Block(BlockListNative.chain_block_list_nth(NativeInstance, (UInt64)n), false);
        }

        public override uint GetCount()
        {
            return (uint) BlockListNative.chain_block_list_count(NativeInstance);
        }

        public override void AddElement(Block element)
        {
            BlockListNative.chain_block_list_push_back(NativeInstance, element.NativeInstance);
        }

        public override void DestroyNativeList()
        {
            Logger.Log("Destroying block list " + NativeInstance.ToString("X"));
            BlockListNative.chain_block_list_destruct(NativeInstance);
        }

        internal BlockList(IntPtr nativeInstance) : base(nativeInstance)
        {
        }
    }

}