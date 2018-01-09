using System;

namespace api
{
    internal static class Utils
    {
        public static string EncodeInBase16(UInt32 number)
        {
            return Convert.ToString(number, 16);
        }

        public static void CheckBitprimApiErrorCode(int errorCode, string errorMsg)
        {
            if(errorCode != 0)
            {
                throw new ApplicationException(errorMsg);
            }
        }
    }
}