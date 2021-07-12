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
                var data = JsonConvert.SerializeObject(Clinic);

                string a = await DataService.Post(data, $"Clinic/{Preferences.Get("TeamId", "")}");
                if (a == "OK")
                {
                    StandardMessagesDisplay.AddDisplayMessage(Clinic.ClinicName);

                    var route = $"//{nameof(ClinicPage)}";
                    await Shell.Current.GoToAsync(route);
                }
                else
                {
                    StandardMessagesDisplay.CanceledDisplayMessage();
                }
            }
            else
            {
                StandardMessagesDisplay.ValidationRulesViolation(result.Errors[0].PropertyName, result.Errors[0].ErrorMessage);
            }
        }
    }
}
