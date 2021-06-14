using System;
using DataAccessLib;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.ViewModels.Base;
using VaxineApp.Views.Help;
using VaxineApp.Views.Home;
using VaxineApp.Views.Home.Profile;
using VaxineApp.Views.Login;
using VaxineApp.Views.Settings;
using Xamarin.Essentials;
using Xamarin.Forms;
using DataAccessLib.Databases;
using Newtonsoft.Json;
using DataAccessLib.Models;
using System.Linq;
using VaxineApp.Views.Settings.Main;

namespace VaxineApp.ViewModels
{
    public class AppShellViewModel : BaseViewModel
    {
        public ICommand GoToProfileCommand { private set; get; }
        public ICommand GoToSettingsPageCommand { private set; get; }
        public ICommand LogginOutCommand { private set; get; }
        public ICommand GoToHelpPageCommand { private set; get; }
        public ICommand RemoveAccountCommand { private set; get; }
        public ICommand GoToDarkThemesPageCommand { private set; get; }
        private string _userName;
        public string UserName {
            get { return _userName; }
            set
            {
                _userName = value;
                RaisedPropertyChanged(nameof(UserName));
            }
        }
        private string _role;

        public string Role
        {
            get { return _role; }
            set
            {
                _role = value;
                RaisedPropertyChanged(nameof(Role));
            }
        }

        public AppShellViewModel()
        {
            GoToSettingsPageCommand = new Command(GoToSettingsPage);
            GoToProfileCommand = new Command(GoToProfile);
            LogginOutCommand = new Command(LogginOut);
            GoToHelpPageCommand = new Command(GoToHelpPage);
            RemoveAccountCommand = new Command(RemoveAccount);
            GoToDarkThemesPageCommand = new Command(GoToDarkThemesPage);
            SqliteDataService sqliteDataService = new SqliteDataService();
            sqliteDataService.Initialize(Preferences.Get("ProfileEmail", ""));
            var profileValue = sqliteDataService.Get("Profile");
            var profile = JsonConvert.DeserializeObject<ProfileModel>(profileValue);
            UserName = profile.FullName;
            Role = profile.Role;
        }

        private async void GoToDarkThemesPage()
        {
            await App.Current.MainPage.DisplayAlert("Not submitted!", "This functionality is under construction", "OK");
        }

        private async void GoToProfile(object obj)
        {
            var route = $"{nameof(ProfilePage)}";
            await Shell.Current.GoToAsync(route);
            Shell.Current.FlyoutIsPresented = false;
        }

        private async void GoToSettingsPage(object obj)
        {
            var route = $"{nameof(SettingsPage)}";
            await Shell.Current.GoToAsync(route);
            Shell.Current.FlyoutIsPresented = false;
        }

        private async void LogginOut(object obj)
        {
            await Xamarin.Essentials.SecureStorage.SetAsync("isLogged", "0");
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
        private async void RemoveAccount(object obj)
        {
            await App.Current.MainPage.DisplayAlert("Not submitted!", "Logout with Removing account and cache functionality is under construction", "OK");
        }
        private async void GoToHelpPage(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(HelpPage)}");
            Shell.Current.FlyoutIsPresented = false;
        }

    }
}
