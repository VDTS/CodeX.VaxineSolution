using Newtonsoft.Json;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using VaxineApp.Views.Home.Family;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Family
{
    public class EditFamilyViewModel : ViewModelBase
    {
        // Validator
        FamilyValidator ValidationRules { get; set; }
        // Property
        private GetFamilyModel family;
        public GetFamilyModel Family
        {
            get
            {
                return family;
            }
            set
            {
                family = value;
                OnPropertyChanged();
            }
        }


        // Commands
        public ICommand PutCommand { private set; get; }
        // Constructor
        public EditFamilyViewModel(GetFamilyModel family)
        {
            // Property
            Family = family;
            ValidationRules = new FamilyValidator();

            // Command
            PutCommand = new Command(Put);
        }
        private async void Put()
        {
            var result = ValidationRules.Validate(Family);
            if (result.IsValid)
            {
                if (!StaticDataStore.FamilyNumbers.Contains(Family.HouseNo))
                {
                    var jsonData = JsonConvert.SerializeObject(Family);
                    var data = await DataService.Put(jsonData, $"Family/{Preferences.Get("TeamId", "")}/{Family.FId}");
                    if (data == "Submit")
                    {
                        StandardMessagesDisplay.EditDisplaymessage($"{Family.ParentName}'s Family ");
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
                    StandardMessagesDisplay.FamilyDuplicateValidator(Family.HouseNo);
                }
            }
            else
            {
                StandardMessagesDisplay.ValidationRulesViolation(result.Errors[0].PropertyName, result.Errors[0].ErrorMessage);
            }
        }
    }
}
