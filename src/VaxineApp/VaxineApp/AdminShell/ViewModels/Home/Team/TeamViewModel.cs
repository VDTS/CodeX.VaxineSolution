using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Utility.Extensions;
using VaxineApp.AdminShell.Views.Home.Team;
using VaxineApp.Core.Models;
using VaxineApp.Core.Models.MixedModels;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using Xamarin.Forms;

namespace VaxineApp.AdminShell.ViewModels.Home.Team
{
    public class TeamViewModel : ViewModelBase
    {
        // Property
        private TeamModel? selectedTeam;
        public TeamModel? SelectedTeam
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

        private ObservableCollection<TeamsGroupedByClusterModel>? teamGroup;
        public ObservableCollection<TeamsGroupedByClusterModel>? TeamGroup
        {
            get
            {
                return teamGroup;
            }
            set
            {
                teamGroup = value;
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
            TeamGroup = new ObservableCollection<TeamsGroupedByClusterModel>();
            SelectedTeam = new TeamModel();

            // Get
            Get();


            // Command
            SaveAsPDFCommand = new Command(SaveAsPDF);
            PullRefreshCommand = new Command(Refresh);
            GoToPostPageCommand = new Command(GoToPostPage);
            GoToDetailsPageCommand = new Command(GoToDetailsPage);
        }

        private async void GoToDetailsPage()
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

        private async void Get()
        {
            var jData = await DataService.Get("Cluster");

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
                try
                {
                    var data = JsonConvert.DeserializeObject<Dictionary<string, ClusterModel>>(jData);

                    if (data != null)
                    {
                        foreach (KeyValuePair<string, ClusterModel> item in data)
                        {
                            var nestedJData = await DataService.Get($"Team/{item.Value.Id}");

                            if (nestedJData == "ConnectionError")
                            {
                                StandardMessagesDisplay.NoConnectionToast();
                            }
                            else if (nestedJData == "null")
                            {
                                StandardMessagesDisplay.NoDataDisplayMessage();
                            }
                            else if (nestedJData == "Error")
                            {
                                StandardMessagesDisplay.Error();
                            }
                            else if (nestedJData == "ErrorTracked")
                            {
                                StandardMessagesDisplay.ErrorTracked();
                            }
                            else
                            {
                                var nestedData = JsonConvert.DeserializeObject<Dictionary<string, TeamModel>>(nestedJData);
                                List<TeamModel> lp = new List<TeamModel>();

                                if (nestedData != null)
                                    foreach (KeyValuePair<string, TeamModel> item2 in nestedData)
                                    {
                                        lp.Add(
                                        new TeamModel
                                        {
                                            Id = item2.Value.Id,
                                            CHWName = item2.Value.CHWName,
                                            FId = item2.Value.FId,
                                            SocialMobilizerId = item2.Value.SocialMobilizerId,
                                            TeamNo = item2.Value.TeamNo

                                        });
                                    }
                                TeamGroup?.Add(new TeamsGroupedByClusterModel(item.Value.ClusterName, lp));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                    StandardMessagesDisplay.InputToast(ex.Message);
                }
            }
        }

        private async void GoToPostPage()
        {
            var route = $"{nameof(AddTeamPage)}";
            await Shell.Current.GoToAsync(route);
        }
        private async void Refresh()
        {
            IsBusy = true;

            Clear();
            Get();
            await Task.Delay(2000);

            IsBusy = false;
        }

        private void Clear()
        {
            TeamGroup?.Clear();
        }
    }
}
