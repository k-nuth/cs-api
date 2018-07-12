using System;
using Bitprim.Native;

namespace Bitprim
{
    /// <summary>
    /// Represents a list of Outputs
    /// </summary>
    public class OutputList : NativeList<Output>
    {
        private bool ownsNativeObject_;

        protected override IntPtr CreateNativeList()
        {
            ownsNativeObject_ = true;
            return OutputListNative.chain_output_list_construct_default();
        }

        protected override Output GetNthNativeElement(uint n)
        {
            return new Output(OutputListNative.chain_output_list_nth(NativeInstance, (UIntPtr) n), false);
        }

        protected override uint GetCount()
        {
            return (uint) OutputListNative.chain_output_list_count(NativeInstance);
        }

        protected override void AddElement(Output element)
        {
            OutputListNative.chain_output_list_push_back(NativeInstance, element.NativeInstance);
        }

        protected override void DestroyNativeList()
        {
            if(ownsNativeObject_)
            {
                //Logger.Log("Destroying output list " + NativeInstance.ToString("X") + " ...");
                OutputListNative.chain_output_list_destruct(NativeInstance);
                //Logger.Log("Output list " + NativeInstance.ToString("X") + " destroyed!");
            }
        }

        internal OutputList(IntPtr nativeInstance, bool ownsNativeObject = true) : base(nativeInstance)
        {
            ownsNativeObject_ = ownsNativeObject;
        }
    }

}