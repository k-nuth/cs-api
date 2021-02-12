// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;

namespace Knuth.Config
{
    public class Authority {
        public string Ip { get; set; }
        public UInt16 Port { get; set; }

        public Knuth.Native.Config.Authority ToNative() {
            var native = new Knuth.Native.Config.Authority();
            // native.ip = Helper.StringToPtr(this.Ip);
            native.ip = this.Ip;
            native.port = this.Port;
            return native;
        }
        public static Authority FromNative(Knuth.Native.Config.Authority native) {
            var res = new Authority();
            // res.Ip = Helper.PtrToString(native.ip);
            res.Ip = native.ip;
            res.Port = native.port;
            return res;
        }
    }
}
