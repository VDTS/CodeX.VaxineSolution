using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.Views.Home.Area.Area;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Area
{
    public class EditAreaViewModel : ViewModelBase
    {
        // Property
        public TeamModel Team { get; set; }

        // Command
        public ICommand PutCommand { get; private set; }

        // ctor
        public EditAreaViewModel(TeamModel team)
        {
            // Property
            Team = team;

            // Command
            PutCommand = new Command(Put);
        }

        private async void Put(object obj)
        {
            var jsonData = JsonConvert.SerializeObject(Team);
            var data = await DataService.Put(jsonData, $"Team/{Preferences.Get("ClusterId", "")}/{Team.FId}");
            if (data == "Submit")
            {
                await App.Current.MainPage.DisplayAlert("Updated", $"item has been updated", "OK");
                var route = $"//{nameof(AreaPage)}";
                await Shell.Current.GoToAsync(route);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Not Updated", "Try again", "OK");
            }
        }
    }
}
