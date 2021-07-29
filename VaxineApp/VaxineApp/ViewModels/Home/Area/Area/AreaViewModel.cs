using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.Views.Home.Area.Area;
using Xamarin.Forms;
using Xamarin.CommunityToolkit.ObjectModel;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;

namespace VaxineApp.ViewModels.Home.Area.Area
{
    public class AreaViewModel : ViewModelBase
    {
        // Property

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

        private TeamModel team;
        public TeamModel Team
        {
            get
            {
                return team;
            }
            set
            {
                team = value;
                OnPropertyChanged();
            }
        }

        private string clusterName;
        public string ClusterName
        {
            get
            {
                return clusterName;
            }
            set
            {
                clusterName = value;
                OnPropertyChanged();
            }
        }


        // Commands
        public ICommand GoToPutPageCommand { private set; get; }
        public ICommand SaveAsPDFCommand { private set; get; }
        public ICommand PullRefreshCommand { private set; get; }

        // ctor
        public AreaViewModel()
        {
            // Property
            Team = new TeamModel();

            // Get
            Get();

            // Command
            SaveAsPDFCommand = new Command(SaveAsPDF);
            GoToPutPageCommand = new Command(GoToPutpage);
            PullRefreshCommand = new AsyncCommand(Refresh);
        }

        private async void GoToPutpage()
        {
            if (Team.TeamNo != null)
            {
                var jsonClinic = JsonConvert.SerializeObject(Team);
                var route = $"{nameof(EditAreaPage)}?Team={jsonClinic}";
                await Shell.Current.GoToAsync(route);
            }
            else
            {
                StandardMessagesDisplay.NoDataDisplayMessage();
            }
        }

        private void SaveAsPDF(object obj)
        {
            StandardMessagesDisplay.FeatureUnderConstructionTitleDisplayMessage();
        }

        public async void Get()
        {
            var data = await DataService.Get($"Team/{Preferences.Get("ClusterId", "")}");
            if (data != "null" & data != "Error")
            {
                var clinic = JsonConvert.DeserializeObject<Dictionary<string, TeamModel>>(data);
                foreach (KeyValuePair<string, TeamModel> item in clinic)
                {
                    if (item.Value.Id.ToString() == Preferences.Get("TeamId", "").ToString())
                    {
                        Team = new TeamModel
                        {
                            Id = item.Value.Id,
                            FId = item.Key.ToString(),
                            CHWName = item.Value.CHWName,
                            SocialMobilizerId = item.Value.SocialMobilizerId,
                            TeamNo = item.Value.TeamNo,
                            TotalChilds = item.Value.TotalChilds,
                            TotalClinics = item.Value.TotalClinics,
                            TotalDoctors = item.Value.TotalDoctors,
                            TotalHouseholds = item.Value.TotalHouseholds,
                            TotalInfluencers = item.Value.TotalInfluencers,
                            TotalMasjeeds = item.Value.TotalMasjeeds,
                            TotalSchools = item.Value.TotalSchools,
                            TotalGuestChilds = item.Value.TotalGuestChilds,
                            TotalRefugeeChilds = item.Value.TotalRefugeeChilds,
                            TotalReturnChilds = item.Value.TotalReturnChilds
                        };
                        StaticDataStore.TeamStats = Team;
                        Preferences.Set("TeamFId", Team.FId);
                    }
                }
            }
            else
            {
                StandardMessagesDisplay.NoDataDisplayMessage();
            }
        }

        public async void GoToPutPage()
        {
            var route = $"{nameof(EditAreaPage)}";
            await Shell.Current.GoToAsync(route);
        }
        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);
            Clear();
            Get();

            IsBusy = false;
        }
        void Clear()
        {
            Team = null;
        }
    }
}

