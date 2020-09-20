using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CafeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AvtomatCountPage : ContentPage
    {
        private AvtomatCountViewModel viewModel;
        public AvtomatCountPage()
        {
            InitializeComponent();
            var ing = App.Database.GetIngridients().ToList()[0];
            BindingContext = viewModel = new AvtomatCountViewModel(ing);

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (viewModel.ReportAvtomatCountList.Count == 0)
                viewModel.IsBusy = true;
        }
    }
}