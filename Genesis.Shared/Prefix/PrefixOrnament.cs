using System;
using System.IO;

namespace Genesis.Shared.Prefix
{
    public class PrefixOrnament : PrefixBase
    {
        public Int16 CombatAdjust;
        public Single CombatAdjustf;
        public Int16 PerceptionAdjust;
        public Single PerceptionAdjustf;
        public Int16 TechAdjust;
        public Single TechAdjustf;
        public Int16 TheoryAdjust;
        public Single TheoryAdjustf;

        public PrefixOrnament(BinaryReader br)
            : base(br)
        {
            CombatAdjust = br.ReadInt16();
            PerceptionAdjust = br.ReadInt16();
            TheoryAdjust = br.ReadInt16();
            TechAdjust = br.ReadInt16();
            CombatAdjustf = br.ReadSingle();
            PerceptionAdjustf = br.ReadSingle();
            TheoryAdjustf = br.ReadSingle();
            TechAdjustf = br.ReadSingle();
        }
    }
}
