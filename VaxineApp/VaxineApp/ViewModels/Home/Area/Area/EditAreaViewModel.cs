using Newtonsoft.Json;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using VaxineApp.Views.Home.Area.Area;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Area
{
    public class EditAreaViewModel : ViewModelBase
    {
        // Validator
        TeamValidator ValidationRules { get; set; }

        // Property
        public TeamModel Team { get; set; }

        // Command
        public ICommand PutCommand { get; private set; }

        // ctor
        public EditAreaViewModel(TeamModel team)
        {
            // Property
            Team = team;
            ValidationRules = new TeamValidator();


            // Command
            PutCommand = new Command(Put);
        }

        private async void Put(object obj)
        {
            var result = ValidationRules.Validate(Team);
            if (result.IsValid)
            {
                var jsonData = JsonConvert.SerializeObject(Team);
                var data = await DataService.Put(jsonData, $"Team/{Preferences.Get("ClusterId", "")}/{Team.FId}");
                if (data == "Submit")
                {
                    StandardMessagesDisplay.EditDisplaymessage(Team.CHWName);
                    var route = $"//{nameof(AreaPage)}";
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
