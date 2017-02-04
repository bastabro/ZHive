/***********************************************************
 * RUSSIAN MAFIA TEAM : vk.com/skynetdz | www.dayzmafia.ru
 * ZHive (DAYZ SA)
 * Copyright: 2015-2017 Pavel Kirikov
 ***********************************************************/

using System;
using System.Net.Http;

namespace ZHive.WebAPI.EventHandlers
{
    public delegate void ErrorEventHandelr(ErrorEventArgs args);

    public class ErrorEventArgs : EventArgs
    {
        public string PlayerUID { get; private set; }
        public HttpRequestMessage HttpRequestMessage { get; private set; }
        public Exception Exception { get; private set; }

        public ErrorEventArgs(string uid, HttpRequestMessage request, Exception exception)
        {
            this.PlayerUID = uid;
            this.HttpRequestMessage = request;
            this.Exception = exception;
        }
    }
}
