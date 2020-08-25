// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using Knuth.Native;

namespace Knuth
{
    /// <summary>
    /// Represents a list of Inputs
    /// </summary>
    public class InputList : NativeReadableWritableList<Input> {
        private bool ownsNativeObject_;

        protected override IntPtr CreateNativeList() {
            ownsNativeObject_ = true;
            return InputListNative.kth_chain_input_list_construct_default();
        }

        protected override Input GetNthNativeElement(UInt64 n) {
            return new Input(InputListNative.kth_chain_input_list_nth(NativeInstance, n), false);
        }

        protected override UInt64 GetCount() {
            return InputListNative.kth_chain_input_list_count(NativeInstance);
        }

        protected override void AddElement(Input element) {
            InputListNative.kth_chain_input_list_push_back(NativeInstance, element.NativeInstance);
        }

        protected override void DestroyNativeList() {
            if (ownsNativeObject_) {
                InputListNative.kth_chain_input_list_destruct(NativeInstance);
            }
        }

        internal InputList(IntPtr nativeInstance, bool ownsNativeObject = true) : base(nativeInstance) {
            ownsNativeObject_ = ownsNativeObject;
        }
        
    }

}