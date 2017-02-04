/***********************************************************
 * RUSSIAN MAFIA TEAM : vk.com/skynetdz | www.dayzmafia.ru
 * ZHive (DAYZ SA)
 * Copyright: 2015-2017 Pavel Kirikov
 ***********************************************************/

using System;

namespace ZHive.NET.EventHandlers
{
    public delegate void DeleteObjectEventHandler(DeleteObjectEventArgs args);

    public class DeleteObjectEventArgs : EventArgs
    {
        public string ObjectId { get; private set; }

        public DeleteObjectEventArgs(string objectId)
        {
            this.ObjectId = objectId;
        }
    }
}
