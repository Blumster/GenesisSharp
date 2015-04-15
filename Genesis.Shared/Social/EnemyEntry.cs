using System;

namespace Genesis.Shared.Social
{
    public class EnemyEntry : SocialEntry
    {
        public Int32 TimesKilled { get; set; }
        public Int32 TimesKilledBy { get; set; }
        public Byte Race { get; set; }
    }
}
