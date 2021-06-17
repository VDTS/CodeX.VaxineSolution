using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.Views.Home;
using VaxineApp.Views.Home.Status;
using Xamarin.Forms;
using VaxineApp.ViewModels.Base;
using Newtonsoft.Json;
using VaxineApp.Views.Home.Family;
using Xamarin.Essentials;

namespace VaxineApp.ViewModels.Home.Family.Child
{
    public class EditChildViewModel : BaseViewModel
    {
        private ChildModel _child;

        public ChildModel Child
        {
            get { return _child; }
            set
            {
                _child = value;
                RaisedPropertyChanged(nameof(Child));
            }
        }

        public ICommand UpdateChildCommand { private set; get; }
        public EditChildViewModel(ChildModel child)
        {
            Child = child;
            UpdateChildCommand = new Command(UpdateChild);
        }

        private async void UpdateChild(object obj)
        {
            await App.Current.MainPage.DisplayAlert("Not submitted!", "This functionality is under construction", "OK");
        }
    }
}
