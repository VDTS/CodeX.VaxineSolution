using Newtonsoft.Json;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.MobilizerShell.ViewModels.Home.Area.Masjeed
{
    public class EidtMasjeedViewModel : ViewModelBase
    {
        // Validator
        MasjeedValidator ValidationRules { get; set; }
        // Property
        private MasjeedModel masjeed;
        public MasjeedModel Masjeed
        {
            get
            {
                return masjeed;
            }
            set
            {
                masjeed = value;
                OnPropertyChanged();
            }
        }

        // Command
        public ICommand PutCommand { private set; get; }
        public ICommand AddLocationCommand { private set; get; }

        // ctor
        public EidtMasjeedViewModel(MasjeedModel masjeed)
        {
            // Property
            Masjeed = masjeed;
            ValidationRules = new MasjeedValidator();

            // Command
            PutCommand = new Command(Put);
        }

        public async void Put()
        {
            var result = ValidationRules.Validate(Masjeed);
            if (result.IsValid)
            {
                var jsonData = JsonConvert.SerializeObject(Masjeed);
                var data = await DataService.Put(jsonData, $"Masjeed/{Preferences.Get("TeamId", "")}/{Masjeed.FId}");
                if (data == "Submit")
                {
                    StandardMessagesDisplay.EditDisplaymessage(Masjeed.MasjeedName);
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
