/***********************************************************
 * RUSSIAN MAFIA TEAM : vk.com/skynetdz | www.dayzmafia.ru
 * ZHive (DAYZ SA)
 * Copyright: 2015-2017 Pavel Kirikov
 ***********************************************************/
 
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZHive.DAL.Entities
{
    [Table("players_data")]
    public class PlayerDb
    {
        [Key]
        public string ID { get; set; }
        public string State { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}, {3}", ID, State, Created.GetValueOrDefault().ToString("dd.MM.yyyy HH:mm:ss"), Updated.GetValueOrDefault().ToString("dd.MM.yyyy HH:mm:ss"));
        }
    }
}
