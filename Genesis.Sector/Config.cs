using System;
using System.Configuration;

namespace Genesis.Sector
{
    public static class Config
    {
        public static void Initialize()
        {
            IsDebugMode = ConfigurationManager.AppSettings["IsDebugMode"] != "false";
            DBConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
            ServerId = Byte.Parse(ConfigurationManager.AppSettings["ServerID"]);
            AssetPath = ConfigurationManager.AppSettings["AssetPath"];
        }

        public static Boolean IsDebugMode { get; private set; }
        public static Byte ServerId { get; private set; }
        public static String DBConnectionString { get; private set; }
        public static String AssetPath { get; private set; }
    }
}
