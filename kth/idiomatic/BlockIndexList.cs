// Copyright (c) 2016-2024 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using Knuth.Native;

namespace Knuth
{
    /*/// <summary>
    /// Represents a list of block's indexes
    /// </summary>
    public class BlockIndexList : NativeList<uint> {
        protected override IntPtr CreateNativeList() {
            return BlockIndexesNative.kth_chain_block_indexes_construct_default();
        }

        protected override uint GetNthNativeElement(uint n) {
            return (uint) BlockIndexesNative.kth_chain_block_indexes_nth(NativeInstance, (UIntPtr)n);
        }

        protected override uint GetCount() {
            return (uint) BlockIndexesNative.kth_chain_block_indexes_count(NativeInstance);
        }

        protected override void AddElement(uint element) {
            BlockIndexesNative.kth_chain_block_indexes_push_back(NativeInstance, (UIntPtr) element);
        }

        protected override void DestroyNativeList() {
            //Logger.Log("Destroying block index list " + NativeInstance.ToString("X"));
            BlockIndexesNative.kth_chain_block_indexes_destruct(NativeInstance);
        }

        internal BlockIndexList(IntPtr nativeInstance) : base(nativeInstance) {
        }

        public BlockIndexList() {

        }
    }*/

}
