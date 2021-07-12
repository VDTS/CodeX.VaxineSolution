using Newtonsoft.Json;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using VaxineApp.Views.Home.Area.School;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.School
{
    public class EditSchoolViewModel : ViewModelBase
    {
        // Validator
        SchoolValidator ValidationRules { get; set; }
        // Property
        private SchoolModel school;
        public SchoolModel School
        {
            get
            {
                return school;
            }
            set
            {
                school = value;
                OnPropertyChanged();
            }
        }

        // Command
        public ICommand PutCommand { private set; get; }
        public ICommand AddLocationCommand { private set; get; }

        public EditSchoolViewModel(SchoolModel school)
        {
            // Property
            School = school;
            ValidationRules = new SchoolValidator();

            // Command
            PutCommand = new Command(Put);
        }

        private async void Put(object obj)
        {
            var result = ValidationRules.Validate(School);
            if (result.IsValid)
            {
                var jsonData = JsonConvert.SerializeObject(School);
                var data = await DataService.Put(jsonData, $"School/{Preferences.Get("TeamId", "")}/{School.FId}");
                if (data == "Submit")
                {
                    StandardMessagesDisplay.EditDisplaymessage(School.SchoolName);
                    var route = $"//{nameof(SchoolPage)}";
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
