using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.Views.Home.Area.Area;
using Xamarin.Forms;
using Xamarin.CommunityToolkit.ObjectModel;
using System.Threading.Tasks;
using VaxineApp.ViewModels.Base;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace VaxineApp.ViewModels.Home.Area.Area
{
    public class AreaViewModel : BaseViewModel
    {
        // Properties
        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                RaisedPropertyChanged(nameof(IsBusy));
            }
        }

        private TeamModel _team;
        public TeamModel Team
        {
            get { return _team; }
            set
            {
                _team = value;
                RaisedPropertyChanged(nameof(Team));
            }
        }
        private string _clusterName;
        public string ClusterName
        {
            get { return _clusterName; }
            set
            {
                _clusterName = value;
                RaisedPropertyChanged(nameof(ClusterName));
            }
        }
        private string _teamNo;
        public string TeamNo
        {
            get { return _teamNo; }
            set
            {
                _teamNo = value;
                RaisedPropertyChanged(nameof(TeamNo));
            }
        }
        private int _socialMobilizerId;
        public int SocialMobilizerId
        {
            get { return _socialMobilizerId; }
            set
            {
                _socialMobilizerId = value;
                RaisedPropertyChanged(nameof(SocialMobilizerId));
            }
        }
        private string _cHWName;
        public string CHWName
        {
            get { return _cHWName; }
            set
            {
                _cHWName = value;
                RaisedPropertyChanged(nameof(CHWName));
            }
        }
        private int _totalHouseholds;
        public int TotalHouseholds
        {
            get { return _totalHouseholds; }
            set
            {
                _totalHouseholds = value;
                RaisedPropertyChanged(nameof(TotalHouseholds));
            }
        }
        private int _totalMasjeeds;
        public int TotalMasjeeds
        {
            get { return _totalMasjeeds; }
            set
            {
                _totalMasjeeds = value;
                RaisedPropertyChanged(nameof(TotalMasjeeds));
            }
        }

        private int _totalSchools;
        public int TotalSchools
        {
            get { return _totalSchools; }
            set
            {
                _totalSchools = value;
                RaisedPropertyChanged(nameof(TotalSchools));
            }
        }

        private int _totalClinics;
        public int TotalClinics
        {
            get { return _totalClinics; }
            set
            {
                _totalClinics = value;
                RaisedPropertyChanged(nameof(TotalClinics));
            }
        }

        private int _totalDoctors;
        public int TotalDoctors
        {
            get { return _totalDoctors; }
            set
            {
                _totalDoctors = value;
                RaisedPropertyChanged(nameof(TotalDoctors));
            }
        }

        private int _totalInfluencers;
        public int TotalInfluencers
        {
            get { return _totalInfluencers; }
            set
            {
                _totalInfluencers = value;
                RaisedPropertyChanged(nameof(TotalInfluencers));
            }
        }
        private int _totalChildren;
        public int TotalChildren
        {
            get { return _totalChildren; }
            set
            {
                _totalChildren = value;
                RaisedPropertyChanged(nameof(TotalChildren));
            }
        }

        // Commands
        public ICommand GoToEditAreaCommand { private set; get; }
        public ICommand SaveAreaCommand { private set; get; }
        public ICommand DeleteCommand { private set; get; }
        public ICommand SaveAsPDFCommand { private set; get; }
        public AsyncCommand GetDataCommand { private set; get; }

        // Constructor
        public AreaViewModel()
        {
            SaveAsPDFCommand = new Command(SaveAsPDF);
            DeleteCommand = new Command(Delete);
            GetArea();
            GoToEditAreaCommand = new Command(GoToEditArea);
            SaveAreaCommand = new Command(SaveArea);
            GetDataCommand = new AsyncCommand(Refresh);
            GetStat();
        }

        private async void Delete(object obj)
        {
            await App.Current.MainPage.DisplayAlert("Not submitted!", "This functionality is under construction", "OK");
        }

        private async void SaveAsPDF(object obj)
        {
            await App.Current.MainPage.DisplayAlert("Not submitted!", "This functionality is under construction", "OK");
        }

        private async void GetStat()
        {

            var data = await DataService.Get($"Family/{Preferences.Get("TeamId","")}");
            var clinic = JsonConvert.DeserializeObject<Dictionary<string, GetFamilyModel>>(data);
            int _children = 0;
            foreach (KeyValuePair<string, GetFamilyModel> item in clinic)
            {
                var data2 = await DataService.Get($"Child/{item.Value.Id}");
                var clinic2 = JsonConvert.DeserializeObject<Dictionary<string, ChildModel>>(data2);
                int _familyChildCount = 0;
                foreach (KeyValuePair<string, ChildModel> item2 in clinic2)
                {
                    _familyChildCount++;
                }
                _children += _familyChildCount;
            }

            TotalChildren = _children;
            
            
            var _familyData = await DataService.Get($"Family/{Preferences.Get("TeamId", "")}");
            var _family = JsonConvert.DeserializeObject<Dictionary<string, ClinicModel>>(_familyData);
            TotalHouseholds = _family.Count;

            var _masjeedData = await DataService.Get($"Masjeed/{Preferences.Get("TeamId", "")}");
            var _masjeed = JsonConvert.DeserializeObject<Dictionary<string, ClinicModel>>(_masjeedData);
            TotalMasjeeds = _masjeed.Count;

            var _schoolData = await DataService.Get($"School/{Preferences.Get("TeamId", "")}");
            var _school = JsonConvert.DeserializeObject<Dictionary<string, ClinicModel>>(_schoolData);
            TotalSchools = _school.Count;

            var _influencerData = await DataService.Get($"Influencer/{Preferences.Get("TeamId", "")}");
            var _influencer = JsonConvert.DeserializeObject<Dictionary<string, ClinicModel>>(_influencerData);
            TotalInfluencers = _influencer.Count;

            var _clinicData = await DataService.Get($"Clinic/{Preferences.Get("TeamId", "")}");
            var _clinic = JsonConvert.DeserializeObject<Dictionary<string, ClinicModel>>(_clinicData);
            TotalClinics = _clinic.Count;

            var _doctorData = await DataService.Get($"Doctor/{Preferences.Get("TeamId", "")}");
            var _doctor = JsonConvert.DeserializeObject<Dictionary<string, ClinicModel>>(_doctorData);
            TotalDoctors = _doctor.Count;
        }

        // Methods
        public async void GetArea()
        {
            var data = await DataService.Get($"Team/{Preferences.Get("ClusterId", "")}");
            var clinic = JsonConvert.DeserializeObject<Dictionary<string, TeamModel>>(data);
            foreach (KeyValuePair<string, TeamModel> item in clinic)
            {
                Team = new TeamModel
                {
                    CHWName = item.Value.CHWName,
                    SocialMobilizerId = item.Value.SocialMobilizerId,
                    TeamNo = item.Value.TeamNo
                };
            }
            CHWName = Team.CHWName;
            SocialMobilizerId = Team.SocialMobilizerId;
            TeamNo = Team.TeamNo;
        }
        public async void SaveArea()
        {
            await App.Current.MainPage.DisplayAlert("Not submitted!", "This functionality is under construction", "OK");
            //    var area = await Data.GetTeam();
            //    if (area == null)
            //    {
            //        try
            //        {
            //            await Data.PostTeam(
            //                new TeamModel
            //                {
            //                    TeamNo = TeamNo,
            //                    CHWName = CHWName,
            //                    SocialMobilizerId = SocialMobilizerId
            //                }
            //                );
            //            var route = $"//{nameof(AreaPage)}";
            //            await Shell.Current.GoToAsync(route);
            //        }
            //        catch (Exception)
            //        {
            //            throw;
            //        }
            //    }
            //    else
            //    {
            //        try
            //        {
            //            await Data.PutTeam(
            //                 new TeamModel
            //                 {
            //                     TeamNo = TeamNo,
            //                     CHWName = CHWName,
            //                     SocialMobilizerId = SocialMobilizerId
            //                 }
            //                );
            //            var route = $"//{nameof(AreaPage)}";
            //            await Shell.Current.GoToAsync(route);
            //        }
            //        catch (Exception)
            //        {

            //            throw;
            //        }
            //    }
        }

        // GoTo Routes
        public async void GoToEditArea()
        {
            var route = $"{nameof(EditAreaPage)}";
            await Shell.Current.GoToAsync(route);
        }
        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);
            Clear();
            GetArea();
            GetStat();

            IsBusy = false;
        }

        void Clear()
        {
            Team = null;
        }
    }
}

