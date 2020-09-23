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
    public partial class ReporterPage : ContentPage
    {
        private ReportViewModel viewModel;
        public ReporterPage()
        {
            InitializeComponent();
            viewModel=new ReportViewModel();
            GetDates();
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
           await Navigation.PopModalAsync();
        }

        private async void GetDates()
        {
            string sss = await viewModel.GetDatesNotSendingRecords();
            DateTime[] dates = (sss.Split('#').Select(DateTime.Parse).ToArray());
            StartDate.Date = dates[0];
            EndDate.Date = dates[1];
        }

        private async void Send_Report(object sender, EventArgs e)
        {
            await viewModel.SendReport(StartDate.Date, EndDate.Date);
            await Navigation.PopModalAsync();
        }
    }
}