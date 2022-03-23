// Copyright (c) 2016-2022 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;

namespace Knuth.Config
{
    public class Endpoint {
        public string Scheme { get; set; }
        public string Host { get; set; }
        public UInt16 Port { get; set; }

        public Knuth.Native.Config.Endpoint ToNative() {
            var native = new Knuth.Native.Config.Endpoint();
            // native.scheme = Helper.StringToPtr(this.Scheme);
            // native.host = Helper.StringToPtr(this.Host);

            native.scheme = this.Scheme;
            native.host = this.Host;
            native.port = this.Port;
            return native;
        }
        public static Endpoint FromNative(Knuth.Native.Config.Endpoint native) {
            var res = new Endpoint();
            // res.Scheme = Helper.PtrToString(native.scheme);
            // res.Host = Helper.PtrToString(native.host);
            res.Scheme = native.scheme;
            res.Host = native.host;
            res.Port = native.port;
            return res;
        }
    }
}
