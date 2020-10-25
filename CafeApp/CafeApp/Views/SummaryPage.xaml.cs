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
    public partial class SummaryPage : ContentPage
    {
        private IngredientCountViewModel viewModel;

        public SummaryPage()
        {
            InitializeComponent();
            ModePicker.SelectedIndex = 0;
            BindingContext = viewModel = new IngredientCountViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (viewModel.ReportAvtomats.Count == 0)
                viewModel.IsBusy = true;
            //BindingContext = viewModel = new IngredientCountViewModel();
            if (viewModel.ReportIngredientCounts.Count == 0)
                viewModel.IsBusy = true;

        }

        private async void OnItemSelected(object sender, EventArgs e)
        {
            var layout = (BindableObject) sender;
            var ingredient = (IngredientCount) layout.BindingContext;
            if (ingredient.Count == 0) return;
            await Navigation.PushAsync(new AvtomatCountPage(new AvtomatCountViewModel(ingredient.Ingredient)));
        }

        private void SelectedMode_Change(object sender, EventArgs e)
        {
            switch (ModePicker.SelectedIndex)
            {
                case 0:
                {
                    IngredientsCollectionView.IsVisible = false;
                    AvtomatsCollectionView.IsVisible = true;
                    break;
                }
                case 1:
                {
                    IngredientsCollectionView.IsVisible = true;
                    AvtomatsCollectionView.IsVisible = false;
                    break;
                }
            }
        }
    }
}