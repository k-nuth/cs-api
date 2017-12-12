using System;
using Bitprim.Native;
using System.Collections;

namespace Bitprim
{

    public class PointList : NativeList<Point>
    {
        public override IntPtr CreateNativeList()
        {
            return PointListNative.chain_point_list_construct_default();
        }

        public override Point GetNthNativeElement(int n)
        {
            return new Point(PointListNative.chain_point_list_nth(NativeInstance, (UIntPtr) n));
        }

        public override uint GetCount()
        {
            return (uint) PointListNative.chain_point_list_count(NativeInstance);
        }

        public override void AddElement(Point element)
        {
            PointListNative.chain_point_list_push_back(NativeInstance, element.NativeInstance);
        }

        public override void DestroyNativeList()
        {
            PointListNative.chain_point_list_destruct(NativeInstance);
        }

        internal new IntPtr NativeInstance
        {
            get
            {
                return base.NativeInstance;
            }
        }

        internal PointList(IntPtr nativeInstance) : base(nativeInstance)
        {            
        }
    }

}