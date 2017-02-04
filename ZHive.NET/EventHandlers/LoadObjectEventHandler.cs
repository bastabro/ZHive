/***********************************************************
 * RUSSIAN MAFIA TEAM : vk.com/skynetdz | www.dayzmafia.ru
 * ZHive (DAYZ SA)
 * Copyright: 2015-2017 Pavel Kirikov
 ***********************************************************/

using System;

namespace ZHive.NET.EventHandlers
{
    public delegate void LoadObjectEventHandler(LoadObjectEventArgs args);

    public class LoadObjectEventArgs : EventArgs
    {
        public string ObjectId { get; private set; }

        public LoadObjectEventArgs(string id)
        {
            this.ObjectId = id;
        }
    }
}
