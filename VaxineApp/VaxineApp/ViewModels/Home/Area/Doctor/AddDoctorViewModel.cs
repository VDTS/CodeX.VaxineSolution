using Newtonsoft.Json;
using System;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using VaxineApp.Views.Home.Area.Doctor;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Doctor
{
    public class AddDoctorViewModel : ViewModelBase
    {
        // Validator
        DoctorValidator ValidationRules { get; set; }
        // Property
        private DoctorModel doctor;
        public DoctorModel Doctor
        {
            get
            {
                return doctor;
            }
            set
            {
                doctor = value;
                OnPropertyChanged();
            }
        }


        // Command
        public ICommand PostCommand { private set; get; }

        // ctor
        public AddDoctorViewModel()
        {
            // Property
            Doctor = new DoctorModel();
            ValidationRules = new DoctorValidator();

            // Command
            PostCommand = new Command(Post);
        }

        private async void Post()
        {
            Doctor.Id = Guid.NewGuid();

            var result = ValidationRules.Validate(Doctor);
            if (result.IsValid)
            {
                var jData = JsonConvert.SerializeObject(Doctor);

                string postResponse = await DataService.Post(jData, $"Doctor/{Preferences.Get("TeamId", "")}");

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
                    _ = await DataService.Put((++StaticDataStore.TeamStats.TotalDoctors).ToString(), $"Team/{Preferences.Get("ClusterId", "")}/{Preferences.Get("TeamFId", "")}/TotalDoctors");

                    StandardMessagesDisplay.AddDisplayMessage(Doctor.Name);

                    var route = "..";
                    await Shell.Current.GoToAsync(route);
                }

            }
            else
            {
                StandardMessagesDisplay.ValidationRulesViolation(result.Errors[0].PropertyName, result.Errors[0].ErrorMessage);
            }
        }
    }
}
