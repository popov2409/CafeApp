using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using CafeApp.Models;
using CafeApp.Services;

namespace CafeApp.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {

        public IDataStore<Avtomat> AvtomatStore => DependencyService.Get<IDataStore<Avtomat>>();
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

        public IDataStore<Ingredient> IngredientStore => DependencyService.Get<IDataStore<Ingredient>>();
        public IDataStore<Record> RecordStore => DependencyService.Get<IDataStore<Record>>();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
