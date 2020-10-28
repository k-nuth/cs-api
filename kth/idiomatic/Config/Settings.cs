// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Knuth.Config
{
    public class Settings {
        public static Settings GetDefault(NetworkType network) {
            var res = new Settings();
            res.Chain = BlockchainSettings.GetDefault(network);
            res.Database = DatabaseSettings.GetDefault(network);
            res.Network = NetworkSettings.GetDefault(network);
            res.Node = NodeSettings.GetDefault(network);

            //TODO(fernando): remove this in c-api version > 0.6.0. It is a workaround.
            res.Network.Services = 1;
            res.Network.HostPoolCapacity = 1000;
            return res;
        }

        public static ApiCallResultWithMessage<Settings> GetFromFile(string path) {
            string errorMessage;
            IntPtr nativePtr;
            var ok = Knuth.Native.Config.SettingsNative.kth_config_settings_get_from_file(path, out nativePtr, out errorMessage);

            if ( ! ok) {
                return new ApiCallResultWithMessage<Settings> {
                    Ok = false,
                    ErrorMessage = errorMessage,
                    Result = null
                };
            }

            var native = (Native.Config.Settings)Marshal.PtrToStructure(nativePtr, typeof(Native.Config.Settings));
            var res = new Settings();
            res.Chain = BlockchainSettings.FromNative(native.chain);
            res.Database = DatabaseSettings.FromNative(native.database);
            res.Network = NetworkSettings.FromNative(native.network);
            res.Node = NodeSettings.FromNative(native.node);
            Knuth.Native.Config.SettingsNative.kth_config_settings_destruct(nativePtr);

            return new ApiCallResultWithMessage<Settings> {
                Ok = true,
                ErrorMessage = null,
                Result = res
            };
        }

        public BlockchainSettings Chain { get; set; }
        public DatabaseSettings Database { get; set; }
        public NetworkSettings Network { get; set; }
        public NodeSettings Node { get; set; }

        public Knuth.Native.Config.Settings ToNative() {
            var native = new Knuth.Native.Config.Settings();
            native.chain = this.Chain.ToNative();
            native.database = this.Database.ToNative();
            native.network = this.Network.ToNative();
            native.node = this.Node.ToNative();
            return native;
        }

    }
}
