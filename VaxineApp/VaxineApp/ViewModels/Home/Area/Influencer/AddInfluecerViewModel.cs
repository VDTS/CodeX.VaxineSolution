using Newtonsoft.Json;
using System;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using VaxineApp.Views.Home.Area.Influencer;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Influencer
{
    public class AddInfluecerViewModel : ViewModelBase
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
        public ICommand PostCommand { private set; get; }

        // ctor
        public AddInfluecerViewModel()
        {
            // Property
            Influencer = new InfluencerModel();
            ValidationRules = new InfluencerValidator();

            // Command
            PostCommand = new Command(Post);
        }

        public async void Post()
        {
            Influencer.Id = Guid.NewGuid();

            var result = ValidationRules.Validate(Influencer);
            if (result.IsValid)
            {
                var data = JsonConvert.SerializeObject(Influencer);

                string a = await DataService.Post(data, $"Influencer/{Preferences.Get("TeamId", "")}");
                if (a == "OK")
                {
                    StandardMessagesDisplay.AddDisplayMessage(Influencer.Name);
                }
                else
                {
                    StandardMessagesDisplay.CanceledDisplayMessage();
                }
                var route = $"//{nameof(InfluencerPage)}";
                await Shell.Current.GoToAsync(route);
            }
            else
            {
                StandardMessagesDisplay.ValidationRulesViolation(result.Errors[0].PropertyName, result.Errors[0].ErrorMessage);
            }
        }
    }
}
