using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Utility.Extensions;
using VaxineApp.AdminShell.Views.Home.Team;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using Xamarin.Forms;

namespace VaxineApp.AdminShell.ViewModels.Home.Team
{
    public class TeamViewModel : ViewModelBase
    {
        // Property
        private TeamModel selectedTeam;
        public TeamModel SelectedTeam
        {
            get
            {
                return selectedTeam;
            }
            set
            {
                selectedTeam = value;
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

        private ObservableCollection<TeamModel> teams;
        public ObservableCollection<TeamModel> Teams
        {
            get
            {
                return teams;
            }
            set
            {
                teams = value;
                OnPropertyChanged();
            }
        }

        // Commands
        public ICommand GoToPostPageCommand { private set; get; }
        public ICommand SaveAsPDFCommand { private set; get; }
        public ICommand PullRefreshCommand { private set; get; }
        public ICommand GoToDetailsPageCommand { private set; get; }

        // ctor
        public TeamViewModel()
        {
            // Property
            Teams = new ObservableCollection<TeamModel>();
            SelectedTeam = new TeamModel();

            // Get
            Get();


            // Command
            SaveAsPDFCommand = new Command(SaveAsPDF);
            PullRefreshCommand = new Command(Refresh);
            GoToPostPageCommand = new Command(GoToPostPage);
            GoToDetailsPageCommand = new Command(GoToDetailsPage);
        }

        public async void GoToDetailsPage()
        {
            if (SelectedTeam.AreEmpty())
            {
                return;
            }
            else
            {
                var SelectedItemJson = JsonConvert.SerializeObject(SelectedTeam);
                var route = $"{nameof(TeamDetailsPage)}?Team={SelectedItemJson}";
                await Shell.Current.GoToAsync(route);
                SelectedTeam = null;
            }
        }


        private void SaveAsPDF(object obj)
        {
            StandardMessagesDisplay.FeatureUnderConstructionTitleDisplayMessage();
        }

        public async void Get()
        {
            var jData = await DataService.Get("Team");

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
                var data = JsonConvert.DeserializeObject<Dictionary<string, TeamModel>>(jData);
                foreach (KeyValuePair<string, TeamModel> item in data)
                {
                    Teams.Add(
                        new TeamModel
                        {
                            Id = item.Value.Id,
                            FId = item.Key,
                            CHWName = item.Value.CHWName,
                            TeamNo = item.Value.TeamNo,
                            SocialMobilizerId = item.Value.SocialMobilizerId
                        }
                        );
                }
            }
        }

        async void GoToPostPage()
        {
            var route = $"{nameof(AddTeamPage)}";
            await Shell.Current.GoToAsync(route);
        }
        async void Refresh()
        {
            IsBusy = true;

            Clear();
            Get();
            await Task.Delay(2000);

            IsBusy = false;
        }

        void Clear()
        {
            Teams.Clear();
        }
    }
}
