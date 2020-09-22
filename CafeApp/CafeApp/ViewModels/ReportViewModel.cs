using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeApp.Models;
using CafeApp.Services;
using Newtonsoft.Json;

namespace CafeApp.ViewModels
{
    public class ReportViewModel:BaseViewModel
    {
        public Reporter Reporter=new Reporter();

        public async Task SendReport(DateTime startDate, DateTime endDate)
        {
            var records = await RecordStore.GetItemsAsync(true);
            List<string> data=new List<string>();
            foreach (Record record in records)
            {
                data.Add(JsonConvert.SerializeObject(record));

            }
            await Reporter.SendReport(data.ToArray());
        }

        public async Task<string> GetDatesNotSendingRecords()
        {
            var rec = await RecordStore.GetItemsAsync(true);
            DateTime startDate = rec.Where(c => !c.IsSend).Min(c => DateTime.Parse(c.Date));
            DateTime endDate =rec.Where(c => !c.IsSend).Max(c => DateTime.Parse(c.Date));
            if (startDate == null) return $"{DateTime.Now.ToShortDateString()}#{DateTime.Now.ToShortDateString()}";
            return $"{startDate}#{endDate}";
        }

    }
}
