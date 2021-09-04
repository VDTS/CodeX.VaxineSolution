using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.Models.Enums;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using VaxineApp.Views.Home.Area.Masjeed;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Masjeed
{
    public class MasjeedDetailsViewModel : ViewModelBase
    {
        // Property
        public MasjeedModel Masjeed { get; }

        // Command
        public ICommand GoToLocationCommand { private set; get; }
        public ICommand ShowLocationCommand { private set; get; }
        public ICommand DeleteCommand { private set; get; }
        public ICommand GoToPutPageCommand { private set; get; }

        // ctor
        public MasjeedDetailsViewModel(MasjeedModel masjeed)
        {
            // Property
            Masjeed = masjeed;

            // Command
            GoToLocationCommand = new Command(GoToLocation);
            ShowLocationCommand = new Command(ShowLocation);
            DeleteCommand = new Command(Delete);
            GoToPutPageCommand = new Command(GoToPutPage);
        }

        private async void GoToPutPage()
        {
            if (Masjeed.MasjeedName != null)
            {
                var jsonClinic = JsonConvert.SerializeObject(Masjeed);
                var route = $"{nameof(EditMasjeedPage)}?Masjeed={jsonClinic}";
                await Shell.Current.GoToAsync(route);
            }
            else
            {
                StandardMessagesDisplay.NoDataDisplayMessage();
            }
        }

        private async void Delete(object obj)
        {

            if (Masjeed.FId != null)
            {
                var isDeleteAccepted = await StandardMessagesDisplay.DeleteDisplayMessage(Masjeed.MasjeedName);
                if (isDeleteAccepted)
                {
                    Masjeed.IsActive = IsActive.MarkAsDelete;
                    var data22 = JsonConvert.SerializeObject(Masjeed);

                    var data = await DataService.Put(data22, $"Masjeed/{Preferences.Get("TeamId", "")}/{Masjeed.FId}");
                    if (data == "Submit")
                    {
                        _ = await DataService.Put((--StaticDataStore.TeamStats.TotalMasjeeds).ToString(), $"Team/{Preferences.Get("ClusterId", "")}/{Preferences.Get("TeamFId", "")}/TotalMasjeeds");
                        var route = $"//{nameof(MasjeedPage)}";
                        await Shell.Current.GoToAsync(route);
                    }
                    else
                    {
                        StandardMessagesDisplay.CanceledDisplayMessage();
                    }
                }
                else
                {
                    return;
                }

            }
            else
            {
                StandardMessagesDisplay.NoItemSelectedDisplayMessage();
            }
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
