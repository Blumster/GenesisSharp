using System;

namespace Genesis.Shared.Structures
{
    public struct WeatherInfo
    {
        public Byte EventTimesPerMinute;
        public String FxName;
        public UInt32 LayerBits;
        public UInt32 MaxTimeToLive;
        public UInt32 MinTimeToLive;
        public Single PercentChance;
        public Int32 SpecialEventSkill;
        public UInt32 SpecialType;
        public UInt32 Type;
    }
}
