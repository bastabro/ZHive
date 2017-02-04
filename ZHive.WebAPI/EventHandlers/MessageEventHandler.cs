/***********************************************************
 * RUSSIAN MAFIA TEAM : vk.com/skynetdz | www.dayzmafia.ru
 * ZHive (DAYZ SA)
 * Copyright: 2015-2017 Pavel Kirikov
 ***********************************************************/

using System;
using ZHive.WebAPI.Enums;

namespace ZHive.WebAPI.EventHandlers
{
    public delegate void MessageEventHandelr(MessageEventArgs args);

    public class MessageEventArgs
    {
        public string Message { get; set; }
        public MessageType MessageType { get; set; }

        public MessageEventArgs(string message, MessageType messageType)
        {
            this.Message = message;
            this.MessageType = messageType;
        }
    }
}
