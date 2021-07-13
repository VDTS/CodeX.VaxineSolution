using DataAccessLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
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
            var data = await DataService.Get($"Profile/{Preferences.Get("UserLocalId", "")}");

            if (data != "null" && data != "Error")
            {
                var clinic = JsonConvert.DeserializeObject<ProfileModel>(data);

                if (Preferences.Get("UserLocalId", "") == clinic.LocalId)
                {
                    Profile = new ProfileModel
                    {
                        ClusterId = clinic.ClusterId,
                        LocalId = clinic.LocalId,
                        Role = clinic.Role,
                        TeamId = clinic.TeamId,
                        Id = clinic.Id,
                        FullName = clinic.FullName,
                        Age = clinic.Age,
                        FatherOrHusbandName = clinic.FatherOrHusbandName,
                        Gender = clinic.Gender
                    };
                }
            }
            else
            {
                StandardMessagesDisplay.NoDataDisplayMessage();
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
