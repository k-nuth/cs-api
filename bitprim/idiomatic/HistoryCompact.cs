using System;
using System.Runtime.InteropServices;
using Bitprim.Native;

namespace Bitprim
{

    /// <summary>
    /// Output points, values, and spends for a payment address.
    /// </summary>
    public class HistoryCompact : IDisposable
    {
        private bool ownsNativeObject_;
        private IntPtr nativeInstance_;

        ~HistoryCompact()
        {
            Dispose(false);
        }

        /// <summary>
        /// The point that identifies the History instance.
        /// </summary>
        public Point Point
        {
            get
            {
                return new Point(HistoryCompactNative.chain_history_compact_get_point(nativeInstance_));
            }
        }

        /// <summary>
        /// Used for distinguishing between values and spends.
        /// </summary>
        public PointKind PointKind
        {
            get
            {
                return (PointKind)Enum.ToObject
                (
                    typeof(PointKind),
                    HistoryCompactNative.chain_history_compact_get_point_kind(nativeInstance_)
                );
            }
        }

        /// <summary>
        /// Height of the block containing the Point.
        /// </summary>
        public UInt32 Height
        {
            get
            {
                return HistoryCompactNative.chain_history_compact_get_height(nativeInstance_);
            }
        }

        /// <summary>
        /// Varies depending on point_kind.
        /// </summary>
        public UInt64 ValueOrChecksum
        {
            get
            {
                return HistoryCompactNative.chain_history_compact_get_value_or_previous_checksum(nativeInstance_);
            }
        }

        /// <summary>
        /// Release resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal HistoryCompact(IntPtr nativeInstance, bool ownsNativeObject = true)
        {
            nativeInstance_ = nativeInstance;
            ownsNativeObject_ = ownsNativeObject;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //Release managed resources and call Dispose for member variables
            }
            //Release unmanaged resources
            if(ownsNativeObject_)
            {
                //Logger.Log("Destroying history compact " + nativeInstance_.ToString("X") + " ...");
                HistoryCompactNative.chain_history_compact_destruct(nativeInstance_);
                //Logger.Log("History compact " + nativeInstance_.ToString("X") + " destroyed!");
            }
        }
    }

}