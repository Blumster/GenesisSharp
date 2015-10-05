using System.IO;

namespace Genesis.Shared.Prefix
{
    public class PrefixOrnament : PrefixBase
    {
        public short CombatAdjust;
        public float CombatAdjustf;
        public short PerceptionAdjust;
        public float PerceptionAdjustf;
        public short TechAdjust;
        public float TechAdjustf;
        public short TheoryAdjust;
        public float TheoryAdjustf;

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
