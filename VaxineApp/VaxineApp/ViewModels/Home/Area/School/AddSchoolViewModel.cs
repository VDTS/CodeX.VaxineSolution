using DataAccessLib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.MVVMHelper;
using VaxineApp.Views.Home.Area.School;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.School
{
    public class AddSchoolViewModel : ViewModelBase
    {

        // Property
        private SchoolModel school;
        public SchoolModel School
        {
            get
            {
                return school;
            }
            set
            {
                school = value;
                OnPropertyChanged();
            }
        }

        // Command
        public ICommand PostCommand { private set; get; }

        // ctor
        public AddSchoolViewModel()
        {
            // Property
            School = new SchoolModel();

            // Command
            PostCommand = new Command(Post);
        }

        private async void Post()
        {
            School.Id = Guid.NewGuid();

            var data = JsonConvert.SerializeObject(School);

            string a = await DataService.Post(data, $"School/{Preferences.Get("TeamId", "")}");
            if (a == "OK")
            {
                await App.Current.MainPage.DisplayAlert(a, "Successfully posted", "OK");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert(a, "Try again", "OK");
            }
            var route = $"//{nameof(SchoolPage)}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
