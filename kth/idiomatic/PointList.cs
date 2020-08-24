// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using Knuth.Native;

namespace Knuth
{
   /*
    public class PointList : NativeList<Point>
    {
        protected override IntPtr CreateNativeList()
        {
            return PointListNative.kth_chain_point_list_construct_default();
        }

        protected override Point GetNthNativeElement(uint n)
        {
            return new Point(PointListNative.kth_chain_point_list_nth(NativeInstance, (UIntPtr) n));
        }

        protected override uint GetCount()
        {
            return (uint) PointListNative.kth_chain_point_list_count(NativeInstance);
        }

        protected override void AddElement(Point element)
        {
            PointListNative.kth_chain_point_list_push_back(NativeInstance, element.NativeInstance);
        }

        protected override void DestroyNativeList()
        {
            //Logger.Log("Destroying point list " + NativeInstance.ToString("X"));
            PointListNative.kth_chain_point_list_destruct(NativeInstance);
        }

        internal PointList(IntPtr nativeInstance) : base(nativeInstance)
        {            
        }
    }
    */
}