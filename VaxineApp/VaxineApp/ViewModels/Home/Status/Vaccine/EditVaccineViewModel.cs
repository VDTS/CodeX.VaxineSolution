using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Models;
using Xamarin.Forms;
using VaxineApp.Views.Home.Status;
using VaxineApp.Views.Home.Family;
using Newtonsoft.Json;
using Xamarin.Essentials;
using VaxineApp.MVVMHelper;

namespace VaxineApp.ViewModels.Home.Status.Vaccine
{
    public class EditVaccineViewModel : ViewModelBase
    {
        // Property
        private VaccineModel vaccine;
        public VaccineModel Vaccine
        {
            get
            {
                return vaccine;
            }
            set
            {
                vaccine = value;
                OnPropertyChanged();
            }
        }

        private Guid ChildId;

        // Command
        public ICommand PutCommand { private set; get; }

        // ctor
        public EditVaccineViewModel(VaccineModel vaccine, Guid childId)
        {
            // Property
            Vaccine = vaccine;
            ChildId = childId;

            // Command
            PutCommand = new Command(Put);
        }


        // Method
        private async void Put()
        {
            // Changing date to UTC time
            Vaccine.VaccinePeriod.ToFileTimeUtc();
            var jsonData = JsonConvert.SerializeObject(Vaccine);
            var data = await DataService.Put(jsonData, $"Vaccine/{ChildId}/{Vaccine.FId}");
            if (data == "Submit")
            {
                await App.Current.MainPage.DisplayAlert("Updated", $"item has been updated", "OK");
                var route = $"//{nameof(StatusPage)}";
                await Shell.Current.GoToAsync(route);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Not Updated", "Try again", "OK");
            }
        }
    }
}
