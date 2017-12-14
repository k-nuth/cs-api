using System;
using Bitprim.Native;
using System.Collections;

namespace Bitprim
{

    public class OutputList : NativeList<Output>
    {
        public override IntPtr CreateNativeList()
        {
            return OutputListNative.chain_output_list_construct_default();
        }

        public override Output GetNthNativeElement(int n)
        {
            return new Output(OutputListNative.chain_output_list_nth(NativeInstance, (UIntPtr) n));
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
            Console.WriteLine("Destroying output list " + NativeInstance.ToString("X"));
            OutputListNative.chain_output_list_destruct(NativeInstance);
        }

        internal OutputList(IntPtr nativeInstance) : base(nativeInstance)
        {            
        }
    }

}