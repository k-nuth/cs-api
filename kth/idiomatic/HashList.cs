// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using Knuth.Native;

namespace Knuth
{
    public class HashList : NativeReadableWritableList<byte[]>
    {
        private bool ownsNativeObject_;

        protected override IntPtr CreateNativeList()
        {
            ownsNativeObject_ = true;
            return HashListNative.kth_core_hash_list_construct_default();
        }

        protected override byte[] GetNthNativeElement(UInt64 n)
        {
            var managedHash = new hash_t();
            HashListNative.kth_core_hash_list_nth_out(NativeInstance, n, ref managedHash);
            return managedHash.hash;
        }

        protected override UInt64 GetCount()
        {
            return HashListNative.kth_core_hash_list_count(NativeInstance);
        }

        protected override void AddElement(byte[] element)
        {
            HashListNative.kth_core_hash_list_push_back(NativeInstance, element);
        }

        protected override void DestroyNativeList()
        {
            if(ownsNativeObject_)
            {
                HashListNative.kth_core_hash_list_destruct(NativeInstance);
            }
        }

        internal HashList(IntPtr nativeInstance, bool ownsNativeObject = true) : base(nativeInstance)
        {
            ownsNativeObject_ = ownsNativeObject;
        }
    }
    

}