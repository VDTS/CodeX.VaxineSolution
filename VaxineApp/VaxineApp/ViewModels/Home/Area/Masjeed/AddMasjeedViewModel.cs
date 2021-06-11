using DataAccessLib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.ViewModels.Base;
using VaxineApp.Views.Home.Area.Masjeed;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Masjeed
{
    public class AddMasjeedViewModel : BaseViewModel
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
        public double _latitude;
        public double Latitude
        {
            get { return _latitude; }
            set
            {
                _latitude = value;
                RaisedPropertyChanged(nameof(Latitude));
            }
        }
        public double _longitude;
        public double Longitude
        {
            get { return _longitude; }
            set
            {
                _longitude = value;
                RaisedPropertyChanged(nameof(Longitude));
            }
        }
        public ICommand SaveMasjeedCommand { private set; get; }
        public AddMasjeedViewModel()
        {

            SaveMasjeedCommand = new Command(SaveMasjeed);

        }
        private async void SaveMasjeed()
        {
            MasjeedModel clinic = new MasjeedModel()
            {
                Id = Guid.NewGuid(),
                MasjeedName = MasjeedName,
                KeyInfluencer = KeyInfluencer,
                DoesImamSupportsVaccine = DoesImamSupportsVaccine,
                DoYouHavePermissionForAdsInMasjeed = DoYouHavePermissionForAdsInMasjeed,
                Longitude = Longitude,
                Latitude = Latitude
            };

            var data = JsonConvert.SerializeObject(clinic);

            string a = DataService.Post(data, $"Masjeed/{Preferences.Get("TeamId", "")}");
            await App.Current.MainPage.DisplayAlert(a, "Successfully posted", "OK");

            var route = $"//{nameof(MasjeedPage)}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
