using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Models.Home.Area;
using VaxineApp.Views.Home.Area.School;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.School
{
    public class SchoolViewModel : BaseViewModel
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


        // Commands
        public ICommand AddSchoolCommand { private set; get; }
        public ICommand SaveSchoolCommand { private set; get; }

        // Constructor
        public SchoolViewModel()
        {
            AddSchoolCommand = new Command(AddSchool);
            SaveSchoolCommand = new Command(SaveSchool);
        }


        // Methods
        private async void SaveSchool()
        {
            await Data.PostSchool(new SchoolModel
            {
                KeyInfluencer = KeyInfluencer,
                SchoolName = SchoolName
            }, "T");

            var route = $"{nameof(SchoolPage)}";
            await Shell.Current.GoToAsync(route);
        }


        // Route Methods
        async void AddSchool()
        {
            var route = $"{nameof(AditSchoolPage)}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
