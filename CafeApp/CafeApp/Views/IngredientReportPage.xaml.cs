using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeApp.Models;
using CafeApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CafeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IngredientReportPage : ContentPage
    {
        private IngredientCountViewModel viewModel;
        public IngredientReportPage(Avtomat avtomat)
        {
            InitializeComponent();
            this.BindingContext = viewModel = new IngredientCountViewModel(avtomat,true);
        }

        private async void Remove_Record(object sender, EventArgs e)
        {
            var layout = (BindableObject)sender;
            var rec = (AvtomatCount)layout.BindingContext;
            bool result = await DisplayAlert("", "Удалить запись?", "Да", "Нет");
            if (!result) return;
            MessagingCenter.Send(this, "DelIngRecord", rec.Id);
            await Navigation.PopAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.IngredientReportList.Count == 0)
                viewModel.IsBusy = true;
        }
    }
}