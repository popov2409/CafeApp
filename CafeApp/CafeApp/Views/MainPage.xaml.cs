﻿using System;
using System.ComponentModel;
using CafeApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace CafeApp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : TabbedPage
    {
        private ReportViewModel report;
        public MainPage()
        {
            InitializeComponent();
            
            //App.Database.ClearRecords();
        }
    }
}