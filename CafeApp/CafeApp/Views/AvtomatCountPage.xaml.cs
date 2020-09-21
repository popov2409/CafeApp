using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeApp.Models;
using CafeApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CafeApp.Views
{
    [DesignTimeVisible(false)]
    public partial class AvtomatCountPage : ContentPage
    {
        private AvtomatCountViewModel viewModel;
        public AvtomatCountPage(AvtomatCountViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.ReportAvtomatCountList.Count == 0)
                viewModel.IsBusy = true;
        }

        private async void Remove_Record(object sender, EventArgs e)
        {
            var layout = (BindableObject)sender;
            var rec = (AvtomatCount)layout.BindingContext;
            bool result = await DisplayAlert("", "Удалить запись?", "Да", "Нет");
            if (!result) return;
            MessagingCenter.Send(this, "DelRecord", rec.Id);
            await Navigation.PopAsync();
        }
    }
}