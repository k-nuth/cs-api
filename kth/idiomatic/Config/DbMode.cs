// Copyright (c) 2016-2024 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Knuth.Config {
    public enum DbMode {
        Pruned = 0,
        Normal = 1,
        FullIndexed = 2
    }
}
