using System.Configuration;

namespace Genesis.Sector
{
    public static class Config
    {
        public static void Initialize()
        {
            IsDebugMode = ConfigurationManager.AppSettings["IsDebugMode"] != "false";
            DBConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
            ServerId = byte.Parse(ConfigurationManager.AppSettings["ServerID"]);
            AssetPath = ConfigurationManager.AppSettings["AssetPath"];
        }

        public static bool IsDebugMode { get; private set; }
        public static byte ServerId { get; private set; }
        public static string DBConnectionString { get; private set; }
        public static string AssetPath { get; private set; }
    }
}
