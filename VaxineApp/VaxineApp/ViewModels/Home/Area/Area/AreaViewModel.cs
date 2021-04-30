using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.Models.Home.Area;
using VaxineApp.Services;
using VaxineApp.Views.Home.Area.Area;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Area
{
    public class AreaViewModel : BaseViewModel
    {
        DbContext firebaseHelper = new DbContext();
        private string _clusterName;
        private AreaModel _area;
        public AreaModel Area {
            get { return _area; }
            set
            {
                _area = value;
                RaisedPropertyChanged(nameof(Area));
            }
        }
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

        public ICommand EditAreaCommand { private set; get; }
        public ICommand SaveAreaCommand { private set; get; }
        public AreaViewModel()
        {
            GetArea();
            EditAreaCommand = new Command(EditArea);
            SaveAreaCommand = new Command(SaveArea);
        }

        public async void GetArea()
        {
            Area = await firebaseHelper.GetArea();
        }
        public async void EditArea()
        {
            var route = $"{nameof(EditAreaPage)}";
            await Shell.Current.GoToAsync(route);
        }

        public async void SaveArea()
        {
            try
            {
                await firebaseHelper.AddArea(
                    new AreaModel
                    {
                        ClusterName = ClusterName,
                        TeamNo = TeamNo,
                        CHWName = CHWName,
                        SocialMobilizerId = SocialMobilizerId
                    }
                    );
                var route = $"{nameof(AreaPage)}";
                await Shell.Current.GoToAsync(route);
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}

