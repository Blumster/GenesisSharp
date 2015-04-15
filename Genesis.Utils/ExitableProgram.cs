using System;
using System.Runtime.InteropServices;

namespace Genesis.Utils
{
    public delegate Boolean ExitEventHandler(Byte sig);

    public abstract class ExitableProgram
    {
        protected static ExitEventHandler ExitHandler { get; set; }

        protected static void Initialize(ExitEventHandler handler)
        {
            ExitHandler += handler;
            NativeMethods.SetConsoleCtrlHandler(ExitHandler, true);
        }

        protected static void WriteLogo()
        {
            Logger.WriteLog(@"    ______    ______    ______    ", LogType.None);
            Logger.WriteLog(@"   /      \  /      \  /      \   ", LogType.None);
            Logger.WriteLog(@"  |  $$$$$$\|  $$$$$$\|  $$$$$$\  ", LogType.None);
            Logger.WriteLog(@"  | $$__| $$| $$__| $$| $$___\$$  ", LogType.None);
            Logger.WriteLog(@"  | $$    $$| $$    $$ \$$    \   ", LogType.None);
            Logger.WriteLog(@"  | $$$$$$$$| $$$$$$$$ _\$$$$$$\  ", LogType.None);
            Logger.WriteLog(@"  | $$  | $$| $$  | $$|  \__| $$  ", LogType.None);
            Logger.WriteLog(@"  | $$  | $$| $$  | $$ \$$    $$  ", LogType.None);
            Logger.WriteLog(@"   \$$   \$$ \$$   \$$  \$$$$$$   ", LogType.None);
            Logger.WriteLog(@"Auto Assault Server - GenesisSharp", LogType.None);
            Logger.WriteLog("", LogType.None);
        }
    }

    internal class NativeMethods
    {
        [DllImport("Kernel32")]
        public static extern Boolean SetConsoleCtrlHandler(ExitEventHandler handler, Boolean add);
    }
}
