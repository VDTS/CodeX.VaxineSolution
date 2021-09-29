using DataAccess.Services;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using VaxineApp.AccessShellDir.ViewModels.Login.Commands;
using VaxineApp.AccessShellDir.Views.AccessAppshell;
using VaxineApp.AccessShellDir.Views.Login.ForgotPassword;
using VaxineApp.AdminShell.Views.AdminAppShell;
using VaxineApp.MobilizerShell.Views.Appshell;
using VaxineApp.Models;
using VaxineApp.Models.Enums;
using VaxineApp.MVVMHelper;
using VaxineApp.ParentShellDir.Views.ParentAppshell;
using VaxineApp.StaticData;
using VaxineApp.SupervisorShellDir.Views.SupervisorAppshell;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.AccessShellDir.ViewModels.Login
{
    public partial class LoginViewModel : ViewModelBase
    {
        // Property
        private TeamModel? team;
        public TeamModel? Team
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

        private string? inputUserEmail;
        public string? InputUserEmail
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

        private string? inputUserPassword;
        public string? InputUserPassword
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

        private ProfileModel? profile;
        public ProfileModel? Profile
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

        public Auth Account { get; set; }

        // Command
        public ICommand CloseAppCommand { private set; get; }
        public ICommand ForgotPasswordCommand { private set; get; }
        public ICommand SignInCommand { private set; get; }


        public LoginViewModel()
        {
            // Property
            Profile = new ProfileModel();
            Account = new Auth(Constants.FirebaseApiKey);

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
                var jSignInResponse = await Account.SignIn(userName, userPassword);
                if (jSignInResponse.Contains("Error"))
                {
                    StandardMessagesDisplay.CommonToastMessage(jSignInResponse);
                }
                else if (jSignInResponse == "ConnectionError")
                {
                    StandardMessagesDisplay.NoConnectionToast();
                }
                else if (jSignInResponse == "ErrorTracked")
                {
                    StandardMessagesDisplay.ErrorTracked();
                }
                else
                {
                    try
                    {
                        var signInResponse = JsonConvert.DeserializeObject<JObject>(jSignInResponse);

                        if (signInResponse != null)
                        {
                            var localId = signInResponse?.GetValue("localId")?.ToString();
                            Preferences.Set("UserLocalId", localId);
                            Preferences.Set("ProfileEmail", signInResponse?.GetValue("email")?.ToString());

                            string accountInfoJson = await Account.AccountInfoLookup(signInResponse?.GetValue("idToken")?.ToString());

                            var accountInfo = JsonConvert.DeserializeObject<JObject>(accountInfoJson);

                            var claimsJson = accountInfo["users"][0]["customAttributes"]?.ToString();

                            var claims = JsonConvert.DeserializeObject<JObject>(claimsJson);

                            var role = claims.GetValue("Role")?.ToString();
                            Preferences.Set("UserRole", role);

                            if (RememberMe)
                            {
                                await Xamarin.Essentials.SecureStorage.SetAsync("isLogged", "1");
                            }

                            LoadProfile(Preferences.Get("UserLocalId", "").ToString());
                            await LoadCluster();
                            await LoadTeam();
                            await LoadVaccinePeriod();
                            GoToApp();
                        }
                    }
                    catch (Exception ex)
                    {
                        Crashes.TrackError(ex);
                        StandardMessagesDisplay.InputToast(ex.Message);
                    }
                    
                }
            }
        }

        private async void LoadProfile(string localId)
        {

            var jData = await DataService.Get($"Profile/{localId}");
            if (jData == "ConnectionError")
            {
                StandardMessagesDisplay.NoConnectionToast();
            }
            else if (jData == "null")
            {
                StandardMessagesDisplay.NoDataDisplayMessage();
            }
            else if (jData == "Error")
            {
                StandardMessagesDisplay.Error();
            }
            else if (jData == "ErrorTracked")
            {
                StandardMessagesDisplay.ErrorTracked();
            }
            else
            {
                try
                {
                    var data = JsonConvert.DeserializeObject<ProfileModel>(jData);

                    if (data != null)
                        if (data.LocalId == Preferences.Get("UserLocalId", ""))
                        {
                            Preferences.Set("ClusterId", data.ClusterId);
                            Preferences.Set("TeamId", data.TeamId);
                            Preferences.Set("UserId", data.Id.ToString());
                            Preferences.Set("UserName", data.FullName);
                            await Xamarin.Essentials.SecureStorage.SetAsync("Role", data.Role);
                        }
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                    StandardMessagesDisplay.InputToast(ex.Message);
                }

            }
        }

        private static void GoToApp()
        {
            Enum.TryParse(Xamarin.Essentials.SecureStorage.GetAsync("Role").Result, out Role role);


            if (role == Role.Mobilizer)
            {
                Application.Current.MainPage = new Mobilizerappshell();
            }
            else if (role == Role.Supervisor)
            {
                Application.Current.MainPage = new SupervisorShell();
            }
            else if (role == Role.Parent)
            {
                Application.Current.MainPage = new ParentShell();
            }
            else if (role == Role.Admin)
            {
                Application.Current.MainPage = new AdminAppShell();
            }
            else
            {
                Application.Current.MainPage = new AccessShell();
            }
        }

        private async Task LoadVaccinePeriod()
        {
            var jData = await DataService.Get("VaccinePeriods");
            if (jData == "ConnectionError")
            {
                StandardMessagesDisplay.NoConnectionToast();
            }
            else if (jData == "null")
            {
                StandardMessagesDisplay.NoDataDisplayMessage();
            }
            else if (jData == "Error")
            {
                StandardMessagesDisplay.Error();
            }
            else if (jData == "ErrorTracked")
            {
                StandardMessagesDisplay.ErrorTracked();
            }
            else
            {
                try
                {
                    var data = JsonConvert.DeserializeObject<Dictionary<string, VaccinePeriods>>(jData);

                    if (data != null)
                        foreach (KeyValuePair<string, VaccinePeriods> item in data)
                        {
                            if (item.Value.Id.ToString() == Preferences.Get("VaccinePeriodId", "").ToString())
                            {
                                Preferences.Set("PeriodStartDate", item.Value.StartDate);
                                Preferences.Set("PeriodEndDate", item.Value.EndDate);
                            }
                        }
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                    StandardMessagesDisplay.InputToast(ex.Message);
                }
            }
        }

        private async Task LoadTeam()
        {
            var jData = await DataService.Get($"Team/{Preferences.Get("ClusterId", "")}");
            if (jData == "ConnectionError")
            {
                StandardMessagesDisplay.NoConnectionToast();
            }
            else if (jData == "null")
            {
                StandardMessagesDisplay.NoDataDisplayMessage();
            }
            else if (jData == "Error")
            {
                StandardMessagesDisplay.Error();
            }
            else if (jData == "ErrorTracked")
            {
                StandardMessagesDisplay.ErrorTracked();
            }
            else
            {
                try
                {
                    var data = JsonConvert.DeserializeObject<Dictionary<string, TeamModel>>(jData);

                    if (data != null)
                        foreach (KeyValuePair<string, TeamModel> item in data)
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
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                    StandardMessagesDisplay.InputToast(ex.Message);
                }
            }
        }

        private async Task LoadCluster()
        {
            var jData = await DataService.Get($"Cluster");
            if (jData == "ConnectionError")
            {
                StandardMessagesDisplay.NoConnectionToast();
            }
            else if (jData == "null")
            {
                StandardMessagesDisplay.NoDataDisplayMessage();
            }
            else if (jData == "Error")
            {
                StandardMessagesDisplay.Error();
            }
            else if (jData == "ErrorTracked")
            {
                StandardMessagesDisplay.ErrorTracked();
            }
            else
            {
                try
                {
                    var cluster = JsonConvert.DeserializeObject<Dictionary<string, ClusterModel>>(jData);

                    if(cluster != null)
                    foreach (KeyValuePair<string, ClusterModel> item in cluster)
                    {
                        if (item.Value?.Id.ToString() == Preferences.Get("ClusterId", ""))
                        {
                            Preferences.Set("VaccinePeriodId", item.Value?.CurrentVaccinePeriodId?.ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                    StandardMessagesDisplay.InputToast(ex.Message);
                }
            }
        }

        async private void ForgotPassword()
        {
            var navigationPage = new NavigationPage(new ForgotPasswordPage());
            await App.Current.MainPage.Navigation.PushModalAsync(navigationPage, true);
        }
    }
}
