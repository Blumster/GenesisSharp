using System;
using System.IO;

namespace Genesis.Shared.Skill
{
    public struct SkillElement
    {
        public Int32 ElementType;
        public Byte EquationType;
        public Int32 SkillId;
        public Single ValueBase;
        public Single ValuePerLevel;

        public static SkillElement Read(BinaryReader br)
        {
            var se = new SkillElement
            {
                SkillId = br.ReadInt32(),
                ElementType = br.ReadInt32(),
                EquationType = br.ReadByte()
            };

            br.ReadBytes(3);

            se.ValueBase = br.ReadSingle();
            se.ValuePerLevel = br.ReadSingle();

            return se;
        }
    }
}
