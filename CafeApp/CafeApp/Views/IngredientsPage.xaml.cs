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
    public partial class IngredientsPage : ContentPage
    {
        private IngredientsViewModel viewModel;
        public IngredientsPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new IngredientsViewModel();
        }
    }
}