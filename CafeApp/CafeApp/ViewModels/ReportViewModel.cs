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
            var records =  (await RecordStore.GetItemsAsync(true)).Where(c => DateTime.Parse(c.Date) >= startDate && DateTime.Parse(c.Date) <= endDate).ToList();
            string data="";

            foreach (Record record in records)
            {
                data+="#"+(JsonConvert.SerializeObject(record));
                record.IsSend = true;
                await RecordStore.UpdateItemAsync(record);
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
