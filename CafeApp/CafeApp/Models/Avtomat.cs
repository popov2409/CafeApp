using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace CafeApp.Models
{
    [Table("Avtomats")]
    public class Avtomat
    {
        [PrimaryKey,Column("_id")]
        public string Id { get; set; }

        public string Value { get; set; }

    }
}
