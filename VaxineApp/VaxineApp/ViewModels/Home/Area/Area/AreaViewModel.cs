using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.Models.Home.Area;
using DataAccess;
using VaxineApp.Views.Home.Area.Area;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Area
{
    public class AreaViewModel : BaseViewModel
    {
        // Properties

        private AreaModel _area;
        public AreaModel Area
        {
            get { return _area; }
            set
            {
                _area = value;
                RaisedPropertyChanged(nameof(Area));
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
        public ICommand GetDataCommand { private set; get; }

        // Constructor
        public AreaViewModel()
        {
            GetArea();
            GoToEditAreaCommand = new Command(GoToEditArea);
            SaveAreaCommand = new Command(SaveArea);
            GetDataCommand = new Command(GetArea);
        }

        // Methods
        public async void GetArea()
        {
            var area = await Data.GetArea();
            if(area != null)
            {
                Area = new AreaModel
                {
                    ClusterName = area.ClusterName,
                    CHWName = area.CHWName,
                    SocialMobilizerId = area.SocialMobilizerId,
                    TeamNo = area.TeamNo
                };

                ClusterName = Area.ClusterName;
                CHWName = Area.CHWName;
                SocialMobilizerId = Area.SocialMobilizerId;
                TeamNo = Area.TeamNo;
            }
        }
        public async void SaveArea()
        {
            var area = await Data.GetArea();
            if(area == null)
            {
                try
                {
                    await Data.AddDataNode(
                        new AreaModel
                        {
                            ClusterName = ClusterName,
                            TeamNo = TeamNo,
                            CHWName = CHWName,
                            SocialMobilizerId = SocialMobilizerId
                        }, "Area"
                        );
                    var route = $"{nameof(AreaPage)}";
                    await Shell.Current.GoToAsync(route);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                try
                {
                    await Data.UpdateArea(ClusterName,
                        new AreaModel
                        {
                            ClusterName = ClusterName,
                            TeamNo = TeamNo,
                            CHWName = CHWName,
                            SocialMobilizerId = SocialMobilizerId
                        }, "Area"
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

        // GoTo Routes
        public async void GoToEditArea()
        {
            var route = $"{nameof(EditAreaPage)}";
            await Shell.Current.GoToAsync(route);
        }
    }
}

