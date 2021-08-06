using Newtonsoft.Json;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using VaxineApp.Views.Home.Area.Influencer;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Influencer
{
    public class EditInfluecerViewModel : ViewModelBase
    {
        // Validator
        InfluencerValidator ValidationRules { get; set; }
        // Property
        private InfluencerModel influencer;
        public InfluencerModel Influencer
        {
            get
            {
                return influencer;
            }
            set
            {
                influencer = value;
                OnPropertyChanged();
            }
        }

        // Command
        public ICommand PutCommand { private set; get; }

        public EditInfluecerViewModel(InfluencerModel influencer)
        {
            // Property
            Influencer = influencer;
            ValidationRules = new InfluencerValidator();

            // Command
            PutCommand = new Command(Put);
        }

        public async void Put()
        {
            var result = ValidationRules.Validate(Influencer);
            if (result.IsValid)
            {
                var jsonData = JsonConvert.SerializeObject(Influencer);
                var data = await DataService.Put(jsonData, $"Influencer/{Preferences.Get("TeamId", "")}/{Influencer.FId}");
                if (data == "Submit")
                {
                    StandardMessagesDisplay.EditDisplaymessage(influencer.Name);
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
