using Newtonsoft.Json;
using System;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.Models.Enums;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using VaxineApp.Views.Home.Area.Masjeed;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Masjeed
{
    public class AddMasjeedViewModel : ViewModelBase
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
        public ICommand PostCommand { private set; get; }
        public ICommand AddLocationCommand { private set; get; }

        // ctor
        public AddMasjeedViewModel()
        {
            // Property
            Masjeed = new MasjeedModel();
            ValidationRules = new MasjeedValidator();

            // Command
            PostCommand = new Command(Post);
            AddLocationCommand = new Command(AddLocation);
        }

        private async void AddLocation()
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            if (location != null)
            {
                Masjeed.Longitude = location.Longitude;
                Masjeed.Latitude = location.Latitude;
            }
        }

        private async void Post()
        {
            Masjeed.Id = Guid.NewGuid();
            var result = ValidationRules.Validate(Masjeed);
            if (result.IsValid)
            {
                Masjeed.IsActive = IsActive.Active;
                var jData = JsonConvert.SerializeObject(Masjeed);

                string postResponse = await DataService.Post(jData, $"Masjeed/{Preferences.Get("TeamId", "")}");
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
                    _ = await DataService.Put((++StaticDataStore.TeamStats.TotalMasjeeds).ToString(), $"Team/{Preferences.Get("ClusterId", "")}/{Preferences.Get("TeamFId", "")}/TotalMasjeeds");
                    StandardMessagesDisplay.AddDisplayMessage(Masjeed.MasjeedName);

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
