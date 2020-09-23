using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CafeApp.Models;
using CafeApp.Services;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace CafeApp.ViewModels
{
    public class ImportDataViewModel:BaseViewModel
    {
        private ImportData Importer;

        public ImportDataViewModel()
        {
            Importer=new ImportData();
        }

        public async Task<bool> Import()
        {
            IsBusy = true;
            List<string> data = Importer.GetData();
            if (data == null)
            {
                IsBusy = false;
               return false;
            }
            User user = JsonConvert.DeserializeObject<User>(data[0]);
            Preferences.Set("user_id", user.Id);
            Preferences.Set("user_name", user.Name);
            bool isNext = false;
            data.RemoveAt(0);
            await AvtomatStore.ClearData();
            await IngredientStore.ClearData();
            foreach (string s in data)
            {
                if (s.Equals("#"))
                {
                    isNext = true;
                    continue;
                }
                if (!isNext)
                {
                    Avtomat av = JsonConvert.DeserializeObject<Avtomat>(s);
                    await AvtomatStore.AddItemAsync(av);
                    continue;
                }
                else
                {
                    Ingredient ing = JsonConvert.DeserializeObject<Ingredient>(s);
                    await IngredientStore.AddItemAsync(ing);

                }
            }

            IsBusy = false;
            return true;
        }
    }
}
