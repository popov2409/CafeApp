using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeApp.Models;
using CafeApp.Views;
using Xamarin.Forms;


namespace CafeApp.ViewModels
{
    public class AvtomatCountViewModel:BaseViewModel
    {
        public ObservableCollection<AvtomatCount> ReportAvtomatCountList { get; set; }
        public Command CreateListCommand { get; set; }

        public AvtomatCountViewModel(Ingredient ingredient)
        {
            Title = ingredient.Value;
            ReportAvtomatCountList=new ObservableCollection<AvtomatCount>();
            CreateListCommand=new Command(async()=>await ExecuteCreateListCommand(ingredient));
            MessagingCenter.Subscribe<AvtomatCountPage, string>(this, "DelRecord", async (obj, record) =>
            {
                var rec = ReportAvtomatCountList.FirstOrDefault(c => c.Id == record);
                ReportAvtomatCountList.Remove(rec);
                await RecordStore.DeleteItemAsync(record);
                MessagingCenter.Unsubscribe<AvtomatCountPage, string>(this, "DelRecord");
            });
        }

        async Task ExecuteCreateListCommand(Ingredient ingredient)
        {
            IsBusy = true;
            try
            {
                ReportAvtomatCountList.Clear();
                var avtomats = await AvtomatStore.GetItemsAsync(true);
                var records = (await RecordStore.GetItemsAsync(true)).Where(c =>
                    DateTime.Parse(c.Date).DayOfYear == DateTime.Now.DayOfYear && c.Ingredient == ingredient.Id);

                foreach (Avtomat avtomat in avtomats.OrderBy(c=>c.Value))
                {
                    var rec = records.Where(c => c.Avtomat == avtomat.Id);
                    if(rec.Count()==0) continue;
                    foreach (Record record in rec)
                    {
                        ReportAvtomatCountList.Add(new AvtomatCount {Id = record.Id,Count = record.Count,Avtomat = avtomat.Value});
                    }

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
            
        }

    }
}
