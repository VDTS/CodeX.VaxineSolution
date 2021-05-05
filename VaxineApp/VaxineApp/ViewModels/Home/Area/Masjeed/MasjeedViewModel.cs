using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Models.Home.Area;
using VaxineApp.Views.Home.Area.Masjeed;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Masjeed
{
    public class MasjeedViewModel : BaseViewModel
    {
        // Properties
        private string _masjeedName;
        public string MasjeedName
        {
            get { return _masjeedName; }
            set
            {
                _masjeedName = value;
                RaisedPropertyChanged(nameof(MasjeedName));
            }
        }
        private string _keyInfluencer;
        public string KeyInfluencer
        {
            get { return _keyInfluencer; }
            set
            {
                _keyInfluencer = value;
                RaisedPropertyChanged(nameof(KeyInfluencer));
            }
        }
        private bool _doesImamSupportsVaccine;
        public bool DoesImamSupportsVaccine
        {
            get { return _doesImamSupportsVaccine; }
            set
            {
                _doesImamSupportsVaccine = value;
                RaisedPropertyChanged(nameof(DoesImamSupportsVaccine));
            }
        }
        private bool _doYouHavePermissionForAdsInMasjeed;
        public bool DoYouHavePermissionForAdsInMasjeed
        {
            get { return _doYouHavePermissionForAdsInMasjeed; }
            set
            {
                _doYouHavePermissionForAdsInMasjeed = value;
                RaisedPropertyChanged(nameof(DoYouHavePermissionForAdsInMasjeed));
            }
        }


        // Commands
        public ICommand AddMasjeedCommand { private set; get; }
        public ICommand SaveMasjeedCommand { private set; get; }


        // Constructor
        public MasjeedViewModel()
        {
            AddMasjeedCommand = new Command(AddMasjeed);
            SaveMasjeedCommand = new Command(SaveMasjeed);
        }

        // Methods
        private async void SaveMasjeed()
        {
            await Data.PostMasjeed(new MasjeedModel
            {
                MasjeedName = MasjeedName,
                KeyInfluencer = KeyInfluencer,
                DoesImamSupportsVaccine = DoesImamSupportsVaccine,
                DoYouHavePermissionForAdsInMasjeed = DoYouHavePermissionForAdsInMasjeed
            }, "T");

            var route = $"{nameof(MasjeedPage)}";
            await Shell.Current.GoToAsync(route);
        }

        // Route Methods
        async void AddMasjeed()
        {
            var route = $"{nameof(AditMasjeedPage)}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
