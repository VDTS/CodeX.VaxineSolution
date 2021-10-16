using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Utility.Generators;
using VaxineApp.AdminShell.Views.Home.User;
using VaxineApp.Core.Models.AccountModels;
using VaxineApp.Core.Models.Enums;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using Xamarin.Forms;

namespace VaxineApp.AdminShell.ViewModels.Home.User.UserClaims
{
    public class CreateUserViewModel : ViewModelBase
    {
        private string role;
        public string Role
        {
            get
            {
                return role;
            }
            set
            {
                role = value;
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
        private string privateKeyJson;
        string Uid;

        PrivateKeyModel privateKey = new PrivateKeyModel()
        {
            AuthProviderX509CertUrl = Constants.AuthProviderX509CertUrl,
            AuthUri = Constants.AuthUri,
            ClientEmail = Constants.ClientEmail,
            ClientId = Constants.ClientId,
            ClientX509CertUrl = Constants.ClientX509CertUrl,
            PrivateKey = Constants.PrivateKey,
            PrivateKeyId = Constants.PrivateKeyId,
            ProjectId = Constants.ProjectId,
            TokenUri = Constants.TokenUri,
            Type = Constants.Type

        };

        public CreateUserViewModel(string fullName, string email, string phoneNumber, string role)
        {
            FullName = fullName;
            Email = email;
            PhoneNumber = phoneNumber;
            Role = role;

            Password = PasswordGenerators.GeneratePassword();
            privateKeyJson = JsonConvert.SerializeObject(privateKey);

            CreateUserCommand = new Command(CreateUser);
        }
        public ICommand CreateUserCommand { private set; get; }

        private async void CreateUser(object obj)
        {
            await CreateUserMethod();
            await AddClaims();
        }

        private async Task AddClaims()
        {
            try
            {
                if (FirebaseApp.DefaultInstance == null)
                {
                    FirebaseApp.Create(new AppOptions()
                    {
                        Credential = GoogleCredential.FromJson(privateKeyJson)
                    });
                }

                // Create the custom user claim that has the role key
                var claims = new Dictionary<string, object>
                    {
                        { CustomClaimTypes.Role, Role }
                    };

                // This will call the Firebase Auth server and set the custom claim for the user
                await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(Uid, claims);
                StandardMessagesDisplay.InputToast("Claims added");

                // route to next
                string route = $"../../../../";
                await Shell.Current.GoToAsync(route);

            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                StandardMessagesDisplay.InputToast(ex.Message);
            }
        }

        private async Task CreateUserMethod()
        {
            try
            {
                UserRecordArgs args = new UserRecordArgs()
                {
                    Email = Email,
                    EmailVerified = false,
                    PhoneNumber = $"+{PhoneNumber}",
                    Password = Password,
                    DisplayName = FullName,
                    Disabled = false,
                    Uid = Guid.NewGuid().ToString()
                };
                UserRecord userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(args);
                StandardMessagesDisplay.UserAdded();
                Uid = args.Uid;
                StandardMessagesDisplay.InputToast("User added");
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                StandardMessagesDisplay.InputToast(ex.Message);
            }
        }
    }
}
