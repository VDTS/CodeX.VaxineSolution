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

        
        private int totalHouseholds;
        public int TotalHouseholds
        {
            get
            {
                return totalHouseholds;
            }
            set
            {
                totalHouseholds = value;
                OnPropertyChanged();
            }
        }

        private int totalMasjeeds;
        public int TotalMasjeeds
        {
            get
            {
                return totalMasjeeds;
            }
            set
            {
                totalMasjeeds = value;
                OnPropertyChanged();
            }
        }

        private int totalSchools;
        public int TotalSchools
        {
            get
            {
                return totalSchools;
            }
            set
            {
                totalSchools = value;
                OnPropertyChanged();
            }
        }

        private int totalClinics;
        public int TotalClinics
        {
            get
            {
                return totalClinics;
            }
            set
            {
                totalClinics = value;
                OnPropertyChanged();
            }
        }

        private int totalDoctors;
        public int TotalDoctors
        {
            get
            {
                return totalDoctors;
            }
            set
            {
                totalDoctors = value;
                OnPropertyChanged();
            }
        }

        private int totalInfluencers;
        public int TotalInfluencers
        {
            get
            {
                return totalInfluencers;
            }
            set
            {
                totalInfluencers = value;
                OnPropertyChanged();
            }
        }

        private int totalChildren;
        public int TotalChildren
        {
            get
            {
                return totalChildren;
            }
            set
            {
                totalChildren = value;
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
            GetStat();

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

        private async void GetStat()
        {
            var data = await DataService.Get($"Family/{Preferences.Get("TeamId", "")}");
            if (data != "null" & data != "Error")
            {
                var clinic = JsonConvert.DeserializeObject<Dictionary<string, GetFamilyModel>>(data);
                int _children = 0;
                foreach (KeyValuePair<string, GetFamilyModel> item in clinic)
                {
                    var data2 = await DataService.Get($"Child/{item.Value.Id}");
                    if (data2 != "null" && data2 != "Error")
                    {
                        var clinic2 = JsonConvert.DeserializeObject<Dictionary<string, ChildModel>>(data2);
                        int _familyChildCount = 0;
                        foreach (KeyValuePair<string, ChildModel> item2 in clinic2)
                        {
                            _familyChildCount++;
                        }
                        _children += _familyChildCount;
                    }
                }

                TotalChildren = _children;
            }
            else
            {
                StandardMessagesDisplay.NoDataDisplayMessage();
            }


            var _familyData = await DataService.Get($"Family/{Preferences.Get("TeamId", "")}");
            if (_familyData != "null" && _familyData != "Error")
            {
                var _family = JsonConvert.DeserializeObject<Dictionary<string, ClinicModel>>(_familyData);
                TotalHouseholds = _family.Count;
            }

            var _masjeedData = await DataService.Get($"Masjeed/{Preferences.Get("TeamId", "")}");
            if (_masjeedData != "null" & _masjeedData != "Error")
            {
                var _masjeed = JsonConvert.DeserializeObject<Dictionary<string, ClinicModel>>(_masjeedData);
                TotalMasjeeds = _masjeed.Count;
            }

            var _schoolData = await DataService.Get($"School/{Preferences.Get("TeamId", "")}");
            if (_schoolData != "null" & _schoolData != "Error")
            {
                var _school = JsonConvert.DeserializeObject<Dictionary<string, ClinicModel>>(_schoolData);
                TotalSchools = _school.Count;
            }

            var _influencerData = await DataService.Get($"Influencer/{Preferences.Get("TeamId", "")}");
            if (_influencerData != "null" & _influencerData != "Error")
            {
                var _influencer = JsonConvert.DeserializeObject<Dictionary<string, ClinicModel>>(_influencerData);
                TotalInfluencers = _influencer.Count;
            }

            var _clinicData = await DataService.Get($"Clinic/{Preferences.Get("TeamId", "")}");
            if (_clinicData != "null" & _clinicData != "Error")
            {
                var _clinic = JsonConvert.DeserializeObject<Dictionary<string, ClinicModel>>(_clinicData);
                TotalClinics = _clinic.Count;
            }

            var _doctorData = await DataService.Get($"Doctor/{Preferences.Get("TeamId", "")}");
            if (_doctorData != "null" & _doctorData != "Error")
            {
                var _doctor = JsonConvert.DeserializeObject<Dictionary<string, ClinicModel>>(_doctorData);
                TotalDoctors = _doctor.Count;
            }
        }
        public async void Get()
        {
            var data = await DataService.Get($"Team/{Preferences.Get("ClusterId", "")}");
            if (data != "null" & data != "Error")
            {
                var clinic = JsonConvert.DeserializeObject<Dictionary<string, TeamModel>>(data);
                foreach (KeyValuePair<string, TeamModel> item in clinic)
                {
                    Team = new TeamModel
                    {
                        Id = item.Value.Id,
                        FId = item.Key.ToString(),
                        CHWName = item.Value.CHWName,
                        SocialMobilizerId = item.Value.SocialMobilizerId,
                        TeamNo = item.Value.TeamNo
                    };
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
            GetStat();

            IsBusy = false;
        }
        void Clear()
        {
            Team = null;
            TotalChildren = 0;
            TotalInfluencers = 0;
            TotalDoctors = 0;
            TotalClinics = 0;
            TotalSchools = 0;
            TotalMasjeeds = 0;
            TotalHouseholds = 0;
        }
    }
}

