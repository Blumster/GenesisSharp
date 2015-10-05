using System.IO;

namespace Genesis.Shared.Structures.Specifics
{
    public struct CommoditySpecific
    {
        public int CommodityGroupType;
        public float DropChance;
        public int Group;
        public byte MaterialDifficulty;
        public int MaxLevel;
        public int MinLevel;
        public byte Purity;
        public byte PurityFrom;
        public int RefineTarget;
        public int RefinesFrom;
        public int Value;

        public static CommoditySpecific Read(BinaryReader br)
        {
            var cs = new CommoditySpecific
            {
                RefineTarget = br.ReadInt32(),
                Value = br.ReadInt32(),
                MaterialDifficulty = br.ReadByte(),
                Purity = br.ReadByte(),
            };

            br.ReadInt16();

            cs.Group = br.ReadInt32();
            cs.RefinesFrom = br.ReadInt32();
            cs.PurityFrom = br.ReadByte();

            br.ReadBytes(3);

            cs.CommodityGroupType = br.ReadInt32();
            cs.MinLevel = br.ReadInt32();
            cs.MaxLevel = br.ReadInt32();
            cs.DropChance = br.ReadSingle();

            return cs;
        }
    }
}
