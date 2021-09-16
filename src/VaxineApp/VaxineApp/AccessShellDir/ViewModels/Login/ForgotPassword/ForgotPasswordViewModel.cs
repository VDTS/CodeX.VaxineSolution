using DataAccess.Services;
using System;
using System.Windows.Input;
using VaxineApp.AccessShellDir.Views.AccessAppshell;
using VaxineApp.AccessShellDir.Views.Login;
using VaxineApp.MVVMHelper;
using Xamarin.Forms;

namespace VaxineApp.AccessShellDir.ViewModels.Login.ForgotPassword
{
    public class ForgotPasswordViewModel : ViewModelBase
    {
        // Property
        private string email;
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        // Command
        public ICommand ResetPasswordByEmailCommand { private set; get; }
        public ForgotPasswordViewModel()
        {
            ResetPasswordByEmailCommand = new Command(ResetPasswordByEmail);
        }

        private async void ResetPasswordByEmail()
        {
            try
            {
                Auth Account = new Auth(Constants.FirebaseApiKey);
                var a = await Account.SendPasswordResetcode(Email);

                if (a == "OK")
                {
                    await App.Current.MainPage.DisplayAlert("Reset Email Sent", $"Check your email: {Email}", "OK");
                    Application.Current.MainPage = new AccessShell();
                    await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Try again later", "Verification code has failed", "OK");
                }
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Try again later", "Verification code has failed", "OK");
            }
        }
    }
}
