using System.IO;

namespace Genesis.Shared.Structures.Specifics
{
    using Utils.Extensions;

    public struct WeaponSpecific
    {
        public float AccucaryModifier;
        public int BulletId;
        public float DamageBonusPerLevel;
        public float DamageScalar;
        public int DmgMaxMax;
        public int DmgMaxMin;
        public int DmgMinMax;
        public int DmgMinMin;
        public int DotDuration;
        public float ExplosionRadius;
        public Vector3 FirePoint;
        public byte Flags;
        public short Heat;
        public float HitBonusPerLevel;
        public DamageArray MaxMax;
        public DamageArray MaxMin;
        public DamageArray MinMax;
        public DamageArray MinMin;
        public short OffenseBonus;
        public short PenetrationModifier;
        public float RangeMax;
        public float RangeMin;
        public int RechargeTime;
        public byte SprayTargets;
        public byte SubType;
        public byte TurretSize;
        public float ValidArc;

        public static WeaponSpecific Read(BinaryReader br)
        {
            return new WeaponSpecific
            {
                FirePoint = Vector3.Read(br),
                MinMin = DamageArray.Read(br),
                MinMax = DamageArray.Read(br),
                MaxMin = DamageArray.Read(br),
                MaxMax = DamageArray.Read(br),
                ValidArc = br.ReadSingle(),
                RangeMin = br.ReadSingle(),
                RangeMax = br.ReadSingle(),
                ExplosionRadius = br.ReadSingle(),
                DamageScalar = br.ReadSingle(),
                AccucaryModifier = br.ReadSingle(),
                RechargeTime = br.ReadInt32(),
                DmgMinMin = br.ReadInt32(),
                DmgMinMax = br.ReadInt32(),
                DmgMaxMin = br.ReadInt32(),
                DmgMaxMax = br.ReadInt32(),
                BulletId = br.ReadInt32(),
                DotDuration = br.ReadInt32(),
                PenetrationModifier = br.ReadInt16(),
                Heat = br.ReadInt16(),
                SubType = br.ReadByte(),
                TurretSize = br.ReadByte(),
                Flags = br.ReadByte(),
                SprayTargets = br.ReadByte(),
                OffenseBonus = br.ReadInt16(),
                HitBonusPerLevel = br.ReadPadding(2).ReadSingle(),
                DamageBonusPerLevel = br.ReadSingle()
            };
        }
    }
}
