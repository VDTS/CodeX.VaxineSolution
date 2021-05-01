using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Views.Home.Area.School;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.School
{
    public class SchoolViewModel
    {
        public ICommand AddSchoolCommand { private set; get; }
        public SchoolViewModel()
        {
            AddSchoolCommand = new Command(AddSchool);
        }
        async void AddSchool()
        {
            var route = $"{nameof(AditSchoolPage)}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
