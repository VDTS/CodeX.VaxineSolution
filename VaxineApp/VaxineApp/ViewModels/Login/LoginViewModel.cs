using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Models;
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
            string Token = await auth.LoginWithEmailPassword(InputUserEmail, InputUserPassword);
            if (Token != "")
            {
                Preferences.Set("PrefEmail", InputUserEmail);
                GetProfile();
                await Shell.Current.GoToAsync($"//{nameof(StatusPage)}");
            }
            else
            {
                ShowError();
            }
        }
        async private void ShowError()
        {
            await Application.Current.MainPage.DisplayAlert("Authentication failed!", "E-mail or password are incorrect. Try again!", "Cancel");
        }
        async private void ForgotPassword() { }
        public async void GetProfile()
        {
            var data = await Data.GetProfile(Preferences.Get("PrefEmail", ""));

            if (data != null)
            {
                Profile = new ProfileModel
                {
                    FullName = data.FullName,
                    Age = data.Age,
                    ConfirmEmail = data.ConfirmEmail,
                    ConfirmPassword = data.ConfirmPassword,
                    Email = data.Email,
                    FatherOrHusbandName = data.FatherOrHusbandName,
                    Gender = data.Gender,
                    Password = data.Password,
                    Role = data.Role,
                    Team = data.Team,
                    Cluster = data.Cluster
                    
                };
                Preferences.Set("PrefFullName", Profile.FullName);
                Preferences.Set("PrefRole", Profile.Role);
                Preferences.Set("PrefTeam", Profile.Team);
                Preferences.Set("PrefCluster", Profile.Cluster);
            }
 }
        #endregion

    }
}
