using System.Collections.Generic;
using System.IO;

namespace Genesis.Shared.Skill
{
    using Utils.Extensions;

    public struct Skill
    {
        public int AffectedObjectType;
        public int AffectedSubType;
        public int AffectedTarget;
        public int CategoryId;
        public int Class;
        public string Description;
        public List<SkillElement> Elements;
        public int GroupId;
        public uint Id;
        public int IsChain;
        public int IsSpray;
        public byte LocationLine;
        public byte LocationTree;
        public byte MaxSkillLevel;
        public byte MinimumLevel;
        public string Name;
        public short NumOfElements;
        public byte OptionalAction;
        public int Race;
        public int SkillOptional1;
        public int SkillOptional2;
        public int SkillOptional3;
        public int SkillOptional4;
        public int SkillPrerequisite1;
        public int SkillPrerequisite2;
        public int SkillPrerequisite3;
        public byte SkillType;
        public int StatusEffect;
        public int SummonedCreatureId;
        public int TargetObjectType;
        public int TargetSubType;
        public int TargetType;
        public int UseBodyForArc;
        public string XMLName;

        public static Skill Read(BinaryReader br)
        {
            var s = new Skill
            {
                Id = br.ReadUInt32(),
                Class = br.ReadInt32(),
                Race = br.ReadInt32(),
                TargetType = br.ReadInt32(),
                TargetSubType = br.ReadInt32(),
                TargetObjectType = br.ReadInt32(),
                AffectedTarget = br.ReadInt32(),
                AffectedSubType = br.ReadInt32(),
                AffectedObjectType = br.ReadInt32(),
                StatusEffect = br.ReadInt32(),
                SkillPrerequisite1 = br.ReadInt32(),
                SkillPrerequisite2 = br.ReadInt32(),
                SkillPrerequisite3 = br.ReadInt32(),
                LocationTree = br.ReadByte(),
                LocationLine = br.ReadByte(),
                MinimumLevel = br.ReadByte(),
                SkillType = br.ReadByte(),
                Name = br.ReadUnicodeString(33),
                Description = br.ReadUnicodeString(1025),
                XMLName = br.ReadUnicodeString(65)
            };

            br.ReadBytes(2);

            s.IsChain = br.ReadInt32();
            s.IsSpray = br.ReadInt32();
            s.OptionalAction = br.ReadByte();
            s.MaxSkillLevel = br.ReadByte();

            br.ReadBytes(2);

            s.UseBodyForArc = br.ReadInt32();
            s.GroupId = br.ReadInt32();
            s.CategoryId = br.ReadInt32();
            s.SummonedCreatureId = br.ReadInt32();
            s.SkillOptional1 = br.ReadInt32();
            s.SkillOptional2 = br.ReadInt32();
            s.SkillOptional3 = br.ReadInt32();
            s.SkillOptional4 = br.ReadInt32();
            s.NumOfElements = br.ReadInt16();

            br.ReadBytes(2);

            s.Elements = s.NumOfElements > 0 ? new List<SkillElement>(s.NumOfElements) : new List<SkillElement>(0);

            for (var i = 0; i < s.NumOfElements; ++i)
                s.Elements.Add(SkillElement.Read(br));

            return s;
        }
    }
}
