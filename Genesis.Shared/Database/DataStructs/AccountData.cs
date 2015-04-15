using System;
using System.Data;

namespace Genesis.Shared.Database.DataStructs
{
    using Utils.Extensions;

    public class AccountData
    {
        public UInt64 Id { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
        public UInt32 OneTimeKey { get; set; }
        public Byte Level { get; set; }
        public UInt32[] FirstTimeFlags { get; set; }

        public static AccountData Read(IDataReader reader)
        {
            if (!reader.Read())
                return null;

            return new AccountData
            {
                Id             = reader.GetUInt64(0),
                UserName       = reader.GetString(1),
                Password       = reader.GetString(2),
                OneTimeKey     = reader.GetUInt32(3),
                Level          = reader.GetByte(4),
                FirstTimeFlags = new[] { reader.GetUInt32(5), reader.GetUInt32(6), reader.GetUInt32(7), reader.GetUInt32(8) }
            };
        }
    }
}
