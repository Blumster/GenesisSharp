using System.IO;

namespace Genesis.Shared.Structures
{
    public struct OutpostSkill
    {
        public byte Layer;
        public float RequiredBeaconPercantage;
        public uint SkillId;
        public uint SkillLevel;

        public static OutpostSkill Read(BinaryReader br)
        {
            var os = new OutpostSkill
            {
                SkillId = br.ReadUInt32(),
                SkillLevel = br.ReadUInt32(),
                RequiredBeaconPercantage = br.ReadSingle(),
                Layer = br.ReadByte()
            };

            br.ReadBytes(3);

            return os;
        }
    }
}
