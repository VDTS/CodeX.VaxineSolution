using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Utility.Extensions;
using VaxineApp.MobilizerShell.Views.Home.Area.Masjeed;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.MobilizerShell.ViewModels.Home.Area.Masjeed
{
    public class MasjeedViewModel : ViewModelBase
    {
        // Property
        private MasjeedModel? selectedMasjeed;
        public MasjeedModel? SelectedMasjeed
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

        private ObservableCollection<MasjeedModel>? masjeeds;
        public ObservableCollection<MasjeedModel>? Masjeeds
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
        public ICommand PullRefreshCommand { private set; get; }
        public ICommand GoToDetailsPageCommand { private set; get; }

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
            PullRefreshCommand = new Command(Refresh);
            GoToPostPageCommand = new Command(GoToPostPage);
            GoToDetailsPageCommand = new Command(GoToDetailsPage);
        }

        public async void GoToDetailsPage()
        {
            if (SelectedMasjeed.AreEmpty())
            {
                return;
            }
            else
            {
                var SelectedItemJson = JsonConvert.SerializeObject(SelectedMasjeed);
                var route = $"{nameof(MasjeedDetailsPage)}?Masjeed={SelectedItemJson}";
                await Shell.Current.GoToAsync(route);
                SelectedMasjeed = null;
            }
        }


        private void SaveAsPDF(object obj)
        {
            StandardMessagesDisplay.FeatureUnderConstructionTitleDisplayMessage();
        }

        public async void Get()
        {
            var jData = await DataService.Get($"Masjeed/{Preferences.Get("TeamId", "")}");

            if (jData == "ConnectionError")
            {
                StandardMessagesDisplay.NoConnectionToast();
            }
            else if (jData == "null")
            {
                StandardMessagesDisplay.NoDataDisplayMessage();
            }
            else if (jData == "Error")
            {
                StandardMessagesDisplay.Error();
            }
            else if (jData == "ErrorTracked")
            {
                StandardMessagesDisplay.ErrorTracked();
            }
            else
            {
                try
                {
                    var data = JsonConvert.DeserializeObject<Dictionary<string, MasjeedModel>>(jData);

                    if(data != null)
                    foreach (KeyValuePair<string, MasjeedModel> item in data)
                    {
                        Masjeeds?.Add(
                            new MasjeedModel
                            {
                                FId = item.Key.ToString(),
                                Id = item.Value.Id,
                                MasjeedName = item.Value.MasjeedName,
                                KeyInfluencer = item.Value.KeyInfluencer,
                                DoYouHavePermissionForAdsInMasjeed = item.Value.DoYouHavePermissionForAdsInMasjeed,
                                DoesImamSupportsVaccine = item.Value.DoesImamSupportsVaccine,
                                Latitude = item.Value.Latitude,
                                Longitude = item.Value.Longitude,
                                IsActive = item.Value.IsActive
                            }
                            );
                    }
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                    StandardMessagesDisplay.InputToast(ex.Message);
                }
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
            Masjeeds?.Clear();
        }
    }
}
