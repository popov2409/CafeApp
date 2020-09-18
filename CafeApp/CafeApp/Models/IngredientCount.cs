using System;
using System.Collections.Generic;
using System.Text;

namespace CafeApp.Models
{
   public class IngredientCount
    {
        public IngredientCount(Ingredient ingredient)
        {
            Ingredient = ingredient;
            Count = 0;
        }

        public Ingredient Ingredient { get; set; }
        public int Count { get; set; }

    }
}
