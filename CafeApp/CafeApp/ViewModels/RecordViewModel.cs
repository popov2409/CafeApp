using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using CafeApp.Models;

namespace CafeApp.ViewModels
{
    public class RecordViewModel:BaseViewModel
    {
        public ObservableCollection<Record> records { get; set; }
        public RecordViewModel(Avtomat avtomat)
        {
            Title = avtomat.Value;
            records=new ObservableCollection<Record>();
        }

    }
}
