// Copyright (c) 2016-2022 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Runtime.InteropServices;

namespace Knuth.Native.Config
{
    [StructLayout(LayoutKind.Sequential)]
    public struct BlockchainSettings {
        public UInt32 cores;
        public bool priority;
        public float byte_fee_satoshis;
        public float sigop_fee_satoshis;
        public UInt64 minimum_output_satoshis;
        public UInt32 notify_limit_hours;
        public UInt32 reorganization_limit;
        public UInt64 checkpoint_count;
        public IntPtr checkpoints;         //kth_checkpoint*
        public bool fix_checkpoints;
        public bool allow_collisions;
        public bool easy_blocks;
        public bool retarget;
        public bool bip16;
        public bool bip30;
        public bool bip34;
        public bool bip66;
        public bool bip65;
        public bool bip90;
        public bool bip68;
        public bool bip112;
        public bool bip113;

#if KTH_CS_CURRENCY_BCH        //TODO(fernando): rename to CURRENCY_BCH or something like that
        public bool bch_uahf;
        public bool bch_daa_cw144;
        public bool bch_pythagoras;
        public bool bch_euclid;
        public bool bch_pisano;
        public bool bch_mersenne;
        public bool bch_fermat;
        public bool bch_euler;
        public bool bch_gauss;
        public bool bch_descartes;
        public bool bch_lobachevski;
        public UInt64 galois_activation_time;
        public UInt64 leibniz_activation_time;
        public UInt64 asert_half_life;
#else
        public bool bip141;
        public bool bip143;
        public bool bip147;
#endif //KTH_CS_CURRENCY_BCH

// #if defined(KTH_WITH_MEMPOOL)
        // UInt64 mempool_max_template_size;
        // UInt64 mempool_size_multiplier;
// #endif
    }

    public static class BlockchainSettingsNative
    {
        [DllImport(Constants.KTH_C_LIBRARY)]
        public static extern BlockchainSettings kth_config_blockchain_settings_default(NetworkType network);
    }
}
