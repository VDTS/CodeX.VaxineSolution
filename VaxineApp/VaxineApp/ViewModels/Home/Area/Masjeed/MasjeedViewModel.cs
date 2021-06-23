using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.ViewModels.Base;
using VaxineApp.Views.Home.Area.Masjeed;
using VaxineApp.Views.Shared;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Masjeed
{
    public class MasjeedViewModel : ViewModelBase
    {
        // Property
        private MasjeedModel selectedMasjeed;
        public MasjeedModel SelectedMasjeed
        {
            get
            {
                return selectedMasjeed;
            }
            set
            {
                selectedMasjeed = value;
                OnPropertyChanged();
            }
        }

        private bool isBusy;
        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                isBusy = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasjeedModel> masjeeds;
        public ObservableCollection<MasjeedModel> Masjeeds
        {
            get
            {
                return masjeeds;
            }
            set
            {
                masjeeds = value;
                OnPropertyChanged();
            }
        }

        // Commands
        public ICommand GoToPostPageCommand { private set; get; }
        public ICommand SaveAsPDFCommand { private set; get; }
        public ICommand DeleteCommand { private set; get; }
        public ICommand GoToPutPageCommand { private set; get; }
        public ICommand GoToMapCommand { private set; get; }
        public ICommand PullRefreshCommand { private set; get; }


        // ctor
        public MasjeedViewModel()
        {
            // Property
            Masjeeds = new ObservableCollection<MasjeedModel>();
            SelectedMasjeed = new MasjeedModel();


            // Get
            Get();


            // Command
            SaveAsPDFCommand = new Command(SaveAsPDF);
            DeleteCommand = new Command(Delete);
            GoToPutPageCommand = new Command(GoToPutPage);
            PullRefreshCommand = new Command(Refresh);
            GoToPostPageCommand = new Command(GoToPostPage);
            GoToMapCommand = new Command(GoToMap);
        }

        private async void GoToPutPage()
        {
            if (SelectedMasjeed.MasjeedName != null)
            {
                var jsonClinic = JsonConvert.SerializeObject(SelectedMasjeed);
                var route = $"{nameof(EditMasjeedPage)}?Masjeed={jsonClinic}";
                await Shell.Current.GoToAsync(route);
                SelectedMasjeed = null;
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("No Masjeed", "Select a Masjeed", "OK");
            }
        }

        private async void Delete(object obj)
        {

            if (SelectedMasjeed.FId != null)
            {
                var isDeleteAccepted = await App.Current.MainPage.DisplayAlert("", $"Do you want to delete {SelectedMasjeed.MasjeedName}?", "Yes", "No");
                if (isDeleteAccepted)
                {
                    var data = await DataService.Delete($"Masjeed/{Preferences.Get("TeamId", "")}/{SelectedMasjeed.FId}");
                    if (data == "Deleted")
                    {
                        Masjeeds.Remove(SelectedMasjeed);
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Not Deleted", "Try again", "OK");
                    }
                }
                else
                {
                    return;
                }

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("No item selected", "Select an item to delete", "OK");
            }
        }

        private async void SaveAsPDF(object obj)
        {
            await App.Current.MainPage.DisplayAlert("Not submitted!", "This functionality is under construction", "OK");
        }

        private async void GoToMap(object obj)
        {
            var route = $"{nameof(GoogleMapPage)}";
            await Shell.Current.GoToAsync(route);
        }

        public async void Get()
        {
            var data = await DataService.Get($"Masjeed/{Preferences.Get("TeamId", "")}");
            if (data != "null" & data != "Error")
            {
                var clinic = JsonConvert.DeserializeObject<Dictionary<string, MasjeedModel>>(data);
                foreach (KeyValuePair<string, MasjeedModel> item in clinic)
                {
                    Masjeeds.Add(
                        new MasjeedModel
                        {
                            FId = item.Key.ToString(),
                            Id = item.Value.Id,
                            MasjeedName = item.Value.MasjeedName,
                            KeyInfluencer = item.Value.KeyInfluencer,
                            DoYouHavePermissionForAdsInMasjeed = item.Value.DoYouHavePermissionForAdsInMasjeed,
                            DoesImamSupportsVaccine = item.Value.DoesImamSupportsVaccine,
                            Latitude = item.Value.Latitude,
                            Longitude = item.Value.Longitude
                        }
                        );
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("No data found!", "Add some data to show here", "OK");
            }
        }

        async void GoToPostPage()
        {
            var route = $"{nameof(AddMasjeedPage)}";
            await Shell.Current.GoToAsync(route);
        }
        async void Refresh()
        {
            IsBusy = true;

            Clear();
            Get();
            await Task.Delay(2000);

            IsBusy = false;
        }

        void Clear()
        {
            Masjeeds.Clear();
        }
    }
}
