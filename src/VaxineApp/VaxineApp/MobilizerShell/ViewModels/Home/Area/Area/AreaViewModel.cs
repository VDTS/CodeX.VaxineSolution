using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using VaxineApp.Core.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.MobilizerShell.ViewModels.Home.Area.Area
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

        private TeamModel? team;
        public TeamModel? Team
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

        private string? clusterName;
        public string? ClusterName
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
            PullRefreshCommand = new AsyncCommand(Refresh);
        }

        private void SaveAsPDF(object obj)
        {
            StandardMessagesDisplay.FeatureUnderConstructionTitleDisplayMessage();
        }

        public async void Get()
        {
            var jData = await DataService.Get($"Team/{Preferences.Get("ClusterId", "")}");

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
                    var data = JsonConvert.DeserializeObject<Dictionary<string, TeamModel>>(jData);

                    if(data != null)
                    foreach (KeyValuePair<string, TeamModel> item in data)
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
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                    StandardMessagesDisplay.InputToast(ex.Message);
                }
            }
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

