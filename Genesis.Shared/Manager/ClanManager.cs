using System;

using TNL.NET.Entities;

namespace Genesis.Shared.Manager
{
    using Constant;
    using TNL;

    public static class ClanManager
    {
        public static void RequestInfo(TNLConnection conn)
        {
            var resp = new Packet(Opcode.RequestClanInfoResponse); // todo: No response, if the character has no clan?

            resp.WriteInteger(-1); // Clan Id
            resp.WriteUtf8StringOn("", 51); // Clan Name
            resp.WriteUtf8StringOn("", 251); // Clan Motd
            resp.WriteUtf8StringOn("", 51); // Rank One
            resp.WriteUtf8StringOn("", 51); // Rank Two
            resp.WriteUtf8StringOn("", 51); // Rank Three
            resp.WritePadding(1);
            resp.WriteInteger(-1); // Monthly Dues
            resp.WriteInteger(-1); // Monthly Upkeep
            resp.WriteLong(-1L); // Clan Owner
            resp.WriteInteger(0); // Num members
            resp.WritePadding(4);

            for (var i = 0; i < 0; ++i)
            {
                resp.WriteLong(-1L); // Member Coid
                resp.WriteUtf8StringOn("", 17); // Character Name
                resp.WritePadding(3);
                resp.WriteInteger(0); // Continent Id
                resp.WriteInteger(0); // Xp
                resp.WriteInteger(0); // Clan Rank
                resp.WriteLong(0); // Last Paid Dues
                resp.WriteInteger(-1); // Cbid
                resp.WriteBoolean(false); // Online
                resp.WritePadding(3);
                resp.WriteLong(0); // Last Online
                resp.WriteLong(0); // Join Date
            }

            conn.SendPacket(resp, RPCGuaranteeType.RPCGuaranteedOrdered);
        }
    }
}
