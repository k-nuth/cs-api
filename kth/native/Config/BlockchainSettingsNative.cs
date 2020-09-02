// Copyright (c) 2016-2020 Knuth Project developers.
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

#if BCH        //TODO(fernando): rename to CURRENCY_BCH or something like that
        public bool bch_uahf;
        public bool bch_daa_cw144;
        public bool bch_monolith;
        public bool bch_magnetic_anomaly;
        public bool bch_great_wall;
        public bool bch_graviton;
        public bool bch_phonon;
        public bool bch_axion;
            
        public UInt64 axion_activation_time;

        // The half life for the ASERTi3-2d DAA. For every (asert_half_life) seconds behind schedule the blockchain gets, difficulty is cut in half. 
        // Doubled if blocks are ahead of schedule.
        public UInt64 asert_half_life;   //two days
#else
        public bool bip141;
        public bool bip143;
        public bool bip147;
#endif //BCH

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


// typedef struct {
//     uint32_t cores;
//     kth_bool_t priority;
//     float byte_fee_satoshis;
//     float sigop_fee_satoshis;
//     uint64_t minimum_output_satoshis;
//     uint32_t notify_limit_hours;
//     uint32_t reorganization_limit;
    
//     size_t checkpoint_count;
//     kth_checkpoint* checkpoints;

//     kth_bool_t allow_collisions;
//     kth_bool_t easy_blocks;
//     kth_bool_t retarget;
//     kth_bool_t bip16;
//     kth_bool_t bip30;
//     kth_bool_t bip34;
//     kth_bool_t bip66;
//     kth_bool_t bip65;
//     kth_bool_t bip90;
//     kth_bool_t bip68;
//     kth_bool_t bip112;
//     kth_bool_t bip113;

// #if defined(KTH_CURRENCY_BCH)
//     kth_bool_t bch_uahf;
//     kth_bool_t bch_daa_cw144;
//     kth_bool_t bch_monolith;
//     kth_bool_t bch_magnetic_anomaly;
//     kth_bool_t bch_great_wall;
//     kth_bool_t bch_graviton;
//     kth_bool_t bch_phonon;      // 2020-May
//     kth_bool_t bch_axion;       // 2020-Nov
        
//     //2020-Nov-15 hard fork, defaults to 1605441600: Nov 15, 2020 12:00:00 UTC protocol upgrade
//     uint64_t axion_activation_time;

//     // The half life for the ASERTi3-2d DAA. For every (asert_half_life) seconds behind schedule the blockchain gets, difficulty is cut in half. 
//     // Doubled if blocks are ahead of schedule.
//     uint64_t asert_half_life;   //two days
// #else
//     kth_bool_t bip141;
//     kth_bool_t bip143;
//     kth_bool_t bip147;
// #endif //KTH_CURRENCY_BCH

// #if defined(KTH_WITH_MEMPOOL)
//     size_t mempool_max_template_size;
//     size_t mempool_size_multiplier;
// #endif
// } kth_blockchain_settings;
