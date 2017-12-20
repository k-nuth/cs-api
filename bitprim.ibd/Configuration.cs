using System;
using System.Configuration;

namespace bitprim.ibd
{
    internal class Configuration
    {
        public static string GetSetting(string settingName)
        {
            return ConfigurationManager.AppSettings[settingName];
        }
    }
}
