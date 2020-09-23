using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeApp.Models;

namespace CafeApp.Services
{
    public class RecordDataStore:IDataStore<Record>
    {
        private readonly List<Record> records;

        public RecordDataStore()
        {
            records = App.Database.GetRecords().ToList();
        }

        public async Task<bool> AddItemAsync(Record item)
        {
            records.Add(item);
            App.Database.SaveItem(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Record item)
        {
            var oldItem = records.FirstOrDefault(c=>c.Id==item.Id);
            records.Remove(oldItem);
            records.Add(item);
            App.Database.UpdateItem(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = records.FirstOrDefault(c => c.Id == id);
            if (oldItem == null) return true;
            records.Remove(oldItem);
            App.Database.DeleteItem(oldItem);
            return await Task.FromResult(true);
        }

        public async Task<Record> GetItemAsync(string id)
        {
            return await Task.FromResult(records.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Record>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(records);
        }

        public Task<IEnumerable<Record>> GetSearchResults(string query)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ClearData()
        {
            return await Task.FromResult(App.Database.ClearRecords());
        }
    }
}
