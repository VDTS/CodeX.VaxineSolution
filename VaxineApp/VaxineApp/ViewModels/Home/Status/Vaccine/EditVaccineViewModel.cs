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
using VaxineApp.StaticData;

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
            var time = DateTime.Now;
            DateTime dateTime = new DateTime(Vaccine.VaccinePeriod.Year, Vaccine.VaccinePeriod.Month, Vaccine.VaccinePeriod.Day, time.Hour, time.Minute, time.Second, DateTimeKind.Utc);

            Vaccine.VaccinePeriod = dateTime;

            var jsonData = JsonConvert.SerializeObject(Vaccine);
            var data = await DataService.Put(jsonData, $"Vaccine/{ChildId}/{Vaccine.FId}");
            if (data == "Submit")
            {
                StandardMessagesDisplay.EditDisplaymessage(Vaccine.VaccineStatus);
                var route = $"//{nameof(StatusPage)}";
                await Shell.Current.GoToAsync(route);
            }
            else
            {
                StandardMessagesDisplay.CanceledDisplayMessage();
            }
        }
    }
}
