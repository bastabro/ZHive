/***********************************************************
 * RUSSIAN MAFIA TEAM : vk.com/skynetdz | www.dayzmafia.ru
 * ZHive (DAYZ SA)
 * Copyright: 2015-2017 Pavel Kirikov
 ***********************************************************/

using System;
using Microsoft.Owin.Hosting;
using ZHive.WebAPI;
using ZHive.WebAPI.EventHandlers;
using ZHive.WebAPI.Controllers;
using ZHive.NET;
using ZHive.Utilities;

namespace ZHive
{
    class Program
    {
        static void Main(string[] args)
        {
            #region < RM Copyright >

            Console.Title = "ZHive [ RUSSIAN MAFIA ]";
            Console.CursorVisible = false;

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine();
            Console.WriteLine("* RUSSIAN MAFIA TEAM : vk.com/skynetdz");
            Console.WriteLine("* ZHive (Dayz SA)");
            Console.WriteLine("* Copyright 2015-2017");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;

            #endregion

            string webAddress = Config.AppSettings("WebAddress");
            string netAddress = Config.AppSettings("NetAddress");
            string netPort = Config.AppSettings("netPort");

            WebApp.Start(webAddress, WebApiConfig.Configuration);

            HiveController.FindPlayerEventHandler += ((e) =>
            {
                zConsole.WriteMessage("Find player: {0}", ConsoleColor.Green, e.Player.ID);
            });

            HiveController.LoadPLayerEventHandler += ((e) =>
            {
                zConsole.WriteMessage("Load player: {0}", ConsoleColor.Green, e.Player.ID);
            });

            HiveController.CreatePlayerEventHandler += ((e) =>
            {
                zConsole.WriteMessage("Create player: {0}", ConsoleColor.Yellow, e.Player.ID);
            });

            HiveController.SavePlayerEventHandler += ((e) =>
            {
                zConsole.WriteMessage("Save player: {0}:", ConsoleColor.Yellow, e.Player.ID);
            });

            HiveController.KillPlayerEventHandler += ((e) =>
            {
                zConsole.WriteMessage("Save player: {0}", ConsoleColor.Red, e.PlayerID);
            });

            HiveController.MessageEventHandelr += ((e) =>
            {
                zConsole.WriteMessage(e.Message);
            });

            HiveController.ErrorEventHandler += ((e) =>
            {
                Logger.WriteError("EX: {0}", e.Exception.ToString());
            });

            var netService = new HiveNetService();
            netService.Address = netAddress;
            netService.Port = int.Parse(netPort);
            netService.Start();

            Console.WriteLine();
            Console.ReadLine();
        }
    }
}
