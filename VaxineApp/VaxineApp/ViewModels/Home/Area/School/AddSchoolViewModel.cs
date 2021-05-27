using DataAccess.Models;
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
            await Data.PostSchool(new SchoolModel
            {
                KeyInfluencer = KeyInfluencer,
                SchoolName = SchoolName,
                Longitude = Longitude,
                Latitude = Latitude
            });

            var route = $"//{nameof(SchoolPage)}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
