using DataAccessLib;
using DataAccessLib.Databases;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.ViewModels.Base;
using VaxineApp.Views.Home.Status;
using VaxineApp.Views.Login.ForgotPassword;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Login
{
    public partial class LoginViewModel : BaseViewModel
    {
        #region Command
        public ICommand CloseAppCommand { private set; get; }
        public ICommand ForgotPasswordCommand { private set; get; }
        public ICommand SignInCommand { private set; get; }

        #endregion

        #region Properties

        IAuth auth;
        private bool _rememberMe;

        public bool RememberMe
        {
            get { return _rememberMe; }
            set
            {
                _rememberMe = value;
                RaisedPropertyChanged(nameof(RememberMe));
            }
        }

        private string _inputUserEmail;

        public string InputUserEmail
        {
            get { return _inputUserEmail; }
            set
            {
                _inputUserEmail = value;
                RaisedPropertyChanged(nameof(InputUserEmail));
            }
        }
        private string _inputUserPassword;

        public string InputUserPassword
        {
            get { return _inputUserPassword; }
            set
            {
                _inputUserPassword = value;
                RaisedPropertyChanged(nameof(InputUserPassword));
            }
        }
        private ProfileModel _profile;

        public ProfileModel Profile
        {
            get { return _profile; }
            set { _profile = value; }
        }

        #endregion

        #region Constructor
        public LoginViewModel()
        {
            auth = DependencyService.Get<IAuth>();

            // Commands init

            SignInCommand = new Command(SignIn);
            ForgotPasswordCommand = new Command(ForgotPassword);
            CloseAppCommand = new Command(CloseApp);

        }

        #endregion

        #region Methods
        private void CloseApp()
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
        private async void SignIn(object sender)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert("No internet", "Check you internet connection", "Ok");
            }
            else
            {
                try
                {
                    string Token = await auth.LoginWithEmailPassword(InputUserEmail, InputUserPassword);
                    if (Token != "")
                    {
                        LoadProfile(InputUserEmail);
                    }
                    else
                    {
                        ShowError();
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
            SqliteDataService sqliteDataService = new SqliteDataService();
            sqliteDataService.Initialize(email);
            var data = await DataService.Get($"Profile");
            var clinic = JsonConvert.DeserializeObject<Dictionary<string, ProfileModel>>(data);
            foreach (KeyValuePair<string, ProfileModel> item in clinic)
            {
                if (item.Value.Email == email)
                {
                    sqliteDataService.InsertData(new Data { Key = "Profile", Value = JsonConvert.SerializeObject(item.Value) });
                    Preferences.Set("ClusterId", item.Value.ClusterId);
                    Preferences.Set("TeamId", item.Value.TeamId);
                    Preferences.Set("UserId", item.Value.Id.ToString());
                }
            }
            if(RememberMe == true)
            {
                await Xamarin.Essentials.SecureStorage.SetAsync("isLogged", "1");
            }
            Application.Current.MainPage = new AppShell();
            await Shell.Current.GoToAsync($"//{nameof(StatusPage)}");
        }

        async private void ShowError()
        {
            await Application.Current.MainPage.DisplayAlert("Authentication failed!", "E-mail or password are incorrect. Try again!", "Cancel");
        }
        async private void ForgotPassword() 
        {
            var navigationPage = new NavigationPage(new ForgotPasswordPage());
            await App.Current.MainPage.Navigation.PushModalAsync(navigationPage, true);
        }
        #endregion

    }
}
