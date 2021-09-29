using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using VaxineApp.Models.AccountModels;
using VaxineApp.Models.Enums;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using Xamarin.Forms;

namespace VaxineApp.AdminShell.ViewModels.Home.User
{
    public class EditUserViewModel : ViewModelBase
    {
        // Propery
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

        private string familyId;
        public string FamilyId
        {
            get
            {
                return familyId;
            }
            set
            {
                familyId = value;
                OnPropertyChanged();
            }
        }

        private string clusterId;
        public string ClusterId
        {
            get
            {
                return clusterId;
            }
            set
            {
                clusterId = value;
                OnPropertyChanged();
            }
        }

        private string teamId;
        public string TeamId
        {
            get
            {
                return teamId;
            }
            set
            {
                teamId = value;
                OnPropertyChanged();
            }
        }
        private string privateKeyJson;
        public FirebaseUserModel User { get; }
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

        public ICommand AddRoleClaimCommand { private set; get; }
        public EditUserViewModel(FirebaseUserModel user)
        {
            // Property
            User = user;

            privateKeyJson = JsonConvert.SerializeObject(privateKey);

            // Command
            AddRoleClaimCommand = new Command(AddRoleClaim);
        }

        public async void AddRoleClaim()
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
                await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(User.UId, claims);
                StandardMessagesDisplay.InputToast("Claims added");
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                StandardMessagesDisplay.InputToast(ex.Message);
            }
        }
        public async void AddFamilyIdClaims()
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
                { CustomClaimTypes.FamilyId, FamilyId }
            };

            // This will call the Firebase Auth server and set the custom claim for the user
            await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(User.UId, claims);
        }
        public async void AddTeamClaims()
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
                { CustomClaimTypes.TeamId, TeamId }
            };

            // This will call the Firebase Auth server and set the custom claim for the user
            await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(User.UId, claims);
        }
        public async void AddClusterClaims()
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
                { CustomClaimTypes.ClusterId, ClusterId }
            };

            // This will call the Firebase Auth server and set the custom claim for the user
            await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(User.UId, claims);
        }
    }
}
