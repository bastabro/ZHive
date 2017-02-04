﻿/***********************************************************
 * RUSSIAN MAFIA TEAM : vk.com/skynetdz | www.dayzmafia.ru
 * ZHive (DAYZ SA)
 * Copyright: 2015-2017 Pavel Kirikov
 ***********************************************************/

using System;
using System.Net.Http;
using ZHive.DAL.Entities;

namespace ZHive.WebAPI.EventHandlers
{
    public delegate void LoadPlayerEventHandler(LoadPlayerEventArgs args);

    public class LoadPlayerEventArgs : EventArgs
    {
        public PlayerDb Player { get; private set; }
        public HttpRequestMessage HttpRequestMessage { get; private set; }

        public LoadPlayerEventArgs(PlayerDb player, HttpRequestMessage request)
        {
            this.Player = player;
            this.HttpRequestMessage = request;
        }
    }
}
