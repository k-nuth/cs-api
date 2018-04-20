using System;
using Bitprim.Native;

namespace Bitprim
{

    public class InputList : NativeList<Input>
    {
        private bool ownsNativeObject_;

        public override IntPtr CreateNativeList()
        {
            ownsNativeObject_ = true;
            return InputListNative.chain_input_list_construct_default();
        }

        public override Input GetNthNativeElement(uint n)
        {
            return new Input(InputListNative.chain_input_list_nth(NativeInstance, (UIntPtr)n), false);
        }

        public override uint GetCount()
        {
            return (uint) InputListNative.chain_input_list_count(NativeInstance);
        }

        public override void AddElement(Input element)
        {
            InputListNative.chain_input_list_push_back(NativeInstance, element.NativeInstance);
        }

        public override void DestroyNativeList()
        {
            if(ownsNativeObject_)
            {
                //Logger.Log("Destroying input list " + NativeInstance.ToString("X") + " ...");
                InputListNative.chain_input_list_destruct(NativeInstance);
                //Logger.Log("Input list " + NativeInstance.ToString("X") + " destroyed!");
            }
        }

        internal InputList(IntPtr nativeInstance, bool ownsNativeObject = true) : base(nativeInstance)
        {
            ownsNativeObject_ = ownsNativeObject;
        }
        
    }

}