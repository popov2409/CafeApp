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
    public partial class AddRecordsPage : ContentPage
    {
        private IngredientCountViewModel viewModel { get; set; }
        public AddRecordsPage(IngredientCountViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;

        }
       

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.IngredientCounts.Count == 0)
                viewModel.IsBusy = true;
        }

        private async void Add_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddRecords", viewModel.IngredientCounts);
            await Navigation.PopAsync();
        }
    }
}