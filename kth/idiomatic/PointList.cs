using System;
using Bitprim.Native;

namespace Bitprim
{
   /*
    public class PointList : NativeList<Point>
    {
        protected override IntPtr CreateNativeList()
        {
            return PointListNative.chain_point_list_construct_default();
        }

        protected override Point GetNthNativeElement(uint n)
        {
            return new Point(PointListNative.chain_point_list_nth(NativeInstance, (UIntPtr) n));
        }

        protected override uint GetCount()
        {
            return (uint) PointListNative.chain_point_list_count(NativeInstance);
        }

        protected override void AddElement(Point element)
        {
            PointListNative.chain_point_list_push_back(NativeInstance, element.NativeInstance);
        }

        protected override void DestroyNativeList()
        {
            //Logger.Log("Destroying point list " + NativeInstance.ToString("X"));
            PointListNative.chain_point_list_destruct(NativeInstance);
        }

        internal PointList(IntPtr nativeInstance) : base(nativeInstance)
        {            
        }
    }
    */
}