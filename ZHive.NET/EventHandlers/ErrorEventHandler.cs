/***********************************************************
 * RUSSIAN MAFIA TEAM : vk.com/skynetdz | www.dayzmafia.ru
 * ZHive (DAYZ SA)
 * Copyright: 2015-2017 Pavel Kirikov
 ***********************************************************/

using System;

namespace ZHive.NET.EventHandlers
{
    public delegate void ErrorEventHandelr(ErrorEventArgs args);

    public class ErrorEventArgs
    {
        public string ObjectId { get; private set; }
        public Exception Exception { get; private set; }

        public ErrorEventArgs(string id, Exception exception)
        {
            this.ObjectId = id;
            this.Exception = exception;
        }
    }
}
