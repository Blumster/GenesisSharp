using System;
using System.IO;

namespace Genesis.Shared.Structures.Specifics
{
    using Utils.Extensions;

    public struct SimpleObjectSpecific
    {
        public Single Alpha;
        public Int32 Armor;
        public Int32 CustomColor;
        public DamageArray DamageArmor;
        public Int32 DisciplineRanks;
        public Int32 DisciplineRequirement;
        public Boolean DropBrokenOnly;
        public Int32 Faction;
        public Int16 Flags;
        public Int32[] Ingredients;
        public Byte InvSizeX;
        public Byte InvSizeY;
        public Boolean IsNotTradeable;
        public Int16 ItemRarity;
        public Single Mass;
        public Int16 MaxHitPoint;
        public UInt16 MaxUses;
        public Byte MaximumEnhancements;
        public Int16 MaximumGadgets;
        public Int16 MinHitPoints;
        public String PhysicsName;
        public Int32 Prefix;
        public Int16 RaceRegenRate;
        public Int16 RaceShieldFactor;
        public Int16 RaceShieldRegenerate;
        public Byte RenderType;
        public Int32 RequiredClass;
        public Int16 RequiredCombat;
        public Int16 RequiredLevel;
        public Int16 RequiredPerception;
        public Int16 RequiredTech;
        public Int16 RequiredTheory;
        public Single Scale;
        public Int32 Skill1;
        public Int32 Skill2;
        public Int32 Skill3;
        public Int32 SkillGroup1;
        public Int32 SkillGroup2;
        public Int32 SkillGroup3;
        public UInt16 StackSize;
        public Int16 SubType;

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
                Ingredients = br.Read<Int32>(5),
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
