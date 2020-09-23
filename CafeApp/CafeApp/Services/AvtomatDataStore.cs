using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeApp.Models;

namespace CafeApp.Services
{
    public class AvtomatDataStore:IDataStore<Avtomat>
    {
        readonly List<Avtomat> avtomats;

        public AvtomatDataStore()
        {
            avtomats = App.Database.GetAvtomats().OrderBy(c=>c.Value).ToList();
        }

        public Task<bool> AddItemAsync(Avtomat item)
        {
            avtomats.Add(item);
            App.Database.SaveItem(item);
            return Task.FromResult(true);
        }

        public Task<bool> UpdateItemAsync(Avtomat item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Avtomat> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Avtomat>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(avtomats.OrderBy(c => c.Value));
        }

        //GetSearchResults
        public async Task<IEnumerable<Avtomat>> GetSearchResults(string query)
        {
            if (query == null)
            {
                return await Task.FromResult(avtomats);
            }
            else
            {
                var res = avtomats.Where(c => c.Value.ToLower().Contains(query.ToLower())).OrderBy(c => c.Value)
                    .ToList();
                return await Task.FromResult(res);
            }
        }

        public async Task<bool> ClearData()
        {
            avtomats.Clear();
            return await Task.FromResult(App.Database.ClearAvotamts());
        }
    }
}
