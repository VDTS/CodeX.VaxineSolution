using DataAccessLib.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using VaxineApp.RealCacheLib;
using Xamarin.Forms;

namespace VaxineApp.MVVMHelper
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        protected DbService DataService = new DbService();
        protected RequestsHandler requestsHandler = new RequestsHandler();

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
