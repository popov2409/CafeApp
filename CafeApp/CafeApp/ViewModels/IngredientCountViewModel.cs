using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using CafeApp.Models;
using Xamarin.Forms;

namespace CafeApp.ViewModels
{
    public class IngredientCountViewModel:BaseViewModel
    {
        public ObservableCollection<IngredientCount> IngredientCounts { get; set; }
        public Avtomat Avtomat { get; set; }

        public Command CreateListCommand { get; set; }
        public IngredientCountViewModel(Avtomat avtomat)
        {
            Title = avtomat.Value;
            IngredientCounts=new ObservableCollection<IngredientCount>();
           // OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://xamarin.com"));
            CreateListCommand =new Command(async ()=>await ExecuteCreateCommand());
        }

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

    }
}
