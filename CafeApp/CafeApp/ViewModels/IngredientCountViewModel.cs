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
using Xamarin.Essentials;

namespace CafeApp.ViewModels
{
    public class IngredientCountViewModel:BaseViewModel
    {
        public ObservableCollection<IngredientCount> IngredientCounts { get; set; }
        public ObservableCollection<IngredientCount> ReportIngredientCounts { get; set; }

        private ObservableCollection<Avtomat> _repAvtomats;
        public ObservableCollection<Avtomat> ReportAvtomats { get; set; }

        public ObservableCollection<AvtomatCount> IngredientReportList { get; set; }

        private Avtomat _avtomat;

        public Command CreateListCommand { get; set; }

        public Command AddDataCommand { get; set; }

        public Command CreateReportListCommand { get; set; }

        public Command CreateIngredientReportCommand { get; set; }

        /// <summary>
        /// Контсруктор для создания списка ингредиентов при добавлении записи о внесении в автомат
        /// </summary>
        /// <param name="avtomat"></param>
        public IngredientCountViewModel(Avtomat avtomat,bool mode=false)
        {
            Title = avtomat.Value;
            IngredientCounts=new ObservableCollection<IngredientCount>();
            this._avtomat = avtomat;
            if (mode)
            {
                IngredientReportList=new ObservableCollection<AvtomatCount>();
                CreateIngredientReportCommand=new Command(async ()=>await ExecuteCreateIngredientReportCommand());
                MessagingCenter.Subscribe<IngredientReportPage, string>(this, "DelIngRecord", async (obj, record) =>
                {
                    var rec = IngredientReportList.FirstOrDefault(c => c.Id == record);
                    IngredientReportList.Remove(rec);
                    await RecordStore.DeleteItemAsync(record);
                    MessagingCenter.Unsubscribe<IngredientReportPage, string>(this, "DelIngRecord");
                });
                return;
            }
            CreateListCommand =new Command(async ()=>await ExecuteCreateCommand());
            AddDataCommand=new Command(async ()=>await ExecuteAddDataCommand());
        }


        async Task ExecuteCreateIngredientReportCommand()
        {
            IsBusy = true;
            try
            {
                IngredientReportList.Clear();
                var ingredients = await IngredientStore.GetItemsAsync(true);
                var records = (await RecordStore.GetItemsAsync(true)).Where(c =>
                    DateTime.Parse(c.Date).DayOfYear == DateTime.Now.DayOfYear && c.Avtomat == _avtomat.Id);

                foreach (Ingredient ingredient in ingredients.OrderBy(c => c.Rank))
                {
                    var rec = records.Where(c => c.Ingredient == ingredient.Id);
                    if (rec.Count() == 0) continue;
                    foreach (Record record in rec)
                    {
                        IngredientReportList.Add(new AvtomatCount { Id = record.Id, Count = record.Count, Avtomat = ingredient.Value });
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

        async Task ExecuteAddDataCommand()
        {
            string userId = Preferences.Get("user_id", "default_value");
            foreach (IngredientCount ingredientCount in IngredientCounts)
            {
                if (ingredientCount.Count == 0) continue;
                Record rec = new Record()
                {
                    Count = ingredientCount.Count,
                    Date = DateTime.Now.ToShortDateString(),
                    Id = Guid.NewGuid().ToString(),
                    Ingredient = ingredientCount.Ingredient.Id,
                    Avtomat = _avtomat.Id,
                    IsSend = false,
                    IsBlock = false,
                    User = userId
                };
                await RecordStore.AddItemAsync(rec);
            }

        }


        /// <summary>
        /// Срздание нулевого списка ингредиентов при выборе автомата для добавления в базу данных
        /// </summary>
        /// <returns></returns>
        async Task ExecuteCreateCommand()
        {
            IsBusy = true;
            try
            {
                IngredientCounts.Clear();
                var ingredients = await IngredientStore.GetItemsAsync(true);
                foreach (Ingredient ingredient in ingredients)
                {
                    IngredientCounts.Add(new IngredientCount(ingredient));
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


        /// <summary>
        /// Конструктор для формирования списка ежедневного отчета
        /// </summary>
        public IngredientCountViewModel()
        {
            Title = "Ингредиенты";
            ReportIngredientCounts = new ObservableCollection<IngredientCount>();
            ReportAvtomats = new ObservableCollection<Avtomat>();
            CreateReportListCommand = new Command(async () => await ExecuteReportListCommand());
            
        }

        async Task ExecuteReportListCommand()
        {
            IsBusy = true;
            try
            {
                ReportIngredientCounts.Clear();
                ReportAvtomats.Clear();
                var ingredients = await IngredientStore.GetItemsAsync(true);
                var avtomats = await AvtomatStore.GetItemsAsync(true);
                var records = (await RecordStore.GetItemsAsync(true)).Where(c =>
                    DateTime.Parse(c.Date).DayOfYear == DateTime.Now.DayOfYear &&
                    DateTime.Parse(c.Date).Year == DateTime.Now.Year);
                foreach (Ingredient ingredient in ingredients)
                {
                    var item = new IngredientCount(ingredient);
                    item.Count = records.Where(c => c.Ingredient == ingredient.Id).Sum(c => c.Count);
                    if(item.Count==0) continue;
                    ReportIngredientCounts.Add(item);
                }

                var avtoEnumerable = (await RecordStore.GetItemsAsync(true)).Where(c =>
                    DateTime.Parse(c.Date).DayOfYear == DateTime.Now.DayOfYear &&
                    DateTime.Parse(c.Date).Year == DateTime.Now.Year).Select(c => c.Avtomat).Distinct();
                foreach (string record in avtoEnumerable)
                {
                    ReportAvtomats.Add(avtomats.First(c => c.Id == record));
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
