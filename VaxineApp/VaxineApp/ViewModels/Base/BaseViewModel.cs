using DataAccess;
using DataAccessLib.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace VaxineApp.ViewModels.Base
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected DbContext Data = new DbContext();
        protected DbService DataService = new DbService();

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisedPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
