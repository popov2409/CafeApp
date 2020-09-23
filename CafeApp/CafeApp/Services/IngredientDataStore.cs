using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeApp.Models;

namespace CafeApp.Services
{
    public class IngredientDataStore:IDataStore<Ingredient>
    {
        private readonly List<Ingredient> ingredients;

        public IngredientDataStore()
        {
            ingredients = App.Database.GetIngridients().OrderBy(c=>c.Rank).ToList();
        }

        public Task<bool> AddItemAsync(Ingredient item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateItemAsync(Ingredient item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Ingredient> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Ingredient>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(ingredients);
        }

        public Task<IEnumerable<Ingredient>> GetSearchResults(string query)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ClearData()
        {
            return await Task.FromResult(App.Database.ClearIngredients());
        }
    }
}
