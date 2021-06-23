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

        public void Get()
        {
            sqliteDataCache.Initialize(Preferences.Get("ProfileEmail", ""));
            var profileValue = sqliteDataCache.Get("Profile");
            var profile = JsonConvert.DeserializeObject<ProfileModel>(profileValue);
            
            Profile = new ProfileModel
            {
                FullName = profile.FullName,
                Age = profile.Age,
                Email = profile.Email,
                FatherOrHusbandName = profile.FatherOrHusbandName,
                Gender = profile.Gender,
                Role = profile.Role,
                TeamId = profile.TeamId,
                ClusterId = profile.ClusterId
            };
        }
    }
}
