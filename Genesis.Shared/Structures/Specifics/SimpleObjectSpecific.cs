using System.IO;

namespace Genesis.Shared.Structures.Specifics
{
    using Utils.Extensions;

    public struct SimpleObjectSpecific
    {
        public float Alpha;
        public int Armor;
        public int CustomColor;
        public DamageArray DamageArmor;
        public int DisciplineRanks;
        public int DisciplineRequirement;
        public bool DropBrokenOnly;
        public int Faction;
        public short Flags;
        public int[] Ingredients;
        public byte InvSizeX;
        public byte InvSizeY;
        public bool IsNotTradeable;
        public short ItemRarity;
        public float Mass;
        public short MaxHitPoint;
        public ushort MaxUses;
        public byte MaximumEnhancements;
        public short MaximumGadgets;
        public short MinHitPoints;
        public string PhysicsName;
        public int Prefix;
        public short RaceRegenRate;
        public short RaceShieldFactor;
        public short RaceShieldRegenerate;
        public byte RenderType;
        public int RequiredClass;
        public short RequiredCombat;
        public short RequiredLevel;
        public short RequiredPerception;
        public short RequiredTech;
        public short RequiredTheory;
        public float Scale;
        public int Skill1;
        public int Skill2;
        public int Skill3;
        public int SkillGroup1;
        public int SkillGroup2;
        public int SkillGroup3;
        public ushort StackSize;
        public short SubType;

        public static SimpleObjectSpecific Read(BinaryReader br)
        {
            return new SimpleObjectSpecific
            {
                Armor = br.ReadInt32(),
                Skill1 = br.ReadInt32(),
                Skill2 = br.ReadInt32(),
                Skill3 = br.ReadInt32(),
                SkillGroup1 = br.ReadInt32(),
                SkillGroup2 = br.ReadInt32(),
                SkillGroup3 = br.ReadInt32(),
                CustomColor = br.ReadInt32(),
                Faction = br.ReadInt32(),
                Prefix = br.ReadInt32(),
                RequiredClass = br.ReadInt32(),
                Mass = br.ReadSingle(),
                Alpha = br.ReadSingle(),
                Scale = br.ReadSingle(),
                RequiredLevel = br.ReadInt16(),
                Flags = br.ReadInt16(),
                SubType = br.ReadInt16(),
                MinHitPoints = br.ReadInt16(),
                MaxHitPoint = br.ReadInt16(),
                RaceRegenRate = br.ReadInt16(),
                RaceShieldFactor = br.ReadInt16(),
                RequiredCombat = br.ReadInt16(),
                RequiredPerception = br.ReadInt16(),
                RequiredTech = br.ReadInt16(),
                RequiredTheory = br.ReadInt16(),
                InvSizeX = br.ReadByte(),
                InvSizeY = br.ReadByte(),
                RenderType = br.ReadByte(),
                MaximumEnhancements = br.ReadByte(),
                PhysicsName = br.ReadUnicodeString(65),
                DamageArmor = DamageArray.Read(br),
                Ingredients = br.Read<int>(5),
                DisciplineRequirement = br.ReadInt32(),
                DisciplineRanks = br.ReadInt32(),
                MaximumGadgets = br.ReadInt16(),
                RaceShieldRegenerate = br.ReadInt16(),
                ItemRarity = br.ReadInt16(),
                StackSize = br.ReadUInt16(),
                MaxUses = br.ReadUInt16(),
                IsNotTradeable = br.ReadBoolean(),
                DropBrokenOnly = br.ReadBoolean(),
            };
        }
    }
}
