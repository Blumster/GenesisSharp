using System;
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
            ClientListenPort = UInt16.Parse(ConfigurationManager.AppSettings["Port"]);
            BlowfishKey = Helper.GetByteArrayFromString(ConfigurationManager.AppSettings["BlowfishKey"]);
            DesKey = Helper.GetByteArrayFromString(ConfigurationManager.AppSettings["DESKey"]);

            IsDebugMode = ConfigurationManager.AppSettings["IsDebugMode"].ToLower() != "false";

            DBConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
        }

        public static IPAddress ClientListenAddr { get; private set; }
        public static UInt16 ClientListenPort { get; private set; }
        public static Boolean IsDebugMode { get; private set; }
        public static Byte MaxServerCount { get; private set; }
        public static String DBConnectionString { get; private set; }
        public static Byte[] BlowfishKey { get; private set; }
        public static Byte[] DesKey { get; private set; }
    }
}
