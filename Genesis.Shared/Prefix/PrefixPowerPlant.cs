using System.IO;

namespace Genesis.Shared.Prefix
{
    public class PrefixPowerPlant : PrefixBase
    {
        public float CoolDownPercent;
        public int CoolingRateAdjust;
        public float CoolingRatePercent;
        public int HeadAdjust;
        public float HeatPercent;
        public int PowerAdjust;
        public float PowerPercent;
        public int PowerRegenRateAdjust;
        public float PowerRegenRatePercent;

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
