using System;
using Bitprim;

namespace api
{
    internal static class Utils
    {
        public static string EncodeInBase16(UInt32 number)
        {
            return Convert.ToString(number, 16);
        }

        public static double SatoshisToBTC(UInt64 satoshis)
        {
            return (double)satoshis / 100000000;
        }

        //TODO Remove this when bitprim wrapper implemented
        public static double BitsToDifficulty(UInt32 bits)
        {
            double diff = 1.0;
            int shift = (int) (bits >> 24) & 0xff;
            diff = (double)0x0000ffff / (double)(bits & 0x00ffffff);
            while (shift < 29)
            {
                diff *= 256.0;
                ++shift;
            }
            while (shift > 29)
            {
                diff /= 256.0;
                --shift;
            }
            return diff;
        }

        public static void CheckBitprimApiErrorCode(ErrorCode errorCode, string errorMsg)
        {
            if(errorCode != ErrorCode.Success)
            {
                throw new ApplicationException(errorMsg + ". ErrorCode: " + errorCode.ToString());
            }
        }

        public static void CheckIfChainIsFresh(Chain chain, bool acceptStaleRequests)
        {
            if(!acceptStaleRequests && chain.IsStale)
            {
                throw new ApplicationException("Node is still synchronizing; API cannot be used yet");
            }
        }
    }
}