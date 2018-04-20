using System;
using Bitprim.Native;

namespace Bitprim
{

    public class PointList : NativeList<Point>
    {
        public override IntPtr CreateNativeList()
        {
            return PointListNative.chain_point_list_construct_default();
        }

        public override Point GetNthNativeElement(uint n)
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
            //Logger.Log("Destroying point list " + NativeInstance.ToString("X"));
            PointListNative.chain_point_list_destruct(NativeInstance);
        }

        internal PointList(IntPtr nativeInstance) : base(nativeInstance)
        {            
        }
    }

}