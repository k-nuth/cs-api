using System;
using Bitprim.Native;

namespace Bitprim
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
            return OutputListNative.chain_output_list_construct_default();
        }

        protected override Output GetNthNativeElement(UInt64 n)
        {
            return new Output(OutputListNative.chain_output_list_nth(NativeInstance, n), false);
        }

        protected override UInt64 GetCount()
        {
            return OutputListNative.chain_output_list_count(NativeInstance);
        }

        protected override void AddElement(Output element)
        {
            OutputListNative.chain_output_list_push_back(NativeInstance, element.NativeInstance);
        }

        protected override void DestroyNativeList()
        {
            if(ownsNativeObject_)
            {
                OutputListNative.chain_output_list_destruct(NativeInstance);
            }
        }

        internal OutputList(IntPtr nativeInstance, bool ownsNativeObject = true) : base(nativeInstance)
        {
            ownsNativeObject_ = ownsNativeObject;
        }
    }

}