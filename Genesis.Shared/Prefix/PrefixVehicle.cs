using System;
using System.IO;

namespace Genesis.Shared.Prefix
{
    public class PrefixVehicle : PrefixBase
    {
        public Single AVDCollisionSpinDampeningAdjust;
        public Single AVDNormalSpinDampeningAdjust;
        public Int32 ArmorAdjust;
        public Single ArmorAdjustPercent;
        public Single BrakesMaxTorqueFrontAdjustPercent;
        public Single BrakesMaxTorqueRearAdjustPercent;
        public Int32 CooldownAdjust;
        public Single CooldownAdjustPercent;
        public Int32 HeatMaximumAdjust;
        public Single HeatMaximumAdjustPercent;
        public Int16 InventorySlotsAdjust;
        public Single MaxWtArmorAdjustPercent;
        public Single MaxWtPowerplantAdjustPercent;
        public Single MaxWtWeaponFrontAdjustPercent;
        public Single MaxWtWeaponRearAdjustPercent;
        public Single MaxWtWeaponTurretAdjustPercent;
        public Int32 PowerAdjust;
        public Single PowerAdjustPercent;
        public Single SpeedAdjustPercent;
        public Single SteeringFullSpeedLimitAdjust;
        public Single SteeringMaxAngleAdjust;
        public Int32 TorqueMaxAdjust;
        public Single TorqueMaxAdjustPercent;

        public PrefixVehicle(BinaryReader br)
            : base(br)
        {
            ArmorAdjustPercent = br.ReadSingle();
            ArmorAdjust = br.ReadInt32();
            PowerAdjustPercent = br.ReadSingle();
            PowerAdjust = br.ReadInt32();
            HeatMaximumAdjustPercent = br.ReadSingle();
            HeatMaximumAdjust = br.ReadInt32();
            CooldownAdjustPercent = br.ReadSingle();
            CooldownAdjust = br.ReadInt32();
            BrakesMaxTorqueFrontAdjustPercent = br.ReadSingle();
            BrakesMaxTorqueRearAdjustPercent = br.ReadSingle();
            SteeringMaxAngleAdjust = br.ReadSingle();
            SteeringFullSpeedLimitAdjust = br.ReadSingle();
            AVDNormalSpinDampeningAdjust = br.ReadSingle();
            AVDCollisionSpinDampeningAdjust = br.ReadSingle();
            TorqueMaxAdjustPercent = br.ReadSingle();
            TorqueMaxAdjust = br.ReadInt32();
            SpeedAdjustPercent = br.ReadSingle();
            MaxWtWeaponFrontAdjustPercent = br.ReadSingle();
            MaxWtWeaponTurretAdjustPercent = br.ReadSingle();
            MaxWtWeaponRearAdjustPercent = br.ReadSingle();
            MaxWtArmorAdjustPercent = br.ReadSingle();
            MaxWtPowerplantAdjustPercent = br.ReadSingle();
            InventorySlotsAdjust = br.ReadInt16();

            br.ReadBytes(2);
        }
    }
}
