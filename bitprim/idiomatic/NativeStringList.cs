using Bitprim.Native;
using System;

namespace Bitprim
{
    public class NativeStringList : NativeWritableList<string>
    {
        private bool ownsNativeObject_;

        protected override IntPtr CreateNativeList()
        {
            ownsNativeObject_ = true;
            return StringListNative.core_string_list_construct();
        }

        protected override void AddElement(string element)
        {
            StringListNative.core_string_list_push_back(NativeInstance, element);
        }

        protected override void DestroyNativeList()
        {
            if(ownsNativeObject_)
            {
                StringListNative.core_string_list_destruct(NativeInstance);
            }
        }
    }
}