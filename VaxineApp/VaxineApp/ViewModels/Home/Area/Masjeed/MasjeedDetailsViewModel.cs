using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Masjeed
{
    public class MasjeedDetailsViewModel
    {
        // Property
        public MasjeedModel Masjeed { get; }

        // Command
        public ICommand GoToLocationCommand { private set; get; }
        public ICommand ShowLocationCommand { private set; get; }
        public MasjeedDetailsViewModel(MasjeedModel masjeed)
        {
            // Property
            Masjeed = masjeed;

            // Command
            GoToLocationCommand = new Command(GoToLocation);
            ShowLocationCommand = new Command(ShowLocation);
        }
        private async void GoToLocation(object obj)
        {
            var location = new Location(Convert.ToDouble(Masjeed.Latitude), Convert.ToDouble(Masjeed.Longitude));
            var options = new MapLaunchOptions
            {
                Name = null,
                NavigationMode = NavigationMode.Walking
            };
            await Map.OpenAsync(location, options);
        }
        private async void ShowLocation(object obj)
        {
            var location = new Location(Convert.ToDouble(Masjeed.Latitude), Convert.ToDouble(Masjeed.Longitude));
            await Map.OpenAsync(location);
        }
    }
}
