using System;
using System.Data;
using System.Net;

using UniversalAuth.Data;

namespace Genesis.Utils
{
    using Extensions;

    public static class Helper
    {
        public static Byte[] GetByteArrayFromString(String val)
        {
            var split = val.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var key = new Byte[split.Length];

            for (var i = 0; i < split.Length; ++i)
                key[i] = Byte.Parse(split[i]);

            return key;
        }

        public static ServerInfoEx ReadServerInfoEx(this IDataReader reader)
        {
            if (reader.Read())
            {
                return new ServerInfoEx
                {
                    ServerId = reader.GetByte(0),
                    Ip = IPAddress.Parse(reader.GetString(1)),
                    Port = reader.GetUInt16(2),
                    AgeLimit = reader.GetByte(3),
                    PKFlag = reader.GetByte(4),
                    CurrentPlayers = reader.GetUInt16(5),
                    MaxPlayers = reader.GetUInt16(6),
                    Status = reader.GetByte(7)
                };
            }

            return null;
        }
    }
}
