using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using VaxineApp.AccessShellDir.Views.Login.ForgotPassword;
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

        private bool isNextPageVisible;
        public bool IsNextPageVisible
        {
            get
            {
                return isNextPageVisible;
            }
            set
            {
                isNextPageVisible = value;
                OnPropertyChanged();
            }
        }

        // Command
        public ICommand ResetPasswordByEmailCommand { private set; get; }
        public ICommand ResetPasswordByPhoneNumberCommand { private set; get; }
        public ICommand ValidateEmailOrPhoneCommand { private set; get; }
        public ForgotPasswordViewModel()
        {
            IsNextPageVisible = false;
            ValidateEmailOrPhoneCommand = new Command(ValidateEmailOrPhone);
            ResetPasswordByEmailCommand = new Command(ResetPasswordByEmail);
            ResetPasswordByPhoneNumberCommand = new Command(ResetPasswordByPhoneNumber);
        }

        private async void ResetPasswordByPhoneNumber(object obj)
        {
            var navigationPage = new NavigationPage(new ResetByPhoneNumber());
            await App.Current.MainPage.Navigation.PushModalAsync(navigationPage, true);
            
        }

        private async void ResetPasswordByEmail(object obj)
        {
            var navigationPage = new NavigationPage(new ResetByEmail());
            await App.Current.MainPage.Navigation.PushModalAsync(navigationPage, true);
        }

        private void ValidateEmailOrPhone(object obj)
        {
            if (!string.IsNullOrEmpty(Email))
            {
                string emailRegex = @"^([\w\. \-]+)@([\w\-]+)((\.(\w){2,3})+)$";
                bool isMatched = Regex.IsMatch(Email, emailRegex);
                if (isMatched)
                {
                    IsNextPageVisible = true;
                }
                else
                {
                    IsNextPageVisible = false;
                }
            }
        }
    }
}
