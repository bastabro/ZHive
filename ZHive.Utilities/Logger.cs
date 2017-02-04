/***********************************************************
 * RUSSIAN MAFIA TEAM : vk.com/skynetdz | www.dayzmafia.ru
 * ZHive (DAYZ SA)
 * Copyright: 2015-2017 Pavel Kirikov
 ***********************************************************/

using System;
using System.IO;

namespace ZHive.Utilities
{
    public class Logger
    {
        private static string DateFormat
        {
            get { return DateTime.Now.ToString("dd.MM HH:mm"); }
        }

        public static void WriteMessage(string message)
        {
            try
            {
                using (FileStream fs = new FileStream("ZHive.log", FileMode.Append, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine("[{0}] {1}", DateFormat, message);
                    sw.WriteLine("-------------------------------------------------------------------------");
                }
            }
            catch { }
        }

        public static void WriteError(string message)
        {
            try
            {
                using (var fs = new FileStream("ZHive.log", FileMode.Append, FileAccess.Write))
                using (var sw = new StreamWriter(fs))
                {
                    zConsole.WriteMessage("*** ВНИМАНИЕ ОШИБКА ***", ConsoleColor.Red);
                    sw.WriteLine("[{0}] {1}", DateFormat, message);
                    sw.WriteLine("***********************************************************************");
                }
            }
            catch { }
         }

        public static void WriteMessage(string format, params object[] args)
        {
            WriteMessage(string.Format(format, args));
        }

        public static void WriteError(string format, params object[] args)
        {
            WriteError(string.Format(format, args));
        }
    }
}
