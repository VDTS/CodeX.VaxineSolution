using DataAccessLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.Views.Home.Profile;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Profile
{
    public class ProfileViewModel : ViewModelBase
    {
        // Property
        public string PrefUserEmail { get; set; }

        private ProfileModel profile;
        public ProfileModel Profile
        {
            get
            {
                return profile;
            }
            set
            {
                profile = value;
                OnPropertyChanged();
            }
        }


        // Command
        public ICommand GoToPutPageCommand { private set; get; }


        // ctor
        public ProfileViewModel()
        {
            // Property
            PrefUserEmail = Preferences.Get("PrefEmail", "");
            Profile = new ProfileModel();

            // Get
            Get();

            // Command
            GoToPutPageCommand = new Command(GoToPutPage);
        }

        private async void GoToPutPage()
        {
            var ProfileJson = JsonConvert.SerializeObject(Profile);
            var route = $"{nameof(EditProfile)}?Profile={ProfileJson}";
            await Shell.Current.GoToAsync(route);
        }

        public async void Get()
        {
            var data = await DataService.Get($"Profile");
            if (data != "null" && data != "Error")
            {
                var clinic = JsonConvert.DeserializeObject<Dictionary<string, ProfileModel>>(data);
                foreach (KeyValuePair<string, ProfileModel> item in clinic)
                {
                    if (item.Value.LocalId == Preferences.Get("UserLocalId", ""))
                    {
                        Profile = new ProfileModel
                        {
                            FId = item.Key.ToString(),
                            Id = item.Value.Id, 
                            FullName = item.Value.FullName,
                            Age = item.Value.Age,
                            FatherOrHusbandName = item.Value.FatherOrHusbandName,
                            Gender = item.Value.Gender
                        };
                    }
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error!", "Profile Not found", "OK");
            }
        }
    }
}
