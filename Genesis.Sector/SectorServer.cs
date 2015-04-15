using System;
using System.Threading;

namespace Genesis.Sector
{
    using Shared.Database;
    using Shared.Database.MySql;
    using Shared.Manager;
    using Shared.TNL;
    using Utils;

    public class SectorServer
    {
        public static SectorServer Instance { get; private set; }

        public UInt16 CurrentPlayerCount { get; set; }
        public Byte ServerId { get; set; }
        public UInt32 Port { get; private set; }

        public TNLInterface Interface { get; private set; }

        public Thread ListenThread { get; private set; }

        private readonly Object _interfaceLock = new Object();
        private Timer _updateTimer;

        public SectorServer()
        {
            Logger.WriteLog("+++ Initializing Server for Sector", LogType.Initialize);
        }

        public Boolean Initialize()
        {
            Config.Initialize();

            DataAccess.Initialize(new MySqlDatabase());
            DataAccess.DatabaseAccess.Initialize(Config.DBConnectionString);

            AssetManager.Initialize(Config.AssetPath);
            COIDManager.Intialize(false);

            TNLInterface.RegisterNetClassReps();

            var data = DataAccess.Realmlist.GetSectorServerData(Config.ServerId);
            if (data == null)
            {
                Logger.WriteLog("Invalid server Id, shutting down...", LogType.Error);
                Thread.Sleep(5000);
                return false;
            }

            ServerId = data.Id;
            Port = data.Port;
            CurrentPlayerCount = 0;

            lock (_interfaceLock)
                Interface = new TNLInterface((Int32) Port, false, 175, false);

            Instance = this;

            _updateTimer = new Timer(UpdatePlayers, null, 30000, 30000);
            return true;
        }

        private void UpdatePlayers(Object o)
        {
            DataAccess.Realmlist.UpdateSectorStatus(ServerId, CurrentPlayerCount, 1);
        }

        private void Listen()
        {
            while (true)
            {
                lock (_interfaceLock)
                {
                    if (Interface == null)
                        return;

                    Interface.CheckIncomingPackets();
                    Interface.ProcessConnections();
                    Interface.DoScoping();
                }

                Thread.Sleep(50);
            }
        }

        public Boolean Start()
        {
            try
            {
                DataAccess.Realmlist.UpdateSectorStatus(ServerId, 0, 1);

                ListenThread = new Thread(Listen);
                ListenThread.Start();

                Logger.WriteLog("*** Listening for clients on port {0}", LogType.Network, Port);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Shutdown()
        {
            if (Interface == null)
                return;

            DataAccess.Realmlist.UpdateSectorStatus(ServerId, 0, 0);

            _updateTimer.Dispose();

            lock (_interfaceLock)
            {
                Interface.Close();
                Interface = null;

                ListenThread.Join();
            }

            Logger.WriteLog("+++ Closing Server...", LogType.None);
        }
    }
}
