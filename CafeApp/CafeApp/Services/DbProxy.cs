using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CafeApp.Models;
using SQLite;

namespace CafeApp.Services
{
    public class DbProxy
    {
        SQLiteConnection database;


        public DbProxy(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<Avtomat>();
            database.CreateTable<Ingredient>();
            database.CreateTable<Record>();

        }

        public IEnumerable<Avtomat> GetAvtomats()
        {
            return database.Table<Avtomat>().ToList();
        }

        public int DeleteItem(object item)
        {
            switch (item)
            {
                case Avtomat avtomat:
                    return database.Delete<Avtomat>(avtomat.Id);
                case Ingredient ingredient:
                    return database.Delete<Ingredient>(ingredient.Id);
                case Record record:
                    return database.Delete<Record>(record.Id);
                default:
                    return 0;
            }
        }

        public Avtomat GetAvtomat(Guid id)
        {
            return database.Get<Avtomat>(id);
        }

        public Ingredient GetIngredient(Guid id)
        {
            return database.Get<Ingredient>(id);
        }

        public IEnumerable<Ingredient> GetIngridients()
        {
            return database.Table<Ingredient>().ToList();
        }

        public IEnumerable<Record> GetRecords()
        {
            return database.Table<Record>().ToList();
        }

        public void UpdateItem(object item)
        {
            database.Update(item);
        }

        public void ClearRecords()
        {
            database.DropTable<Record>();
            database.CreateTable<Record>();
        }


        public void ClearData()
        {
            database.DropTable<Avtomat>();
            database.CreateTable<Avtomat>();
            database.DropTable<Ingredient>();
            database.CreateTable<Ingredient>();
        }

        public void DropTables()
        {
            database.DropTable<Avtomat>();
            database.DropTable<Ingredient>();
            database.DropTable<Record>();
        }

        /// <summary>
        /// Сохранение данных
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public void SaveItem(Object item)
        {
            database.Insert(item);
        }


        private List<string> testAvtomats = new List<string>
            {"Хлебозавод", "Мираторг строитель", "Площадь Василевского", "Дом профсоюзов", "Бау Мосиев"};
        private List<string> testIngredientsList = new List<string>
            {"Стаканы", "Палочки", "Сахар", "Вода", "Сливки"};

        public void CreateTestData()
        {
            foreach (string testAvtomat in testAvtomats)
            {
                App.Database.SaveItem(new Avtomat() {Id = Guid.NewGuid().ToString(), Value = testAvtomat});
            }

            foreach (string s in testIngredientsList)
            {
                App.Database.SaveItem(new Ingredient() { Id = Guid.NewGuid().ToString(), Value = s });
            }
        }

    }

}
//Сохранение пользователя
// using Xamarin.Essentials;
            //Preferences.Set("user_id", Guid.NewGuid().ToString());
            //Preferences.Set("user_name", "Попов Т.А.");
            // var id = Preferences.Get("user_id", "default_value");
            //var name = Preferences.Get("user_name", "default_value");
            //bool hasKey = Preferences.ContainsKey("my_key");
            //Preferences.Remove("my_key");
            //Preferences.Clear();
