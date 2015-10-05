using System.IO;

namespace Genesis.Shared.Prefix
{
    public class PrefixRaceItem : PrefixBase
    {
        public short HazardCountBonus;
        public float HazardCountBonusf;
        public short HazardSecondsBonus;
        public float HazardSecondsBonusf;

        public PrefixRaceItem(BinaryReader br)
            : base(br)
        {
            HazardCountBonus = br.ReadInt16();

            br.ReadBytes(2);

            HazardCountBonusf = br.ReadSingle();
            HazardSecondsBonus = br.ReadInt16();

            br.ReadBytes(2);

            HazardSecondsBonusf = br.ReadSingle();
        }
    }
}
