using System;

namespace Bitprim
{
    public interface IHistoryCompact : IDisposable
    {
        /// <summary>
        /// The point that identifies the History instance.
        /// </summary>
        IPoint Point { get; }

        /// <summary>
        /// Used for distinguishing between values and spends.
        /// </summary>
        PointKind PointKind { get; }

        /// <summary>
        /// Height of the block containing the Point.
        /// </summary>
        UInt32 Height { get; }

        /// <summary>
        /// Varies depending on point_kind.
        /// </summary>
        UInt64 ValueOrChecksum { get; }
    }
}