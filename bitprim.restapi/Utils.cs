using System;

namespace api
{
    internal static class Utils
    {
        public static void CheckBitprimApiErrorCode(int errorCode, string errorMsg)
        {
            if(errorCode != 0)
            {
                throw new ApplicationException(errorMsg);
            }
        }
    }
}