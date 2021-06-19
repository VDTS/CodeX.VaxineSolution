using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.ViewModels.Base;
using VaxineApp.Views.Home.Area.Clinic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Clinic
{
    public class EditClinicViewModel : BaseViewModel
    {
        private ClinicModel _clinic;
        public ClinicModel Clinic
        {
            get { return _clinic; }
            set
            {
                _clinic = value;
                RaisedPropertyChanged(nameof(Clinic));
            }
        }
        public ICommand UpdateClinicCommand { private set; get; }

        public EditClinicViewModel(ClinicModel clinic)
        {
            Clinic = clinic;
            UpdateClinicCommand = new Command(UpdateClinic);
        }

        private async void UpdateClinic(object obj)
        {
            Clinic.ClinicName = "New";
            var jsonData = JsonConvert.SerializeObject(Clinic);
            var data = await DataService.Put(jsonData, $"Clinic/{Preferences.Get("TeamId", "")}/{Clinic.FId}");
            if (data == "Not updated")
            {
                await App.Current.MainPage.DisplayAlert("Updated", $"item has been updated", "OK");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Not Updated", "Try again", "OK");
            }

        }
    }
}
