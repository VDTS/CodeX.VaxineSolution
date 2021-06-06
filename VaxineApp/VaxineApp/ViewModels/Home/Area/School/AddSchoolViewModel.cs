using DataAccessLib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.ViewModels.Base;
using VaxineApp.Views.Home.Area.School;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.School
{
    public class AddSchoolViewModel : BaseViewModel
    {

        // Properties
        private string _schoolName;
        public string SchoolName
        {
            get { return _schoolName; }
            set
            {
                _schoolName = value;
                RaisedPropertyChanged(nameof(SchoolName));
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
        public ICommand SaveSchoolCommand { private set; get; }
        public AddSchoolViewModel()
        {
            SaveSchoolCommand = new Command(SaveSchool);

        }
        private async void SaveSchool()
        {
            SchoolModel clinic = new SchoolModel()
            {
                Id = Guid.NewGuid(),
                KeyInfluencer = KeyInfluencer,
                SchoolName = SchoolName,
                Longitude = Longitude,
                Latitude = Latitude
            };

            var data = JsonConvert.SerializeObject(clinic);

            string a = DataService.Post(data, "School/c0cda6a9-759a-4e87-b8cb-49af170bd24e");
            await App.Current.MainPage.DisplayAlert(a, "Successfully posted", "OK");

            var route = $"//{nameof(SchoolPage)}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
