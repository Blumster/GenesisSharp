using System;
using System.Diagnostics;
using System.Threading;

using UniversalAuth.Encryption;
using UniversalAuth.Network.Packet.Client;

namespace Genesis.Auth
{
    using Shared.Database;
    using Shared.Database.MySql;
    using Utils;

    public class Program : ExitableProgram
    {
        private static Server _server;

        public static void Main()
        {
            Initialize(Handler);

            Console.Title = "Genesis Sharp - Auth Server";

            WriteLogo();

            Config.Load();
            CryptEngine.Initialize(Config.BlowfishKey);
            LoginPacket.InitializeCryptoService(Config.DesKey);

            DataAccess.Initialize(new MySqlDatabase());
            DataAccess.DatabaseAccess.Initialize(Config.DBConnectionString);

            try
            {
                _server = new Server();
                Logger.WriteLog("*** Initialized Auth Server...", LogType.Initialize);

                _server.Start(Config.ClientListenAddr, Config.ClientListenPort);
                Logger.WriteLog("*** Listening for clients on port " + Config.ClientListenPort, LogType.Network);
            }
            catch (Exception e)
            {
                Logger.WriteLog("Unable to start the server! Exception: {0}", LogType.Error, e);
                Thread.Sleep(5000);
                Environment.Exit(1);
            }

            GC.Collect();

            Process.GetCurrentProcess().WaitForExit();
        }

        private static bool Handler(byte sig)
        {
            Logger.WriteLog("Shutting down the server...", LogType.None);

            GlobalServerHandler.Shutdown();

            _server?.Stop();
            return false;
        }
    }
}
