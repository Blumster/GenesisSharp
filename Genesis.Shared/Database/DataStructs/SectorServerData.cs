using System.Data;
using System.Net;

namespace Genesis.Shared.Database.DataStructs
{
    using Utils.Extensions;

    public class SectorServerData
    {
        public byte Id { get; set; }
        public IPAddress Address { get; set; }
        public ushort Port { get; set; }
        public ushort CurrentPlayers { get; set; }
        public byte Status { get; set; }

        public static SectorServerData Read(IDataReader reader)
        {
            if (reader.Read())
            {
                return new SectorServerData
                {
                    Id             = reader.GetByte(0),
                    Address        = IPAddress.Parse(reader.GetString(1)),
                    Port           = reader.GetUInt16(2),
                    CurrentPlayers = reader.GetUInt16(3),
                    Status         = reader.GetByte(4),
                };
            }

            return null;
        }
    }
}
