using System.IO;

namespace Genesis.Shared.Prefix
{
    public class PrefixVehicle : PrefixBase
    {
        public float AVDCollisionSpinDampeningAdjust;
        public float AVDNormalSpinDampeningAdjust;
        public int ArmorAdjust;
        public float ArmorAdjustPercent;
        public float BrakesMaxTorqueFrontAdjustPercent;
        public float BrakesMaxTorqueRearAdjustPercent;
        public int CooldownAdjust;
        public float CooldownAdjustPercent;
        public int HeatMaximumAdjust;
        public float HeatMaximumAdjustPercent;
        public short InventorySlotsAdjust;
        public float MaxWtArmorAdjustPercent;
        public float MaxWtPowerplantAdjustPercent;
        public float MaxWtWeaponFrontAdjustPercent;
        public float MaxWtWeaponRearAdjustPercent;
        public float MaxWtWeaponTurretAdjustPercent;
        public int PowerAdjust;
        public float PowerAdjustPercent;
        public float SpeedAdjustPercent;
        public float SteeringFullSpeedLimitAdjust;
        public float SteeringMaxAngleAdjust;
        public int TorqueMaxAdjust;
        public float TorqueMaxAdjustPercent;

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
