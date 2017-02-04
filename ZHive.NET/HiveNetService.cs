/***********************************************************
 * RUSSIAN MAFIA TEAM : vk.com/skynetdz | www.dayzmafia.ru
 * ZHive (DAYZ SA)
 * Copyright: 2015-2017 Pavel Kirikov
 ***********************************************************/

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using ZHive.Utilities;
using ZHive.DAL.Entities;
using ZHive.DAL.Repositories;
using ZHive.NET.EventHandlers;

namespace ZHive.NET
{
    public class HiveNetService
    {
        public string Address = "";
        public int Port = 0;

        public event RespawnObjectEventHandler RespawnEventHandler;
        public event LoadObjectEventHandler LoadEventHandler;
        public event SaveObjectEventHandler SaveEventHandler;
        public event DeleteObjectEventHandler DeleteEventHandler;

        private TcpListener _listener;
        private ObjectsRepository _objectsRepository;

        public HiveNetService()
        { _objectsRepository = new ObjectsRepository(); }

        public void Start()
        {
            if (Helpers.ContainsNullOrEmpty(Address, Port))
                throw new ArgumentNullException("NetService: Не настроен адрес и/или порт");

            _listener = new TcpListener(IPAddress.Parse(Address), Port);
            _listener.Start();
            BeginAccept();
        }

        public void Stop()
        {
            _listener.Stop();
        }

        public void BeginAccept()
        {
            _listener.BeginAcceptTcpClient(new AsyncCallback(AcceptCallback), _listener);
        }

        public void AcceptCallback(IAsyncResult result)
        {
            try
            {
                var listener = (TcpListener)result.AsyncState;
                BeginAccept();
                
                int recvBytes = 0;
                byte[] buffer = new byte[1024];
                var sb = new StringBuilder();

                using (var client = listener.EndAcceptTcpClient(result))
                using (var stream = client.GetStream())
                {
                    while (stream.DataAvailable)
                    {
                        recvBytes = stream.Read(buffer, 0, buffer.Length);
                        string request = Encoding.UTF8.GetString(buffer, 0, recvBytes);
                        sb.Append(request).Replace("\x00", "");
                    }

                    string response = ProcessRequest(sb.ToString());
                    byte[] data = Encoding.UTF8.GetBytes(response);
                    stream.Write(data, 0, data.Length);
                }
            }
            catch (Exception ex)
            { Logger.WriteError("Exc: {0}", ex.ToString()); }
        }

        public string ProcessRequest(string request)
        {
            ObjectDb entity;
            string result = "";

            int index = request.IndexOf(':');
            if (index == -1)
            {
                Console.WriteLine("REQUEST: NEGATIVE INDEX (-1)");
                return result;
            }

            string messageBody = request.Substring(index + 1);
            string messageType = request.Substring(0, index);
            switch (messageType)
            {
                case "Respawn":

                    result = _objectsRepository.GetRespawnIdList();
                    OnRespawnObject(result);

                    return result;

                case "Load":

                        entity = _objectsRepository.Find(messageBody);
                        if (entity == null)
                            throw new NullReferenceException(messageBody + " не найден в базе данных");

                        result = string.Format("[['{0}','{1}',{2},{3},{4}],{5}]",
                            entity.ID, entity.Classname, entity.Dir, entity.Pos, entity.Damage, entity.Items);

                        OnLoadObject(result);

                    return result;

                case "Save":

                    entity = JsonConvert.DeserializeObject<ObjectDb>(messageBody);
                    entity.Updated = DateTime.Now;
                    _objectsRepository.Update(entity);

                    OnSaveObject(entity);

                    return "Ok";

                case "Delete":

                    _objectsRepository.Delete(messageBody);
                    OnDeleteObject(messageBody);

                    return "Ok";

                default:
                    zConsole.WriteMessage("Object: unknown request");
                    return result;
            }
        }

        #region < EVENTS >

        private void OnRespawnObject(string result)
        {
            if (this.RespawnEventHandler != null)
                this.RespawnEventHandler(new RespawnObjectEventArgs(result));
        }

        private void OnLoadObject(string result)
        {
            if (LoadEventHandler != null)
                this.LoadEventHandler(new LoadObjectEventArgs(result));
        }

        private void OnSaveObject(ObjectDb objectDb)
        {
            if (SaveEventHandler != null)
                this.SaveEventHandler(new SaveObjectEventArgs(objectDb));
        }

        private void OnDeleteObject(string objectId)
        {
            if (DeleteEventHandler != null)
                this.DeleteEventHandler(new DeleteObjectEventArgs(objectId));
        }

        #endregion
    }
}
