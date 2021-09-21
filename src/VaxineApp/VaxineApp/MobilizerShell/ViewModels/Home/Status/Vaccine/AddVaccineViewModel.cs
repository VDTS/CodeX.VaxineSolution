using Newtonsoft.Json;
using System;
using System.Windows.Input;
using Utility.Validations;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.MobilizerShell.ViewModels.Home.Status.Vaccine
{
    public class AddVaccineViewModel : ViewModelBase
    {
        // Property
        private VaccineModel? vaccine;
        public VaccineModel? Vaccine
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

        readonly ChildModel Child;

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
            if (Vaccine != null)
            {
                if (VaccinePeriodValidator.IsPeriodAvailable(Vaccine.VaccinePeriod))
                {
                    // if condition to validate that child haven't eat vaccine
                    Vaccine.Id = Guid.NewGuid();
                    Vaccine.RegisteredBy = Guid.Parse(Preferences.Get("UserId", ""));

                    var time = DateTime.Now;
                    DateTime dateTime = new DateTime(Vaccine.VaccinePeriod.Year, Vaccine.VaccinePeriod.Month, Vaccine.VaccinePeriod.Day, time.Hour, time.Minute, time.Second, DateTimeKind.Utc);

                    Vaccine.VaccinePeriod = dateTime;

                    var jData = JsonConvert.SerializeObject(Vaccine);

                    string postResponse = await DataService.Post(jData, $"Vaccine/{Child.Id}");
                    if (postResponse == "ConnectionError")
                    {
                        StandardMessagesDisplay.NoConnectionToast();
                    }
                    else if (postResponse == "Error")
                    {
                        StandardMessagesDisplay.Error();
                    }
                    else if (postResponse == "ErrorTracked")
                    {
                        StandardMessagesDisplay.ErrorTracked();
                    }
                    else
                    {
                        StandardMessagesDisplay.AddDisplayMessage(Vaccine.VaccineStatus);
                        var route = "..";
                        await Shell.Current.GoToAsync(route);
                    }

                }
                else
                {
                    StandardMessagesDisplay.PeriodNotAvailable();
                }
            }
        }
    }
}
