/***********************************************************
 * RUSSIAN MAFIA TEAM : vk.com/skynetdz | www.dayzmafia.ru
 * ZHive (DAYZ SA)
 * Copyright: 2016-2017 Pavel Kirikov
 ***********************************************************/

using System;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using ZHive.DAL.Entities;
using ZHive.DAL.Repositories;
using ZHive.Utilities;
using ZHive.WebAPI.EventHandlers;
using ZHive.WebAPI.Enums;

namespace ZHive.WebAPI.Controllers
{
    public class HiveController : ApiController
    {
        public static event FindPlayerEventHandler FindPlayerEventHandler;
        public static event LoadPlayerEventHandler LoadPLayerEventHandler;
        public static event CreatePlayerEventHandler CreatePlayerEventHandler;
        public static event SavePlayerEventHandler SavePlayerEventHandler;
        public static event KillPlayerEventHandler KillPlayerEventHandler;
        public static event MessageEventHandelr MessageEventHandelr;
        public static event ErrorEventHandelr ErrorEventHandler;

        private PlayersRepository _playersRepository;

        #region < CONTROLLER >

        public HiveController()
        { _playersRepository = new PlayersRepository(); }

        [HttpPost]
        public object Find(string uid, HttpRequestMessage request)
        {
            try
            {
                var entity = _playersRepository.Find(uid);
                if (entity == null)
                    throw new NullReferenceException(uid + " не найден в базе данных");

                OnFindPlayer(entity, request);

                return JObject.Parse(entity.State);
            }
            catch(Exception e)
            {
                OnError(uid, request, e);
                return new object { };
            }
        }

        [HttpGet]
        public object Load(string uid, HttpRequestMessage request)
        {
            try
            {
                var entity = _playersRepository.Find(uid);
                if (entity == null)
                    throw new NullReferenceException(uid + " не найден в базе данных");

                OnLoadPlayer(entity, request);

                return JObject.Parse(entity.State);
            }
            catch(Exception e)
            {
                OnError(uid, request, e);
                return new object { };
            }
        }

        [HttpPost]
        public void Create(string uid, HttpRequestMessage request)
        {
            var entity = new PlayerDb
            {
                ID = uid,
                State = "{}",
                Created = DateTime.Now
            };

            try
            {
                _playersRepository.Insert(entity);

                OnCreatePlayer(entity, request);
            }
            catch (Exception e) { OnError(uid, request, e); }
        }

        [HttpPost]
        public async Task Save(string uid, HttpRequestMessage request)
        {
            try
            {
                var state = await request.Content.ReadAsStringAsync();
                var entity = _playersRepository.Find(uid);

                if (entity != null)
                {
                    entity.State = state;
                    entity.Updated = DateTime.Now;
                    _playersRepository.Update(entity);

                    OnSavePlayer(entity, request);
                }
                else
                {
                    entity = new PlayerDb();
                    entity.ID = uid;
                    entity.State = state;
                    entity.Created = DateTime.Now;
                    entity.Updated = DateTime.Now;
                    _playersRepository.Insert(entity);

                    OnCreatePlayer(entity, request);
                    OnMessage(uid + " небыл найден в бд, поэтому был создан", MessageType.Warning);
                }
            }
            catch (Exception e) { OnError(uid, request, e); }
        }

        [HttpPost]
        public void Kill(string uid, HttpRequestMessage request)
        {
            try
            {
                _playersRepository.Delete(uid);

                OnKillPlayer(uid, request);
            }
            catch (Exception e) { OnError(uid, request, e); }
        }

        [HttpPost]
        public void Queue(string uid, HttpRequestMessage request) { }

        #endregion

        #region < EVENTS >

        private void OnFindPlayer(PlayerDb player, HttpRequestMessage request)
        {
            if (FindPlayerEventHandler != null)
                FindPlayerEventHandler(new FindPlayerEventArgs(player, request));
        }

        private void OnLoadPlayer(PlayerDb player, HttpRequestMessage request)
        {
            if (LoadPLayerEventHandler != null)
                LoadPLayerEventHandler(new LoadPlayerEventArgs(player, request));
        }

        private void OnCreatePlayer(PlayerDb player, HttpRequestMessage request)
        {
            if (CreatePlayerEventHandler != null)
                CreatePlayerEventHandler(new CreatePlayerEventArgs(player, request));
        }

        private void OnSavePlayer(PlayerDb player, HttpRequestMessage request)
        {
            if (SavePlayerEventHandler != null)
                SavePlayerEventHandler(new SavePlayerEventArgs(player, request));
        }

        private void OnKillPlayer(string uid, HttpRequestMessage request)
        {
            if (KillPlayerEventHandler != null)
                KillPlayerEventHandler(new KillPlayerEventArgs(uid, request));
        }

        private void OnMessage(string message, MessageType messageType = MessageType.Default)
        {
            if (MessageEventHandelr != null)
                MessageEventHandelr(new MessageEventArgs(message, messageType));
        }

        private void OnError(string uid, HttpRequestMessage request, Exception exception)
        {
            if (ErrorEventHandler != null)
                ErrorEventHandler(new ErrorEventArgs(uid, request, exception));
        }

        #endregion
    }
}
