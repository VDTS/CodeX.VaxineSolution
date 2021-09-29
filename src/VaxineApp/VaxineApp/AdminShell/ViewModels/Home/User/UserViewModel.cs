using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using VaxineApp.AdminShell.Views.Home.User;
using VaxineApp.Models.AccountModels;
using VaxineApp.MVVMHelper;
using Xamarin.Forms;

namespace VaxineApp.AdminShell.ViewModels.Home.User
{
    public class UserViewModel : ViewModelBase
    {
        // Property
        private ObservableCollection<FirebaseUserModel> firebaseUsers;
        public ObservableCollection<FirebaseUserModel> FirebaseUsers
        {
            get
            {
                return firebaseUsers;
            }
            set
            {
                firebaseUsers = value;
                OnPropertyChanged();
            }
        }

        private FirebaseUserModel selectedUser;
        public FirebaseUserModel SelectedUser
        {
            get
            {
                return selectedUser;
            }
            set
            {
                selectedUser = value;
                OnPropertyChanged();
            }
        }


        private bool isBusy;
        public bool IsBusy
        {

            get
            {
                return isBusy;
            }
            set
            {
                isBusy = value;
                OnPropertyChanged();
            }
        }

        // Command
        public ICommand GoToPostPageCommand { private set; get; }
        public ICommand SelectionCommand { private set; get; }
        public ICommand PullRefreshCommand { private set; get; }
        public UserViewModel()
        {
            // Propery
            FirebaseUsers = new ObservableCollection<FirebaseUserModel>();

            // Command
            SelectionCommand = new Command(GoToDetailsPage);
            PullRefreshCommand = new Command(Refresh);
            GoToPostPageCommand = new Command(GoToPostPage);
        }
        public async void Get()
        {
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
            var privateKeyJson = JsonConvert.SerializeObject(privateKey);

            if (FirebaseApp.DefaultInstance == null)
            {
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromJson(privateKeyJson)
                });
            }

            // Start listing users from the beginning, 1000 at a time.
            var pagedEnumerable = FirebaseAuth.DefaultInstance.ListUsersAsync(null);
            var responses = pagedEnumerable.AsRawResponses().GetAsyncEnumerator();
            while (await responses.MoveNextAsync())
            {
                ExportedUserRecords response = responses.Current;
                foreach (ExportedUserRecord user in response.Users)
                {
                    FirebaseUsers.Add(
                        new FirebaseUserModel
                        {
                            UId = user.Uid,
                            DisplayName = user.DisplayName,
                            Email = user.Email,
                            Role = user?.CustomClaims["Role"]?.ToString(),
                            EmailVerified = user.EmailVerified
                        }
                        );
                }
            }
        }

        public async void GoToDetailsPage()
        {
            if (SelectedUser == null)
            {
                return;
            }
            else
            {
                var JsonSelectedFamily = JsonConvert.SerializeObject(SelectedUser);
                var route = $"{nameof(UserDetailsPage)}?User={JsonSelectedFamily}";
                await Shell.Current.GoToAsync(route);

                SelectedUser = null;
            }
        }

        public async void GoToPostPage()
        {
            var route = $"{nameof(AddUserPage)}";
            await Shell.Current.GoToAsync(route);
        }

        public void Clear()
        {
            FirebaseUsers?.Clear();
        }
        public async void Refresh()
        {
            IsBusy = true;

            Clear();
            Get();
            await Task.Delay(2000);

            IsBusy = false;
        }
    }
}
