using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.ViewModels.Base;
using VaxineApp.Views.Home.Area.School;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
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
        public AsyncCommand GetSchoolCommand { private set; get; }
        // Constructor
        public SchoolViewModel()
        {
            School = new List<SchoolModel>();
            GetSchool();
            GetSchoolCommand = new AsyncCommand(Refresh);
            AddSchoolCommand = new Command(AddSchool);
        }


        // Methods
        public async void GetSchool()
        {
            var data = await DataService.Get($"School/{Preferences.Get("TeamId", "")}");
            var clinic = JsonConvert.DeserializeObject<Dictionary<string, SchoolModel>>(data);
            foreach (KeyValuePair<string, SchoolModel> item in clinic)
            {
                School.Add(
                    new SchoolModel
                    {
                        KeyInfluencer = item.Value.KeyInfluencer,
                        SchoolName = item.Value.SchoolName,
                        Longitude = item.Value.Longitude,
                        Latitude = item.Value.Latitude
                    }
                    );
            }
        }
        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                RaisedPropertyChanged(nameof(IsBusy));
            }
        }

        // Route Methods
        async void AddSchool()
        {
            var route = $"{nameof(AditSchoolPage)}";
            await Shell.Current.GoToAsync(route);
        }
        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);
            Clear();
            GetSchool();

            IsBusy = false;
        }

        void Clear()
        {
            School.Clear();
        }
    }
}
