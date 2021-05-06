using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
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
                SchoolName = SchoolName
            }, "T");

            var route = $"//{nameof(SchoolPage)}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
