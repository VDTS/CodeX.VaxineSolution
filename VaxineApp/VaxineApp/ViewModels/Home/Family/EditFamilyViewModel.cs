using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.ViewModels.Base;
using VaxineApp.Views.Home.Family;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Family
{
    public class EditFamilyViewModel : BaseViewModel
    {
        private GetFamilyModel _family;

        public GetFamilyModel Family
        {
            get { return _family; }
            set
            {
                _family = value;
                RaisedPropertyChanged(nameof(Family));
            }
        }


        // Commands

        public ICommand UpdateFamilyCommand { private set; get; }
        // Constructor
        public EditFamilyViewModel(GetFamilyModel family)
        {
            Family = family;
            UpdateFamilyCommand = new Command(UpdateFamily);
        }
        private async void UpdateFamily(object obj)
        {
            await App.Current.MainPage.DisplayAlert("Not submitted!", "This functionality is under construction", "OK");
        }
    }
}
