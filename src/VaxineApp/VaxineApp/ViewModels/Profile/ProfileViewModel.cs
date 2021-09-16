using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using VaxineApp.Views.Home.Profile;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Profile
{
    public class ProfileViewModel : ViewModelBase
    {
        // Property
        public string PrefUserEmail { get; set; }

        private ProfileModel profile;
        public ProfileModel Profile
        {
            get
            {
                return profile;
            }
            set
            {
                profile = value;
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
        public ICommand PullRefreshCommand { private set; get; }
        public ICommand GoToPutPageCommand { private set; get; }


        // ctor
        public ProfileViewModel()
        {
            // Property
            PrefUserEmail = Preferences.Get("PrefEmail", "");
            Profile = new ProfileModel();

            // Get
            Get();

            // Command
            GoToPutPageCommand = new Command(GoToPutPage);
            PullRefreshCommand = new Command(Refresh);
        }

        private async void GoToPutPage()
        {
            var ProfileJson = JsonConvert.SerializeObject(Profile);
            var route = $"{nameof(EditProfile)}?Profile={ProfileJson}";
            await Shell.Current.GoToAsync(route);
        }

        public async void Get()
        {
            var jData = await DataService.Get($"Profile/{Preferences.Get("UserLocalId", "")}");


            if (jData == "ConnectionError")
            {
                StandardMessagesDisplay.NoConnectionToast();
            }
            else if (jData == "null")
            {
                StandardMessagesDisplay.NoDataDisplayMessage();
            }
            else if (jData == "Error")
            {
                StandardMessagesDisplay.Error();
            }
            else if (jData == "ErrorTracked")
            {
                StandardMessagesDisplay.ErrorTracked();
            }
            else
            {
                var data = JsonConvert.DeserializeObject<ProfileModel>(jData);
                if (Preferences.Get("UserLocalId", "") == data.LocalId)
                {
                    Profile = new ProfileModel
                    {
                        ClusterId = data.ClusterId,
                        LocalId = data.LocalId,
                        Role = data.Role,
                        TeamId = data.TeamId,
                        Id = data.Id,
                        FullName = data.FullName,
                        Age = data.Age,
                        FatherOrHusbandName = data.FatherOrHusbandName,
                        Gender = data.Gender
                    };
                }
            }

        }

        public async void Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);
            Clear();
            Get();

            IsBusy = false;
        }
        public void Clear()
        {
            Profile = new ProfileModel();
        }
    }
}
