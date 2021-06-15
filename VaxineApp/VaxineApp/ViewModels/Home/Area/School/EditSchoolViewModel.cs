using DataAccessLib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.ViewModels.Base;
using VaxineApp.Views.Home.Area.School;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.School
{
    public class EditSchoolViewModel : BaseViewModel
    {
        private SchoolModel _school;
        public SchoolModel School
        {
            get { return _school; }
            set
            {
                _school = value;
                RaisedPropertyChanged(nameof(School));
            }
        }
        public ICommand UpdateSchoolCommand { private set; get; }

        public EditSchoolViewModel(SchoolModel school)
        {
            School = school;
            UpdateSchoolCommand = new Command(UpdateSchool);
        }

        private async void UpdateSchool(object obj)
        {
            await App.Current.MainPage.DisplayAlert("Not submitted!", "This functionality is under construction", "OK");
        }
    }
}
