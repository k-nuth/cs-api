// Copyright (c) 2016-2022 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using Knuth.Native;

namespace Knuth
{

    /// <summary>
    /// Output points, values, and spends for a payment address.
    /// </summary>
    public class HistoryCompact : IHistoryCompact
    {
        private bool ownsNativeObject_;
        private IntPtr nativeInstance_;
        private Point point_;

        ~HistoryCompact() {
            Dispose(false);
        }

        /// <summary>
        /// The point that identifies the History instance.
        /// </summary>
        public IPoint Point
        {
            get
            {
                return point_;
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
                    HistoryCompactNative.kth_chain_history_compact_get_point_kind(nativeInstance_)
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
                return HistoryCompactNative.kth_chain_history_compact_get_height(nativeInstance_);
            }
        }

        /// <summary>
        /// Varies depending on point_kind.
        /// </summary>
        public UInt64 ValueOrChecksum
        {
            get
            {
                return HistoryCompactNative.kth_chain_history_compact_get_value_or_previous_checksum(nativeInstance_);
            }
        }

        /// <summary>
        /// Release resources
        /// </summary>
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal HistoryCompact(IntPtr nativeInstance, bool ownsNativeObject = true) {
            nativeInstance_ = nativeInstance;
            ownsNativeObject_ = ownsNativeObject;
            point_ = new Point(HistoryCompactNative.kth_chain_history_compact_get_point(nativeInstance_));
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                //Release managed resources and call Dispose for member variables
            }
            //Release unmanaged resources
            if (ownsNativeObject_) {
                //Logger.Log("Destroying history compact " + nativeInstance_.ToString("X") + " ...");
                HistoryCompactNative.kth_chain_history_compact_destruct(nativeInstance_);
                //Logger.Log("History compact " + nativeInstance_.ToString("X") + " destroyed!");
            }
        }
    }

}