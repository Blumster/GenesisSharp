using System;
using System.Collections.Generic;
using System.Threading;

using UniversalAuth.Data;

namespace Genesis.Auth
{
    using Shared.Database;

    public class GlobalServerHandler
    {
        public static List<ServerInfoEx> GlobalServers { get; private set; }
        public static ReaderWriterLockSlim ListLock { get; private set; }

        public static Timer RefreshTimer { get; private set; }

        static GlobalServerHandler()
        {
            ListLock = new ReaderWriterLockSlim();
            RefreshTimer = new Timer(ReloadServerDatas, null, 0, 10000);
            GlobalServers = new List<ServerInfoEx>();

            ReloadServerDatas(null);
        }

        public static void Shutdown()
        {
            RefreshTimer.Dispose();
        }

        private static void ReloadServerDatas(Object o)
        {
            ListLock.EnterWriteLock();

            GlobalServers = DataAccess.Realmlist.GetGlobalServers();

            ListLock.ExitWriteLock();
        }
    }
}
