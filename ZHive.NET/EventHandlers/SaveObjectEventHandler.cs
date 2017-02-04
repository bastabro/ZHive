/***********************************************************
 * RUSSIAN MAFIA TEAM : vk.com/skynetdz | www.dayzmafia.ru
 * ZHive (DAYZ SA)
 * Copyright: 2015-2017 Pavel Kirikov
 ***********************************************************/

using System;
using ZHive.DAL.Entities;

namespace ZHive.NET.EventHandlers
{
    public delegate void SaveObjectEventHandler(SaveObjectEventArgs args);

    public class SaveObjectEventArgs : EventArgs
    {
        public ObjectDb ObjectDb { get; private set; }

        public SaveObjectEventArgs(ObjectDb objectDb)
        {
            this.ObjectDb = objectDb;
        }
    }
}
