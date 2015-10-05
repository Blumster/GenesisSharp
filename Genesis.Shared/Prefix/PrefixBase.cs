using System.IO;

namespace Genesis.Shared.Prefix
{
    using Utils.Extensions;

    public class PrefixBase
    {
        public float AttributeRequirementIncrease;
        public int BaseValue;
        public int Class;
        public int Complexity;
        public uint Id;
        public int[] Ingredients;
        public int IsComponent;
        public int IsGadgetOnly;
        public int IsPrefix;
        public short ItemRarity;
        public short LevelOffset;
        public float MassPercent;
        public string Name;
        public int ObjectType;
        public string PrefixName;
        public int Race;
        public float Rarity;
        public short RequiredCombat;
        public short RequiredPerception;
        public short RequiredTech;
        public short RequiredTheory;
        public int Skill;
        public float ValuePercent;

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
            Ingredients = br.Read<int>(5);
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
