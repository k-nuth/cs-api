// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;

namespace Knuth {
    public static class Helper {
        public static int BoolToC(bool x) {
            return x ? 1 : 0;
        }
    }
}