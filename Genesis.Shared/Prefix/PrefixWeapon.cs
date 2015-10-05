using System.IO;

namespace Genesis.Shared.Prefix
{
    using Structures;
    using Utils.Extensions;

    public class PrefixWeapon : PrefixBase
    {
        public float AccucaryBonusPercent;
        public DamageArray DamageAdjustMaximum;
        public DamageArray DamageAdjustMinimum;
        public float DamagePercentAll;
        public float[] DamagePercentMaximum;
        public float[] DamagePercentMinimum;
        public float FiringArcPercent;
        public short HeatAdjust;
        public float HeatPercent;
        public short OffenseBonus;
        public float OffenseBonusPercent;
        public short PenetrationBonus;
        public short PowerPerShot;
        public float RangePercent;
        public float RechargeTimePercent;

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
            DamagePercentMinimum = br.Read<float>(6);
            DamagePercentMaximum = br.Read<float>(6);
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
