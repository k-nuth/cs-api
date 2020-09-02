// Copyright (c) 2016-2020 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Knuth.Config
{
    public struct BlockchainSettings {
        public static BlockchainSettings GetDefault(NetworkType network) {
            var native = Knuth.Native.Config.BlockchainSettingsNative.kth_config_blockchain_settings_default(network);
            return FromNative(native);
        }

        public int Cores { get; set; }
        public bool Priority { get; set; }
        public float ByteFeeSatoshis { get; set; }
        public float SigopFeeSatoshis { get; set; }
        public UInt64 MinimumOutputSatoshis { get; set; }
        public int NotifyLimitHours { get; set; }
        public int ReorganizationLimit { get; set; }
        public IList<Checkpoint> Checkpoints { get; set; }
        public bool AllowCollisions { get; set; }
        public bool EasyBlocks { get; set; }
        public bool Retarget { get; set; }
        public bool Bip16 { get; set; }
        public bool Bip30 { get; set; }
        public bool Bip34 { get; set; }
        public bool Bip66 { get; set; }
        public bool Bip65 { get; set; }
        public bool Bip90 { get; set; }
        public bool Bip68 { get; set; }
        public bool Bip112 { get; set; }
        public bool Bip113 { get; set; }

#if BCH        //TODO(fernando): rename to CURRENCY_BCH or something like that
        public bool BchUahf { get; set; }
        public bool BchDaaCw144 { get; set; }
        public bool BchMonolith { get; set; }
        public bool BchMagneticAnomaly { get; set; }
        public bool BchGreatWall { get; set; }
        public bool BchGraviton { get; set; }
        public bool BchPhonon { get; set; }
        public bool BchAxion { get; set; }
            
        public UInt64 AxionActivationTime { get; set; }

        public UInt64 AsertHalfLife { get; set; }
#else
        public bool Bip141 { get; set; }
        public bool Bip143 { get; set; }
        public bool Bip147 { get; set; }
#endif //BCH


        private static BlockchainSettings FromNative(Knuth.Native.Config.BlockchainSettings native) {
            var res = new BlockchainSettings();
            res.Cores = native.cores;
            res.Priority = native.priority;
            res.ByteFeeSatoshis = native.byte_fee_satoshis;
            res.SigopFeeSatoshis = native.sigop_fee_satoshis;
            res.MinimumOutputSatoshis = native.minimum_output_satoshis;
            res.NotifyLimitHours = native.notify_limit_hours;
            res.ReorganizationLimit = native.reorganization_limit;
            
            // res.Checkpoints = native.checkpoints;


            // InnerStruct[] iStructArray = new InnerStruct[count];
            // int infoSize = Marshal.SizeOf(new iStructArray());
            // for (int i = 0; i < count; i++)
            // {
            //     IntPtr targetPtr = new IntPtr(pIStruct.ToInt32() + infoSize * i);
            //     iStructArray[i] = (InnerStruct)Marshal.PtrToStructure(targetPtr, typeof(InnerStruct));
            // }            
            

            // var checks = new List<Checkpoint>();
            // int infoSize = Marshal.SizeOf(new Native.Config.Checkpoint());
            // int count = (int)native.checkpoint_count;
            // IntPtr ptr = native.checkpoints;
            // for (int i = 0; i < count; ++i) {
            //     var mem = ptr.ToInt64() + infoSize * i;
            //     IntPtr targetPtr = new IntPtr(mem);
            //     var nativeCheckpoint = (Native.Config.Checkpoint)Marshal.PtrToStructure(targetPtr, typeof(Native.Config.Checkpoint));
            //     var checkpoint = Checkpoint.FromNative(nativeCheckpoint);
            //     checks.Add(checkpoint);
            // }            


            res.Checkpoints = Helper.ArrayOfPointersToManaged<Checkpoint, Native.Config.Checkpoint>(native.checkpoints, native.checkpoint_count, Checkpoint.FromNative);

            res.AllowCollisions = native.allow_collisions;
            res.EasyBlocks = native.easy_blocks;
            res.Retarget = native.retarget;
            res.Bip16 = native.bip16;
            res.Bip30 = native.bip30;
            res.Bip34 = native.bip34;
            res.Bip66 = native.bip66;
            res.Bip65 = native.bip65;
            res.Bip90 = native.bip90;
            res.Bip68 = native.bip68;
            res.Bip112 = native.bip112;
            res.Bip113 = native.bip113;

#if BCH        //TODO(fernando): rename to CURRENCY_BCH or something like that
            res.BchUahf = native.bch_uahf;
            res.BchDaaCw144 = native.bch_daa_cw144;
            res.BchMonolith = native.bch_monolith;
            res.BchMagneticAnomaly = native.bch_magnetic_anomaly;
            res.BchGreatWall = native.bch_great_wall;
            res.BchGraviton = native.bch_graviton;
            res.BchPhonon = native.bch_phonon;
            res.BchAxion = native.bch_axion;
            res.AxionActivationTime = native.axion_activation_time;
            res.AsertHalfLife = native.asert_half_life;
#else
            res.Bip141 = native.bip141;
            res.Bip141 = native.bip141;
            res.Bip147 = native.bip147;
#endif //BCH
            return res;
        }

    }
}
