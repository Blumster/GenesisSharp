using System.IO;

namespace Genesis.Shared.Prefix
{
    using Structures;

    public class PrefixArmor : PrefixBase
    {
        public short ArmorFactorAdjust;
        public float ArmorFactorPercent;
        public DamageArray ResistAdjust;

        public PrefixArmor(BinaryReader br)
            : base(br)
        {
            ArmorFactorPercent = br.ReadSingle();
            ArmorFactorAdjust = br.ReadInt16();
            ResistAdjust = DamageArray.Read(br);

            br.ReadBytes(2);
        }
    }
}
