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
using ZHive.DAL.Entities;

namespace ZHive.DAL.Repositories
{
    public class PlayersRepository
    {
        public PlayerDb Find(string key)
        {
            using (var ctx = new HiveDbContext())
                return ctx.Set<PlayerDb>().Find(key);
        }

        public PlayerDb Find(Expression<Func<PlayerDb, bool>> predicate)
        {
            using (var ctx = new HiveDbContext())
                return ctx.Set<PlayerDb>().FirstOrDefault(predicate);
        }

        public List<PlayerDb> List(Expression<Func<PlayerDb, bool>> predicate)
        {
            using (var ctx = new HiveDbContext())
                return ctx.Set<PlayerDb>().Where(predicate)
                                          .ToList();
        }

        public void Insert(PlayerDb entry)
        {
            using (var ctx = new HiveDbContext())
            {
                var entity = ctx.Players.Find(entry.ID);
                if (entity == null)
                {
                    ctx.Players.Add(entry);
                    ctx.SaveChanges();
                }
            }
        }

        public void Update(PlayerDb entry)
        {
            using (var ctx = new HiveDbContext())
            {
                ctx.Entry(entry).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public void Delete(string key)
        {
            using (var ctx = new HiveDbContext())
            {
                var entity = ctx.Players.Find(key);
                if (entity != null)
                {
                    ctx.Players.Remove(entity);
                    ctx.SaveChanges();
                }
            }
        }
    }
}
