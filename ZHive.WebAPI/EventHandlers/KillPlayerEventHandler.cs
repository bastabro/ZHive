/***********************************************************
 * RUSSIAN MAFIA TEAM : vk.com/skynetdz | www.dayzmafia.ru
 * ZHive (DAYZ SA)
 * Copyright: 2015-2017 Pavel Kirikov
 ***********************************************************/

using System;
using System.Net.Http;
using ZHive.DAL.Entities;

namespace ZHive.WebAPI.EventHandlers
{
    public delegate void KillPlayerEventHandler(KillPlayerEventArgs args);

    public class KillPlayerEventArgs : EventArgs
    {
        public string PlayerID { get; private set; }
        public HttpRequestMessage HttpRequestMessage { get; private set; }

        public KillPlayerEventArgs(string uid, HttpRequestMessage request)
        {
            this.PlayerID = uid;
            this.HttpRequestMessage = request;
        }
    }
}
