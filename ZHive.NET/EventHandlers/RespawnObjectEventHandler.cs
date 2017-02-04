/***********************************************************
 * RUSSIAN MAFIA TEAM : vk.com/skynetdz | www.dayzmafia.ru
 * ZHive (DAYZ SA)
 * Copyright: 2015-2017 Pavel Kirikov
 ***********************************************************/

using System;

namespace ZHive.NET.EventHandlers
{
    public delegate void RespawnObjectEventHandler(RespawnObjectEventArgs args);

    public class RespawnObjectEventArgs : EventArgs
    {
        public string Result { get; private set; }

        public RespawnObjectEventArgs(string result)
        {
            this.Result = result;
        }
    }
}
