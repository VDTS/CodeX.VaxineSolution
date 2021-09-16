using Newtonsoft.Json;
using System;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.MobilizerShell.ViewModels.Home.Area.Influencer
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
                var jData = JsonConvert.SerializeObject(Influencer);

                string postResponse = await DataService.Post(jData, $"Influencer/{Preferences.Get("TeamId", "")}");

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
                    _ = await DataService.Put((++StaticDataStore.TeamStats.TotalInfluencers).ToString(), $"Team/{Preferences.Get("ClusterId", "")}/{Preferences.Get("TeamFId", "")}/TotalInfluencers");

                    StandardMessagesDisplay.AddDisplayMessage(Influencer.Name);

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
