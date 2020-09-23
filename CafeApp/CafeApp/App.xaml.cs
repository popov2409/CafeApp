using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CafeApp.Services;
using CafeApp.Views;

namespace CafeApp
{
    public partial class App : Application
    {

        private const string DATABASE_NAME = "mainbase.db";
        private static DbProxy database;

        public static DbProxy Database
        {
            get
            {
                if (database == null)
                {
                    string path = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME);
                    database = new DbProxy(path);
                }

                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            DependencyService.Register<AvtomatDataStore>();
            DependencyService.Register<IngredientDataStore>();
            DependencyService.Register<RecordDataStore>();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
