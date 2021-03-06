﻿using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace CafeApp.Models
{
    [Table("Records")]
    public class Record
    {
        [PrimaryKey, Column("_id")]
        public string Id { get; set; }
        public string Avtomat { get; set; }
        public string Ingredient { get; set; }
        public int Count { get; set; }
        public string Date { get; set; }
        public bool IsSend { get; set; }
        public bool IsBlock { get; set; }
        public string User { get; set; }
    }
}
