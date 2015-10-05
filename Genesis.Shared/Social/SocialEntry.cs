using System.Data;

namespace Genesis.Shared.Social
{
    using Constant;
    using Utils.Extensions;

    public class SocialEntry
    {
        public long Character { get; set; }
        public long OtherCharacter { get; set; }
        public int Level { get; set; }
        public uint LastContinentId { get; set; }
        public byte Class { get; set; }
        public bool Online { get; set; }
        public string Name { get; set; }
        public SocialType Type { get; set; }

        public static SocialEntry Read(long character, IDataReader reader, SocialType type)
        {
            if (!reader.Read())
                return null;

            SocialEntry entry;

            if (type == SocialType.Enemy)
            {
                var eentry = new EnemyEntry
                {
                    Race = reader.GetByte(1)
                };
                //eentry.TimesKilled = reader.GetByte("TimesKilled");
                //eentry.TimesKilledBy = reader.GetByte("TimesKilledBy");

                entry = eentry;
            }
            else
                entry = new SocialEntry();

            entry.Character = character;
            entry.OtherCharacter = reader.GetInt64(0);
            entry.Level = reader.GetByte(3);
            entry.LastContinentId = reader.GetUInt32(4);
            entry.Class = reader.GetByte(2);
            entry.Online = false;
            entry.Name = reader.GetString(5);
            entry.Type = (SocialType)reader.GetByte(6);

            return entry;
        }
    }
}
