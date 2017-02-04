/***********************************************************
 * RUSSIAN MAFIA TEAM : vk.com/skynetdz | www.dayzmafia.ru
 * ZHive (DAYZ SA)
 * Copyright: 2015-2017 Pavel Kirikov
 ***********************************************************/

using System;
using System.Globalization;

namespace ZHive.Utilities
{
    public class zConsole
    {
        private static string DateFormat
        {
            get { return DateTime.Now.ToString("dd.MM HH:mm"); }
        }

        public static void WriteMessage(string message, ConsoleColor cColor = ConsoleColor.Gray)
        {
            Console.ForegroundColor = cColor;
            Console.WriteLine("[{0}] {1}", DateFormat, message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void WriteMessage(string format, ConsoleColor cColor, params object[] args)
        {
            Console.ForegroundColor = cColor;
            Console.WriteLine("[{0}] {1}", DateFormat, string.Format(format, args));
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void WriteMessage(string format, params object[] args)
        {
            WriteMessage(format, ConsoleColor.Gray, args);
        }
    }
}
