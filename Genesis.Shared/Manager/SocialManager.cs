using System;
using System.Collections.Generic;
using System.Linq;
using TNL.NET.Entities;

namespace Genesis.Shared.Manager
{
    using Constant;
    using Database;
    using Social;
    using TNL;

    public static class SocialManager
    {
        private static readonly Dictionary<Int64, Tuple<DateTime, IList<SocialEntry>>> Cache = new Dictionary<Int64, Tuple<DateTime, IList<SocialEntry>>>();

        private static void CheckCache(Int64 coid)
        {
            if (Cache.ContainsKey(coid) && DateTime.Now - Cache[coid].Item1 > TimeSpan.FromMinutes(5.0D))
                Cache.Remove(coid);

            if (!Cache.ContainsKey(coid))
                Cache.Add(coid, new Tuple<DateTime, IList<SocialEntry>>(DateTime.Now, DataAccess.Social.GetEntries(coid, SocialType.All)));
        }

        public static void GetEnemies(TNLConnection session)
        {
            //return;
            var coid = session.CurrentCharacter.GetCOID();
            CheckCache(coid);

            var packet = new Packet(Opcode.GetEnemiesResponse);
            var t = Cache[coid];

            var enemyEntries = t.Item2.Where(e => e.Type == SocialType.Enemy).ToList();
            var count = enemyEntries.Count;

            packet.WriteInteger(count >= 20 ? 20 : count);

            var j = 0;
            foreach (var se in enemyEntries)
            {
                if (!(se is EnemyEntry)) // should never happen
                    continue;

                packet.WriteLong(se.Character);
                packet.WriteLong(se.OtherCharacter);
                packet.WriteInteger(se.Level);
                packet.WriteInteger(se.LastContinentId);
                packet.WriteInteger((se as EnemyEntry).TimesKilled);
                packet.WriteInteger((se as EnemyEntry).TimesKilledBy);
                packet.WriteByte((se as EnemyEntry).Race);
                packet.WriteByte(se.Class);
                packet.WriteBoolean(se.Online);
                packet.WriteUtf8StringOn(se.Name, 17);
                packet.WritePadding(4);

                if (++j == 20)
                    break;
            }

            for (var i = 0; i < 20 - j; ++i)
                packet.WritePadding(56);

            session.SendPacket(packet, RPCGuaranteeType.RPCGuaranteedOrdered);
        }

        public static void GetFriends(TNLConnection session)
        {
            //return;
            var coid = session.CurrentCharacter.GetCOID();
            CheckCache(coid);

            var packet = new Packet(Opcode.GetFriendsResponse);
            var t = Cache[coid];

            var friendEntries = t.Item2.Where(e => e.Type == SocialType.Friend).ToList();
            var count = friendEntries.Count;

            packet.WriteInteger(count >= 20 ? 20 : count);
            var j = 0;

            foreach (var se in friendEntries)
            {
                packet.WriteLong(se.Character);
                packet.WriteLong(se.OtherCharacter);
                packet.WriteInteger(se.Level);
                packet.WriteInteger(se.LastContinentId);
                packet.WriteByte(se.Class);
                packet.WriteBoolean(se.Online);
                packet.WriteUtf8StringOn(se.Name, 17);
                packet.WritePadding(5);

                if (++j == 20)
                    break;
            }

            for (var i = 0; i < 20 - j; ++i)
                packet.WritePadding(48);

            session.SendPacket(packet, RPCGuaranteeType.RPCGuaranteedOrdered);
        }

        public static void GetIgnored(TNLConnection session)
        {
            //return;
            var coid = session.CurrentCharacter.GetCOID();
            CheckCache(coid);

            var packet = new Packet(Opcode.GetIgnoredResponse);
            var t = Cache[coid];

            var ignoredEntries = t.Item2.Where(e => e.Type == SocialType.Ignore).ToList();
            var count = ignoredEntries.Count;

            packet.WriteInteger(count >= 20 ? 20 : count);

            var j = 0;

            foreach (var se in ignoredEntries)
            {
                packet.WriteLong(se.OtherCharacter);

                if (++j == 20)
                    break;
            }

            for (var i = 0; i < 20 - j; ++i)
                packet.WritePadding(8);

            packet.WriteUtf8StringOn("", 17);
            packet.WritePadding(7);

            session.SendPacket(packet, RPCGuaranteeType.RPCGuaranteedOrdered);
        }

        public static void AddEntry(TNLConnection session, Packet packet, SocialType type)
        {
            packet.ReadPadding(4);
            var coid = packet.ReadLong();
            /*var name = */packet.ReadUtf8StringOn(17);
            packet.ReadPadding(7);

            DataAccess.Social.AddEntry(session.CurrentCharacter.GetCOID(), coid, type);
        }

        public static void RemoveEntry(TNLConnection session, Int64 coid, SocialType type)
        {
            DataAccess.Social.RemoveEntry(session.CurrentCharacter.GetCOID(), coid, type);
        }
    }
}
