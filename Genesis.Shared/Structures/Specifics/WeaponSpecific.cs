using System;
using System.IO;

namespace Genesis.Shared.Structures.Specifics
{
    using Utils.Extensions;

    public struct WeaponSpecific
    {
        public Single AccucaryModifier;
        public Int32 BulletId;
        public Single DamageBonusPerLevel;
        public Single DamageScalar;
        public Int32 DmgMaxMax;
        public Int32 DmgMaxMin;
        public Int32 DmgMinMax;
        public Int32 DmgMinMin;
        public Int32 DotDuration;
        public Single ExplosionRadius;
        public Vector3 FirePoint;
        public Byte Flags;
        public Int16 Heat;
        public Single HitBonusPerLevel;
        public DamageArray MaxMax;
        public DamageArray MaxMin;
        public DamageArray MinMax;
        public DamageArray MinMin;
        public Int16 OffenseBonus;
        public Int16 PenetrationModifier;
        public Single RangeMax;
        public Single RangeMin;
        public Int32 RechargeTime;
        public Byte SprayTargets;
        public Byte SubType;
        public Byte TurretSize;
        public Single ValidArc;

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
