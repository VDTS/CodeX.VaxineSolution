using DataAccessLib.Account;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.AccessShellDir.Views.Login;
using VaxineApp.AccessShellDir.Views.Login.ForgotPassword;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.ParentShellDir.Views.Home;
using VaxineApp.RealCacheLib;
using VaxineApp.Views.Home.Profile;
using VaxineApp.Views.Home.Status;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.AccessShellDir.ViewModels.Login
{
    public partial class LoginViewModel : ViewModelBase
    {
        // Property
        private bool rememberMe;
        public bool RememberMe
        {
            get
            {
                return rememberMe;
            }
            set
            {
                rememberMe = value;
                OnPropertyChanged();
            }
        }

        private string inputUserEmail;
        public string InputUserEmail
        {
            get
            {
                return inputUserEmail;
            }
            set
            {
                inputUserEmail = value;
                OnPropertyChanged();
            }
        }

        private string inputUserPassword;
        public string InputUserPassword
        {
            get
            {
                return inputUserPassword;
            }
            set
            {
                inputUserPassword = value;
                OnPropertyChanged();
            }
        }

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

        public AccountManagement Account { get; set; }

        // Command
        public ICommand CloseAppCommand { private set; get; }
        public ICommand ForgotPasswordCommand { private set; get; }
        public ICommand SignInCommand { private set; get; }


        public LoginViewModel()
        {
            // Property
            Profile = new ProfileModel();
            Account = new AccountManagement();

            // Commands init
            SignInCommand = new Command(SignIn);
            ForgotPasswordCommand = new Command(ForgotPassword);
            CloseAppCommand = new Command(CloseApp);
        }

        private async void CloseApp()
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private async void SignIn()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert("No internet", "Check you internet connection", "Ok");
            }
            else
            {
                try
                {
                    string Token = await Account.SignIn(InputUserEmail, InputUserPassword);
                    if (Token != "Error" && Token != "null")
                    {
                        LoadProfile(InputUserEmail.ToLower()); ;
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Authentication failed!", "E-mail or password are incorrect. Try again!", "Cancel");
                    }
                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Error", ex.Message.ToString(), "OK");
                    return;
                }

            }
        }

        private async void LoadProfile(string email)
        {
            Preferences.Set("ProfileEmail", email);

            sqliteDataCache.Initialize(email);
            var data = await DataService.Get($"Profile");
            if (data != "Error" && data != "null")
            {
                var clinic = JsonConvert.DeserializeObject<Dictionary<string, ProfileModel>>(data);
                foreach (KeyValuePair<string, ProfileModel> item in clinic)
                {
                    if (item.Value.LocalId == Preferences.Get("UserLocalId", ""))
                    {
                        sqliteDataCache.InsertData(new Data { Key = "Profile", Value = JsonConvert.SerializeObject(item.Value) });
                        Preferences.Set("ClusterId", item.Value.ClusterId);
                        Preferences.Set("TeamId", item.Value.TeamId);
                        Preferences.Set("UserId", item.Value.Id.ToString());
                        await Xamarin.Essentials.SecureStorage.SetAsync("Role", item.Value.Role);
                    }
                }
                if (RememberMe == true)
                {
                    await Xamarin.Essentials.SecureStorage.SetAsync("isLogged", "1");
                }

                var clusterData = await DataService.Get($"Cluster");
                if (data != "Error" && data != "null")
                {
                    var cluster = JsonConvert.DeserializeObject<Dictionary<string, ClusterModel>>(clusterData);
                    foreach (KeyValuePair<string, ClusterModel> item in cluster)
                    {
                        if (item.Value.Id.ToString() == Preferences.Get("ClusterId", ""))
                        {
                            Preferences.Set("VaccinePeriodId", item.Value.CurrentVaccinePeriodId.ToString());
                        }
                    }
                }

                var VaccinePeriod = await DataService.Get("VaccinePeriods");
                if (data != "Error" && data != "null")
                {
                    var vaccine = JsonConvert.DeserializeObject<Dictionary<string, VaccinePeriods>>(VaccinePeriod);
                    foreach (KeyValuePair<string, VaccinePeriods> item in vaccine)
                    {
                        if (item.Value.Id.ToString() == Preferences.Get("VaccinePeriodId", "").ToString())
                        {
                            Preferences.Set("PeriodStartDate", item.Value.StartDate);
                            Preferences.Set("PeriodEndDate", item.Value.EndDate);
                        }
                    }
                }
                var role = await Xamarin.Essentials.SecureStorage.GetAsync("Role");
                if (role == "Mobilizer")
                {
                    Application.Current.MainPage = new AppShell();
                    await Shell.Current.GoToAsync($"//{nameof(StatusPage)}");
                }
                else if (role == "Supervisor")
                {
                    Application.Current.MainPage = new SupAppShell();
                    await Shell.Current.GoToAsync($"//{nameof(ProfilePage)}");
                }
                else if (role == "Parent")
                {
                    Application.Current.MainPage = new ParentAppShell();
                    await Shell.Current.GoToAsync($"//{nameof(FamilyPage)}");
                }
                else
                {
                    Application.Current.MainPage = new AccessShell();
                    await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error!", "Profile Not found", "OK");
            }
        }

        async private void ForgotPassword()
        {
            var navigationPage = new NavigationPage(new ForgotPasswordPage());
            await App.Current.MainPage.Navigation.PushModalAsync(navigationPage, true);
        }
    }
}
