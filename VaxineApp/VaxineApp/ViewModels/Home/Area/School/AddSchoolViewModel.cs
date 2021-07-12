using Newtonsoft.Json;
using System;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using VaxineApp.Views.Home.Area.School;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.School
{
    public class AddSchoolViewModel : ViewModelBase
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
        public ICommand PostCommand { private set; get; }
        public ICommand AddLocationCommand { private set; get; }

        // ctor
        public AddSchoolViewModel()
        {
            // Property
            School = new SchoolModel();
            ValidationRules = new SchoolValidator();

            // Command
            PostCommand = new Command(Post);
        }

        private async void Post()
        {
            School.Id = Guid.NewGuid();

            var result = ValidationRules.Validate(School);
            if (result.IsValid)
            {
                var data = JsonConvert.SerializeObject(School);

                string a = await DataService.Post(data, $"School/{Preferences.Get("TeamId", "")}");
                if (a == "OK")
                {
                    StandardMessagesDisplay.AddDisplayMessage(School.SchoolName);
                }
                else
                {
                    StandardMessagesDisplay.CanceledDisplayMessage();
                }
                var route = $"//{nameof(SchoolPage)}";
                await Shell.Current.GoToAsync(route);
            }
            else
            {
                StandardMessagesDisplay.ValidationRulesViolation(result.Errors[0].PropertyName, result.Errors[0].ErrorMessage);
            }
        }
    }
}
