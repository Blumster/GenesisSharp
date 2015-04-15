using System;
using System.IO;

namespace Genesis.Shared.Prefix
{
    using Utils.Extensions;

    public class PrefixBase
    {
        public Single AttributeRequirementIncrease;
        public Int32 BaseValue;
        public Int32 Class;
        public Int32 Complexity;
        public UInt32 Id;
        public Int32[] Ingredients;
        public Int32 IsComponent;
        public Int32 IsGadgetOnly;
        public Int32 IsPrefix;
        public Int16 ItemRarity;
        public Int16 LevelOffset;
        public Single MassPercent;
        public String Name;
        public Int32 ObjectType;
        public String PrefixName;
        public Int32 Race;
        public Single Rarity;
        public Int16 RequiredCombat;
        public Int16 RequiredPerception;
        public Int16 RequiredTech;
        public Int16 RequiredTheory;
        public Int32 Skill;
        public Single ValuePercent;

        public PrefixBase(BinaryReader br)
        {
            Id = br.ReadUInt32();
            ObjectType = br.ReadInt32();
            ValuePercent = br.ReadSingle();
            IsComponent = br.ReadInt32();
            Rarity = br.ReadSingle();
            Race = br.ReadInt32();
            Class = br.ReadInt32();
            Name = br.ReadUnicodeString(51);

            br.ReadBytes(2);

            MassPercent = br.ReadSingle();
            Skill = br.ReadInt32();
            Ingredients = br.Read<Int32>(5);
            BaseValue = br.ReadInt32();
            IsGadgetOnly = br.ReadInt32();
            LevelOffset = br.ReadInt16();

            br.ReadBytes(2);

            AttributeRequirementIncrease = br.ReadSingle();
            RequiredCombat = br.ReadInt16();
            RequiredPerception = br.ReadInt16();
            RequiredTech = br.ReadInt16();
            RequiredTheory = br.ReadInt16();
            ItemRarity = br.ReadInt16();

            br.ReadBytes(2);

            Complexity = br.ReadInt32();
            IsPrefix = br.ReadInt32();
            PrefixName = br.ReadUnicodeString(33);

            br.ReadBytes(2);
        }
    }
}
