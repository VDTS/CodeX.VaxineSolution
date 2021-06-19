using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.ViewModels.Base;
using VaxineApp.Views.Home.Area.Clinic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Clinic
{
    public class EditClinicViewModel : ViewModelBase
    {
        // Property
        private ClinicModel clinic;
        public ClinicModel Clinic
        {
            get
            {
                return clinic;
            }
            set
            {
                clinic = value;
                OnPropertyChanged();
            }
        }

        // Command
        public ICommand PutCommand { private set; get; }

        public EditClinicViewModel(ClinicModel clinic)
        {
            // Property
            Clinic = clinic;


            // Command
            PutCommand = new Command(Put);
        }

        public async void Put()
        {
            var jsonData = JsonConvert.SerializeObject(Clinic);
            var data = await DataService.Put(jsonData, $"Clinic/{Preferences.Get("TeamId", "")}/{Clinic.FId}");
            if (data == "Submit")
            {
                await App.Current.MainPage.DisplayAlert("Updated", $"item has been updated", "OK");
                var route = $"//{nameof(ClinicPage)}";
                await Shell.Current.GoToAsync(route);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Not Updated", "Try again", "OK");
            }
        }
    }
}
