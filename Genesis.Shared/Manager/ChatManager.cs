namespace Genesis.Shared.Manager
{
    using Constant;
    using TNL;
    using Utils;

    public static class ChatManager
    {
        public static void HandleChat(TNLConnection conn, Packet packet)
        {
            var type = (ChatType) packet.ReadUInteger();
            var isGm = packet.ReadBoolean();
            var recipient = packet.ReadUtf8StringOn(17);
            var sender = packet.ReadUtf8StringOn(17);
            packet.ReadPadding(1);
            var msglen = packet.ReadUShort();

            var msg = packet.ReadUtf8StringOn(msglen);
            if (msg.StartsWith("/"))
            {
                if (msg.Equals("/save"))
                    conn.CurrentCharacter.SaveToDB();

                return;
            }

            var pack = ConstructChatPacket(type, isGm, recipient, sender, msg);

            switch (type)
            {
                default:
                    Logger.WriteLog("Unhandled ChatType in HandleChat: {0}", LogType.Error, type);
                    break;
            }
        }

        public static void HandleBroadcast(TNLConnection conn, Packet packet)
        {
            var type = (ChatType) packet.ReadUInteger();
            var sendercoid = packet.ReadLong();
            var isGm = packet.ReadBoolean();

            packet.ReadPadding(1);

            var msglen = packet.ReadUShort();
            var sender = packet.ReadUtf8StringOn(17);

            var msg = packet.ReadUtf8StringOn(msglen);
            if (msg.StartsWith("/"))
            {
                if (msg.Equals("/save"))
                    conn.CurrentCharacter.SaveToDB();

                return;
            }

            conn.CurrentCharacter.GetMap().BroadcastChat(type, ConstructBroadcastPacket(type, isGm, sender, sendercoid, msg), conn.CurrentCharacter);
        }

        private static Packet ConstructChatPacket(ChatType type, bool isGm, string recipient, string sender, string msg)
        {
            var msglen = (short) msg.Length;

            var p = new Packet(Opcode.Chat);

            p.WriteInteger((uint) type);
            p.WriteBoolean(isGm);
            p.WriteUtf8StringOn(recipient, 17);
            p.WriteUtf8StringOn(sender, 17);
            p.WritePadding(1).WriteShort(msglen);
            p.WriteUtf8StringOn(msg, msglen);
            p.WriteByte(0);

            return p;
        }

        private static Packet ConstructBroadcastPacket(ChatType type, bool isGm, string sender, long sendercoid, string msg)
        {
            var msglen = (short) msg.Length;

            var p = new Packet(Opcode.Broadcast);
                
            p.WriteInteger((uint) type);
            p.WriteLong(sendercoid);
            p.WriteBoolean(isGm);
            p.WritePadding(1).WriteShort(msglen);
            p.WriteUtf8StringOn(sender, 17);
            p.WriteUtf8StringOn(msg, msglen);
            p.WriteByte(0);

            return p;
        }
    }
}
