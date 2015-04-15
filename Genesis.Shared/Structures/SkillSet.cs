using System;
using System.IO;

namespace Genesis.Shared.Structures
{
    public struct SkillSet
    {
        public Byte AnimationId;
        public Int32 MaxHealth;
        public UInt16 MinCastTime;
        public Int32 MinHealth;
        public UInt16 PauseTime;
        public Int32 SkillId;
        public UInt16 SkillLevel;
        public Boolean StopsToAttack;
        public Single Weight;

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
            return String.Format("Id: {0}", SkillId);
        }
    }
}
