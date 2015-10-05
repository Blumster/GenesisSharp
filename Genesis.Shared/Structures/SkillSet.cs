using System.IO;

namespace Genesis.Shared.Structures
{
    public struct SkillSet
    {
        public byte AnimationId;
        public int MaxHealth;
        public ushort MinCastTime;
        public int MinHealth;
        public ushort PauseTime;
        public int SkillId;
        public ushort SkillLevel;
        public bool StopsToAttack;
        public float Weight;

        public static SkillSet Read(BinaryReader br)
        {
            return new SkillSet
            {
                SkillId = br.ReadInt32(),
                PauseTime = br.ReadUInt16(),
                MinCastTime = br.ReadUInt16(),
                SkillLevel = br.ReadUInt16(),
                StopsToAttack = br.ReadBoolean(),
                AnimationId = br.ReadByte(),
                MinHealth = br.ReadInt32(),
                MaxHealth = br.ReadInt32(),
                Weight = br.ReadSingle()
            };
        }

        public override string ToString()
        {
            return $"Id: {SkillId}";
        }
    }
}
