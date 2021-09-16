using Newtonsoft.Json;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.MobilizerShell.ViewModels.Home.Area.Doctor
{
    public class EditDoctorViewModel : ViewModelBase
    {
        // Validator
        DoctorValidator ValidationRules { get; set; }
        // Propery
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
        public ICommand PutCommand { private set; get; }
        public EditDoctorViewModel(DoctorModel doctor)
        {
            // Property
            Doctor = doctor;
            ValidationRules = new DoctorValidator();

            // Command
            PutCommand = new Command(Put);
        }

        private async void Put()
        {
            var result = ValidationRules.Validate(Doctor);
            if (result.IsValid)
            {
                var jsonData = JsonConvert.SerializeObject(Doctor);
                var data = await DataService.Put(jsonData, $"Doctor/{Preferences.Get("TeamId", "")}/{Doctor.FId}");
                if (data == "Submit")
                {
                    StandardMessagesDisplay.EditDisplaymessage(Doctor.Name);
                    var route = "..";
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
