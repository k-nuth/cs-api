using Bitprim.Native;

namespace Bitprim
{
    /// <summary>
    /// Holds node settings
    /// </summary>
    public class NodeSettings
    {
        /// <summary>
        /// Returns the node's currency
        /// </summary>
        public static CurrencyType CurrencyType
        {
            get
            {
                return NodeSettingsNative.node_settings_get_currency();
            }
        }

    }
}