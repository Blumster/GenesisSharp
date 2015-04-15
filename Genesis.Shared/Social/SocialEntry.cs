using System;
using System.Data;

namespace Genesis.Shared.Social
{
    using Constant;
    using Utils.Extensions;

    public class SocialEntry
    {
        public Int64 Character { get; set; }
        public Int64 OtherCharacter { get; set; }
        public Int32 Level { get; set; }
        public UInt32 LastContinentId { get; set; }
        public Byte Class { get; set; }
        public Boolean Online { get; set; }
        public String Name { get; set; }
        public SocialType Type { get; set; }

        public static SocialEntry Read(Int64 character, IDataReader reader, SocialType type)
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
