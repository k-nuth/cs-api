using System;
using Bitprim.Native;
using System.Collections;

namespace Bitprim
{

    public class OutputList : NativeList<Output>
    {
        private bool ownsNativeObject_;

        public override IntPtr CreateNativeList()
        {
            ownsNativeObject_ = true;
            return OutputListNative.chain_output_list_construct_default();
        }

        public override Output GetNthNativeElement(int n)
        {
            return new Output(OutputListNative.chain_output_list_nth(NativeInstance, (UIntPtr) n), false);
        }

        public override uint GetCount()
        {
            return (uint) OutputListNative.chain_output_list_count(NativeInstance);
        }

        public override void AddElement(Output element)
        {
            OutputListNative.chain_output_list_push_back(NativeInstance, element.NativeInstance);
        }

        public override void DestroyNativeList()
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