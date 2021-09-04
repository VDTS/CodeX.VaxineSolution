using DataAccessLib.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
                    var signInResponse = JsonConvert.DeserializeObject<JObject>(jSignInResponse);
                    var localId = signInResponse.GetValue("localId").ToString();
                    
                    Preferences.Set("UserLocalId", localId);
                    Preferences.Set("ProfileEmail", signInResponse.GetValue("email").ToString());

                    if (RememberMe == true)
                    {
                        await Xamarin.Essentials.SecureStorage.SetAsync("isLogged", "1");
                    }

                    LoadProfile(localId);
                    await LoadCluster();
                    await LoadTeam();
                    await LoadVaccinePeriod();
                    await GoToApp();
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
                var data = JsonConvert.DeserializeObject<ProfileModel>(jData);

                if (data.LocalId == Preferences.Get("UserLocalId", ""))
                {
                    Preferences.Set("ClusterId", data.ClusterId);
                    Preferences.Set("TeamId", data.TeamId);
                    Preferences.Set("UserId", data.Id.ToString());
                    Preferences.Set("UserName", data.FullName);
                    Preferences.Set("UserRole", data.Role);
                    await Xamarin.Essentials.SecureStorage.SetAsync("Role", data.Role);
                }

            }
        }

        private static async Task GoToApp()
        {
            var role = await Xamarin.Essentials.SecureStorage.GetAsync("Role");
            if (role == "Mobilizer")
            {
                Application.Current.MainPage = new AppShell();
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
                var data = JsonConvert.DeserializeObject<Dictionary<string, VaccinePeriods>>(jData);
                foreach (KeyValuePair<string, VaccinePeriods> item in data)
                {
                    if (item.Value.Id.ToString() == Preferences.Get("VaccinePeriodId", "").ToString())
                    {
                        Preferences.Set("PeriodStartDate", item.Value.StartDate);
                        Preferences.Set("PeriodEndDate", item.Value.EndDate);
                    }
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
                var data = JsonConvert.DeserializeObject<Dictionary<string, TeamModel>>(jData);
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
                var cluster = JsonConvert.DeserializeObject<Dictionary<string, ClusterModel>>(jData);
                foreach (KeyValuePair<string, ClusterModel> item in cluster)
                {
                    if (item.Value.Id.ToString() == Preferences.Get("ClusterId", ""))
                    {
                        Preferences.Set("VaccinePeriodId", item.Value.CurrentVaccinePeriodId.ToString());
                    }
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
