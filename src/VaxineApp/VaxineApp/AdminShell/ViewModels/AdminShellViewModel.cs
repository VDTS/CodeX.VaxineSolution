﻿using System;
using System.Windows.Input;
using VaxineApp.AccessShellDir.Views.AccessAppshell;
using VaxineApp.AccessShellDir.Views.Login;
using VaxineApp.AdminShell.Views.Announcements;
using VaxineApp.MVVMHelper;
using VaxineApp.Views.Help;
using VaxineApp.Views.Home.Profile;
using VaxineApp.Views.Settings.Main;
using VaxineApp.Views.Settings.Themes;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.AdminShell.ViewModels
{
    public class AdminShellViewModel : ViewModelBase
    {
        // Property
        private string? userName;
        public string? UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
                OnPropertyChanged();
            }
        }

        private string? role;
        public string? Role
        {
            get
            {
                return role;
            }
            set
            {
                role = value;
                OnPropertyChanged();
            }
        }

        // Command
        public ICommand GoToProfileCommand { private set; get; }
        public ICommand GoToSettingsPageCommand { private set; get; }
        public ICommand LogginOutCommand { private set; get; }
        public ICommand GoToHelpPageCommand { private set; get; }
        public ICommand RemoveAccountCommand { private set; get; }
        public ICommand GoToThemesPageCommand { private set; get; }
        public ICommand GoToAnnouncementsPageCommand { private set; get; }

        // ctor
        public AdminShellViewModel()
        {
            // Property
            UserName = Preferences.Get("UserName", "");
            Role = Preferences.Get("UserRole", "");

            // Command
            GoToSettingsPageCommand = new Command(GoToSettingsPage);
            GoToProfileCommand = new Command(GoToProfile);
            LogginOutCommand = new Command(LogginOut);
            GoToHelpPageCommand = new Command(GoToHelpPage);
            RemoveAccountCommand = new Command(RemoveAccount);
            GoToThemesPageCommand = new Command(GoToThemesPage);
            GoToAnnouncementsPageCommand = new Command(GoToAnnouncementsPage);
        }

        private async void GoToAnnouncementsPage(object obj)
        {
            var route = $"{nameof(AnnouncementsPage)}";
            await Shell.Current.GoToAsync(route);
            Shell.Current.FlyoutIsPresented = false;
        }

        private async void GoToThemesPage()
        {
            var route = $"{nameof(ThemesPage)}";
            await Shell.Current.GoToAsync(route);
            Shell.Current.FlyoutIsPresented = false;
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
            Xamarin.Essentials.SecureStorage.RemoveAll();
            Preferences.Clear();

            Application.Current.MainPage = new AccessShell();
            await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
        }
        private async void RemoveAccount(object obj)
        {
            var isDeleteAccount = await App.Current.MainPage.DisplayAlert("Do you want to remove cache", "This will cause deleting data that ain't synced with database", "Yes", "No");
            if (isDeleteAccount)
            {
                Preferences.Clear();
                await Xamarin.Essentials.SecureStorage.SetAsync("isLogged", "0");
                await Xamarin.Essentials.SecureStorage.SetAsync("role", "0");
                Application.Current.MainPage = new AccessShell();
                await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
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
            else
            {
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
