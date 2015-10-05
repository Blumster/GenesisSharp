using System;
using System.Diagnostics;
using System.Threading;

namespace Genesis.Sector
{
    using Utils;

    public class Program : ExitableProgram
    {
        private static SectorServer _server;

        public static void Main(string[] args)
        {
            Initialize(Handler);

            Console.Title = "Genesis Sharp - Sector Server";

            WriteLogo();

            _server = new SectorServer();
            if (!_server.Initialize() || !_server.Start())
            {
                Logger.WriteLog("Unable to start the server!", LogType.Error);
                Thread.Sleep(5000);
                return;
            }

            GC.Collect();

            Process.GetCurrentProcess().WaitForExit();
        }

        private static bool Handler(byte sig)
        {
            Logger.WriteLog("Shutting down the server...", LogType.None);

            Logger.WriteLog("Saving players!", LogType.None);

            _server?.Shutdown();

            Logger.WriteLog("Press any key to exit...", LogType.None);

            return false;
        }
    }
}
