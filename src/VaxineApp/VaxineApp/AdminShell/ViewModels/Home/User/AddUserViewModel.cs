using FirebaseAdmin.Auth;
using Microsoft.AppCenter.Crashes;
using System;
using System.Windows.Input;
using Utility.Generators;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using Xamarin.Forms;

namespace VaxineApp.AdminShell.ViewModels.Home.User
{
    public class AddUserViewModel : ViewModelBase
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

        private string fullName;
        public string FullName
        {
            get
            {
                return fullName;
            }
            set
            {
                fullName = value;
                OnPropertyChanged();
            }
        }

        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        private string phoneNumber;
        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                phoneNumber = value;
                OnPropertyChanged();
            }
        }

        // Command

        public ICommand PostCommand { private set; get; }
        public AddUserViewModel()
        {
            // Command
            PostCommand = new Command(Post);
            Password = PasswordGenerator.GeneratePassword();
        }
        public async void Post()
        {
            try
            {
                UserRecordArgs args = new UserRecordArgs()
                {
                    Email = Email,
                    EmailVerified = false,
                    PhoneNumber = PhoneNumber,
                    Password = Password,
                    DisplayName = FullName,
                    Disabled = false,
                };
                UserRecord userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(args);
                StandardMessagesDisplay.UserAdded();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                StandardMessagesDisplay.InputToast(ex.Message);
            }
        }
    }
}
