/***********************************************************
 * RUSSIAN MAFIA TEAM : vk.com/skynetdz | www.dayzmafia.ru
 * ZHive (DAYZ SA)
 * Copyright: 2015-2017 Pavel Kirikov
 ***********************************************************/

using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ZHive.DAL.Entities
{
    public class HiveDbContext : DbContext
    {
        public DbSet<PlayerDb> Players { get; set; }
        public DbSet<ObjectDb> Objects { get; set; }
        public DbSet<SpawnDb> Spawns { get; set; }

        public HiveDbContext() : base("name=ZHiveDbContext")
        {

            //Configuration.AutoDetectChangesEnabled = false;
            //Configuration.ValidateOnSaveEnabled = false;

            //Database.SetInitializer<HiveDbContext>(new DropCreateDatabaseIfModelChanges<HiveDbContext>());
            Database.SetInitializer<HiveDbContext>(null);

#if DEBUG
            //Database.Log = System.Console.WriteLine;
#endif

        }
    }
}
