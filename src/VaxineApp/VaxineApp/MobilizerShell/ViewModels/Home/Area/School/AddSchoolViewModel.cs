using Newtonsoft.Json;
using System;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.MobilizerShell.ViewModels.Home.Area.School
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
                var jData = JsonConvert.SerializeObject(School);

                string postResponse = await DataService.Post(jData, $"School/{Preferences.Get("TeamId", "")}");
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
                    _ = await DataService.Put((++StaticDataStore.TeamStats.TotalSchools).ToString(), $"Team/{Preferences.Get("ClusterId", "")}/{Preferences.Get("TeamFId", "")}/TotalSchools");
                    StandardMessagesDisplay.AddDisplayMessage(School.SchoolName);

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
