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
    public class IngredientCountViewModel:BaseViewModel
    {
        public ObservableCollection<IngredientCount> IngredientCounts { get; set; }
        public ObservableCollection<IngredientCount> ReportIngredientCounts { get; set; }

        public Command CreateListCommand { get; set; }

        public Command CreateIngredientListCommand { get; set; }

        /// <summary>
        /// Контсруктор для создания списка ингредиентов при добавлении записи о внесении в автомат
        /// </summary>
        /// <param name="avtomat"></param>
        public IngredientCountViewModel(Avtomat avtomat)
        {
            Title = avtomat.Value;
            IngredientCounts=new ObservableCollection<IngredientCount>();
           // OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://xamarin.com"));
            CreateListCommand =new Command(async ()=>await ExecuteCreateCommand());
            MessagingCenter.Subscribe<AddRecordsPage, ObservableCollection<IngredientCount>>(this, "AddRecords", async (obj, records) =>
            {
                foreach (IngredientCount ingredientCount in records)
                {
                    if(ingredientCount.Count==0) continue;
                    Record rec=new Record()
                    {
                        Count = ingredientCount.Count,
                        Date = DateTime.Now.ToShortDateString(),
                        Id = Guid.NewGuid().ToString(),
                        IngredientId = ingredientCount.Ingredient.Id,
                        AvtomatId = avtomat.Id,
                        IsSend = false,
                        IsBlock = false
                    };
                    await RecordStore.AddItemAsync(rec);
                }
            });
        }

        /// <summary>
        /// Срздание нулевого списка ингредиентов при выбореавтомата для добавления в базу данных
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
            CreateIngredientListCommand = new Command(async () => await ExecuteCreateIngredientListCommand());
        }

        async Task ExecuteCreateIngredientListCommand()
        {
            IsBusy = true;
            try
            {
                ReportIngredientCounts.Clear();
                var ingredients = await IngredientStore.GetItemsAsync(true);
                var records = (await RecordStore.GetItemsAsync(true)).Where(c =>
                    DateTime.Parse(c.Date).DayOfYear == DateTime.Now.DayOfYear &&
                    DateTime.Parse(c.Date).Year == DateTime.Now.Year);
                foreach (Ingredient ingredient in ingredients)
                {
                    var item = new IngredientCount(ingredient);
                    item.Count = records.Where(c => c.IngredientId == ingredient.Id).Sum(c => c.Count);
                    ReportIngredientCounts.Add(item);
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
