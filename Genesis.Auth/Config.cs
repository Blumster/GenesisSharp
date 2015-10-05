using System.Configuration;
using System.Net;

namespace Genesis.Auth
{
    using Utils;

    public static class Config
    {
        public static void Load()
        {
            ClientListenAddr = IPAddress.Parse(ConfigurationManager.AppSettings["ListenIP"]);
            ClientListenPort = ushort.Parse(ConfigurationManager.AppSettings["Port"]);
            BlowfishKey = Helper.GetByteArrayFromString(ConfigurationManager.AppSettings["BlowfishKey"]);
            DesKey = Helper.GetByteArrayFromString(ConfigurationManager.AppSettings["DESKey"]);

            IsDebugMode = ConfigurationManager.AppSettings["IsDebugMode"].ToLower() != "false";

            DBConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
        }

        public static IPAddress ClientListenAddr { get; private set; }
        public static ushort ClientListenPort { get; private set; }
        public static bool IsDebugMode { get; private set; }
        public static byte MaxServerCount { get; private set; }
        public static string DBConnectionString { get; private set; }
        public static byte[] BlowfishKey { get; private set; }
        public static byte[] DesKey { get; private set; }
    }
}
