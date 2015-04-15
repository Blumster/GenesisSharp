using System;
using System.IO;

namespace Genesis.Shared.Structures.Specifics
{
    public struct CommoditySpecific
    {
        public Int32 CommodityGroupType;
        public Single DropChance;
        public Int32 Group;
        public Byte MaterialDifficulty;
        public Int32 MaxLevel;
        public Int32 MinLevel;
        public Byte Purity;
        public Byte PurityFrom;
        public Int32 RefineTarget;
        public Int32 RefinesFrom;
        public Int32 Value;

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
