using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace CafeApp.Models
{
    [Table("Ingredients")]
    public class Ingredient
    {
        [PrimaryKey, Column("_id")]
        public string Id { get; set; }

        public string Value { get; set; }
        public int Rank { get; set; }
    }
}
