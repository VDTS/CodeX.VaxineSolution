using DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace VaxineApp.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected DbContext Data = new DbContext();
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisedPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
