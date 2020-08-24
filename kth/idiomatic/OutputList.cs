// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using Knuth.Native;

namespace Knuth
{
    /// <summary>
    /// Represents a list of Outputs
    /// </summary>
    public class OutputList : NativeReadableWritableList<Output>
    {
        private bool ownsNativeObject_;

        protected override IntPtr CreateNativeList()
        {
            ownsNativeObject_ = true;
            return OutputListNative.kth_chain_output_list_construct_default();
        }

        protected override Output GetNthNativeElement(UInt64 n)
        {
            return new Output(OutputListNative.kth_chain_output_list_nth(NativeInstance, n), false);
        }

        protected override UInt64 GetCount()
        {
            return OutputListNative.kth_chain_output_list_count(NativeInstance);
        }

        protected override void AddElement(Output element)
        {
            OutputListNative.kth_chain_output_list_push_back(NativeInstance, element.NativeInstance);
        }

        protected override void DestroyNativeList()
        {
            if(ownsNativeObject_)
            {
                OutputListNative.kth_chain_output_list_destruct(NativeInstance);
            }
        }

        internal OutputList(IntPtr nativeInstance, bool ownsNativeObject = true) : base(nativeInstance)
        {
            ownsNativeObject_ = ownsNativeObject;
        }
    }

}