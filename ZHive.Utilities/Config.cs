/***********************************************************
 * RUSSIAN MAFIA TEAM : vk.com/skynetdz | www.dayzmafia.ru
 * ZHive (DAYZ SA)
 * Copyright: 2015-2017 Pavel Kirikov
 ***********************************************************/

using System;
using System.Configuration;

namespace ZHive.Utilities
{
    public class Config
    {
        public static string AppSettings(string value)
        {
            return ConfigurationManager.AppSettings[value];
        }

        public static string ConnString(string value)
        {
            return ConfigurationManager.ConnectionStrings[value].ConnectionString;
        }
    }
}
