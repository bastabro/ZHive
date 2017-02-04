/***********************************************************
 * RUSSIAN MAFIA TEAM : vk.com/skynetdz | www.dayzmafia.ru
 * ZHive (DAYZ SA)
 * Copyright: 2015-2017 Pavel Kirikov
 ***********************************************************/

using System;

namespace ZHive.NET.EventHandlers
{
    public delegate void MessageEventHandelr(MessageEventArgs args);

    public class MessageEventArgs
    {
        public string Message { get; set; }

        public MessageEventArgs(string message)
        {
            this.Message = message;
        }
    }
}
