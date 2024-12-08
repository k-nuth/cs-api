// Copyright (c) 2016-2024 Knuth Project developers.
// Distributed under the MIT software license, see the accompanying
// file COPYING or http://www.opensource.org/licenses/mit-license.php.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Knuth.Config
{
    public class BlockchainSettings {
        public static BlockchainSettings GetDefault(NetworkType network) {
            var native = Knuth.Native.Config.BlockchainSettingsNative.kth_config_blockchain_settings_default(network);
            return FromNative(native);
        }

        public UInt32 Cores { get; set; }
        public bool Priority { get; set; }
        public float ByteFeeSatoshis { get; set; }
        public float SigopFeeSatoshis { get; set; }
        public UInt64 MinimumOutputSatoshis { get; set; }
        public UInt32 NotifyLimitHours { get; set; }
        public UInt32 ReorganizationLimit { get; set; }
        public IList<Checkpoint> Checkpoints { get; set; }
        public bool FixCheckpoints { get; set; }
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

#if KTH_CS_CURRENCY_BCH        //TODO(fernando): rename to CURRENCY_BCH or something like that
        public bool BchUahf { get; set; }
        public bool BchDaaCw144 { get; set; }
        public bool BchPythagoras { get; set; }
        public bool BchEuclid { get; set; }
        public bool BchPisano { get; set; }
        public bool BchMersenne { get; set; }
        public bool BchFermat { get; set; }
        public bool BchEuler { get; set; }
        public bool BchGauss { get; set; }
        public bool BchDescartes { get; set; }
        public bool BchLobachevski { get; set; }
        public UInt64 GaloisActivationTime { get; set; }
        public UInt64 LeibnizActivationTime { get; set; }
        public UInt64 AsertHalfLife { get; set; }
#else
        public bool Bip141 { get; set; }
        public bool Bip143 { get; set; }
        public bool Bip147 { get; set; }
#endif //KTH_CS_CURRENCY_BCH

        public Knuth.Native.Config.BlockchainSettings ToNative() {
            var native = new Knuth.Native.Config.BlockchainSettings();
            native.cores = this.Cores;
            native.priority = this.Priority;
            native.byte_fee_satoshis = this.ByteFeeSatoshis;
            native.sigop_fee_satoshis = this.SigopFeeSatoshis;
            native.minimum_output_satoshis = this.MinimumOutputSatoshis;
            native.notify_limit_hours = this.NotifyLimitHours;
            native.reorganization_limit = this.ReorganizationLimit;

            native.checkpoints = Helper.ListToNative(this.Checkpoints,
                Knuth.Native.Config.CheckpointNative.kth_config_checkpoint_allocate_n,
                x => x.ToNative(),
                ref native.checkpoint_count);

            native.fix_checkpoints = this.FixCheckpoints;
            native.allow_collisions = this.AllowCollisions;
            native.easy_blocks = this.EasyBlocks;
            native.retarget = this.Retarget;
            native.bip16 = this.Bip16;
            native.bip30 = this.Bip30;
            native.bip34 = this.Bip34;
            native.bip66 = this.Bip66;
            native.bip65 = this.Bip65;
            native.bip90 = this.Bip90;
            native.bip68 = this.Bip68;
            native.bip112 = this.Bip112;
            native.bip113 = this.Bip113;

#if KTH_CS_CURRENCY_BCH        //TODO(fernando): rename to CURRENCY_BCH or something like that
            native.bch_uahf = this.BchUahf;
            native.bch_daa_cw144 = this.BchDaaCw144;
            native.bch_pythagoras = this.BchPythagoras;
            native.bch_euclid = this.BchEuclid;
            native.bch_pisano = this.BchPisano;
            native.bch_mersenne = this.BchMersenne;
            native.bch_fermat = this.BchFermat;
            native.bch_euler = this.BchEuler;
            native.bch_gauss = this.BchGauss;
            native.bch_descartes = this.BchDescartes;
            native.bch_lobachevski = this.BchLobachevski;
            native.galois_activation_time = this.GaloisActivationTime;
            native.leibniz_activation_time = this.LeibnizActivationTime;
            native.asert_half_life = this.AsertHalfLife;
#else
            native.bip141 = this.Bip141;
            native.bip141 = this.Bip141;
            native.bip147 = this.Bip147;
#endif //KTH_CS_CURRENCY_BCH
            return native;
        }

        public static BlockchainSettings FromNative(Knuth.Native.Config.BlockchainSettings native) {
            var res = new BlockchainSettings();
            res.Cores = native.cores;
            res.Priority = native.priority;
            res.ByteFeeSatoshis = native.byte_fee_satoshis;
            res.SigopFeeSatoshis = native.sigop_fee_satoshis;
            res.MinimumOutputSatoshis = native.minimum_output_satoshis;
            res.NotifyLimitHours = native.notify_limit_hours;
            res.ReorganizationLimit = native.reorganization_limit;
            res.Checkpoints = Helper.ArrayOfPointersToManaged<Checkpoint, Native.Config.Checkpoint>(native.checkpoints, native.checkpoint_count, Checkpoint.FromNative);
            res.FixCheckpoints = native.fix_checkpoints;
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

#if KTH_CS_CURRENCY_BCH        //TODO(fernando): rename to CURRENCY_BCH or something like that
            res.BchUahf = native.bch_uahf;
            res.BchDaaCw144 = native.bch_daa_cw144;
            res.BchPythagoras = native.bch_pythagoras;
            res.BchEuclid = native.bch_euclid;
            res.BchPisano = native.bch_pisano;
            res.BchMersenne = native.bch_mersenne;
            res.BchFermat = native.bch_fermat;
            res.BchEuler = native.bch_euler;
            res.BchGauss = native.bch_gauss;
            res.BchDescartes = native.bch_descartes;
            res.BchLobachevski = native.bch_lobachevski;
            res.GaloisActivationTime = native.galois_activation_time;
            res.LeibnizActivationTime = native.leibniz_activation_time;
            res.AsertHalfLife = native.asert_half_life;
#else
            res.Bip141 = native.bip141;
            res.Bip141 = native.bip141;
            res.Bip147 = native.bip147;
#endif //KTH_CS_CURRENCY_BCH
            return res;
        }

    }
}
