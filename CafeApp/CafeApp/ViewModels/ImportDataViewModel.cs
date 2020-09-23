using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CafeApp.Services;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace CafeApp.ViewModels
{
    public class ImportDataViewModel:BaseViewModel
    {
        private ImportData Importer;
        public Command ImportData { get; set; }

        public ImportDataViewModel()
        {
            Importer=new ImportData();
            List<string> data = Importer.GetData();
            if (data == null)
            {
               return;
            }

            ImportData=new Command(async() =>await ExecuteImportData(data));
        }

        async Task ExecuteImportData(List<string> data)
        {
            User user = JsonConvert.DeserializeObject<User>(data[0]);
            bool isNext = false;
            data.RemoveAt(0);
            foreach (string s in data)
            {
                if (s.Equals("#")) isNext = true;
                if (!isNext)
                {

                    continue;
                }
                else
                {
                    
                }
            }
        }

    }
}
