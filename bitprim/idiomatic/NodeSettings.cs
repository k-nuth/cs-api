using Bitprim.Native;

namespace Bitprim
{
    public class NodeSettings
    {

        public static CurrencyType CurrencyType
        {
            get
            {
                return NodeSettingsNative.node_settings_get_currency();
            }
        }

    }
}