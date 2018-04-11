using System.IO;

namespace Bitprim
{
    //TODO Use dependency injection instead of a static class
    internal static class Logger
    {
        public static void Log(string msg)
        {
            using (StreamWriter sw = File.AppendText("bitprim-cs-log.txt")) 
            {
                sw.WriteLine(msg);
            }
        }
    }
}