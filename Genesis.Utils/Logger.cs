using System;
using System.Configuration;

namespace Genesis.Utils
{
    public enum LogType
    {
        Debug,
        AI,
        Network,
        Error,
        Test,
        Initialize,
        None
    }

    public class Logger
    {
        public static void WriteLog(String log, LogType type)
        {
            switch (type)
            {
                case LogType.AI:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("[AI] " + log);
                    break;

                case LogType.Debug:
                    if (ConfigurationManager.AppSettings["IsDebugMode"] == "false")
                        break;

                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("[Debug] " + log);
                    break;

                case LogType.Network:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("[Network] " + log);
                    break;

                case LogType.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[Error] " + log);
                    break;

                case LogType.Test:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("[Test] " + log);
                    break;

                case LogType.Initialize:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("[Initialize] " + log);
                    break;

                default:
                    Console.WriteLine(log);
                    break;
            }
            Console.ResetColor();
        }

        public static void WriteLog(String format, LogType type, params Object[] args)
        {
            WriteLog(String.Format(format, args), type);
        }
    }
}
