using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeApp.Models;
using CafeApp.Services;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace CafeApp.ViewModels
{
    public class ReportViewModel:BaseViewModel
    {
        public Reporter Reporter=new Reporter();

        public async Task SendReport(DateTime startDate, DateTime endDate)
        {
            User user = new User()
            {
                Id = Preferences.Get("user_id", "default_value"),
                Name = Preferences.Get("user_name", "default_value")
            };
            var records = await RecordStore.GetItemsAsync(true);
            string data=JsonConvert.SerializeObject(user);
            foreach (Record record in records.Where(c=>DateTime.Parse(c.Date)>=startDate&& DateTime.Parse(c.Date) <= endDate))
            {
                data+="#"+(JsonConvert.SerializeObject(record));

            }
            await Reporter.SendReport(data);
        }

        public async Task<string> GetDatesNotSendingRecords()
        {
            var rec = (await RecordStore.GetItemsAsync(true)).ToList();
            if(rec.Count==0) return $"{DateTime.Now.ToShortDateString()}#{DateTime.Now.ToShortDateString()}";
            DateTime startDate = rec.Where(c => !c.IsSend).Min(c => DateTime.Parse(c.Date));
            DateTime endDate =rec.Where(c => !c.IsSend).Max(c => DateTime.Parse(c.Date));
            if (startDate == null) return $"{DateTime.Now.ToShortDateString()}#{DateTime.Now.ToShortDateString()}";
            return $"{startDate}#{endDate}";
        }

    }

    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

}
