using Newtonsoft.Json;
using System;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using VaxineApp.Views.Home.Area.Clinic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Clinic
{
    public class AddClinicViewModel : ViewModelBase
    {
        ClinicValidator ValidationRules { get; set; }
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

        // Commands
        public ICommand PostCommand { private set; get; }

        // ctor
        public AddClinicViewModel()
        {
            // Property
            Clinic = new ClinicModel();
            ValidationRules = new ClinicValidator();

            // Command
            PostCommand = new Command(Post);
        }

        public async void Post()
        {
            var result = ValidationRules.Validate(Clinic);
            Clinic.Id = new Guid();
            
            if (result.IsValid)
            {
                var jData = JsonConvert.SerializeObject(Clinic);

                string postResponse = await DataService.Post(jData, $"Clinic/{Preferences.Get("TeamId", "")}");

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
                    string b = await DataService.Put((++StaticDataStore.TeamStats.TotalClinics).ToString(), $"Team/{Preferences.Get("ClusterId","")}/{Preferences.Get("TeamFId", "")}/TotalClinics");
                    StandardMessagesDisplay.AddDisplayMessage(Clinic.ClinicName);

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
