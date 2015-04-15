using System;

using TNL.NET.Entities;

namespace Genesis.Shared.Manager
{
    using Constant;
    using TNL;

    public static class ConvoyManager
    {
        public static void MissionsRequest(TNLConnection conn)
        {
            var resp = new Packet(Opcode.ConvoyMissionsResponse);

            resp.WritePadding(4);
            resp.WriteLong(-1L); // Member coid
            resp.WriteShort(0); // Mission Num
            resp.WritePadding(6);

            for (var i = 0; i < 0; ++i)
                resp.WriteShort(0);

            conn.SendPacket(resp, RPCGuaranteeType.RPCGuaranteedOrdered);
        }
    }
}
