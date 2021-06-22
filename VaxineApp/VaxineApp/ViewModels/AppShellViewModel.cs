﻿using System;
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
using VaxineApp.Views.Settings.Themes;

namespace VaxineApp.ViewModels
{
    public class AppShellViewModel : BaseViewModel
    {
        public ICommand GoToProfileCommand { private set; get; }
        public ICommand GoToSettingsPageCommand { private set; get; }
        public ICommand LogginOutCommand { private set; get; }
        public ICommand GoToHelpPageCommand { private set; get; }
        public ICommand RemoveAccountCommand { private set; get; }
        public ICommand GoToThemesPageCommand { private set; get; }
        private string _userName;
        public string UserName
        {
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
            GoToThemesPageCommand = new Command(GoToThemesPage);
            UserName = Preferences.Get("UserName", "");
            Role = Preferences.Get("Role","");
        }

        private async void GoToThemesPage()
        {
            var route = $"{nameof(ThemesPage)}";
            await Shell.Current.GoToAsync(route);
            Shell.Current.FlyoutIsPresented = false;
        }

        private async void GoToProfile(object obj)
        {
            var route = $"//{nameof(ProfilePage)}";
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
            var isDeleteAccount = await App.Current.MainPage.DisplayAlert("Do you want to remove cache", "This will cause deleting data that ain't synced with database", "Yes", "No");
            if (isDeleteAccount)
            {
                await Xamarin.Essentials.SecureStorage.SetAsync("isLogged", "0");
                await Xamarin.Essentials.SecureStorage.SetAsync("role", "0");
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                try
                {
                    var dataPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);

                    var cachePath = System.IO.Path.GetTempPath();

                    // If exist, delete the cache directory and everything in it recursivly
                    if (System.IO.Directory.Exists(cachePath))
                        System.IO.Directory.Delete(cachePath, true);

                    // If not exist, restore just the directory that was deleted
                    //if (!System.IO.Directory.Exists(cachePath))
                    //    System.IO.Directory.CreateDirectory(cachePath);

                    // If exist, delete the cache directory and everything in it recursivly
                    if (System.IO.Directory.Exists(dataPath))
                        System.IO.Directory.Delete(dataPath, true);

                    // If not exist, restore just the directory that was deleted
                    //if (!System.IO.Directory.Exists(dataPath))
                    //    System.IO.Directory.CreateDirectory(dataPath);
                }
                catch (Exception) { }
            }
            else {
                await App.Current.MainPage.DisplayAlert("Canceled", "If you want to logout, go click on logout", "OK");
            }

        }
        private async void GoToHelpPage(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(HelpPage)}");
            Shell.Current.FlyoutIsPresented = false;
        }

    }
}
