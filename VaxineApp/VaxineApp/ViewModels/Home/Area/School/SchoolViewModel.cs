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

        private List<SchoolModel> _school;
        public List<SchoolModel> School
        {
            get { return _school; }
            set
            {
                _school = value;
                RaisedPropertyChanged(nameof(School));
            }
        }

        // Commands
        public ICommand AddSchoolCommand { private set; get; }
        public ICommand GetSchoolCommand { private set; get; }
        // Constructor
        public SchoolViewModel()
        {
            School = new List<SchoolModel>();
            GetSchool();
            GetSchoolCommand = new Command(GetSchool);
            AddSchoolCommand = new Command(AddSchool);
        }


        // Methods
        public async void GetSchool()
        {
            var data = await Data.GetSchool("T");
            foreach (var item in data)
            {
                School.Add(
                    new SchoolModel
                    {
                        KeyInfluencer = item.KeyInfluencer,
                        SchoolName = item.SchoolName
                    }
                    );
            }
        }


        // Route Methods
        async void AddSchool()
        {
            var route = $"{nameof(AditSchoolPage)}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
