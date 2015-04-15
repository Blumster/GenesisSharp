using System;
using System.IO;

namespace Genesis.Shared.Prefix
{
    public class PrefixRaceItem : PrefixBase
    {
        public Int16 HazardCountBonus;
        public Single HazardCountBonusf;
        public Int16 HazardSecondsBonus;
        public Single HazardSecondsBonusf;

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
