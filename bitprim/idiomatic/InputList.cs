using System;
using Bitprim.Native;
using System.Collections;

namespace Bitprim
{

    public class InputList : NativeList<Input>
    {
        public override IntPtr CreateNativeList()
        {
            return InputListNative.chain_input_list_construct_default();
        }

        public override Input GetNthNativeElement(int n)
        {
            return new Input(InputListNative.chain_input_list_nth(NativeInstance, (UIntPtr)n));
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
            Console.WriteLine("Destroying input list " + NativeInstance.ToString("X"));
            InputListNative.chain_input_list_destruct(NativeInstance);
        }

        internal InputList(IntPtr nativeInstance) : base(nativeInstance)
        {            
        }
        
    }

}