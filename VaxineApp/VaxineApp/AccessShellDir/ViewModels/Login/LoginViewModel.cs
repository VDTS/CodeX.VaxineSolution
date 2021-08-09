﻿using DataAccessLib.Account;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.AccessShellDir.ViewModels.Login.Commands;
using VaxineApp.AccessShellDir.Views.Login;
using VaxineApp.AccessShellDir.Views.Login.ForgotPassword;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.ParentShellDir.Views.Home;
using VaxineApp.RealCacheLib;
using VaxineApp.StaticData;
using VaxineApp.Views.Home.Profile;
using VaxineApp.Views.Home.Status;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.AccessShellDir.ViewModels.Login
{
    public partial class LoginViewModel : ViewModelBase
    {
        // Property
        private TeamModel team;
        public TeamModel Team
        {
            get
            {
                return team;
            }
            set
            {
                team = value;
                OnPropertyChanged();
            }
        }
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
            SignInCommand = new SignInCommand(this);
            ForgotPasswordCommand = new Command(ForgotPassword);
            CloseAppCommand = new Command(CloseApp);
        }

        private void CloseApp()
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        public async void SignIn(string userName, string userPassword)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                StandardMessagesDisplay.NoConnectionToast();
            }
            else
            {
                try
                {
                    string Token = await Account.SignIn(userName, userPassword);
                    if (Token != "Error" && Token != "null")
                    {
                        Preferences.Set("ProfileEmail", InputUserEmail.ToLower());

                        LoadProfile(Preferences.Get("UserLocalId", ""));
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

        private async void LoadProfile(string localId)
        {

            var data = await DataService.Get($"Profile/{localId}");
            if (data != "Error" && data != "null")
            {
                var clinic = JsonConvert.DeserializeObject<ProfileModel>(data);

                if (clinic.LocalId == Preferences.Get("UserLocalId", ""))
                {
                    Preferences.Set("ClusterId", clinic.ClusterId);
                    Preferences.Set("TeamId", clinic.TeamId);
                    Preferences.Set("UserId", clinic.Id.ToString());
                    Preferences.Set("UserName", clinic.FullName);
                    Preferences.Set("UserRole", clinic.Role);
                    await Xamarin.Essentials.SecureStorage.SetAsync("Role", clinic.Role);
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



                var teamVar = await DataService.Get($"Team/{Preferences.Get("ClusterId", "")}");
                if (teamVar != "null" & teamVar != "Error")
                {
                    var teamData = JsonConvert.DeserializeObject<Dictionary<string, TeamModel>>(teamVar);
                    foreach (KeyValuePair<string, TeamModel> item in teamData)
                    {
                        if (item.Value.Id.ToString() == Preferences.Get("TeamId", "").ToString())
                        {
                            Team = new TeamModel
                            {
                                Id = item.Value.Id,
                                FId = item.Key.ToString(),
                                CHWName = item.Value.CHWName,
                                SocialMobilizerId = item.Value.SocialMobilizerId,
                                TeamNo = item.Value.TeamNo,
                                TotalChilds = item.Value.TotalChilds,
                                TotalClinics = item.Value.TotalClinics,
                                TotalDoctors = item.Value.TotalDoctors,
                                TotalHouseholds = item.Value.TotalHouseholds,
                                TotalInfluencers = item.Value.TotalInfluencers,
                                TotalMasjeeds = item.Value.TotalMasjeeds,
                                TotalSchools = item.Value.TotalSchools,
                                TotalGuestChilds = item.Value.TotalGuestChilds,
                                TotalRefugeeChilds = item.Value.TotalRefugeeChilds,
                                TotalReturnChilds = item.Value.TotalReturnChilds
                            };
                            StaticDataStore.TeamStats = Team;
                            Preferences.Set("TeamFId", Team.FId);
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
