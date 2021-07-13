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
using VaxineApp.Validations;

namespace VaxineApp.ViewModels.Home.Status.Vaccine
{
    public class AddVaccineViewModel : ViewModelBase
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

        ChildModel Child;

        // Command
        public ICommand PostCommand { private set; get; }

        public AddVaccineViewModel(ChildModel _child)
        {
            // Property
            Child = _child;
            Vaccine = new VaccineModel();


            // Command
            PostCommand = new Command(Post);
        }

        private async void Post(object obj)
        {
            if (VaccinePeriodValidator.IsPeriodAvailable(Vaccine.VaccinePeriod))
            {
                // if condition to validate that child haven't eat vaccine
                Vaccine.Id = Guid.NewGuid();
                Vaccine.RegisteredBy = Guid.Parse(Preferences.Get("UserId", ""));

                var time = DateTime.Now;
                DateTime dateTime = new DateTime(Vaccine.VaccinePeriod.Year, Vaccine.VaccinePeriod.Month, Vaccine.VaccinePeriod.Day, time.Hour, time.Minute, time.Second, DateTimeKind.Utc);

                Vaccine.VaccinePeriod = dateTime;

                var data = JsonConvert.SerializeObject(Vaccine);

                string a = await DataService.Post(data, $"Vaccine/{Child.Id}");
                if (a == "OK")
                {
                    StandardMessagesDisplay.AddDisplayMessage(Vaccine.VaccineStatus);
                }
                else
                {
                    StandardMessagesDisplay.CanceledDisplayMessage();
                }
                var route = $"//{nameof(StatusPage)}";
                await Shell.Current.GoToAsync(route);
            }
            else
            {
                StandardMessagesDisplay.PeriodNotAvailable();
            }
        }
    }
}
