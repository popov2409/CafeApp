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
    public class AvtomatsViewModel:BaseViewModel
    {
        public ObservableCollection<Avtomat> Avtomats { get; set; }

        public Command LoadAvtomatsCommand { get; set; }
        public AvtomatsViewModel()
        {
            Title = "Автоматы";
            Avtomats=new ObservableCollection<Avtomat>();
            LoadAvtomatsCommand = new Command(async () => await ExecuteLoadAvtomatCommand());
            MessagingCenter.Subscribe<AvtomatsPage, string>(this, "SearchItems", async (obj, query) =>
            {
                var avt= await AvtomatStore.GetSearchResults(query);
                Avtomats.Clear();
                foreach (var item in avt)
                {
                    Avtomats.Add(item);
                }
            });
        }


        async Task ExecuteLoadAvtomatCommand()
        {
            IsBusy = true;

            try
            {
                Avtomats.Clear();
                var avtomats = await AvtomatStore.GetItemsAsync(true);
                foreach (var item in avtomats)
                {
                    Avtomats.Add(item);
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
