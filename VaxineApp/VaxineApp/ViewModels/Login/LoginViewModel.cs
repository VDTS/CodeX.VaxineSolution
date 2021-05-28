using DataAccess;
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
                    SharedData.Email = InputUserEmail;
                    GetProfile();
                    await Shell.Current.GoToAsync($"//{nameof(StatusPage)}");
                }
                else
                {
                    ShowError();
                }
            }
        }
        async private void ShowError()
        {
            await Application.Current.MainPage.DisplayAlert("Authentication failed!", "E-mail or password are incorrect. Try again!", "Cancel");
        }
        async private void ForgotPassword() { }
        public async void GetProfile()
        {
            var data = await Data.GetProfile();
            SharedData.Area = data.Area;
            SharedData.ClusterName = data.Cluster;
            SharedData.Team = data.Team;
            SharedData.FullName = data.FullName;
            SharedData.Role = data.Role;

            if (data != null)
            {
                Profile = new ProfileModel
                {
                    FullName = data.FullName,
                    Age = data.Age,
                    Email = data.Email,
                    FatherOrHusbandName = data.FatherOrHusbandName,
                    Gender = data.Gender,
                    Role = data.Role,
                    Team = data.Team,
                    Cluster = data.Cluster,
                    Area = data.Area
                };
            }
        }
        #endregion

    }
}
