using Newtonsoft.Json;
using System.Windows.Input;
using VaxineApp.AdminShell.Views.Home.User;
using VaxineApp.Models.AccountModels;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using Xamarin.Forms;

namespace VaxineApp.AdminShell.ViewModels.Home.User
{
    public class UserDetailsViewModel : ViewModelBase
    {
        // Property
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
        public FirebaseUserModel User { get; }

        

        private string privateKeyJson;

        // Command
        public ICommand DeleteCommand { private set; get; }
        public ICommand GoToPutPageCommand { private set; get; }
        public ICommand DisableCommand { private set; get; }

        public UserDetailsViewModel(FirebaseUserModel user)
        {
            // Property
            User = user;

            privateKeyJson = JsonConvert.SerializeObject(privateKey);

            // Command
            DeleteCommand = new Command(Delete);
            GoToPutPageCommand = new Command(GoToPutPage);
            DisableCommand = new Command(Disable);
        }

        

        private async void GoToPutPage()
        {
            if (User.UId != null)
            {
                var jsonUser = JsonConvert.SerializeObject(User);
                var route = $"{nameof(EditUserPage)}?User={jsonUser}";
                await Shell.Current.GoToAsync(route);
            }
            else
            {
                StandardMessagesDisplay.NoDataDisplayMessage();
            }
        }

        private async void Delete(object obj)
        {
        }
        private void Disable(object obj)
        {
        }
    }
}
