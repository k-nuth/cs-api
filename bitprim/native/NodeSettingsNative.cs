using System;
using System.Runtime.InteropServices;

namespace Bitprim.Native
{

    public static class NodeSettingsNative
    {
        [DllImport(Constants.BITPRIM_C_LIBRARY)]
        public static extern CurrencyType node_settings_get_currency();
    }

}