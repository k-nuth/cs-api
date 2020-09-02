// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;

namespace Knuth.Config
{
    public struct Checkpoint {
        public byte[] Hash { get; set; }
        public UInt64 Height { get; set; }

        public static Checkpoint FromNative(Knuth.Native.Config.Checkpoint native) {
            var res = new Checkpoint();
            res.Height = native.height;
            res.Hash = native.hash.hash;
            return res;
        }
    }
}
