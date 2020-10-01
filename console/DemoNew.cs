// using System;
// using System.Threading.Tasks;
// using Knuth;
// using Knuth.Config;

// namespace HelloKnuth
// {
//     public class Program {
//         public static void Main(string[] args) {
//             var settings = Settings.GetDefault(NetworkType.Mainnet);

//             var nativeBlk = settings.Chain.ToNative();
//             var nativeNet = settings.Network.ToNative();
//             Knuth.Native.Config.NetworkSettingsNative.kth_config_network_settings_test_something(nativeNet);

//             settings.Network.UserAgentBlacklist.Add("/Bitcoin ABC:");
//             var nativeNet2 = settings.Network.ToNative();
//             Knuth.Native.Config.NetworkSettingsNative.kth_config_network_settings_test_something(nativeNet2);

//             TestNative();
//         }
//     }
// }