using System.IO;

namespace Genesis.Shared.Skill
{
    public struct SkillElement
    {
        public int ElementType;
        public byte EquationType;
        public int SkillId;
        public float ValueBase;
        public float ValuePerLevel;

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
