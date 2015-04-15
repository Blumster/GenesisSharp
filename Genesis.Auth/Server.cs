using System;
using System.Collections.Generic;
using System.Linq;

using UniversalAuth.Data;
using UniversalAuth.Server;

namespace Genesis.Auth
{
    using Shared.Database;
    using Utils;

    public class Server : AuthServer
    {
        public Server()
        {
            OnConnect += c => Logger.WriteLog("*** Client connected from {0}", LogType.Network, c.Socket.RemoteAddress);
            OnDisconnect += c => Logger.WriteLog("*** Client disconnected! Ip: {0}", LogType.Network, c.Socket.RemoteAddress);
        }

        public override Boolean ValidateServer(Client c, Byte serverId)
        {
            return GlobalServerHandler.GlobalServers.Any(s => s.ServerId == serverId && s.Status == 1);
        }

        public override Boolean ValidateLogin(Client c, String user, String password, UInt32 subscription, UInt16 cdkey)
        {
            var data = DataAccess.Account.LoginAccount(user, password);
            if (data != null)
                DataAccess.Account.UpdateAccountValues(data.Id, c.OneTimeKey, c.SessionId1, c.SessionId2, c.Socket.RemoteAddress);

            return data != null;
        }

        public override Boolean GetServerInfos(Client c, out List<ServerInfoEx> servers)
        {
            servers = GlobalServerHandler.GlobalServers;

            return true;
        }
    }
}
