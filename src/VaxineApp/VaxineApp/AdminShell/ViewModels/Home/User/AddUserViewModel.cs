using FirebaseAdmin.Auth;
using Microsoft.AppCenter.Crashes;
using System;
using System.Windows.Input;
using Utility.Generators;
using VaxineApp.AdminShell.Views.Home.User;
using VaxineApp.AdminShell.Views.Home.User.UserClaims;
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

        public ICommand NextRolePageCommand { private set; get; }
        public AddUserViewModel()
        {
            // Command
            NextRolePageCommand = new Command(NextRolePage);
        }

        private async void NextRolePage(object obj)
        {
            string route = $"{nameof(AddUserRolePage)}?FullName={FullName}&Email={Email}&PhoneNumber={PhoneNumber}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
