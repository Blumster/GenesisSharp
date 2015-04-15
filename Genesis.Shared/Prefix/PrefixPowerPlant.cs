using System;
using System.IO;

namespace Genesis.Shared.Prefix
{
    public class PrefixPowerPlant : PrefixBase
    {
        public Single CoolDownPercent;
        public Int32 CoolingRateAdjust;
        public Single CoolingRatePercent;
        public Int32 HeadAdjust;
        public Single HeatPercent;
        public Int32 PowerAdjust;
        public Single PowerPercent;
        public Int32 PowerRegenRateAdjust;
        public Single PowerRegenRatePercent;

        public PrefixPowerPlant(BinaryReader br)
            : base(br)
        {
            HeatPercent = br.ReadSingle();
            HeadAdjust = br.ReadInt32();
            PowerPercent = br.ReadSingle();
            PowerAdjust = br.ReadInt32();
            CoolingRatePercent = br.ReadSingle();
            CoolingRateAdjust = br.ReadInt32();
            PowerRegenRatePercent = br.ReadSingle();
            PowerRegenRateAdjust = br.ReadInt32();
            CoolDownPercent = br.ReadSingle();
        }
    }
}
