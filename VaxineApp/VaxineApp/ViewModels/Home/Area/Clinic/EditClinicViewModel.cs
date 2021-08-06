using Newtonsoft.Json;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using VaxineApp.Views.Home.Area.Clinic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Clinic
{
    public class EditClinicViewModel : ViewModelBase
    {
        // Validator
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

        // Command
        public ICommand PutCommand { private set; get; }
        public ICommand AddLocationCommand { private set; get; }

        public EditClinicViewModel(ClinicModel clinic)
        {
            // Property
            Clinic = clinic;
            ValidationRules = new ClinicValidator();


            // Command
            PutCommand = new Command(Put);
        }

        public async void Put()
        {
            var result = ValidationRules.Validate(Clinic);
            if (result.IsValid)
            {
                var jsonData = JsonConvert.SerializeObject(Clinic);
                var data = await DataService.Put(jsonData, $"Clinic/{Preferences.Get("TeamId", "")}/{Clinic.FId}");
                if (data == "Submit")
                {
                    StandardMessagesDisplay.EditDisplaymessage(Clinic.ClinicName);
                    var route = "..";
                    await Shell.Current.GoToAsync(route);
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Not Updated", "Try again", "OK");
                }
            }
            else
            {
                StandardMessagesDisplay.ValidationRulesViolation(result.Errors[0].PropertyName, result.Errors[0].ErrorMessage);
            }
        }
    }
}
