using System;
using System.IO;

namespace Genesis.Shared.Prefix
{
    using Structures;
    using Utils.Extensions;

    public class PrefixWeapon : PrefixBase
    {
        public Single AccucaryBonusPercent;
        public DamageArray DamageAdjustMaximum;
        public DamageArray DamageAdjustMinimum;
        public Single DamagePercentAll;
        public Single[] DamagePercentMaximum;
        public Single[] DamagePercentMinimum;
        public Single FiringArcPercent;
        public Int16 HeatAdjust;
        public Single HeatPercent;
        public Int16 OffenseBonus;
        public Single OffenseBonusPercent;
        public Int16 PenetrationBonus;
        public Int16 PowerPerShot;
        public Single RangePercent;
        public Single RechargeTimePercent;

        public PrefixWeapon(BinaryReader br)
            : base(br)
        {
            FiringArcPercent = br.ReadSingle();
            RangePercent = br.ReadSingle();
            RechargeTimePercent = br.ReadSingle();
            HeatPercent = br.ReadSingle();
            HeatAdjust = br.ReadInt16();
            PowerPerShot = br.ReadInt16();
            DamagePercentAll = br.ReadSingle();
            DamagePercentMinimum = br.Read<Single>(6);
            DamagePercentMaximum = br.Read<Single>(6);
            DamageAdjustMinimum = DamageArray.Read(br);
            DamageAdjustMaximum = DamageArray.Read(br);
            OffenseBonus = br.ReadInt16();

            br.ReadBytes(2);

            OffenseBonusPercent = br.ReadSingle();
            AccucaryBonusPercent = br.ReadSingle();
            PenetrationBonus = br.ReadInt16();

            br.ReadBytes(2);
        }
    }
}
