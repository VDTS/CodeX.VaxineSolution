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
                }
            }
            await Shell.Current.GoToAsync($"//{nameof(StatusPage)}");
        }

        async private void ShowError()
        {
            await Application.Current.MainPage.DisplayAlert("Authentication failed!", "E-mail or password are incorrect. Try again!", "Cancel");
        }
        async private void ForgotPassword() { }
        #endregion

    }
}
