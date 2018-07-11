using System;
using Bitprim.Native;

namespace Bitprim
{
    /// <summary>
    /// Represents a list of Inputs
    /// </summary>
    public class InputList : NativeList<Input>
    {
        private bool ownsNativeObject_;

        protected override IntPtr CreateNativeList()
        {
            ownsNativeObject_ = true;
            return InputListNative.chain_input_list_construct_default();
        }

        protected override Input GetNthNativeElement(uint n)
        {
            return new Input(InputListNative.chain_input_list_nth(NativeInstance, (UIntPtr)n), false);
        }

        protected override uint GetCount()
        {
            return (uint) InputListNative.chain_input_list_count(NativeInstance);
        }

        protected override void AddElement(Input element)
        {
            InputListNative.chain_input_list_push_back(NativeInstance, element.NativeInstance);
        }

        protected override void DestroyNativeList()
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