namespace Genesis.Shared.Social
{
    public class EnemyEntry : SocialEntry
    {
        public int TimesKilled { get; set; }
        public int TimesKilledBy { get; set; }
        public byte Race { get; set; }
    }
}
