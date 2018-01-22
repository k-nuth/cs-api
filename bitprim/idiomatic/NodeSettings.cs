using Bitprim.Native;

namespace Bitprim
{
    public class NodeSettings
    {
        public static bool UseTestnetRules
        {
            get
            {
                return NetworkType == NetworkType.Testnet;
            }
        }

        public static CurrencyType CurrencyType
        {
            get
            {
                return NodeSettingsNative.node_settings_get_currency();
            }
        }

        public static NetworkType NetworkType
        {
            get
            {
                return NodeSettingsNative.node_settings_get_network();
            }
        }
    }
}