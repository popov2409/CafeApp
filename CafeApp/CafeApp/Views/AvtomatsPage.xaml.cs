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
    public partial class AvtomatsPage : ContentPage
    {
        private AvtomatsViewModel viewModel;
        public AvtomatsPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new AvtomatsViewModel();
        }

        async void OnAvtomatSelected(object sender, EventArgs args)
        {
            //await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
            var layout = (BindableObject)sender;
            var avtomat = (Avtomat)layout.BindingContext;
            await Navigation.PushAsync(new AddRecordsPage(new IngredientCountViewModel(avtomat)));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Avtomats.Count == 0)
                viewModel.IsBusy = true;
        }

        private void Seacrh_Items(object sender, TextChangedEventArgs e)
        {
            MessagingCenter.Send(this, "SearchItems", searchBar.Text);
        }
    }
}