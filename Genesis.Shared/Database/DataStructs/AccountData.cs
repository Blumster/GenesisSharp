using System.Data;

namespace Genesis.Shared.Database.DataStructs
{
    using Utils.Extensions;

    public class AccountData
    {
        public ulong Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public uint OneTimeKey { get; set; }
        public byte Level { get; set; }
        public uint[] FirstTimeFlags { get; set; }

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
