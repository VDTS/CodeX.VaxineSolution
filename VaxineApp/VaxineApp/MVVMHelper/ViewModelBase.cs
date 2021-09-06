using DataAccessLib.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace VaxineApp.MVVMHelper
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        // This will be removed after moving codebase to new logic
        protected DbService DataService = new DbService(Constants.FirebaseBaseUrl, Constants.FirebaseApiKey);


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
