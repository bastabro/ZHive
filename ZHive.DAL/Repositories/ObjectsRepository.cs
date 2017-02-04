/***********************************************************
 * RUSSIAN MAFIA TEAM : vk.com/skynetdz | www.dayzmafia.ru
 * ZHive (DAYZ SA)
 * Copyright: 2015-2017 Pavel Kirikov
 ***********************************************************/

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using ZHive.DAL.Entities;
using ZHive.Utilities;

namespace ZHive.DAL.Repositories
{
    public class ObjectsRepository
    {
        public string GetRespawnIdList()
        {
            int maxVehicles = int.Parse(Config.AppSettings("VehiclesCount"));

            using (var ctx = new HiveDbContext())
            {
                var expireDate = (DateTime.Now - TimeSpan.FromDays(10));
                var deleted = ctx.Objects.Where(x => x.Damage == "1" || x.Updated < expireDate)
                                         .ToList();

                ctx.Set<ObjectDb>().RemoveRange(deleted);
                ctx.SaveChanges();

                var objects = ctx.Objects.AsEnumerable()
                                         .Except(deleted)
                                         .ToList();

                var count = (objects.Count < maxVehicles) ?
                            (maxVehicles - objects.Count) : 0;

                zConsole.WriteMessage("Loaded: {0}", objects.Count);
                zConsole.WriteMessage("Respawned: {0}", count);

                var spawns = ctx.Spawns
                                .OrderBy(x => Guid.NewGuid())
                                .Where(x => !ctx.Objects.Select(y => y.ID)
                                .Contains(x.ID))
                                .Take(count);

                objects.Clear();
                spawns.ForEachAsync(x =>
                {
                    objects.Add(new ObjectDb
                    {
                        ID = x.ID,
                        Classname = x.Classname,
                        Dir = x.Dir,
                        Pos = x.Pos,
                        Damage = x.Damage,
                        Items = x.Items,
                        Updated = DateTime.Now
                    });

                }).Wait();

                if (objects.Count > 0)
                {
                    ctx.Set<ObjectDb>().AddRange(objects);
                    ctx.SaveChanges();
                }

                return string.Join(",", ctx.Objects.Select(x => x.ID));
            }
        }

        public ObjectDb Find(string key)
        {
            using (var ctx = new HiveDbContext())
                return ctx.Objects.Find(key);
        }

        public ObjectDb Find(Expression<Func<ObjectDb, bool>> predicate)
        {
            using (var ctx = new HiveDbContext())
                return ctx.Objects.FirstOrDefault(predicate);
        }

        public List<ObjectDb> List(Expression<Func<ObjectDb, bool>> predicate)
        {
            using (var ctx = new HiveDbContext())
                return ctx.Objects.Where(predicate)
                                  .ToList();
        }

        public void Update(ObjectDb entity)
        {
            using (var ctx = new HiveDbContext())
            {
                ctx.Objects.AddOrUpdate(entity);
                ctx.SaveChanges();
            }
        }

        public void Delete(string key)
        {
            using (var ctx = new HiveDbContext())
            {
                var entity = ctx.Objects.Find(key);
                if (entity != null)
                {
                    ctx.Objects.Remove(entity);
                    ctx.SaveChanges();
                }
            }
        }
    }
}
