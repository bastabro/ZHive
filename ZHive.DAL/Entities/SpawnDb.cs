/***********************************************************
 * RUSSIAN MAFIA TEAM : vk.com/skynetdz | www.dayzmafia.ru
 * ZHive (DAYZ SA)
 * Copyright: 2015-2017 Pavel Kirikov
 ***********************************************************/

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZHive.DAL.Entities
{
    [Table("objects_spawn")]
    public class SpawnDb
    {
        [Key]
        public string ID { get; set; }
        public string Classname { get; set; }
        public string Dir { get; set; }
        public string Pos { get; set; }
        public string Damage { get; set; }
        public string Items { get; set; }
    }
}
